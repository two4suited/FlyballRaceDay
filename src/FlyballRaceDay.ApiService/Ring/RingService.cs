using FlyballRaceDay.ApiService.Services;

namespace FlyballRaceDay.ApiService.Ring;

public class RingService(FlyballRaceDayDbContext context,ILoggerFactory loggerFactory) : DataService<Database.Ring,RingCreate,RingView>(loggerFactory,context),IRingService
{
    public async Task<IResult> CreateRing(RingCreate ring)
    {
        return await Create(ring);
    }

    public async Task<IResult> DeleteRing(int ringId)
    {
       return await Delete(ringId);
    }

    public async Task<IResult> GetRingByTournamentId(int tournamentId)
    {
        return await Where(x => x.TournamentId == tournamentId);
    }

    public async Task<IResult> UpdateRing(RingCreate ring, int id)
    {
        return await Update(ring, id);
    }
}