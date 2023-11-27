using System.Data.Common;
using FlyballRaceDay.ApiService.Race;
using FlyballRaceDay.ApiService.Ring;
using FlyballRaceDay.ApiService.Tournament;
using FlyballRaceDay.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace FlyballRaceDay.Tests.ApiService;

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
    }

    public async Task InitializeAsync() => await _container.StartAsync();

    public new async Task DisposeAsync() => await _container.DisposeAsync();
}