namespace FlyballRaceDay.ApiService.Tournament;

public interface ITournamentService
{
    Task<IResult> CreateTournament(TournamentCreate tournamentCreate);
    Task<IResult> GetActiveTournaments();
    Task<IResult> UpdateTournament(TournamentCreate tournamentCreate, string id);
    Task<IResult> DeleteTournament(string id);
    Task<IResult> GetTournament(string id);
}