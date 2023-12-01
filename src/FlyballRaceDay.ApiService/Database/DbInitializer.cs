namespace FlyballRaceDay.ApiService.Database;

internal class DbInitializer
{
    internal static void Initialize(FlyballRaceDayDbContext dbContext)
    {
  
        
        ArgumentNullException.ThrowIfNull(dbContext, nameof(FlyballRaceDayDbContext));
        dbContext.Database.EnsureCreated();
        var random = new Random();
        var numberOfTournaments = random.Next(3, 10);;
        var ringId = 1;
        var raceId = 1;
        for (var i = 1; i <= numberOfTournaments; i++)
        {
            var numberOfRings = random.Next(1, 4);
             
            dbContext.Tournaments.Add(new Tournament()
            {
                Id = i,
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
                StartDate = DateOnly.FromDateTime(DateTime.Now.AddDays(i)),
                EventName = $"Tournament Number {i}",
                NumberOfRings = numberOfRings
            });
  
            for (var ringCounters = 1; ringCounters <=  numberOfRings; ringCounters++)
            {
                dbContext.Rings.Add(new Ring()
                {
                    Id = ringId,
                    TournamentId = i,
                    Color = "Blue",
                    Name = "Blue Ring"
                });
                ringId++;
            }
            
            var numberOfRaces = random.Next(40, 100);
            for(var r=1;r <= numberOfRaces;r++)
            {
                var minutes = 0;
                var isBreak = false;
                if (r % 20 == 0)
                {
                    minutes = 15;
                    isBreak = true;
                }

                dbContext.Races.Add(new Race()
                {
                    Id = raceId,
                    Breakout = "20.0",
                    Division = "Regular 1",
                    BreakTimeInMinutes = minutes,
                    IsBreak = isBreak,
                    RaceNumber = r,
                    TournamentId = i,
                    Done = false,
                    Format = "4 of 4",
                    LeftLaneTeam = "Bar",
                    RightLaneTeam = "Foo"
                });
                raceId++;
            }

        }

        dbContext.SaveChanges();
    }
    
}