using FlyballRaceDay.ApiService.Services;

namespace FlyballRaceDay.ApiService.Tournament;

public class TournamentService(FlyballRaceDayDbContext context,TimeProvider timeProvider,ILogger<TournamentService> logger) : DataService<Database.Tournament,TournamentCreate,TournamentView>(logger,context),ITournamentService
{
    public async Task<IResult> CreateTournament(TournamentCreate tournamentCreate)
    {
        return await Create(tournamentCreate);
    }

    public async Task<IResult> GetActiveTournaments()
    {
        var currentDate = timeProvider.GetLocalNow().DateTime.Date;
        return await Where(x => x.StartDate >= currentDate);
    }
    public async Task<IResult> UpdateTournament(TournamentCreate tournamentCreate, string id)
    {
        return await Update(tournamentCreate, id);
    }

    public async Task<IResult> DeleteTournament(string id)
    {
       return await Delete(id);
    }

    public async Task<IResult> GetTournament(string id)
    {
        return await GetById(id);
    }
}