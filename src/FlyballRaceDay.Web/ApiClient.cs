using FlyballRaceDay.Shared;

namespace FlyballRaceDay.Web;

public class ApiClient(HttpClient client) 
{
    private string tournamentPath = "/tournament";
    public async Task<TournamentView?> AddTournament(TournamentCreate tournamentCreate)
    {
        var response =  await client.PostAsJsonAsync(tournamentPath, tournamentCreate);
        return await response.Content.ReadFromJsonAsync<TournamentView>();
    }
}