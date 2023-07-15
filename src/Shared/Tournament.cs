using System;

namespace BlazorApp.Shared
{
    public class Tournament
    {
        public string Id => Guid.NewGuid().ToString();
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfLanes { get; set; }
    }
}