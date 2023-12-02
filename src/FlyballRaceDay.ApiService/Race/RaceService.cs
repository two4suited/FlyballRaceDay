using FlyballRaceDay.ApiService.Services;
using Microsoft.EntityFrameworkCore;

namespace FlyballRaceDay.ApiService.Race;

public class RaceService(FlyballRaceDayDbContext context,ILoggerFactory loggerFactory) : DataService<Database.Race,RaceCreate,RaceView>(loggerFactory,context),IRaceService
{
    public async Task<IResult> CreateRace(RaceCreate newRace)
    {
        return await Create(newRace);
    }

    public async Task<IResult> CreateSchedule(string schedule)
    {
        throw new NotImplementedException();
    }

    public async Task<IResult> GetScheduleByTournamentId(int tournamentId)
    {
        return await Where(x => x.TournamentId == tournamentId);
    }

    public async Task<IResult> DeleteRace(int raceId)
    {
        return await Delete(raceId);
    }

    public async Task<IResult> GetUpcomingRaces(int tournamentId)
    {
        var upcomingRaces = await Where(x => x.TournamentId == tournamentId && x.Done == false);
        return upcomingRaces;
    }

    public async Task<IResult> MarkRaceAsDone(int raceId)
    {
        var raceToUpdate = await context.Races.SingleAsync(x => x.Id == raceId);
        raceToUpdate.Done = true;
        context.Update(raceToUpdate);
        await context.SaveChangesAsync();

        return Results.Ok();
    }

    public async Task<IResult> AddRaceToRing(int raceId, int ringId)
    {
        var raceToUpdate = await context.Races.SingleAsync(x => x.Id == raceId);
        raceToUpdate.RingId = ringId;
        context.Update(raceToUpdate);
        await context.SaveChangesAsync();
        
        return Results.Ok();
    }
}