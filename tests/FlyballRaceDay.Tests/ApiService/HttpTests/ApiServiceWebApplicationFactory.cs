
using FlyballRace.APIClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Testcontainers.MongoDb;
using TournamentCreate = FlyballRaceDay.ApiService.Tournament.TournamentCreate;

namespace FlyballRaceDay.Tests.ApiService.HttpTests;

public class ApiServiceWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>,IAsyncLifetime where TProgram :class 
{
    private MongoDbContainer _container  { get; set; }

    public Faker<Tournament> TournamentGenerator = new Faker<Tournament>()
        .RuleFor(x => x.Id,Guid.NewGuid().ToString)
        .RuleFor(x => x.StartDate, DateTime.Now)
        .RuleFor(x => x.EventName, faker => faker.Lorem.Sentence())
        .RuleFor(x => x.EndDate, DateTime.Now)
        .RuleFor(x => x.NumberOfRings, faker => faker.Random.Number(1,10));
    
    public Faker<TournamentCreate> TournamentCreateGenerator = new Faker<TournamentCreate>()
        .RuleFor(x => x.StartDate, DateTime.Now)
        .RuleFor(x => x.EventName, faker => faker.Lorem.Sentence())
        .RuleFor(x => x.EndDate, DateTime.Now)
        .RuleFor(x => x.NumberOfRings, faker => faker.Random.Number(1,10));
    
    public Faker<RaceCreate> RaceCreateGenerator = new Faker<RaceCreate>()
        .RuleFor(x => x.TournamentId, Guid.NewGuid().ToString())
        .RuleFor(x => x.Done, false)
        .RuleFor(x => x.Breakout, "18.0")
        .RuleFor(x => x.RaceNumber, faker => faker.Random.Number(1,10))
        .RuleFor(x => x.RingId, Guid.NewGuid().ToString())
        .RuleFor(x => x.BreakTimeInMinutes, 0)
        .RuleFor(x => x.IsBreak, false);
    
    
    public ApiServiceWebApplicationFactory()
    {
        _container = new MongoDbBuilder().Build();
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContext>();
            services.RemoveAll<IMongoDatabase>();
            
            var client = new MongoClient(_container.GetConnectionString());
            IMongoDatabase database = client.GetDatabase(Guid.NewGuid().ToString());
            services.AddScoped<IMongoDatabase>(d => database);
            services.AddDbContext<FlyballRaceDayDbContext>();
        });
        
        builder.UseEnvironment("Tests");
    }

    public async Task InitializeAsync() => await _container.StartAsync();

    public new async Task DisposeAsync() => await _container.DisposeAsync();
}