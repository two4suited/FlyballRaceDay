namespace FlyballRaceDay.ApiService.Database;

public class Ring : DataModel
{
    public Guid TournamentId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}