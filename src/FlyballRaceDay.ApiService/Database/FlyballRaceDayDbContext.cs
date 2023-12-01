using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlyballRaceDay.ApiService.Database;

public class FlyballRaceDayDbContext : DbContext
{
    public FlyballRaceDayDbContext(DbContextOptions<FlyballRaceDayDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Tournament> Tournaments => Set<Tournament>();
    public DbSet<Race> Races => Set<Race>();
    public DbSet<Ring> Rings => Set<Ring>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DefineTournament(modelBuilder.Entity<Tournament>());
    }

    private static void DefineTournament(EntityTypeBuilder<Tournament> builder)
    {
        builder.ToTable(nameof(Tournament));
        builder.HasKey(k => k.Id);
        builder.Property(cb => cb.Id)
            .UseHiLo("catalog_type_hilo")
            .IsRequired();
    }

   
}