namespace Functions.Tests;

public class TestingDateTimeService : IDateTimeService
{
    public DateTime CurrentDay => DateTime.Now;
}