
using Testcontainers.MongoDb;

namespace FlyballRaceDay.Tests.ApiService.HttpTests;

public class ApiServiceWebApplicationFactory<TProgram,TDbContext> : WebApplicationFactory<TProgram>,IAsyncLifetime where TProgram :class where TDbContext : DbContext
{
    private MongoDbContainer _container  { get; set; }

    public ApiServiceWebApplicationFactory()
    {
        _container = new MongoDbBuilder().Build();
    }
    

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        
        var connectionStrings = new Dictionary<string, string>
        {
            { "flyballraceday", _container.GetConnectionString() }
        };
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(connectionStrings)
            .Build();
        builder.UseConfiguration(config);
        builder.UseEnvironment("Tests");
    }

    public async Task InitializeAsync() => await _container.StartAsync();

    public new async Task DisposeAsync() => await _container.DisposeAsync();
}