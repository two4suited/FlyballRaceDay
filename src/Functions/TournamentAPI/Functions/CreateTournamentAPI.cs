namespace TournamentAPI;

public class CreateTournamentApi : BaseService<TournamentDataModel>
{
    private readonly ILogger<CreateTournamentApi> _logger;
    protected CreateTournamentApi(ILoggerFactory loggerFactory,IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings) : base(flyballStoreDatabaseSettings,nameof(Tournament))
    {
        _logger = loggerFactory.CreateLogger<CreateTournamentApi>();
    }
    
    [Function("Create")]
    public async Task<HttpResponseData> CreateTournament([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData request)
    {
        if (request.Body.Length == 0)
        {
            var response = request.CreateResponse(HttpStatusCode.BadRequest);
            _logger.LogInformation("The Body of the request was empty");
            return response;
        }
            
        using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);

        var requestBody = await new StreamReader(request.Body).ReadToEndAsync(cancellationSource.Token);
        var newItem = JsonSerializer.Deserialize<TournamentDataModel>(requestBody);

        _logger.LogInformation("New Item: {@NewItem}", newItem);
            
        try
        {
            await Collection.InsertOneAsync(newItem, cancellationToken: cancellationSource.Token);

            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(newItem, cancellationToken: cancellationSource.Token);

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