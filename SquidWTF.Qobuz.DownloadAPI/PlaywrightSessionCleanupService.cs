namespace SquidWTF.Qobuz.DownloadAPI;

public class PlaywrightSessionCleanupService(IPlaywrightSessionService playwrightSessionService) : BackgroundService
{
    private readonly TimeSpan _timeout = TimeSpan.FromMinutes(10);
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(2);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var sessions = playwrightSessionService.GetAll().ToList();
            if (sessions.Count == 0)
                await playwrightSessionService.Reset();

            foreach (var session in sessions)
            {
                if (DateTime.UtcNow - session.LastUsed > _timeout)
                {
                    await playwrightSessionService.Remove(session.Id);
                }
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}
