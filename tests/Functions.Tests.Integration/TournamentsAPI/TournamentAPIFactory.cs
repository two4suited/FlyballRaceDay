using Azure.Core.Serialization;
using DotNet.Testcontainers.Builders;
using FunctionHelper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using TournamentAPI;

namespace Functions.Tests.TournamentsAPI;

public class TournamentApiFactory : FunctionFactory,IAsyncLifetime
{
    private readonly MongoDbContainer _mongoDbContainer =
        new MongoDbBuilder().Build();
    public Task InitializeAsync()
        => _mongoDbContainer.StartAsync();
    public Task DisposeAsync()
        => _mongoDbContainer.DisposeAsync().AsTask();

    public override FunctionContext Context { get; set; }
    public override HttpRequestData Request { get; set; }

    public override void CreateFunction()
    {
        var tournamentApiSettings = new FlyballGameDaySettings()
        {
            CollectionName = "FlyballGameDay",
            ConnectionString = _mongoDbContainer.GetConnectionString(),
            DatabaseName = "FlyballGameDayDB"
        };
        
        Context = Substitute.For<FunctionContext>();
        Request = Substitute.For<HttpRequestData>(Context);
        var response = Substitute.For<HttpResponseData>(Context);
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(Options.Create(new WorkerOptions{Serializer = new JsonObjectSerializer()}));
        serviceCollection.AddSingleton(Options.Create(tournamentApiSettings));
        serviceCollection.AddSingleton(new TestingDateTimeService(DateTime.Now));

        Request.Headers.ReturnsForAnyArgs(new HttpHeadersCollection());
        response.Headers.ReturnsForAnyArgs(new HttpHeadersCollection());
        response.Body.ReturnsForAnyArgs(new MemoryStream());
        Request.CreateResponse().ReturnsForAnyArgs(response);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        Context.InstanceServices.ReturnsForAnyArgs(serviceProvider);
        
    }
}