using System;

namespace ApiIsolated.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime CurrentDay => DateTime.Now;
}