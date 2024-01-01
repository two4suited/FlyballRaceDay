namespace FlyballRaceDay.Tests.ApiService;

[CollectionDefinition("IntegrationTests collection")]
public class TournamentServiceTests(IntegrationTestFixture fixture) : IClassFixture<IntegrationTestFixture>
{
    [Fact]
    public async Task GetActiveTournaments_ShouldReturnSameDayTournaments()
    {
        var provider = new IntegrationTestDatabaseProvider(fixture.ConnectionString());
        var dbContext = provider.CreateDbContext();

        var currentDayTournament = fixture.TournamentGenerator.Generate();
        currentDayTournament.EndDate = DateTime.Now.Date;
        currentDayTournament.StartDate = DateTime.Now.Date;

        dbContext.Tournaments.Add(currentDayTournament);
        await dbContext.SaveChangesAsync();

        var timeProvider = TimeProvider.System;
        
        var sut = new TournamentService(dbContext, timeProvider, fixture.Logger.CreateLogger<TournamentService>());
        var result = await sut.GetActiveTournaments();
        
        var okResult = (Ok<List<TournamentView>>)result;
        
        okResult.Value.Count.ShouldBe(1);
    }

    [Fact]
    public async Task GetActiveTournaments_ShouldReturnSameDayTournaments_WithDatetimeOffset()
    {
        var provider = new IntegrationTestDatabaseProvider(fixture.ConnectionString());
        var dbContext = provider.CreateDbContext();

        var currentDayTournament = fixture.TournamentCreateGenerator.Generate();
        //currentDayTournament.EndDate = DateOnly.FromDateTime(DateTimeOffset.Now);
    }
}

