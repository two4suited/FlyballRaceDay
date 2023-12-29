namespace FlyballRaceDay.ApiService.Race;

public interface IRaceService
{
    Task<IResult> CreateRace(RaceCreate newRace);
    Task<IResult> CreateSchedule(string schedule);
    Task<IResult> GetScheduleByTournamentId(string tournamentId);
    Task<IResult> DeleteRace(string raceId);
    Task<IResult> GetUpcomingRaces(string tournamentId);
    Task<IResult> MarkRaceAsDone(string raceId);
    Task<IResult> AddRaceToRing(string raceId, string ringId);
}