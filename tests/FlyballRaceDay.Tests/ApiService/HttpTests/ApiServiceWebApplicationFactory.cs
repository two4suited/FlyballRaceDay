
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace FlyballRaceDay.Tests.ApiService.HttpTests;

public class ApiServiceWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>,IAsyncLifetime where TProgram :class 
{
    private MongoDbContainer _container  { get; set; }

    public ApiServiceWebApplicationFactory()
    {
        _container = new MongoDbBuilder().Build();
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContext>();
            services.RemoveAll<IMongoDatabase>();
            
            var client = new MongoClient(_container.GetConnectionString());
            IMongoDatabase database = client.GetDatabase(Guid.NewGuid().ToString());
            services.AddScoped<IMongoDatabase>(d => database);
            services.AddDbContext<FlyballRaceDayDbContext>();
        });
        
        builder.UseEnvironment("Tests");
    }

    public async Task InitializeAsync() => await _container.StartAsync();

    public new async Task DisposeAsync() => await _container.DisposeAsync();
}