using MongoDB.Driver;

namespace FlyballRaceDay.Tests.ApiService;

public class IntegrationTestDatabaseProvider(string connectionString)
{
    public FlyballRaceDayDbContext CreateDbContext()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<FlyballRaceDayDbContext>();
        dbContextOptionsBuilder.UseMongoDB(connectionString,Guid.NewGuid().ToString());
        var client = new MongoClient(connectionString);
        IMongoDatabase database = client.GetDatabase(Guid.NewGuid().ToString());
        var context = new FlyballRaceDayDbContext(database);
        return context;
    }
}