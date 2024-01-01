using System.Net;
using FlyballRace.APIClient;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using TournamentCreate = FlyballRace.APIClient.TournamentCreate;
using TournamentView = FlyballRace.APIClient.TournamentView;

namespace FlyballRaceDay.Tests.ApiService.HttpTests;

[CollectionDefinition("Tournament Collection",DisableParallelization = true)]
public class TournamentCollection : ICollectionFixture<ApiServiceWebApplicationFactory<Program>>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

[Collection("Tournament Collection")]
public class TournamentApiTests(ApiServiceWebApplicationFactory<Program> factory) 
{
    [Fact]
    public async Task Get_ShouldReturn200()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/tournament");
        
        response.EnsureSuccessStatusCode();
    }
  
    [Fact]
    public async Task Create_SameDayTournamentShouldReturnOnGet()
    {
        var context = factory.Services.GetRequiredService<FlyballRaceDayDbContext>();
        var tournamentCreate = factory.TournamentGenerator.Generate();
        tournamentCreate.Id = Guid.NewGuid().ToString();
        context.Tournaments.Add(tournamentCreate);
        await context.SaveChangesAsync();
        
        using var client = factory.CreateClient();
        var apiClient = RestService.For<IFlyballRaceDayApiService>(client);
        var response = await apiClient.TournamentGetActive();
        
        context.Tournaments.Remove(tournamentCreate);
        await context.SaveChangesAsync();
        
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
        var context = factory.Services.GetRequiredService<FlyballRaceDayDbContext>();
        var tournamentCreate = factory.TournamentGenerator.Generate();
        context.Tournaments.Add(tournamentCreate);
        await context.SaveChangesAsync();

        var tournamentToUpdate = new TournamentCreate()
        {
            EventName = tournamentCreate.EventName,
            StartDate = tournamentCreate.StartDate,
            EndDate = tournamentCreate.EndDate,
            NumberOfRings = tournamentCreate.NumberOfRings
        };
        
        using var client = factory.CreateClient();
        var apiClient = RestService.For<IFlyballRaceDayApiService>(client);
        var tournamentResponse = await apiClient.TournamentGetById(tournamentCreate.Id);

        tournamentToUpdate.EventName = "Updated";
        var updateResponse = await apiClient.TournamentUpdate(tournamentCreate.Id,tournamentToUpdate);
        var updatedTournament = updateResponse.Content;
        
        context.Tournaments.Remove(tournamentCreate);
        await context.SaveChangesAsync();
        
        updatedTournament!.EventName.ShouldBe("Updated");

    }

    [Fact]
    public async Task GetById_ShouldReturnOneItemWithSameID()
    {
        var context = factory.Services.GetRequiredService<FlyballRaceDayDbContext>();
        var tournamentCreate = factory.TournamentGenerator.Generate();
        context.Tournaments.Add(tournamentCreate);
        await context.SaveChangesAsync();
        
        using var client = factory.CreateClient();
        var apiClient = RestService.For<IFlyballRaceDayApiService>(client);
        var tournamentResponse = await apiClient.TournamentGetById(tournamentCreate.Id);

        var tournament = tournamentResponse.Content;

        context.Tournaments.Remove(tournamentCreate);
        await context.SaveChangesAsync();
        
        tournament.Id.ShouldBe(tournamentCreate.Id);



    }
}