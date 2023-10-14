namespace Functions.Tests;

public class TestingDateTimeService : IDateTimeService
{
    public TestingDateTimeService(DateTime date)
    {
        CurrentDay = date;
    }

    public DateTime CurrentDay { get; }
}