using System;

namespace ApiIsolated.Services;

public interface IDateTimeService
{
    DateTime CurrentDay { get;  }
}