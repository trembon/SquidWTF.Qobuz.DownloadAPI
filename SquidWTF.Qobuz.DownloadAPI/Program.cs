using SquidWTF.Qobuz.DownloadAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables(prefix: "APP_");

builder.Services.AddSingleton<IPlaywrightSessionService, PlaywrightSessionService>();
builder.Services.AddHostedService<PlaywrightSessionCleanupService>();
builder.Services.AddHostedService<DownloadBackgroundService>();

var app = builder.Build();

app.MapSearchEndpoint();
app.MapDownloadEndpoint();
app.MapDownloadStatusEndpoint();

app.Run();