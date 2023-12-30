using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace FlyballRaceDay.ApiService.Database;

public class FlyballRaceDayDbContext : DbContext
{
    private readonly IMongoDatabase _database;

    /*
    public static FlyballRaceDayDbContext Create(IMongoDatabase database) =>
        new(new DbContextOptionsBuilder<FlyballRaceDayDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);
 */
    
    
    public FlyballRaceDayDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Tournament>().ToCollection("tournaments");
        modelBuilder.Entity<Race>().ToCollection("races");
        modelBuilder.Entity<Ring>().ToCollection("rings");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMongoDB(_database.Client, _database.DatabaseNamespace.DatabaseName);
    }

    public DbSet<Tournament> Tournaments => Set<Tournament>();
    public DbSet<Race> Races => Set<Race>();
    public DbSet<Ring> Rings => Set<Ring>();

  

   
}