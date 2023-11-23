namespace FlyballRaceDay.ApiService.Ring;

public static class RingApi
{
    public static RouteGroupBuilder MapRingApis(this RouteGroupBuilder group)
    {
        group.MapPost("/", (RingCreate ring, IRingService service) => service.CreateRing(ring));
        return group;
    }
}