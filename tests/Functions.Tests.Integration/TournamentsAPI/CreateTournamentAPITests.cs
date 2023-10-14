namespace Functions.Tests.TournamentsAPI;

public sealed class CreateTournamentApiTests : IClassFixture<FunctionFactory>
{
  
    private FunctionFactory _factory;
    private TournamentApiFunctions _fut;
    public CreateTournamentApiTests(FunctionFactory factory)
    {
        factory.CreateFunction();
        _factory = factory;
        _fut = new TournamentApiFunctions(_factory.Logger, _factory.OptionsForDatabase, _factory.DateTimeService);
    }
  
    [Fact]
    public async Task Create_ShouldReturnOk_WhenCalledWithValidTournament()
    {
       
        var tournament = new Tournament()
        {
            EndDate = DateTime.Now,
            EventName = "Test Event",
            StartDate = DateTime.Now,
            NumberOfLanes = 2
        };
        
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(tournament));
        var body = new MemoryStream(bytes);
        _factory.Request.Body.Returns(body);
        
        var result = await _fut.Create(_factory.Request);

        result.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenCalledWithNullTournament()
    {
        var body = new MemoryStream();
        _factory.Request.Body.Returns(body);
        
        var result = await _fut.Create(_factory.Request);

        result.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Create_ShouldID_WhenCalledWithValidTournament()
    {
       
        var tournament = new Tournament()
        {
            EndDate = DateTime.Now,
            EventName = "Test Event",
            StartDate = DateTime.Now,
            NumberOfLanes = 2
        };
        
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(tournament));
        var body = new MemoryStream(bytes);
        _factory.Request.Body.Returns(body);
        
        var result = await _fut.Create(_factory.Request);
        var returnedTournament = JsonSerializer.Deserialize<Tournament>(result.Body);

        returnedTournament.Id.ShouldNotBeNull();
    }
}