namespace BlazorApp.Shared
{
    public interface IRace
    {
        string RaceNumber { get; set; }
        string LeftLaneTeam { get; set; }
        string RightLaneTeam { get; set; }
        string Format { get; set; }
        string Division { get; set; }
        string Breakout { get; set; }
        string RingId { get; set; }
        bool IsBreak { get; set; }
        int BreakTimeInMinutes { get; set; }
    }
}