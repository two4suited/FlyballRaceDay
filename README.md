# FlyballRaceDay
This is an application to keep track what races are up next at a flyball tournament.  

It was built using .net 8 mininimal api's and blazor.

## Projects
- FlyballRaceDay.ApiService - Minimal Api Project with EF Core PostgreSQL
- FlyballRaceDay.AppHost - Aspire Project for local development
- FlyballRaceDay.ServiceDefaults - Service Collection Defaults for project for middleware
- FlyballRaceDay.Shared - Models and API Service Client that is shared between Server and Client Blazor Projects
- FlyballRaceDay.Web - Blazor Server Project
- FlyballRaceDay.Web.Client - Blazor Wasm Project
- FlyballRaceDay.Web.Shared - Razor Component Library
- FlyballRaceDay.Tests - Test project  

## Tools Used

- [Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview)
- [Blazor](https://learn.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-8.0)
- [NSwag](https://github.com/RicoSuter/NSwag/wiki/CommandLine)

## Packages 

- [Redis](https://redis.io/docs/connect/clients/dotnet/)
- [Blazor Quick Grid](https://aspnet.github.io/quickgridsamples/)
- [Serilog](https://serilog.net/)
- [EF Core PostgresSQL](https://www.npgsql.org/efcore/)
- [Faker](https://github.com/bchavez/Bogus)
- [Test Containes](https://testcontainers.com/)
- [NSubstitute](https://nsubstitute.github.io/)
- [Shouldly](https://github.com/shouldly/shouldly)

## Instructions

- To Run Application you need to run the FlyballRaceDay.AppHost project
- To auto create api client run this:
```
nswag openapi2csclient /input:http://localhost:5458/swagger/v1/swagger.json /className:ApiServiceClient /namespace:FlyballRaceDay.Web /output:ApServiceClient.cs /UseBaseUrl:false
```

