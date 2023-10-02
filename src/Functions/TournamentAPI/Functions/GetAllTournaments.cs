
namespace TournamentAPI.Functions;

public class GetAllTournaments : BaseService<TournamentDataModel>
{
    private readonly IDateTimeService _dateTimeService;
    private readonly ILogger<GetAllTournaments> _logger;
    public GetAllTournaments(ILoggerFactory loggerFactory,IOptions<FlyballGameDaySettings> flyballStoreDatabaseSettings,IDateTimeService dateTimeService) : base(flyballStoreDatabaseSettings,nameof(Tournament))
    {
        _dateTimeService = dateTimeService;
        _logger = loggerFactory.CreateLogger<GetAllTournaments>();
    }
    
    [Function("GetAll")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData request)
    {
        using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(request.FunctionContext.CancellationToken);
        
        try
        {
            var filter = Builders<TournamentDataModel>.Filter.Where(x => x.StartDate > _dateTimeService.CurrentDay);
            var documents =  await Collection.FindAsync(filter, cancellationToken: cancellationSource.Token);
            var tournamentsData = documents.ToList();
            var tournaments = tournamentsData.MapList(Mapper.Map<TournamentDataModel, Tournament>);
            
            _logger.LogInformation(@"Found Records");

            var response = request.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(tournaments, cancellationToken: cancellationSource.Token);

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