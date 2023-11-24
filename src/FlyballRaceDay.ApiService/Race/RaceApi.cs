using System.Diagnostics;

namespace FlyballRaceDay.ApiService.Race;

public static class RaceApi
{
    public static RouteGroupBuilder MapRaceApis(this RouteGroupBuilder group)
    {
        group.MapPost("/", (RaceCreate race, IRaceService service) => service.CreateSchedule(race));
        group.MapGet("/schedule/{tournamentId}",
            (int tournamentId, IRaceService service) => service.GetScheduleByTournamentId(tournamentId));
        group.MapGet("/upcoming/{tournamentId}",
            (int tournamentId, IRaceService service) => service.GetUpcomingRaces(tournamentId));
        group.MapPut("{raceId}/done", (int raceId, IRaceService service) => service.MarkRaceAsDone(raceId));
        group.MapDelete("/{raceId}", (int raceId, IRaceService service) => service.DeleteRace(raceId));
        group.MapPut("/{raceId}/{ringId}",
            (int raceId, int ringId, IRaceService service) => service.AddRaceToRing(raceId, ringId));
        return group;
    }
}