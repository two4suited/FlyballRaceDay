namespace FlyballRaceDay.ApiService.Tournament;

public class TournamentView
{
    public string Id { get; set; }
    public string EventName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int NumberOfRings { get; set; }
}