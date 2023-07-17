using System.Collections.Generic;
using System.Linq;
using BlazorApp.Shared;

namespace ApiIsolated.Models;

public class RingDataModel : IRing
{
    public string Id { get; set; }
    public string TournamentId { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    
    public static Ring ToRing(RingDataModel model)
    {
        return new Ring()
        {
            Id = model.Id,
            Name = model.Name,
            TournamentId = model.TournamentId,
            Color = model.Color
        };
    }

    public static List<Ring> ToRingList(IEnumerable<RingDataModel> models)
    {
        var list = new List<Ring>();
        foreach (var model in models)
        {
            list.Add(RingDataModel.ToRing(model));
        }

        return list;
    }
}