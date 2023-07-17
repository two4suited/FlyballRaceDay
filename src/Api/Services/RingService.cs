using System.Collections.Generic;
using System.Threading.Tasks;
using ApiIsolated.Helpers;
using ApiIsolated.Models;
using BlazorApp.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiIsolated.Services;

public class RingService : BaseService<RingDataModel>,IRingService
{
    public RingService(IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings)  : base(flyballStoreDatabaseSettings,nameof(Ring))
    {
        
    }
    public async Task Update(RingDataModel model)
    {
        var filter = Builders<RingDataModel>.Filter.Where(x => x.Id == model.Id);
        await Collection.ReplaceOneAsync(filter,model);  
    }

    public async Task Create(RingDataModel model) => await Collection.InsertOneAsync(model);
    

    public async Task<List<Ring>> GetByTournamentId(string tournamentId)
    {
        var filter = Builders<RingDataModel>.Filter.Where(x => x.TournamentId == tournamentId);
        var documents =  await Collection.FindAsync(filter);
        var races = documents.ToList();
        return races.MapList(Mapper.Map<RingDataModel,Ring>);
    }

    public async Task Delete(string ringId)
    {
        var filter = Builders<RingDataModel>.Filter.Where(x => x.Id == ringId);
        await Collection.DeleteOneAsync(filter);
    }
}