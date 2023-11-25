using FlyballRaceDay.ApiService.Race;
using FlyballRaceDay.ApiService.Ring;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<FlyballRaceDayDbContext>(ServicesLocator.Database);

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddScoped<ITournamentService,TournamentService>();
builder.Services.AddScoped<IRingService, RingService>();
builder.Services.AddScoped<IRaceService, RaceService>();

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGroup("/tournament").MapTournamentApis().WithTags("Tournament").WithOpenApi();
app.MapGroup("/ring").MapRingApis().WithTags("Ring").WithOpenApi();
app.MapGroup("/race").MapRaceApis().WithTags("Race").WithOpenApi();

app.MapDefaultEndpoints();

app.Run();
