namespace FlyballRaceDay.Tests.ApiService;

public class IntegrationTestDatabaseProvider(string connectionString)
{
    public FlyballRaceDayDbContext CreateDbContext()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<FlyballRaceDayDbContext>();
        dbContextOptionsBuilder.UseMongoDB(connectionString,Guid.NewGuid().ToString());
        var context = new FlyballRaceDayDbContext(dbContextOptionsBuilder.Options);
        return context;
    }
}