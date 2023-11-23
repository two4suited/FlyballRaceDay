namespace FlyballRaceDay.ApiService.Race;

public static class RaceApi
{
    public static RouteGroupBuilder MapRaceApis(this RouteGroupBuilder group)
    {
        group.MapPost("/", (RaceCreate race, IRaceService service) => service.CreateSchedule(race));
        //group.MapGet("/", (IRaceService service) =>  service.GetActiveTournaments());
        //group.MapPut("/", () => "");
        //group.MapDelete("/", () => "");

        return group;
    }
}