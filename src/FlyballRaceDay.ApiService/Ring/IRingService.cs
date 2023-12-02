namespace FlyballRaceDay.ApiService.Ring;

public interface IRingService
{
    Task<IResult> CreateRing(RingCreate ring);
    Task<IResult> DeleteRing(int ringId);
    Task<IResult> GetRingByTournamentId(int tournamentId);
    Task<IResult> UpdateRing(RingCreate ring, int id);
}