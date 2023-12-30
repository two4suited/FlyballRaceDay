namespace FlyballRaceDay.ApiService.Tournament;

public class TournamentView
{
    public string Id { get; set; }
    public string EventName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfRings { get; set; }
}