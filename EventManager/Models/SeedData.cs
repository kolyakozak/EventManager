using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EventManager.Data;
using EventManager.Models;
using System;
using System.Linq;

namespace EventManager.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcEventContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcEventContext>>()))
            {
                // Look for any movies.
                if (context.Event.Any())
                {
                    return;   // DB has been seeded
                }

                context.Event.AddRange(
                    new Event
                    {
                        Title = "New Year Celebrating",
                        StartDate = DateTime.Parse("2020-12-31"),
                        EndDate = DateTime.Parse("2021-1-1"),
                        Type = "Event",
                        Region = "Kyiv",
                        KeyWords = "Culture Rest Holiday",
                        Price = 99.99M,
                        UserName = "kozak.k1892@gmail.com"
                    }

                );
                context.SaveChanges();
            }
        }
    }
}