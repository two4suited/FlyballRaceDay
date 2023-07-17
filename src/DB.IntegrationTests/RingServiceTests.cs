using ApiIsolated.Models;
using BlazorApp.Shared;
using MongoDB.Driver;

namespace DB.IntegrationTests;

public class RingServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly IRingService _sut;
    private readonly IMongoCollection<RingDataModel> _collection;
    public RingServiceTests(DatabaseFixture fixture)
    {
        _sut = new RingService(fixture.Settings);
        _collection = fixture.Database.GetCollection<RingDataModel>(
            nameof(Ring));
        fixture.Database.DropCollection(nameof(Tournament));
    }
}