using FlyballRace.APIClient;
using FlyballRaceDay.Shared;
using FlyballRaceDay.Web.Client.Pages;
using FlyballRaceDay.Web.Components;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisOutputCache(ServicesLocator.RedisCache);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRefitClient<IFlyballRaceDayApiService>().ConfigureHttpClient(client=> client.BaseAddress = new($"http://{ServicesLocator.ApiApplication}"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);
    
app.MapDefaultEndpoints();

app.Run();
