namespace RaceAPI;

public class RaceApiFunctions : APIBaseClass<RaceApiFunctions,RaceDataModel,Race>
{
    public RaceApiFunctions(ILoggerFactory loggerFactory,IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings) : base(loggerFactory,flyballStoreDatabaseSettings,nameof(Race))
    {
    }
    
    [Function("CreateSchedule")]
    public async Task<HttpResponseData> CreateSchedule([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData request)
    {
        return await Create(request);
    }
    [Function("GetScheduleByTournamentId")]
    public async Task<HttpResponseData> GetScheduleByTournamentId([HttpTrigger(AuthorizationLevel.Function, "get",Route = "{tournamentId}")] HttpRequestData request,string tournamentId)
    {
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.TournamentId == tournamentId);
        return await GetByFilter(request, filter);
    }
    
    [Function("Delete")]
    public async Task<HttpResponseData> DeleteRing([HttpTrigger(AuthorizationLevel.Function, "delete",Route = "{id}")] HttpRequestData request,string id)
    {
        return await Delete(request, id);
    }
    
    [Function("GetUpcomingRaces")]
    public async Task<HttpResponseData> GetUpcomingRaces([HttpTrigger(AuthorizationLevel.Function, "get",Route = "{tournamentId}")] HttpRequestData request,string tournamentId)
    {
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.TournamentId == tournamentId);
        return await GetByFilter(request, filter);
    }
    [Function("MarkRaceAsDone")]
    public async Task<HttpResponseData> MarkRaceAsDone([HttpTrigger(AuthorizationLevel.Function, "put",Route = "{id}")] HttpRequestData request,string raceId)
    {
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.Id == raceId);
        return await Update(request, raceId,filter);
    }
    [Function("AddRaceToRing")]
    public async Task<HttpResponseData> AddRaceToRing([HttpTrigger(AuthorizationLevel.Function, "put",Route = "{id}")] HttpRequestData request,string ringId)
    {
        var filter = Builders<RaceDataModel>.Filter.Where(x => x.RingId == ringId);
        return await Update(request, ringId,filter);
    }
}