using ApiIsolated.Models;
using BlazorApp.Shared;
using FluentAssertions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DB.IntegrationTests;

public class TournamentServiceTests : IClassFixture<DatabaseFixture>
{
    private readonly ITournamentService _sut;
    private readonly IMongoCollection<TournamentDataModel> _collection;
    private readonly IDateTimeService _dateTimeService;

    public TournamentServiceTests(DatabaseFixture fixture)
    {
        _dateTimeService = new TestingDataTimeService();
        _sut = new TournamentService(fixture.Settings,_dateTimeService);
        _collection = fixture.Database.GetCollection<TournamentDataModel>(
            nameof(Tournament));
        fixture.Database.DropCollection(nameof(Tournament));
        
    }

    [Theory, AutoData]
    public async Task Create_Should_InsertOneRecord(TournamentDataModel tournamentDataModel)
    {
        await _sut.Create(tournamentDataModel);
        
        var filter = Builders<TournamentDataModel>.Filter.Where(x => x.Id == tournamentDataModel.Id);
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

    [Theory, AutoData]
    public async Task Update_Should_UpdateEventName(TournamentDataModel tournamentDataModel)
    {
        var newName = "I Am New";
        await _collection.InsertOneAsync(tournamentDataModel);
        tournamentDataModel.EventName = "I Am New";

        await _sut.Update(tournamentDataModel);
        
        var filter = Builders<TournamentDataModel>.Filter.Where(x => x.Id == tournamentDataModel.Id);
        var documents = await _collection.Find(filter).FirstAsync();
        documents.EventName.Should().Be(newName);
    }

    [Theory, AutoData]
    public async Task Delete_Should_DeleteRecord(TournamentDataModel tournamentDataModel)
    {
        await _collection.InsertOneAsync(tournamentDataModel);
        var filter = Builders<TournamentDataModel>.Filter.Where(x => x.Id == tournamentDataModel.Id);
        var documents = await _collection.FindAsync(filter);
        documents.ToList().Count.Should().Be(1);

        await _sut.Delete(tournamentDataModel.Id);
        documents = await _collection.FindAsync(filter);
        documents.ToList().Count.Should().Be(0);
    }

    [Theory, AutoData]
    public async Task GetAllActive_Should_OnlyReturnActive(TournamentDataModel futureTournament1,
        TournamentDataModel futureTournament2, TournamentDataModel pastTournament1)
    {
        futureTournament1.StartDate = _dateTimeService.CurrentDay.AddDays(1);
        futureTournament1.EndDate = futureTournament1.StartDate.AddDays(1);
        futureTournament2.StartDate = _dateTimeService.CurrentDay.AddDays(5);
        futureTournament2.EndDate = futureTournament2.StartDate.AddDays(1);
        pastTournament1.StartDate = _dateTimeService.CurrentDay.AddDays(-5);
        pastTournament1.EndDate = pastTournament1.StartDate.AddDays(1);
        await _collection.InsertOneAsync(futureTournament1);
        await _collection.InsertOneAsync(futureTournament2);
        await _collection.InsertOneAsync(pastTournament1);
        

        var tournaments = await _sut.GetAllActive();

        tournaments.Count().Should().Be(2);
    }

    [Fact]
    public async Task GetAllActive_Should_ReturnZeroWithNoFutureTournaments()
    {
        var tournaments = await _sut.GetAllActive();

        tournaments.Count().Should().Be(0);
    }
}