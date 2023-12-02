namespace FlyballRaceDay.ApiService.Race;

public interface IRaceService
{
    Task<IResult> CreateRace(RaceCreate newRace);
    Task<IResult> CreateSchedule(string schedule);
    Task<IResult> GetScheduleByTournamentId(int tournamentId);
    Task<IResult> DeleteRace(int raceId);
    Task<IResult> GetUpcomingRaces(int tournamentId);
    Task<IResult> MarkRaceAsDone(int raceId);
    Task<IResult> AddRaceToRing(int raceId, int ringId);
}