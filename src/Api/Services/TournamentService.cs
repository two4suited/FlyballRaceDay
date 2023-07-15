using System.Collections.Generic;
using System.Threading.Tasks;
using ApiIsolated.Models;
using BlazorApp.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiIsolated.Services;

public class TournamentService : BaseService<TournamentDataModel>,ITournamentService
{
    public TournamentService(IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings) : base(flyballStoreDatabaseSettings,nameof(Tournament))
    {
       
    }
    
    public async Task<IEnumerable<Tournament>> GetAllActive()
    {
        throw new System.NotImplementedException();
    }

    public async Task Create(TournamentDataModel tournament) =>
        await Collection.InsertOneAsync(tournament);


    public async Task Update(TournamentDataModel tournament)
    {
        var filter = Builders<TournamentDataModel>.Filter.Where(x => x.Id == tournament.Id);
        await Collection.ReplaceOneAsync(filter,tournament);  
    } 

    public async Task Delete(string tournamentId)
    {
        throw new System.NotImplementedException();
    }
}