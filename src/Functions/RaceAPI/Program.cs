using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Template;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
      .ConfigureServices((hostcontext, services) =>
    {
        if (hostcontext.HostingEnvironment.IsDevelopment())
        {
            services.AddInMemoryCosmosRepository();
        }
        else
        {
            services.AddCosmosRepository(options =>
            {
                options.ContainerPerItemType = true;
                options.ContainerBuilder.Configure<TemplateModel>(containerOptions => containerOptions   
                    .WithServerlessThroughput()                                
                );
            });
        }
    })
    .Build();

host.Run();
