namespace Functions.Tests.TournamentsAPI;

public class BaseTournamentApiTests : IClassFixture<FunctionFactory>
{
    protected FunctionFactory _factory;
    protected TournamentApiFunctions _fut;
    
    protected readonly Faker<Tournament> _tournamentGenerator = new Faker<Tournament>()
        .RuleFor(x => x.StartDate, DateTime.Now.AddDays(5))
        .RuleFor(x => x.EventName, faker => faker.Lorem.Sentence())
        .RuleFor(x => x.EndDate, DateTime.Now.AddDays(5))
        .RuleFor(x => x.Id, faker => Guid.NewGuid().ToString()) 
        .RuleFor(x => x.NumberOfLanes, faker => faker.Random.Number(1,10));
    
    protected readonly Faker<TournamentDataModel> _tournamentDataGenerator = new Faker<TournamentDataModel>()
        .RuleFor(x => x.StartDate, DateTime.Now.AddDays(5))
        .RuleFor(x => x.EventName, faker => faker.Lorem.Sentence())
        .RuleFor(x => x.EndDate, DateTime.Now.AddDays(5))
        .RuleFor(x => x.Id, faker => Guid.NewGuid().ToString()) 
        .RuleFor(x => x.NumberOfLanes, faker => faker.Random.Number(1,10));

    protected BaseTournamentApiTests(FunctionFactory factory)
    {
        factory.CreateFunction(nameof(Tournament));
        _factory = factory;
        _fut = new TournamentApiFunctions(_factory.Logger, _factory.OptionsForDatabase, _factory.DateTimeService);
    }
}