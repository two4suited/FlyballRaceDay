namespace RingAPI
{
    public class Ring : IRing
    {
        public string Id { get; set; }
        public string TournamentId { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
    }
}