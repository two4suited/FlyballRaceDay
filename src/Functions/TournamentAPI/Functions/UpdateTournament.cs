namespace TournamentAPI.Functions;


public class UpdateTournament : APIBaseClass<CreateTournamentApi,TournamentDataModel,Tournament>
{
    public UpdateTournament(ILoggerFactory loggerFactory, IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings)
        : base(loggerFactory, flyballStoreDatabaseSettings, nameof(Tournament))
    {
    }
    
    [Function("Update")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "put",Route = "{id}")] HttpRequestData request,string id)
    {
        return await Update(request, id);
    }
}