using FlyballRaceDay.ApiService.Race;
using Serilog;



Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddMongoDBClient(ServicesLocator.Database);
builder.Services.AddDbContext<FlyballRaceDayDbContext>();
//builder.AddNpgsqlDbContext<FlyballRaceDayDbContext>(ServicesLocator.Database);

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddScoped<ITournamentService,TournamentService>();
builder.Services.AddScoped<IRingService, RingService>();
builder.Services.AddScoped<IRaceService, RaceService>();

builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);

builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    if (!app.Environment.IsEnvironment("Tests"))
    {
        // This Sleep was added to let the database get created in docker
        Thread.Sleep(2000);
        app.SeedDatabase();
    }
    
   
    
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGet("/test", () => "Hello World!");
app.MapGroup("/tournament").MapTournamentApis().WithTags("Tournament").WithOpenApi();
app.MapGroup("/ring").MapRingApis().WithTags("Ring").WithOpenApi();
app.MapGroup("/race").MapRaceApis().WithTags("Race").WithOpenApi();

app.MapDefaultEndpoints();



app.Run();



public partial class Program { }