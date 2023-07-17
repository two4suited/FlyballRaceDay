namespace BlazorApp.Shared
{
    public interface IRing
    {
        string Id { get; set; }
        string TournamentId { get; set; }
        string Name { get; set; }
        string Color { get; set; }
        
    }
}