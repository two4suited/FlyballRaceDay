using FlyballRaceDay.ApiService.Services;
using Microsoft.EntityFrameworkCore;

namespace FlyballRaceDay.ApiService.Race;

public class RaceService(FlyballRaceDayDbContext context,ILogger<RaceService> logger) : DataService<Database.Race,RaceCreate,RaceView>(logger,context),IRaceService
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
        return await Where(x => x.TournamentId == tournamentId);
    }

    public async Task<IResult> DeleteRace(string raceId)
    {
        return await Delete(raceId);
    }

    public async Task<IResult> GetUpcomingRaces(string tournamentId)
    {
        var upcomingRaces = await Where(x => x.TournamentId == tournamentId && x.Done == false);
        return upcomingRaces;
    }

    public async Task<IResult> MarkRaceAsDone(string raceId)
    {
        var raceToUpdate = await context.Races.SingleAsync(x => x.Id == raceId);
        raceToUpdate.Done = true;
        context.Update(raceToUpdate);
        await context.SaveChangesAsync();

        return Results.Ok();
    }

    public async Task<IResult> AddRaceToRing(string raceId, string ringId)
    {
        var raceToUpdate = await context.Races.SingleAsync(x => x.Id == raceId);
        raceToUpdate.RingId = ringId;
        context.Update(raceToUpdate);
        await context.SaveChangesAsync();
        
        return Results.Ok();
    }
}