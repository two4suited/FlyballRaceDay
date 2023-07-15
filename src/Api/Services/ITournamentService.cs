using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorApp.Shared;

namespace ApiIsolated.Services;

public interface ITournamentService
{
    Task<IEnumerable<Tournament>> GetAllActive();
    Task<Tournament> Create(Tournament tournament);
    Task<Tournament> Update(Tournament tournament);
    Task Delete(string tournamentId);
}