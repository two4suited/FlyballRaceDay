namespace FlyballRaceDay.ApiService.Database;

public class Tournament 
{
    public int Id { get; set; }
    public string EventName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfLanes { get; set; }
}