namespace TournamentAPI.Functions;


public class DeleteTournament : BaseService<TournamentDataModel>
{
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<GetAllTournaments> _logger;
    public DeleteTournament(ILoggerFactory loggerFactory,IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings,IDateTimeService dateTimeService) : base(flyballStoreDatabaseSettings,nameof(Tournament))
    {
        _dateTimeService = dateTimeService;
        _logger = loggerFactory.CreateLogger<GetAllTournaments>();
    }
    
    [Function("Delete")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "delete",Route = "{id}")] HttpRequestData request,string id)
    {
        using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);
       
        try
        {
            var filter = Builders<TournamentDataModel>.Filter.Where(x => x.Id == id);
            var documents =  await Collection.FindAsync(filter, cancellationToken: cancellationSource.Token);
            await Collection.DeleteOneAsync(filter, cancellationToken: cancellationSource.Token);  
            
            _logger.LogInformation(@"Updated Record Records");

            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"{id} was deleted!", cancellationToken: cancellationSource.Token);

            return response;
        }
        catch (Exception ex)
        {
            var response = request.CreateResponse(HttpStatusCode.InternalServerError);
            await response.WriteStringAsync(ex.Message, cancellationSource.Token);
            return response;
        }
    }
}