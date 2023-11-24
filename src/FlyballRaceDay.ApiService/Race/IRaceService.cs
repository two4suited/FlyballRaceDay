namespace FlyballRaceDay.ApiService.Race;

public interface IRaceService
{
    Task<RaceView> CreateRace(RaceCreate newRace);
    Task<List<RaceView>> CreateSchedule(string schedule);
    Task<List<RaceView>> GetScheduleByTournamentId(int tournamentId);
    Task DeleteRace(int raceId);
    Task<List<RaceView>> GetUpcomingRaces(int tournamentId);
    Task MarkRaceAsDone(int raceId);
    Task AddRaceToRing(int raceId, int ringId);
}