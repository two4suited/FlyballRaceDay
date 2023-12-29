using FlyballRaceDay.Shared;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer(ServicesLocator.RedisCache);

var mongodb = builder.AddMongoDBContainer(ServicesLocator.DatabaseContainer).AddDatabase(ServicesLocator.Database);

var apiservice = builder.AddProject<Projects.FlyballRaceDay_ApiService>(ServicesLocator.ApiApplication)
    .WithReference(mongodb);

builder.AddProject<Projects.FlyballRaceDay_Web>(ServicesLocator.WebApplication)
    .WithReference(apiservice)
    .WithReference(cache);

builder.Build().Run();
