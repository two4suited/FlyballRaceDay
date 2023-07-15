using System;

namespace BlazorApp.Shared
{
    public class Tournament : ITournament
    {
        public string Id { get; set; }
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfLanes { get; set; }
    }
}