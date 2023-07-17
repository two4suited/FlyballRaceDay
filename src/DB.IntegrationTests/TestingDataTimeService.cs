namespace DB.IntegrationTests;

public class TestingDataTimeService : IDateTimeService
{
    public DateTime CurrentDay =>  new DateTime(2023, 1, 1);
}