using FlyballRaceDay.ApiService.Tournament;
using Shouldly;
using Testcontainers.PostgreSql;

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
        currentDayTournament.EndDate = DateOnly.FromDateTime(DateTime.Now);
        currentDayTournament.StartDate = DateOnly.FromDateTime(DateTime.Now);

        dbContext.Tournaments.Add(currentDayTournament);
        await dbContext.SaveChangesAsync();
        
        var sut = new TournamentService(dbContext, TimeProvider.System, fixture.Logger);
        var tournaments = await sut.GetActiveTournaments();
        
        tournaments.Count.ShouldBe(1);
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

