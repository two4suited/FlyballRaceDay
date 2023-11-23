namespace FlyballRaceDay.ApiService.Database;

public class Tournament
{
    public int Id { get; set; }
    public string EventName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int NumberOfLanes { get; set; }
}