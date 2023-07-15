using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Shared;

namespace ApiIsolated.Services;

public class TournamentService : ITournamentService
{
    public async Task<IEnumerable<Tournament>> GetAllActive()
    {
        throw new System.NotImplementedException();
    }

    public async Task<Tournament> Create(Tournament tournament)
    {
        throw new System.NotImplementedException();
    }

    public async Task<Tournament> Update(Tournament tournament)
    {
        throw new System.NotImplementedException();
    }

    public async Task Delete(string tournamentId)
    {
        throw new System.NotImplementedException();
    }
}