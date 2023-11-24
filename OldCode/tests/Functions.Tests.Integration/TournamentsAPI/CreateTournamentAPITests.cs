namespace Functions.Tests.TournamentsAPI;

public sealed class CreateTournamentApiTests : BaseTournamentApiTests
{
  
    public CreateTournamentApiTests(FunctionFactory factory) : base(factory) { }
    
    
  
    [Fact]
    public async Task Create_ShouldReturnOk_WhenCalledWithValidTournament()
    {
        var tournament = _tournamentGenerator.Generate();
        
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
        var tournament = _tournamentGenerator.Generate();
        
        var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(tournament));
        var body = new MemoryStream(bytes);
        _factory.Request.Body.Returns(body);
        
        var result = await _fut.Create(_factory.Request);
        var returnedTournament = TestHelpers.DeserializeHttpResponseData<Tournament>(result);

        returnedTournament.Id.ShouldNotBeNull();
    }
}