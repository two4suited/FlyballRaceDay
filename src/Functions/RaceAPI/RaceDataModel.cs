using System;
using BlazorApp.Shared;

namespace ApiIsolated.Models;

public class RaceDataModel : IRace
{
    public RaceDataModel()
    {
        Done = false;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TournamentId { get; set; }
    public string RaceNumber { get; set; }
    public string LeftLaneTeam { get; set; }
    public string RightLaneTeam { get; set; }
    public string Format { get; set; }
    public string Division { get; set; }
    public string Breakout { get; set; }
    public string RingId { get; set; }
    public bool Done { get; set; }
    public bool IsBreak { get; set; }
    public int BreakTimeInMinutes { get; set; }
}