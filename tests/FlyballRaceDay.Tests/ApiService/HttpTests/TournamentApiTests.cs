using System.Net;
using FlyballRace.APIClient;
using Refit;
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
        var apiClient = RestService.For<IFlyballRaceDayApiService>(client);
   
        var newTournament = new TournamentCreate()
        {
            EventName = "test",
            EndDate = DateTimeOffset.Now,
            StartDate = DateTimeOffset.Now,
            NumberOfRings = 2
        };
        await apiClient.TournamentCreate(newTournament);

        var response = await apiClient.TournamentGetActive();
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response.Content?.Count.ShouldBe(1);
    }
    
    [Fact]
    public async Task Delete_ShouldRemoveTournament()
    {
        using var client = factory.CreateClient();
        var apiClient = RestService.For<IFlyballRaceDayApiService>(client);

        var newTournament = new TournamentCreate()
        {
            EventName = "test",
            EndDate = DateTimeOffset.Now,
            StartDate = DateTimeOffset.Now,
            NumberOfRings = 2
        };
        var createResponse = await apiClient.TournamentCreate(newTournament);
        createResponse.StatusCode.ShouldBe(HttpStatusCode.Created);
        var newTournamentResponse = createResponse.Content;
        
        await apiClient.TournamentDelete(newTournamentResponse!.Id);

        var getResponse = await apiClient.TournamentGetById(newTournamentResponse.Id);
        getResponse.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Update_ChangeEventNameReturnsNewEventName()
    {
        using var client = factory.CreateClient();
        var apiClient = RestService.For<IFlyballRaceDayApiService>(client);

        var getResponse = await apiClient.TournamentGetActive();
        getResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var tournament = getResponse.Content!.First();

        var tournamentCreate = new TournamentCreate()
        {
            EventName = "Updated",
            EndDate = tournament.EndDate,
            StartDate = tournament.StartDate,
            NumberOfRings = tournament.NumberOfRings
        };

        var updateResponse = await apiClient.TournamentUpdate(tournament.Id,tournamentCreate);
        var updatedTournament = updateResponse.Content;
        
        updatedTournament!.EventName.ShouldBe("Updated");

    }
}