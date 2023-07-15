using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Shared;

namespace ApiIsolated.Services;

public interface IScheduleService
{
    Task UploadSchedule(List<Race> races);
    Task<List<Race>> GetSchedule(string tournamentId);
}