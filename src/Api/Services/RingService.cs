using System.Collections.Generic;
using System.Threading.Tasks;
using ApiIsolated.Models;
using BlazorApp.Shared;
using Microsoft.Extensions.Options;

namespace ApiIsolated.Services;

public class RingService : BaseService<RingDataModel>,IRingService
{
    public RingService(IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings)  : base(flyballStoreDatabaseSettings,nameof(Ring))
    {
        
    }
    public Task UpdateRing(string tournamentId)
    {
        throw new System.NotImplementedException();
    }

    public Task CreateRing(string tournamentId, string ringId)
    {
        throw new System.NotImplementedException();
    }

    public Task<List<Ring>> GetRings(string tournamentId)
    {
        throw new System.NotImplementedException();
    }
}