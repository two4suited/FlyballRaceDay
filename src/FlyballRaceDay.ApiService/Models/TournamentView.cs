namespace FlyballRaceDay.ApiService.Models;

public class TournamentView
{
    public int Id { get; set; }
    public string EventName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfLanes { get; set; }
}