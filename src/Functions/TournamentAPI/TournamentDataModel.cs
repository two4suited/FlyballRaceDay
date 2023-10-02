using MongoDB.Bson.Serialization.Attributes;

namespace TournamentAPI;

public class TournamentDataModel : DataModel
{
    public string EventName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int NumberOfLanes { get; set; }
  
}