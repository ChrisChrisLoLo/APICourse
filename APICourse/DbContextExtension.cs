using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

using APICourse.Models;

//Code taken from https://garywoodfine.com/how-to-seed-your-ef-core-database/

namespace APICourse
{
    public static class DbContextExtension
    {
        public static bool AllMigrationsApplied(this DbContext context)
        {
            var applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }


        public static void EnsureSeeded(this APICourseContext context)
        {
            //Seed database with course information if non is found.
            if (!context.Faculties.Any() && !context.Subjects.Any() && !context.Courses.Any())
            {
                using(var reader = new StreamReader(@"\UAlbertaCoursesSingleTable.csv"))
                {

                }
            }
        }
    }
}
