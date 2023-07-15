using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Shared;

namespace ApiIsolated.Services;

public interface IRingService
{
    Task UpdateRing(string tournamentId);
    Task CreateRing(string tournamentId,string ringId);
    Task<List<Ring>> GetRings(string tournamentId);
}