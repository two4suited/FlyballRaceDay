namespace FlyballRaceDay.Tests.ApiService.HttpTests;

public class ApiServiceWebApplicationFactory<TProgram,TDbContext> : WebApplicationFactory<TProgram>,IAsyncLifetime where TProgram :class where TDbContext : DbContext
{
    public PostgreSqlContainer? _container { get; set; }

    public ApiServiceWebApplicationFactory()
    {
        _container = new PostgreSqlBuilder().WithDatabase(Guid.NewGuid().ToString()).Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
                { { "ConnectionStrings:flyballraceday", _container.GetConnectionString() } })
            .Build();
        builder.UseConfiguration(config);
        builder.UseEnvironment("Tests");
    }

    public async Task InitializeAsync() => await _container.StartAsync();

    public new async Task DisposeAsync() => await _container.DisposeAsync();
}