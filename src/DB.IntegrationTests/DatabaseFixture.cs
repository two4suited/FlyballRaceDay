using ApiIsolated;
using Google.Protobuf;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DB.IntegrationTests;

public class DatabaseFixture
{
    public IOptions<FlyballGameDaySettings> Settings { get; private set; }
    public IMongoDatabase Database { get; private set; }
    public DatabaseFixture()
    {
        var settings = new FlyballGameDaySettings()
        {
            ConnectionString = "mongodb://localhost:27017",
            DatabaseName = "FlyballGameDayDB-Test",
            CollectionName = "FlyballGameDay"
        };
        
        var mongoClient = new MongoClient(
            settings.ConnectionString);

        Database = mongoClient.GetDatabase(
            settings.DatabaseName);
        
        
        Settings = new OptionsWrapper<FlyballGameDaySettings>(settings);
    }
}