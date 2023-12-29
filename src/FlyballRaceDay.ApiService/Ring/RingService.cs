using FlyballRaceDay.ApiService.Services;

namespace FlyballRaceDay.ApiService.Ring;

public class RingService(FlyballRaceDayDbContext context,ILoggerFactory loggerFactory) : DataService<Database.Ring,RingCreate,RingView>(loggerFactory,context),IRingService
{
    public async Task<IResult> CreateRing(RingCreate ring)
    {
        return await Create(ring);
    }

    public async Task<IResult> DeleteRing(string ringId)
    {
       return await Delete(ringId);
    }

    public async Task<IResult> GetRingByTournamentId(string tournamentId)
    {
        return await Where(x => x.TournamentId == new Guid(tournamentId));
    }

    public async Task<IResult> UpdateRing(RingCreate ring, string id)
    {
        return await Update(ring, id);
    }
}