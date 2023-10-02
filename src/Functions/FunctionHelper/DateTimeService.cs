using System;

namespace FunctionHelper;

public class DateTimeService : IDateTimeService
{
    public DateTime CurrentDay => DateTime.Now;
}