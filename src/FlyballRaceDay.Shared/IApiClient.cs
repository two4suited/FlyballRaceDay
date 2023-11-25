using FlyballRaceDay.Shared.Tournament;

namespace FlyballRaceDay.Shared;

public interface IApiClient
{
    Task<TournamentView?> AddTournament(TournamentCreate tournamentCreate);
}