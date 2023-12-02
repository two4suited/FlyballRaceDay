using FlyballRaceDay.ApiClient;
using Microsoft.AspNetCore.Http;
using Shouldly;

namespace FlyballRaceDay.Tests.ApiService.HttpTests;

[CollectionDefinition("Tournament Collection",DisableParallelization = true)]
public class TournamentCollection : ICollectionFixture<ApiServiceWebApplicationFactory<Program,FlyballRaceDayDbContext>>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

[Collection("Tournament Collection")]
public class TournamentApiTests(ApiServiceWebApplicationFactory<Program,FlyballRaceDayDbContext> factory) 
{
    [Fact]
    public async Task Create_ShouldReturn200()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/tournament");
        
        response.EnsureSuccessStatusCode();
    }
  
    [Fact]
    public async Task Create_SameDayTournamentShouldReturnOnGet()
    {
        using var client = factory.CreateClient();
        var apiClient = new ApiServiceClient(client);

        var newTournament = new TournamentCreate()
        {
            EventName = "test",
            EndDate = DateTimeOffset.Now,
            StartDate = DateTimeOffset.Now,
            NumberOfRings = 2
        };
        await apiClient.TournamentCreateAsync(newTournament, new CancellationToken());

        var tournaments = await apiClient.TournamentGetActiveAsync();
        
        tournaments.Count.ShouldBe(1);
    }
    
    [Fact]
    public async Task Delete_ShouldRemoveTournament()
    {
        using var client = factory.CreateClient();
        var apiClient = new ApiServiceClient(client);

        var newTournament = new TournamentCreate()
        {
            EventName = "test",
            EndDate = DateTimeOffset.Now,
            StartDate = DateTimeOffset.Now,
            NumberOfRings = 2
        };
        var newTournamentResponse = await apiClient.TournamentCreateAsync(newTournament, new CancellationToken());

        await apiClient.TournamentDeleteAsync(newTournamentResponse.Id);

        var statusCode = 200;
        
        try
        {
            await  apiClient.TournamentGetByIdAsync(newTournamentResponse.Id);
        }
        catch (ApiException e)
        {
            statusCode = e.StatusCode;
        }
        statusCode.ShouldBe(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Update_ChangeEventNameReturnsNewEventName()
    {
        using var client = factory.CreateClient();
        var apiClient = new ApiServiceClient(client);

        var tournaments = await apiClient.TournamentGetActiveAsync();
        var tournament = tournaments.First();

        var tournamentCreate = new TournamentCreate()
        {
            EventName = "Updated",
            EndDate = tournament.EndDate,
            StartDate = tournament.StartDate,
            NumberOfRings = tournament.NumberOfRings
        };

        var updatedTournament = await apiClient.TournamentUpdateAsync(tournament.Id,tournamentCreate);
        
        updatedTournament.EventName.ShouldBe("Updated");

    }
}