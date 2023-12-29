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

    public async Task<IResult> GetScheduleByTournamentId(string tournamentId)
    {
        return await Where(x => x.TournamentId == new Guid(tournamentId));
    }

    public async Task<IResult> DeleteRace(string raceId)
    {
        return await Delete(raceId);
    }

    public async Task<IResult> GetUpcomingRaces(string tournamentId)
    {
        var upcomingRaces = await Where(x => x.TournamentId == new Guid(tournamentId) && x.Done == false);
        return upcomingRaces;
    }

    public async Task<IResult> MarkRaceAsDone(string raceId)
    {
        var raceToUpdate = await context.Races.SingleAsync(x => x.Id == new Guid(raceId));
        raceToUpdate.Done = true;
        context.Update(raceToUpdate);
        await context.SaveChangesAsync();

        return Results.Ok();
    }

    public async Task<IResult> AddRaceToRing(string raceId, string ringId)
    {
        var raceToUpdate = await context.Races.SingleAsync(x => x.Id == new Guid(raceId));
        raceToUpdate.RingId = new Guid(ringId);
        context.Update(raceToUpdate);
        await context.SaveChangesAsync();
        
        return Results.Ok();
    }
}