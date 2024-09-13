using Microsoft.EntityFrameworkCore;
using PRODUCTSERVICE.API.Models;

namespace PRODUCTSERVICE.API.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }

    public DbSet<Lookup> Lookups { get; set; }
    public DbSet<LookupType> LookupTypes { get; set; }
    public DbSet<Art> Arts { get; set; }
    public DbSet<Artist> Artists { get; set; }
    public DbSet<ArtCollection> ArtCollections { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Banner> Banners { get; set; }
  }
}