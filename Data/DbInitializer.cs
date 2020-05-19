using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ConstellationWebApp.Models;

namespace ConstellationWebApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(ConstellationWebAppContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.User.Any())
            {
                return;   // DB has been seeded
            }

            var postingType = new PostingType[]
            {
              new PostingType { PostingTypeName = "Internship"},
              new PostingType { PostingTypeName = "Full-Time"},
              new PostingType { PostingTypeName = "Part-Time"},
              new PostingType { PostingTypeName = "Salary"},
              new PostingType { PostingTypeName = "Hourly Wage"},
              new PostingType { PostingTypeName = "Remote"},
              new PostingType { PostingTypeName = "On Site"}
            };

            foreach (PostingType s in postingType)
            {
                context.PostingTypes.Add(s);
            }
            context.SaveChanges();

        }
    }
}
