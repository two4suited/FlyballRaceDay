using Microsoft.AspNetCore.Mvc;

namespace FlyballRaceDay.ApiService.Ring;

public static class RingApi
{
    public static RouteGroupBuilder MapRingApis(this RouteGroupBuilder group)
    {
        group.MapPost("/", (RingCreate ring, IRingService service) => service.CreateRing(ring));
        group.MapDelete("/", (string id, IRingService service) => service.DeleteRing(id));
        group.MapGet("{tournamentId}/GetRings",
            (IRingService service, string tournamentId) => service.GetRingByTournamentId(tournamentId));
        group.MapPut("{id}",
            (string id, [FromBody] RingCreate ring, [FromServices] IRingService service) => service.UpdateRing(ring, id));
        return group;
    }
}