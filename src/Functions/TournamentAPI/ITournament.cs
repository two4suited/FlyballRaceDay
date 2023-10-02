using System;

namespace TournamentAPI
{
    public interface ITournament
    {
        string Id { get; }
        string EventName { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        int NumberOfLanes { get; set; }
    }
}