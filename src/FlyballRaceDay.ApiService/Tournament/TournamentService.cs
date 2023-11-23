namespace FlyballRaceDay.ApiService.Tournament;

public class TournamentService(FlyballRaceDayDbContext context,TimeProvider timeProvider) {

    
    public async Task<TournamentView> CreateTournament(TournamentCreate tournamentCreate)
    {
        var newTournament = Mapper.Map<TournamentCreate,Database.Tournament>(tournamentCreate);
        
        var tournament = context.Tournaments.Add(newTournament);
        await context.SaveChangesAsync();

        return Mapper.Map<Database.Tournament, TournamentView>(tournament.Entity);
    }

    public async Task<List<TournamentView>> GetActiveTournaments()
    {
        var activeTournaments =
            context.Tournaments.Where(x => x.StartDate >= DateOnly.FromDateTime(timeProvider.GetLocalNow().DateTime));

        return activeTournaments.ToList().MapList(Mapper.Map<Database.Tournament, TournamentView>);
    }
}