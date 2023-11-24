namespace FlyballRaceDay.ApiService.Database;

public class Race : DataModel
{
    public int TournamentId { get; set; }
    public int RaceNumber { get; set; }
    public string LeftLaneTeam { get; set; }
    public string RightLaneTeam { get; set; }
    public string Format { get; set; }
    public string Division { get; set; }
    public string Breakout { get; set; }
    public int? RingId { get; set; }
    public bool Done { get; set; }
    public bool IsBreak { get; set; }
    public int BreakTimeInMinutes { get; set; }
}