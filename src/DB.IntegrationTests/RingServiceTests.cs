using ApiIsolated.Models;
using BlazorApp.Shared;
using FluentAssertions;
using MongoDB.Driver;

namespace DB.IntegrationTests;

public class RingServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly IRingService _sut;
    private readonly IMongoCollection<RingDataModel> _collection;
    private readonly string _tournamentId = "1";
    public RingServiceTests(DatabaseFixture fixture)
    {
        _sut = new RingService(fixture.Settings);
        _collection = fixture.Database.GetCollection<RingDataModel>(
            nameof(Ring));
        fixture.Database.DropCollection(nameof(Ring));
    }
    [Theory, AutoData]
    public async Task Create_Should_InsertOneRecord(RingDataModel model)
    {
        await _sut.Create(model);
        
        var filter = Builders<RingDataModel>.Filter.Where(x => x.Id == model.Id);
        var documents = await _collection.FindAsync(filter);
        documents.ToList().Count.Should().Be(1);
    }
    [Theory, AutoData]
    public async Task Create_Should_InsertOneRecord_NameMatch(RingDataModel model)
    {
        await _sut.Create(model);
        
        var filter = Builders<RingDataModel>.Filter.Where(x => x.Id == model.Id);
        var documents = await _collection.Find(filter).FirstAsync();
        documents.Name.Should().Be(model.Name);
    }
    [Theory, AutoData]
    public async Task Update_Should_UpdateName(RingDataModel model)
    {
        var newName = "I Am New";
        await _collection.InsertOneAsync(model);
        model.Name = "I Am New";

        await _sut.Update(model);
        
        var filter = Builders<RingDataModel>.Filter.Where(x => x.Id == model.Id);
        var documents = await _collection.Find(filter).FirstAsync();
        documents.Name.Should().Be(newName);
    }

    [Theory, AutoData]
    public async Task Delete_Should_DeleteRecord(RingDataModel model)
    {
        await _collection.InsertOneAsync(model);
        var filter = Builders<RingDataModel>.Filter.Where(x => x.Id == model.Id);
        var documents = await _collection.FindAsync(filter);
        documents.ToList().Count.Should().Be(1);

        await _sut.Delete(model.Id);
        documents = await _collection.FindAsync(filter);
        documents.ToList().Count.Should().Be(0);
    }
    [Theory, AutoData]
    public async Task GetAllActive_Should_OnlyReturnActive(RingDataModel tournament1,
        RingDataModel tournament2)
    {
        tournament1.TournamentId = _tournamentId;
        tournament2.TournamentId = "2";
        await _collection.InsertOneAsync(tournament1);
        await _collection.InsertOneAsync(tournament2);
       

        var tournaments = await _sut.GetByTournamentId(_tournamentId);

        tournaments.Count().Should().Be(1);
    }

    [Fact]
    public async Task GetAllActive_Should_ReturnZeroWithNoFutureTournaments()
    {
        var tournaments = await _sut.GetByTournamentId(_tournamentId);

        tournaments.Count().Should().Be(0);
    }
}