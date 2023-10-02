namespace TournamentAPI.Functions;


public class UpdateTournament : BaseService<TournamentDataModel>
{
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<GetAllTournaments> _logger;
    public UpdateTournament(ILoggerFactory loggerFactory,IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings,IDateTimeService dateTimeService) : base(flyballStoreDatabaseSettings,nameof(Tournament))
    {
        _dateTimeService = dateTimeService;
        _logger = loggerFactory.CreateLogger<GetAllTournaments>();
    }
    
    [Function("Update")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "put",Route = "{id}")] HttpRequestData request,string id)
    {
        if (request.Body.Length == 0)
        {
            var response = request.CreateResponse(HttpStatusCode.BadRequest);
            _logger.LogInformation("The Body of the request was empty");
            return response;
        }
        
        using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);
        var requestBody = await new StreamReader(request.Body).ReadToEndAsync(cancellationSource.Token);
        var itemToUpdate = JsonSerializer.Deserialize<TournamentDataModel>(requestBody);
        itemToUpdate.Id = id;
        
        try
        {
            var filter = Builders<TournamentDataModel>.Filter.Where(x => x.Id == id);
            var documents =  await Collection.FindAsync(filter, cancellationToken: cancellationSource.Token);
            await Collection.ReplaceOneAsync(filter,itemToUpdate, cancellationToken: cancellationSource.Token);  
            
            _logger.LogInformation(@"Updated Record Records");

            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(itemToUpdate, cancellationToken: cancellationSource.Token);

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