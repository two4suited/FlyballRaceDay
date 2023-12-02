using FlyballRaceDay.ApiService.Services;

namespace FlyballRaceDay.ApiService.Tournament;

public class TournamentService(FlyballRaceDayDbContext context,TimeProvider timeProvider,ILoggerFactory loggerFactory) : DataService<Database.Tournament,TournamentCreate,TournamentView>(loggerFactory,context),ITournamentService
{
    public async Task<IResult> CreateTournament(TournamentCreate tournamentCreate)
    {
        return await Create(tournamentCreate);
    }

    public async Task<IResult> GetActiveTournaments()
    {
        return await Where(x => x.StartDate >= DateOnly.FromDateTime(timeProvider.GetLocalNow().DateTime));
    }
    public async Task<IResult> UpdateTournament(TournamentCreate tournamentCreate, int id)
    {
        return await Update(tournamentCreate, id);
    }

    public async Task<IResult> DeleteTournament(int id)
    {
       return await Delete(id);
    }

    public async Task<IResult> GetTournament(int id)
    {
        return await GetById(id);
    }
}