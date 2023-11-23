using FlyballRaceDay.ApiService;
using FlyballRaceDay.ApiService.Models;
using FlyballRaceDay.ApiService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<FlyballRaceDayDbContext>(ServicesLocator.Database);

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddScoped<TournamentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapPost("/tournament", (TournamentCreate tournament, TournamentService service) => service.CreateTournament(tournament));

app.MapDefaultEndpoints();

app.Run();

namespace FlyballRaceDay.ApiService
{
    record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
    {
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
