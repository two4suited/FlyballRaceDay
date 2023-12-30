using Testcontainers.MongoDb;

namespace FlyballRaceDay.Tests.ApiService;

public class IntegrationTestFixture : IAsyncLifetime
{
    public MongoDbContainer? _databaseTestContainer { get; set; }
    public ILoggerFactory Logger { get; set; }
    
    public Faker<Tournament> TournamentGenerator = new Faker<Tournament>()
        .RuleFor(x => x.StartDate, DateTime.Now)
        .RuleFor(x => x.EventName, faker => faker.Lorem.Sentence())
        .RuleFor(x => x.EndDate, DateTime.Now)
        .RuleFor(x => x.NumberOfRings, faker => faker.Random.Number(1,10));
    
    public Faker<TournamentCreate> TournamentCreateGenerator = new Faker<TournamentCreate>()
        .RuleFor(x => x.StartDate, DateTime.Now)
        .RuleFor(x => x.EventName, faker => faker.Lorem.Sentence())
        .RuleFor(x => x.EndDate, DateTime.Now)
        .RuleFor(x => x.NumberOfRings, faker => faker.Random.Number(1,10));
    
    public async Task InitializeAsync()
    {
        _databaseTestContainer = new MongoDbBuilder().Build();
        await _databaseTestContainer.StartAsync();
        Logger = Logger = Substitute.For<ILoggerFactory>();
    }

    public async Task DisposeAsync() => await _databaseTestContainer.DisposeAsync();
    public string ConnectionString() => _databaseTestContainer.GetConnectionString();


}
[CollectionDefinition("IntegrationTests collection")]
public class IntegrationTestCollection : ICollectionFixture<IntegrationTestFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}