using FlyballRaceDay.ApiService.Services;
using Microsoft.EntityFrameworkCore;

namespace FlyballRaceDay.ApiService.Race;

public class RaceService(FlyballRaceDayDbContext context,ILoggerFactory loggerFactory) : DataService<Database.Race,RaceCreate,RaceView>(loggerFactory,context),IRaceService
{
    public async Task<RaceView> CreateRace(RaceCreate newRace)
    {
        return await Create(newRace);
    }

    public async Task<List<RaceView>> CreateSchedule(string schedule)
    {
        throw new NotImplementedException();
    }

    public async Task<List<RaceView>> GetScheduleByTournamentId(int tournamentId)
    {
        return await Where(x => x.TournamentId == tournamentId);
    }

    public async Task DeleteRace(int raceId)
    {
        await Delete(raceId);
    }

    public async Task<List<RaceView>> GetUpcomingRaces(int tournamentId)
    {
        var upcomingRaces = await Where(x => x.TournamentId == tournamentId && x.Done == false);
        return upcomingRaces;
    }

    public async Task MarkRaceAsDone(int raceId)
    {
        var raceToUpdate = await context.Race.SingleAsync(x => x.Id == raceId);
        raceToUpdate.Done = true;
        context.Update(raceToUpdate);
        await context.SaveChangesAsync();
    }

    public async Task AddRaceToRing(int raceId, int ringId)
    {
        var raceToUpdate = await context.Race.SingleAsync(x => x.Id == raceId);
        raceToUpdate.RingId = ringId;
        context.Update(raceToUpdate);
        await context.SaveChangesAsync();
    }
}