using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Shared;

namespace ApiIsolated.Services;

public interface IRaceService
{
    Task<IEnumerable<Race>> GetUpcomingRaces(string tournamentId);
    Task MarkRaceAsDone(string tournamentId, string raceNumber);
    Task AddRaceToRing(string tournamentId, string raceNumber, string ringId);
}