using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiIsolated.Models;
using BlazorApp.Shared;

namespace ApiIsolated.Services;

public interface ITournamentService
{
    Task<IEnumerable<Tournament>> GetAllActive();
    Task Create(TournamentDataModel tournament);
    Task Update(TournamentDataModel tournament);
    Task Delete(string tournamentId);
}