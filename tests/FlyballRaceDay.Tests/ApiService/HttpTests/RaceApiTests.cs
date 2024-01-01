namespace FlyballRaceDay.Tests.ApiService.HttpTests;

[CollectionDefinition("Race Collection",DisableParallelization = true)]
public class RaceCollection : ICollectionFixture<ApiServiceWebApplicationFactory<Program>>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

[Collection("Race Collection")]

public class RaceApiTests(ApiServiceWebApplicationFactory<Program> factory)
{
    [Fact]
    public async Task Get_ShouldReturn200()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/race/schedule/1");
        
        response.EnsureSuccessStatusCode();
    }
}