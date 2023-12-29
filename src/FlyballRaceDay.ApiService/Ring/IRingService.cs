namespace FlyballRaceDay.ApiService.Ring;

public interface IRingService
{
    Task<IResult> CreateRing(RingCreate ring);
    Task<IResult> DeleteRing(string ringId);
    Task<IResult> GetRingByTournamentId(string tournamentId);
    Task<IResult> UpdateRing(RingCreate ring, string id);
}