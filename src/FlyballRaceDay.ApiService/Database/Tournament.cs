namespace FlyballRaceDay.ApiService.Database;

public class Tournament : DataModel
{
    public string EventName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfRings { get; set; }
}