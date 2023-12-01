using System.Net.Http.Json;
using FlyballRaceDay.Shared;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Shouldly;
using Xunit.Abstractions;

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

        var newTournament = new FlyballRaceDay.Shared.TournamentCreate()
        {
            EventName = "test",
            EndDate = DateTimeOffset.Now,
            StartDate = DateTimeOffset.Now,
            NumberOfRings = 2
        };
        await apiClient.TournamentPOSTAsync(newTournament, new CancellationToken());

        var tournaments = await apiClient.TournamentAllAsync();
        
        tournaments.Count.ShouldBe(1);
    }
    
    [Fact]
    public async Task Delete_ShouldRemoveTournament()
    {
        using var client = factory.CreateClient();
        var apiClient = new ApiServiceClient(client);

        var newTournament = new FlyballRaceDay.Shared.TournamentCreate()
        {
            EventName = "test",
            EndDate = DateTimeOffset.Now,
            StartDate = DateTimeOffset.Now,
            NumberOfRings = 2
        };
        var newTournamentResponse = await apiClient.TournamentPOSTAsync(newTournament, new CancellationToken());

        await apiClient.TournamentDELETEAsync(newTournamentResponse.Id);

        var blankTournament = apiClient.TournamentGETAsync(newTournamentResponse.Id);
        
        blankTournament.ShouldBeNull();
    }

    [Fact]
    public async Task Update_ChangeEventNameReturnsNewEventName()
    {
        using var client = factory.CreateClient();
        var apiClient = new ApiServiceClient(client);

        var tournaments = await apiClient.TournamentAllAsync();
        var tournament = tournaments.First();

        var tournamentCreate = new TournamentCreate()
        {
            EventName = "Updated",
            EndDate = tournament.EndDate,
            StartDate = tournament.StartDate,
            NumberOfRings = tournament.NumberOfRings
        };

        var updatedTournament = await apiClient.TournamentPUTAsync(tournament.Id,tournamentCreate);
        
        updatedTournament.EventName.ShouldBe("Updated");

    }
}