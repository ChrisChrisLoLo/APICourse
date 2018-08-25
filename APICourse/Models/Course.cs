using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APICourse.Models
{
    //TODO: Move around each class into their own file.
    public class APICourseContext : DbContext
    {
        public APICourseContext(DbContextOptions<APICourseContext> options)
            : base(options)
        { }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }

    }

    public class Faculty
    {   
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Subject> Subjects { get; set; }
    }

    public class Subject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string LetterCode { get; set; }

        public Faculty Faculty { get; set; }
        public List<Course> Courses { get; set; }
    }

    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int NumberCode { get; set; }
        public string Description { get; set; }

        public Subject Subject { get; set; }
    }

    
}
