namespace FlyballRaceDay.ApiService.Race;

public class RaceService : IRaceService
{
    public async Task<List<RaceView>> CreateSchedule(RaceCreate newRace)
    {
        throw new NotImplementedException();
    }

    public async Task<List<RaceView>> GetScheduleByTournamentId(int tournamentId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteRace(int raceId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<RaceView>> GetUpcomingRaces(int tournamentId)
    {
        throw new NotImplementedException();
    }

    public async Task MarkRaceAsDone(int raceId)
    {
        throw new NotImplementedException();
    }

    public async Task AddRaceToRing(int raceId, int ringId)
    {
        throw new NotImplementedException();
    }
}