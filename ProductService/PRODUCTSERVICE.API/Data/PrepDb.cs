using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PRODUCTSERVICE.API.Models;

namespace PRODUCTSERVICE.API.Data
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

      if (!context.LookupTypes.Any())
      {
        Console.WriteLine("--> Seeding LookupTypes data...");
        context.LookupTypes.AddRange(
            new LookupType { Value = "ART STYLE" },
            new LookupType { Value = "PRODUCT TYPE" }
        );
        context.SaveChanges();
      }
      else
      {
        Console.WriteLine("--> We already have data of LookupTypes");
      }

      if (!context.Lookups.Any())
      {
        Console.WriteLine("--> Seeding Lookup data...");
        context.Lookups.AddRange(
            new Lookup { Value = "ABSTRACT", LookupTypeId = 1 },
            new Lookup { Value = "CONCEPTUAL ART", LookupTypeId = 1 },
            new Lookup { Value = "CONSTRUCTIVISM", LookupTypeId = 1 },
            new Lookup { Value = "WALL PAINTING", LookupTypeId = 2 },
            new Lookup { Value = "FUNITURE", LookupTypeId = 2 },
            new Lookup { Value = "CERAMICS", LookupTypeId = 2 },
            new Lookup { Value = "THROW PILLOWS", LookupTypeId = 2 }
        );
        context.SaveChanges();
      }
      else
      {
        Console.WriteLine("--> We already have data of Lookups");
      }
    }
  }
}