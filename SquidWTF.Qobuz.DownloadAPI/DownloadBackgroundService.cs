using Microsoft.Playwright;
using System.Collections.Concurrent;

namespace SquidWTF.Qobuz.DownloadAPI;

public class DownloadBackgroundService(IPlaywrightSessionService sessionService) : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(5);

    public static ConcurrentQueue<string> DownloadQueue { get; } = [];

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (DownloadQueue.TryDequeue(out var downloadId))
            {
                var session = sessionService.GetByDownloadId(downloadId);
                if (session is null)
                    continue;

                session.Page.Download += async (_, d) =>
                {
                    await HandleDownload(d, session);
                };

                try
                {
                    Directory.CreateDirectory(session.DownloadFolder!);
                }
                catch
                {
                    continue;
                }

                var coverImage = session.Page.Locator($"img[src*='{downloadId}']");
                var container = coverImage.Locator("xpath=..").Locator("xpath=..");

                await container.HoverAsync();
                await container.Locator("button[aria-haspopup='menu']").ClickAsync();

                await session.Page.Locator("p", new() { HasText = "No ZIP Archive" }).Locator("xpath=..").ClickAsync();

                while (true)
                {
                    await Task.Delay(500, stoppingToken);
                    if (session.DownloadedTracks == session.TracksToDownload)
                        break;
                }

                await sessionService.Cleanup(session.Id);
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }

    private static readonly Lock updateDownloadedTrackLock = new();
    private static async Task HandleDownload(IDownload download, BrowserSession session)
    {
        await download.SaveAsAsync(Path.Combine(session.DownloadFolder!, download.SuggestedFilename));
        lock (updateDownloadedTrackLock)
        {
            session.LastUsed = DateTime.UtcNow;
            session.DownloadedTracks += 1;
        }
    }
}
