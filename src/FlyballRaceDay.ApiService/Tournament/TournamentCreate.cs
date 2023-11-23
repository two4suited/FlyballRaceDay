namespace FlyballRaceDay.ApiService.Tournament;

public class TournamentCreate
{
    public string EventName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int NumberOfLanes { get; set; }
}