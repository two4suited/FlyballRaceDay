namespace FlyballRaceDay.ApiService.Race;

public interface IRaceService
{
    Task<List<RaceView>> CreateSchedule(RaceCreate newRace);
    Task<List<RaceView>> GetScheduleByTournamentId(int tournamentId);
    Task DeleteRace(int raceId);
    Task<List<RaceView>> GetUpcomingRaces(int tournamentId);
    Task MarkRaceAsDone(int raceId);
    Task AddRaceToRing(int raceId, int ringId);
}