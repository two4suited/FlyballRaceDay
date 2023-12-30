namespace FlyballRaceDay.ApiService.Tournament;

public class TournamentCreate
{
    public string EventName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfRings { get; set; }
}