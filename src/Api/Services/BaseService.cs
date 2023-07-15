using System;
using ApiIsolated.Models;
using BlazorApp.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiIsolated.Services;

public abstract class BaseService<T> where T: class
{
    public readonly IMongoCollection<T> Collection;
    public string DatabaseName;

    public BaseService(IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            flyballStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            flyballStoreDatabaseSettings.Value.DatabaseName);

        Collection = mongoDatabase.GetCollection<T>(
            DatabaseName);
    }
}