using System.Threading.Tasks;
using ApiIsolated.Services;
using BlazorApp.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiIsolated
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(s =>
                {
                    s.AddScoped<ITournamentService, TournamentService>();
                    s.AddOptions<FlyballGameDaySettings>().Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection(nameof(FlyballGameDaySettings)).Bind(settings);
                    });
                })
                .Build();

            host.Run();
        }
    }
}