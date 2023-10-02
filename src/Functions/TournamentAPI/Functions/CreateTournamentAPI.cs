namespace TournamentAPI.Functions;

public class CreateTournamentApi : APIBaseClass<CreateTournamentApi,TournamentDataModel,Tournament>
{
    private readonly ILogger<CreateTournamentApi> _logger;
    public CreateTournamentApi(ILoggerFactory loggerFactory,IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings) : base(loggerFactory,flyballStoreDatabaseSettings,nameof(Tournament))
    {
        _logger = loggerFactory.CreateLogger<CreateTournamentApi>();
    }
    
    [Function("Create")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData request)
    {
        return await Create(request);
    }
}