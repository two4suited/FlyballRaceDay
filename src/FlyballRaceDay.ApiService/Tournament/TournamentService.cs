using FlyballRaceDay.ApiService.Services;

namespace FlyballRaceDay.ApiService.Tournament;

public class TournamentService(FlyballRaceDayDbContext context,TimeProvider timeProvider,ILoggerFactory loggerFactory) : DataService<Database.Tournament,TournamentCreate,TournamentView>(loggerFactory,context),ITournamentService
{
    public async Task<TournamentView> CreateTournament(TournamentCreate tournamentCreate)
    {
        return await Create(tournamentCreate);
    }

    public async Task<List<TournamentView>> GetActiveTournaments()
    {
        return await Where(x => x.StartDate >= DateOnly.FromDateTime(timeProvider.GetLocalNow().DateTime));
    }
    public async Task<TournamentView> UpdateTournament(TournamentCreate tournamentCreate, int id)
    {
        return await Update(tournamentCreate, id);
    }

    public async Task DeleteTournament(int id)
    {
        await Delete(id);
    }
}