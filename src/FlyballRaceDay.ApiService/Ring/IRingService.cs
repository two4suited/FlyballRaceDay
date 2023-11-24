namespace FlyballRaceDay.ApiService.Ring;

public interface IRingService
{
    Task<RingView> CreateRing(RingCreate ring);
    Task DeleteRing(int ringId);
    Task<List<RingView>> GetRingByTournamentId(int tournamentId);
    Task<RingView> UpdateRing(RingCreate ring, int id);
}