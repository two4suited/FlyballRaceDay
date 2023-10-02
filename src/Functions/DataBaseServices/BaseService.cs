using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DataBaseServices;

public abstract class BaseService<T> where T: class
{
    public readonly IMongoCollection<T> Collection;
    public string DatabaseName;

    public BaseService(IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings,string databaseName)
    {
        var mongoClient = new MongoClient(
            flyballStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            flyballStoreDatabaseSettings.Value.DatabaseName);

        Collection = mongoDatabase.GetCollection<T>(
            databaseName);
    }
}