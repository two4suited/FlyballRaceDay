using System.Net;
using System.Text;
using System.Text.Json;
using Azure.Core.Serialization;
using FunctionHelper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using NSubstitute;
using Shouldly;
using TournamentAPI;

namespace Functions.Tests.TournamentsAPI;

public sealed class CreateTournamentApiTests : IClassFixture<TournamentApiFactory>
{
  
    private TournamentApiFunctions _sut;
    private HttpRequestData _request;

    public CreateTournamentApiTests(TournamentApiFactory factory)
    {
        factory.CreateFunction();

        _request = factory.Request;
        var serviceProvider = factory.Context.InstanceServices;
        ILoggerFactory logger = Substitute.For<ILoggerFactory>();
        var flyballGameDaySettings = serviceProvider.GetService<IOptions<FlyballGameDaySettings>>();
        var dateTimeService = (IDateTimeService)serviceProvider.GetService(typeof(IDateTimeService))!;
        _sut = new TournamentApiFunctions(logger, flyballGameDaySettings, dateTimeService);
    }
  
    [Fact]
    public async Task Create_ShouldReturnOkWithTournament_WhenCalledWithValidTournament()
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
        _request.Body.Returns(body);
        
        var result = await _sut.Create(_request);

        result.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
}