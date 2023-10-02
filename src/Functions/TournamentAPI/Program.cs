using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(s =>
                {
                    s.AddSingleton<IDateTimeService, DateTimeService>();
                    s.AddOptions<FlyballGameDaySettings>().Configure<IConfiguration>((settings, configuration) =>
                    {
                        configuration.GetSection(nameof(FlyballGameDaySettings)).Bind(settings);
                    });
                })
                .Build();

            host.Run();
