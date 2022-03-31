// TCDev.de 2022/03/31
// GenericControllers.ApplicationDbContext.cs
// https://www.github.com/deejaytc/dotnet-utils

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GenericControllers.Data;

public class GenericDbContext : DbContext
{
   public static IModel StaticModel { get; } = BuildStaticModel();

   public DbSet<Something> Somethings { get; set; }
   public DbSet<SomeOtherThing> OtherThing { get; set; }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      if (!optionsBuilder.IsConfigured) optionsBuilder.UseInMemoryDatabase("ApplicationDb");
   }

   protected override void OnModelCreating(ModelBuilder builder)
   {
      base.OnModelCreating(builder);
   }

   private static IModel BuildStaticModel()
   {
      using var dbContext = new GenericDbContext();
      return dbContext.Model;
   }
}
