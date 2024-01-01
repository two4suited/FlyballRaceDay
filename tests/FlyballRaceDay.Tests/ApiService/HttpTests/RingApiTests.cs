namespace FlyballRaceDay.Tests.ApiService.HttpTests;

[CollectionDefinition("Ring Collection",DisableParallelization = true)]
public class RingCollection : ICollectionFixture<ApiServiceWebApplicationFactory<Program>>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}

[Collection("Ring Collection")]

public class RingApiTests(ApiServiceWebApplicationFactory<Program> factory)
{
    [Fact]
    public async Task Get_ShouldReturn200()
    {
        using var client = factory.CreateClient();

        var response = await client.GetAsync("/ring/1/getrings");
        
        response.EnsureSuccessStatusCode();
    }
}