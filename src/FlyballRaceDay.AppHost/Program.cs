var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.FlyballRaceDay_ApiService>("apiservice");

builder.AddProject<Projects.FlyballRaceDay_Web>("webfrontend")
    .WithReference(apiservice);

builder.Build().Run();
