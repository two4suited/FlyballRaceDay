namespace FlyballRaceDay.ApiService.Race;

public static class RaceApi
{
    public static RouteGroupBuilder MapRaceApis(this RouteGroupBuilder group)
    {
        group.MapPost("/", (RaceCreate race, IRaceService service) => service.CreateRace(race));
        group.MapGet("/schedule/{tournamentId}",
            (string tournamentId, IRaceService service) => service.GetScheduleByTournamentId(tournamentId));
        group.MapGet("/upcoming/{tournamentId}",
            (string tournamentId, IRaceService service) => service.GetUpcomingRaces(tournamentId));
        group.MapPut("{raceId}/done", (string raceId, IRaceService service) => service.MarkRaceAsDone(raceId));
        group.MapDelete("/{raceId}", (string raceId, IRaceService service) => service.DeleteRace(raceId));
        group.MapPut("/{raceId}/{ringId}",
            (string raceId, string ringId, IRaceService service) => service.AddRaceToRing(raceId, ringId));
        return group;
    }
}