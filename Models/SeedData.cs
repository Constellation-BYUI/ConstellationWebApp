using ConstellationWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ConstellationWebApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ConstellationWebAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ConstellationWebAppContext>>()))
            {
                // Look for any movies.
                if (context.User.Any())
                {
                    return;   // DB has been seeded
                }                                                                                                                                     
            }
        }
    }
}       