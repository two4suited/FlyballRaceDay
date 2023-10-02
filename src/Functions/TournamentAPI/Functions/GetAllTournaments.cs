
namespace TournamentAPI.Functions;

public class GetAllTournaments : APIBaseClass<GetAllTournaments,TournamentDataModel,Tournament>
{
    private readonly IDateTimeService _dateTimeService;

    public GetAllTournaments(ILoggerFactory loggerFactory,
        IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings, IDateTimeService dateTimeService) : base(
        loggerFactory, flyballStoreDatabaseSettings, nameof(Tournament)) 
    {
        _dateTimeService = dateTimeService;
    }
    
    [Function("GetAll")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData request)
    {
        var filter = Builders<TournamentDataModel>.Filter.Where(x => x.StartDate > _dateTimeService.CurrentDay);
        return await GetByFilter(request, filter);
    }
}