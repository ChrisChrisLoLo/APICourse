using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Csv;


namespace APICourse.Models
{
    public class SeedData
    {
        /*
        private static T GetOrCreate<T>(APICourseContext context,string[] queryData){
            try
            {
                var queryObject = context.Courses;
                return (T)Convert.ChangeType(queryObject,typeof(T));
            }

            catch
            {
                return ;
            }
                
        }
        */

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (APICourseContext context = new APICourseContext(
                serviceProvider.GetRequiredService<DbContextOptions<APICourseContext>>()))
            {
                // Look for any movies.
                if (context.Faculties.Any() || context.Subjects.Any() || context.Courses.Any())
                {
                    return;   // DB has been seeded
                }

                string file = Path.Combine(Directory.GetCurrentDirectory(),"Models","SeedDataInput","UAlbertaCoursesSingleTable.csv");

                var csv = File.ReadAllText(file);
               
                foreach(var csvRowData in CsvReader.ReadFromText(csv)){
                    Faculty queryFaculty;
                    Subject querySubject;

                    string csvFacultyName = csvRowData[0];
                    string csvSubjectName = csvRowData[1];
                    string csvSubjectLetters = csvRowData[2];
                    string csvCourseNumbers = csvRowData[3];
                    string csvCourseTitle = csvRowData[4];
                    string csvCourseSummary = csvRowData[5];

                    //Csv rows contain duplicate faculty and subject entries. Only add entries to DB if they are unique.
                    try
                    {
                        //Assume entry already exists in DB.
                        queryFaculty = context.Faculties.Single(f => f.Name == csvFacultyName);
                    }
                    catch
                    {
                        Faculty newFaculty = new Faculty { Name = csvFacultyName };
                        context.Faculties.Add(newFaculty);
                        context.SaveChanges();
                        //Querying after adding newFaculty since the queried faculty should now have an id generated after saving.
                        queryFaculty = context.Faculties.Single(f => f.Name == csvFacultyName);

                    }

                    try
                    {
                        querySubject = context.Subjects.Single(s => s.Name == csvSubjectName && s.LetterCode == csvSubjectLetters);
                    }
                    catch
                    {
                        Subject newSubject = new Subject
                        {
                            Name = csvSubjectName,
                            LetterCode = csvSubjectLetters,
                            FacultyID = queryFaculty.ID,
                            Faculty = queryFaculty
                        };
                        context.Subjects.Add(newSubject);
                        context.SaveChanges();
                        querySubject = context.Subjects.Single(s => s.Name == csvSubjectName && s.LetterCode == csvSubjectLetters);
                    }

                    Course newCourse = new Course
                    {
                        NumberCode = Int32.Parse(csvCourseNumbers),
                        Name = csvCourseTitle,
                        Description = csvCourseSummary,
                        SubjectID = querySubject.ID,
                        Subject = querySubject
                    };
                    context.Courses.Add(newCourse);
                    context.SaveChanges();
                }
            }   
        }
    }
}
