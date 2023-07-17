using System.Collections.Generic;
using System.Threading.Tasks;
using ApiIsolated.Helpers;
using ApiIsolated.Models;
using BlazorApp.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiIsolated.Services;

public class RaceService : BaseService<RaceDataModel>, IRaceService
{
    private readonly IOptions<FlyballGameDaySettings> _flyballStoreDatabaseSettings;

    public RaceService(IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings)  : base(flyballStoreDatabaseSettings,nameof(Race)) {}

    public Task<IEnumerable<Race>> GetUpcomingRaces(string tournamentId, List<Ring> rings)
    {
        throw new System.NotImplementedException();
    }

    public async Task MarkRaceAsDone(string tournamentId, string raceNumber)
    {
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.TournamentId == tournamentId && x.RaceNumber == raceNumber);
        var update = Builders<RaceDataModel>.Update.Set(nameof(RaceDataModel.Done), true);
        await Collection.UpdateOneAsync(filter, update);
    }

    public async Task AddRaceToRing(string tournamentId, string raceNumber, string ringId)
    {
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.TournamentId == tournamentId && x.RaceNumber == raceNumber);
        var update = Builders<RaceDataModel>.Update.Set(x => x.RingId, ringId);
        await Collection.UpdateOneAsync(filter, update);
    }

    public async Task UploadSchedule(List<RaceDataModel> races)
    {
        await Collection.InsertManyAsync(races);
    }

    public async Task<List<Race>> GetSchedule(string tournamentId)
    {
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.TournamentId == tournamentId);
        var documents = await Collection.FindAsync(filter);
        var races = documents.ToList();
        return races.MapList(Mapper.Map<RaceDataModel, Race>);
    }
}