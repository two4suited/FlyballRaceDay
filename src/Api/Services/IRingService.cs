using System.Collections.Generic;
using System.Threading.Tasks;
using ApiIsolated.Models;
using BlazorApp.Shared;

namespace ApiIsolated.Services;

public interface IRingService
{
    Task Update(RingDataModel model);
    Task Create(RingDataModel model);
    Task<List<Ring>> GetByTournamentId(string tournamentId);
    Task Delete(string ringId);
}