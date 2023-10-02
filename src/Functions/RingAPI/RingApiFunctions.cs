namespace RingAPI;

public class RingApiFunctions : APIBaseClass<RingApiFunctions,RingDataModel,Ring>
{
    public RingApiFunctions(ILoggerFactory loggerFactory,IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings) : base(loggerFactory,flyballStoreDatabaseSettings,nameof(Ring))
    {
    }
    
    [Function("Create")]
    public async Task<HttpResponseData> CreateRing([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData request)
    {
        return await Create(request);
    }
    [Function("Delete")]
    public async Task<HttpResponseData> DeleteRing([HttpTrigger(AuthorizationLevel.Function, "delete",Route = "{id}")] HttpRequestData request,string id)
    {
        return await Delete(request, id);
    }
    
    [Function("GetRingsByTournamentId")]
    public async Task<HttpResponseData> GetRingsByTournamentId([HttpTrigger(AuthorizationLevel.Function, "get",Route = "{tournamentId}")] HttpRequestData request,string tournamentId)
    {
        var filter = Builders<RingDataModel>.Filter.Where(x => x.TournamentId == tournamentId);
        return await GetByFilter(request, filter);
    }
    [Function("Update")]
    public async Task<HttpResponseData> UpdateRing([HttpTrigger(AuthorizationLevel.Function, "put",Route = "{id}")] HttpRequestData request,string id)
    {
        return await Update(request, id);
    }
}