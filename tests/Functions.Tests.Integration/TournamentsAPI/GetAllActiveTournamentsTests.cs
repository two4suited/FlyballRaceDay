using MongoDB.Driver;

namespace Functions.Tests.TournamentsAPI;

public class GetAllActiveTournamentsTests  : BaseTournamentApiTests
{
    public GetAllActiveTournamentsTests(FunctionFactory factory) : base(factory) { }
    
    [Fact]
    public async Task GetAllActiveTournaments_ShouldReturnOk()
    {
        //Arrange
        var tournament1 = _tournamentDataGenerator.Generate();
        var tournament2 = _tournamentDataGenerator.Generate();
        tournament2.StartDate.AddDays(-10);
        tournament2.EndDate.AddDays(-10);
        
        var mongoClient = new MongoClient(
            _factory.OptionsForDatabase.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            _factory.OptionsForDatabase.Value.DatabaseName);

        var collection = mongoDatabase.GetCollection<TournamentDataModel>(
            _factory.OptionsForDatabase.Value.CollectionName);
        
        await collection.InsertOneAsync(tournament1);
        await collection.InsertOneAsync(tournament2);
        
        //Act
        var result = await _fut.GetAllActiveTournaments(_factory.Request);

        result.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}