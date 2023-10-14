using MongoDB.Driver;

namespace Functions.Tests;

public sealed class MongoDbContainerTest : IAsyncLifetime
{
    private readonly MongoDbContainer _mongoDbContainer =
        new MongoDbBuilder().Build();

    [Fact]
    public async Task ReadFromMongoDbDatabase()
    {
        var client = new MongoClient(_mongoDbContainer.GetConnectionString());

        using var databases = await client.ListDatabasesAsync();

        Assert.True(await databases.AnyAsync());
    }

    public Task InitializeAsync()
        => _mongoDbContainer.StartAsync();

    public Task DisposeAsync()
        => _mongoDbContainer.DisposeAsync().AsTask();
}