namespace FlyballRaceDay.Tests.ApiService;

public class TournamentApiTests(ApiServiceWebApplicationFactory<Program,FlyballRaceDayDbContext> factory) : IClassFixture<ApiServiceWebApplicationFactory<Program,FlyballRaceDayDbContext>>
{
    [Fact]
    public async Task Create_ShouldReturn200()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/tournament");
        
        response.EnsureSuccessStatusCode();
    }
}