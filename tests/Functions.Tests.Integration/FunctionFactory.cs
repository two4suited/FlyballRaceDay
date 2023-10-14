namespace Functions.Tests;

public  class FunctionFactory : IAsyncLifetime
{
    private readonly MongoDbContainer _mongoDbContainer =
        new MongoDbBuilder().Build();
    public Task InitializeAsync()
        => _mongoDbContainer.StartAsync();
    public Task DisposeAsync()
        => _mongoDbContainer.DisposeAsync().AsTask();

    public  FunctionContext Context { get; set; }
    public  HttpRequestData Request { get; set; }
    public IOptions<FlyballGameDaySettings> OptionsForDatabase { get; set; }
    public ILoggerFactory Logger { get; set; }
    public IDateTimeService DateTimeService { get; set; }
   
    public  void CreateFunction()
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
        
        
       Logger = Substitute.For<ILoggerFactory>();
       OptionsForDatabase = serviceProvider.GetService<IOptions<FlyballGameDaySettings>>();
       DateTimeService = (IDateTimeService)serviceProvider.GetService(typeof(IDateTimeService))!;
       
    }
}