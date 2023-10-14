using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using TournamentAPI;

namespace Functions.Tests;

public abstract class FunctionFactory
{
    public abstract void CreateFunction();
    public abstract FunctionContext Context { get; set; }
    public abstract HttpRequestData Request { get; set; }
}