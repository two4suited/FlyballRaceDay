namespace FlyballRaceDay.Tests.ApiService;

public class IntegrationTestDatabaseProvider(string connectionString)
{
    public FlyballRaceDayDbContext CreateDbContext()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<FlyballRaceDayDbContext>();
        dbContextOptionsBuilder.UseNpgsql(connectionString);
        var context = new FlyballRaceDayDbContext(dbContextOptionsBuilder.Options);
        return context;
    }
}