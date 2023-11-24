using Microsoft.AspNetCore.Mvc;

namespace FlyballRaceDay.ApiService.Tournament;

public static class TournamentApi
{
    public static RouteGroupBuilder MapTournamentApis(this RouteGroupBuilder group)
    {
        group.MapPost("/", (TournamentCreate tournament, ITournamentService service) => service.CreateTournament(tournament));
        group.MapGet("/", (ITournamentService service) =>  service.GetActiveTournaments());
        group.MapPut("/{id}", (int id,[FromBody]TournamentCreate tournament,[FromServices]ITournamentService service) => service.UpdateTournament(tournament,id));
        group.MapDelete("/{id}", (int id,ITournamentService service) => service.DeleteTournament(id));

        return group;
    }
}