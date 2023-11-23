namespace FlyballRaceDay.ApiService.Tournament;

public static class TournamentApi
{
    public static RouteGroupBuilder MapTournamentApis(this RouteGroupBuilder group)
    {
        group.MapPost("/", (TournamentCreate tournament, TournamentService service) => service.CreateTournament(tournament));
        group.MapGet("/", (TournamentService service) =>  service.GetActiveTournaments());
        group.MapPut("/", () => "");
        group.MapDelete("/", () => "");

        return group;
    }
}