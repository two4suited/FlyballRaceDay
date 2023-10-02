namespace RaceAPI
{
    public interface IRace
    {
        string Id { get; set; }
        string TournamentId { get; set; }
        string RaceNumber { get; set; }
        string LeftLaneTeam { get; set; }
        string RightLaneTeam { get; set; }
        string Format { get; set; }
        string Division { get; set; }
        string Breakout { get; set; }
        string RingId { get; set; }
        bool Done { get; set; }
        bool IsBreak { get; set; }
        int BreakTimeInMinutes { get; set; }
    }
}