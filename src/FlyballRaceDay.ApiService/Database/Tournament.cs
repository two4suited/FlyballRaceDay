namespace FlyballRaceDay.ApiService.Database;

public class Tournament : DataModel
{
    public string EventName { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int NumberOfLanes { get; set; }
}