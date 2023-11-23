using FlyballRaceDay.ApiService.Models;

namespace FlyballRaceDay.ApiService.Services;

public class TournamentService(FlyballRaceDayDbContext context) {

    
    public async Task<TournamentView> CreateTournament(TournamentCreate tournamentCreate)
    {
        var newTournament = Mapper.Map<TournamentCreate,Tournament>(tournamentCreate);
        
        var tournament = context.Tournaments.Add(newTournament);
        await context.SaveChangesAsync();

        return Mapper.Map<Tournament, TournamentView>(tournament.Entity);
    }
}