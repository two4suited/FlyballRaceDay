using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace FlyballRaceDay.ApiService.Database;

public class FlyballRaceDayDbContext : DbContext
{
    
    public static FlyballRaceDayDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<FlyballRaceDayDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

    public FlyballRaceDayDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Tournament>().ToCollection("tournaments");
        modelBuilder.Entity<Race>().ToCollection("races");
        modelBuilder.Entity<Ring>().ToCollection("rings");
    }
   

    public DbSet<Tournament> Tournaments => Set<Tournament>();
    public DbSet<Race> Races => Set<Race>();
    public DbSet<Ring> Rings => Set<Ring>();

  

   
}