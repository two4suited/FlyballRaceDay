using System;
using System.Collections.Generic;
using System.Linq;
using ApiIsolated.Helpers;
using BlazorApp.Shared;

namespace ApiIsolated.Models;

public class RingDataModel : IRing
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TournamentId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}