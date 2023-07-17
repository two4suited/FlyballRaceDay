using System.Collections.Generic;
using System.Threading.Tasks;
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
        return RingDataModel.ToRingList(documents.ToList());
    }

    public async Task Delete(string ringId)
    {
        var filter = Builders<RingDataModel>.Filter.Where(x => x.Id == ringId);
        await Collection.DeleteOneAsync(filter);
    }
}