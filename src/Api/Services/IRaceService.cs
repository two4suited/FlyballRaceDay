using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiIsolated.Models;
using BlazorApp.Shared;

namespace ApiIsolated.Services;

public interface IRaceService
{
    Task<IEnumerable<Race>> GetUpcomingRaces(string tournamentId,List<Ring> rings);
    Task MarkRaceAsDone(string tournamentId, string raceNumber);
    Task AddRaceToRing(string tournamentId, string raceNumber, string ringId);
    Task UploadSchedule(List<RaceDataModel> races);
    Task<List<Race>> GetSchedule(string tournamentId);
}