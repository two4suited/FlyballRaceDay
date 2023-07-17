using BlazorApp.Shared;

namespace ApiIsolated.Models;

public class RingDataModel : IRing
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
}