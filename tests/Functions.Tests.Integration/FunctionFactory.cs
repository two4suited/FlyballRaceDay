namespace Functions.Tests;

public  class FunctionFactory : IAsyncLifetime
{
    public readonly MongoDbContainer _mongoDbContainer =
        new MongoDbBuilder().Build();

    public Task InitializeAsync()
        => _mongoDbContainer.StartAsync();
    public Task DisposeAsync()
        => _mongoDbContainer.DisposeAsync().AsTask();
   
    public  HttpRequestData Request { get; set; }
    public IOptions<FlyballGameDaySettings> OptionsForDatabase { get; set; }
    public ILoggerFactory Logger { get; set; }
    public IDateTimeService DateTimeService { get; set; }
    
    
   
    public  void CreateFunction(string databaseName)
    {
        var tournamentApiSettings = new FlyballGameDaySettings()
        {
            CollectionName = "FlyballGameDay",
            ConnectionString = _mongoDbContainer.GetConnectionString(),
            DatabaseName = databaseName
        };
        
        var context = Substitute.For<FunctionContext>();
        Request = Substitute.For<HttpRequestData>(context);
        var response = Substitute.For<HttpResponseData>(context);
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(Options.Create(new WorkerOptions{Serializer = new JsonObjectSerializer()}));
        serviceCollection.AddSingleton(Options.Create(tournamentApiSettings));
        serviceCollection.AddSingleton<IDateTimeService, TestingDateTimeService>();

        Request.Headers.ReturnsForAnyArgs(new HttpHeadersCollection());
        response.Headers.ReturnsForAnyArgs(new HttpHeadersCollection());
        response.Body.ReturnsForAnyArgs(new MemoryStream());
        Request.CreateResponse().ReturnsForAnyArgs(response);
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        context.InstanceServices.ReturnsForAnyArgs(serviceProvider);
        
       Logger = Substitute.For<ILoggerFactory>();
       OptionsForDatabase = serviceProvider.GetService<IOptions<FlyballGameDaySettings>>();
       DateTimeService = (IDateTimeService)serviceProvider.GetService(typeof(IDateTimeService))!;
       
    }
}