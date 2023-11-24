using FlyballRaceDay.ApiService.Services;

namespace FlyballRaceDay.ApiService.Ring;

public class RingService(FlyballRaceDayDbContext context,ILoggerFactory loggerFactory) : DataService<Database.Ring,RingCreate,RingView>(loggerFactory,context),IRingService
{
    public async Task<RingView> CreateRing(RingCreate ring)
    {
        return await Create(ring);
    }

    public async Task DeleteRing(int ringId)
    {
        await Delete(ringId);
    }

    public async Task<List<RingView>> GetRingByTournamentId(int tournamentId)
    {
        return await Where(x => x.TournamentId == tournamentId);
    }

    public async Task<RingView> UpdateRing(RingCreate ring, int id)
    {
        return await Update(ring, id);
    }
}