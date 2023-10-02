using MongoDB.Bson.Serialization.Attributes;

namespace TournamentAPI;

public class TournamentDataModel : ITournament
{
    [BsonId]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string EventName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfLanes { get; set; }
  
}