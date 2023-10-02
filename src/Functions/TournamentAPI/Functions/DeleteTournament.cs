namespace TournamentAPI.Functions;


public class DeleteTournament : APIBaseClass<DeleteTournament,TournamentDataModel,Tournament>
{
    public DeleteTournament(ILoggerFactory loggerFactory, IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings)
        : base(loggerFactory, flyballStoreDatabaseSettings, nameof(Tournament))
    {
    }


    [Function("Delete")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "delete",Route = "{id}")] HttpRequestData request,string id)
    {
        return await Delete(request, id);
    }
}