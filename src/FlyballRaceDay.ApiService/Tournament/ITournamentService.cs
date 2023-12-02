namespace FlyballRaceDay.ApiService.Tournament;

public interface ITournamentService
{
    Task<IResult> CreateTournament(TournamentCreate tournamentCreate);
    Task<IResult> GetActiveTournaments();
    Task<IResult> UpdateTournament(TournamentCreate tournamentCreate, int id);
    Task<IResult> DeleteTournament(int id);
    Task<IResult> GetTournament(int id);
}