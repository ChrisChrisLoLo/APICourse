using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APICourse.Models
{
    public class SeedData
    {
        //private readonly IHostingEnvironment _hostingEnvironment;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new APICourseContext(
                serviceProvider.GetRequiredService<DbContextOptions<APICourseContext>>()))
            {
                // Look for any movies.
                if (context.Faculties.Any() || context.Subjects.Any() || context.Courses.Any())
                {
                    return;   // DB has been seeded
                }
                var file = Path.Combine(Directory.GetCurrentDirectory(),"Models","SeedDataInput","UAlbertaCoursesSingleTable.csv");

                using (var reader = new StreamReader(file))
                {
                    Console.WriteLine("SDFFSDDFSFDSFDFSD");
                }


                context.SaveChanges();
            }
        }
    }
}
