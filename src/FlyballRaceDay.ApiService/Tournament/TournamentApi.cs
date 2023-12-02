using Microsoft.AspNetCore.Mvc;

namespace FlyballRaceDay.ApiService.Tournament;

public static class TournamentApi
{
    public static RouteGroupBuilder MapTournamentApis(this RouteGroupBuilder group)
    {
        group.MapPost("/", (TournamentCreate tournament, ITournamentService service) => service.CreateTournament(tournament)).WithName("Tournament-Create").Produces<TournamentView>(StatusCodes.Status201Created);
        group.MapGet("/", (ITournamentService service) =>  service.GetActiveTournaments()).WithName("Tournament-GetActive").Produces<List<TournamentView>>();
        group.MapGet("/{id}", (int id, ITournamentService service) =>  service.GetTournament(id)).WithName("Tournament-GetById").Produces<TournamentView>().Produces(StatusCodes.Status404NotFound);
        group.MapPut("/{id}", (int id,[FromBody]TournamentCreate tournament,[FromServices]ITournamentService service) => service.UpdateTournament(tournament,id)).WithName("Tournament-Update").Produces<TournamentView>().Produces(StatusCodes.Status404NotFound);
        group.MapDelete("/{id}", (int id,ITournamentService service) => service.DeleteTournament(id)).WithName("Tournament-Delete").Produces(StatusCodes.Status404NotFound).Produces(StatusCodes.Status200OK);

        return group;
    }
}