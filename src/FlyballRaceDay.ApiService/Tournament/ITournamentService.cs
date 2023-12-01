namespace FlyballRaceDay.ApiService.Tournament;

public interface ITournamentService
{
    Task<TournamentView> CreateTournament(TournamentCreate tournamentCreate);
    Task<List<TournamentView>> GetActiveTournaments();
    Task<TournamentView> UpdateTournament(TournamentCreate tournamentCreate, int id);
    Task DeleteTournament(int id);
    Task<TournamentView> GetTournament(int id);
}