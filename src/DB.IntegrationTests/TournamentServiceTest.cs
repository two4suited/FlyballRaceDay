using ApiIsolated.Models;
using BlazorApp.Shared;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DB.IntegrationTests;

public class TournamentServiceTest : IClassFixture<DatabaseFixture>
{
    private readonly ITournamentService _sut;
    private readonly IMongoCollection<TournamentDataModel> _collection;

    public TournamentServiceTest(DatabaseFixture fixture)
    {
        _sut = new TournamentService(fixture.Settings);
        _collection = fixture.Database.GetCollection<TournamentDataModel>(
            nameof(Tournament));
        fixture.Database.DropCollection(nameof(Tournament));
        
    }

    [Theory, AutoData]
    public async Task Create_Should_InsertOneRecord(TournamentDataModel tournamentDataModel)
    {
        await _sut.Create(tournamentDataModel);
        
        var filter = Builders<TournamentDataModel>.Filter.Empty;
        var documents = await _collection.FindAsync(filter);
        documents.ToList().Count.Should().Be(1);
    }
    
    [Theory, AutoData]
    public async Task Create_Should_InsertOneRecord_EventNameMatch(TournamentDataModel tournamentDataModel)
    {
        await _sut.Create(tournamentDataModel);
        
        var filter = Builders<TournamentDataModel>.Filter.Where(x => x.Id == tournamentDataModel.Id);
        var documents = await _collection.Find(filter).FirstAsync();
        documents.EventName.Should().Be(tournamentDataModel.EventName);
    }
}