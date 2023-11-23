var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<FlyballRaceDayDbContext>(ServicesLocator.Database);

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddScoped<TournamentService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGroup("/tournament").MapTournamentApis().WithTags("Tournament").WithOpenApi();

app.MapDefaultEndpoints();

app.Run();
