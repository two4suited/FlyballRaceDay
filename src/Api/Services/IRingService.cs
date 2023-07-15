using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Shared;

namespace ApiIsolated.Services;

public interface IRingService
{
    Task<Ring> UpdateRing(string tournamentId);
    Task<Ring> CreateRing(string tournamentId,string ringId);
    Task<List<Ring>> GetRings(string tournamentId);
}