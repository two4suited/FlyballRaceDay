using System.Collections.Generic;
using System.Threading.Tasks;
using ApiIsolated.Models;
using BlazorApp.Shared;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiIsolated.Services;

public class TournamentService : BaseService<TournamentDataModel>,ITournamentService
{
    private readonly IDateTimeService _dateTimeService;

    public TournamentService(IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings,IDateTimeService dateTimeService) : base(flyballStoreDatabaseSettings,nameof(Tournament))
    {
        _dateTimeService = dateTimeService;
    }
    
    public async Task<IEnumerable<Tournament>> GetAllActive()
    {
        var filter = Builders<TournamentDataModel>.Filter.Where(x => x.StartDate > _dateTimeService.CurrentDay);
        var documents =  await Collection.FindAsync(filter);
        return TournamentDataModel.ToTournamentList(documents.ToList());
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
        var filter = Builders<TournamentDataModel>.Filter.Where(x => x.Id == tournamentId);
        await Collection.DeleteOneAsync(filter);
    }
}