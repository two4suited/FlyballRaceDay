using System;
using System.Collections.Generic;
using BlazorApp.Shared;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiIsolated.Models;

public class TournamentDataModel : ITournament
{
    [BsonId]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string EventName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfLanes { get; set; }

    public static Tournament ToTournament(TournamentDataModel model)
    {
        return new Tournament()
        {
            Id = model.Id,
            EventName = model.EventName,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            NumberOfLanes = model.NumberOfLanes
        };
    }

    public static IEnumerable<Tournament> ToTournamentList(List<TournamentDataModel> models)
    {
        var tournaments = new List<Tournament>();
        foreach (var model in models)
        {
            tournaments.Add(TournamentDataModel.ToTournament(model));
        }

        return tournaments;
    }
}