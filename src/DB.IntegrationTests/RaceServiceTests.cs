using ApiIsolated.Models;
using BlazorApp.Shared;
using FluentAssertions;
using MongoDB.Driver;

namespace DB.IntegrationTests;

public class RaceServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly IRaceService _sut;
    private readonly IMongoCollection<RaceDataModel> _collection;
    private readonly string _tournamentId = "1";
    public RaceServiceTests(DatabaseFixture fixture)
    {
        _sut = new RaceService(fixture.Settings);
        _collection = fixture.Database.GetCollection<RaceDataModel>(
            nameof(Race));
        fixture.Database.DropCollection(nameof(Race));
    }
    
    [Theory, AutoData]
    public async Task UploadSchedule_Should_CreateNumberOfRaces(List<RaceDataModel> races)
    {
        foreach (var race in races)
        {
            race.TournamentId = _tournamentId;
        }
        
        await _sut.UploadSchedule(races);
        
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.TournamentId == _tournamentId);
        var documents = await _collection.FindAsync(filter);
        documents.ToList().Count.Should().Be(races.Count);
    }
    [Theory, AutoData]
    public async Task UploadSchedule_Should_CreateNumberOfRecords(RaceDataModel race1, RaceDataModel race2)
    {
        race1.TournamentId = _tournamentId;
        race2.TournamentId = "2";
        var races = new List<RaceDataModel>
        {
            race1,
            race2
        };

        await _sut.UploadSchedule(races);
        
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.TournamentId == _tournamentId);
        var documents = await _collection.FindAsync(filter);
        documents.ToList().Count.Should().Be(1);
    }
    
    [Theory, AutoData]
    public async Task GetSchedule_Should_CreateNumberOfRaces(List<RaceDataModel> races)
    {
        foreach (var race in races)
        {
            race.TournamentId = _tournamentId;
        }
        await _collection.InsertManyAsync(races);
        
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.TournamentId == _tournamentId);
        var documents = await _collection.FindAsync(filter);
        documents.ToList().Count.Should().Be(races.Count);
    }
    [Theory, AutoData]
    public async Task AddRaceToRing_Should_BeRingId(RaceDataModel race)
    {
        string ringId = "1";
        await _collection.InsertOneAsync(race);

        await _sut.AddRaceToRing(race.TournamentId, race.RaceNumber, ringId);
        
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.TournamentId == race.TournamentId && x.RaceNumber == race.RaceNumber);
        var documents = await _collection.FindAsync(filter);
        var updatedRace = await documents.FirstAsync();
        updatedRace.RingId.Should().Be(ringId);
    }
    
    [Theory, AutoData]
    public async Task MarkAsDone_Should_Done(RaceDataModel race)
    {
        string ringId = "1";
        race.Done = false;
        await _collection.InsertOneAsync(race);

        await _sut.MarkRaceAsDone(race.TournamentId, race.RaceNumber);
        
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.TournamentId == race.TournamentId && x.RaceNumber == race.RaceNumber);
        var documents = await _collection.FindAsync(filter);
        var updatedRace = await documents.FirstAsync();
        updatedRace.Done.Should().BeTrue();
    }
}