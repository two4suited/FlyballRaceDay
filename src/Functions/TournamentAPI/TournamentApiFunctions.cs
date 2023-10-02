namespace TournamentAPI;

public class TournamentApiFunctions : APIBaseClass<TournamentApiFunctions,TournamentDataModel,Tournament>
{
    private readonly IDateTimeService _dateTimeService;

    public TournamentApiFunctions(ILoggerFactory loggerFactory,
        IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings, IDateTimeService dateTimeService) : base(
        loggerFactory, flyballStoreDatabaseSettings, nameof(Tournament)) 
    {
        _dateTimeService = dateTimeService;
    }
    
    [Function("GetAll")]
    public async Task<HttpResponseData> GetAllActiveTournaments([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData request)
    {
        var filter = Builders<TournamentDataModel>.Filter.Where(x => x.StartDate > _dateTimeService.CurrentDay);
        return await GetByFilter(request, filter);
    }
    [Function("Update")]
    public async Task<HttpResponseData> UpdateTournaments([HttpTrigger(AuthorizationLevel.Function, "put",Route = "{id}")] HttpRequestData request,string id)
    {
        return await Update(request, id);
    }
    [Function("Delete")]
    public async Task<HttpResponseData> DeleteTournaments([HttpTrigger(AuthorizationLevel.Function, "delete",Route = "{id}")] HttpRequestData request,string id)
    {
        return await Delete(request, id);
    }
    [Function("Create")]
    public async Task<HttpResponseData> CreateTournaments([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData request)
    {
        return await Create(request);
    }
}