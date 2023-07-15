namespace BlazorApp.Shared
{
    public class Race : IRace
    {
        public string RaceNumber { get; set; }
        public string LeftLaneTeam { get; set; }
        public string RightLaneTeam { get; set; }
        public string Format { get; set; }
        public string Division { get; set; }
        public string Breakout { get; set; }
        public string RingId { get; set; }
        public bool IsBreak { get; set; }
        public int BreakTimeInMinutes { get; set; }
    }
}