using System;
using System.Linq;
using AuthService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Data
{
    public static class PrepDb
    {

        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            Console.WriteLine("--> Attempting to apply migration...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Can not run migrations: {ex.Message}");
            }

            if (!context.Roles.Any())
            {
                Console.WriteLine("--> Seeding Roles data...");
                context.Roles.AddRange(
                    new Role { Name = "ADMIN" },
                    new Role { Name = "CUSTOMER" },
                    new Role { Name = "ARTIST" }
                );
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> We already have data of Roles");
            }
        }
    }
}