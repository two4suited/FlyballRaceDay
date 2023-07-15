using System;
using BlazorApp.Shared;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiIsolated.Models;

public class TournamentDataModel : ITournament
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string EventName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfLanes { get; set; }
}