using Microsoft.Playwright;
using System.Collections.Concurrent;

namespace SquidWTF.Qobuz.DownloadAPI;

public interface IPlaywrightSessionService
{
    Task<IBrowserContext> CreateContext();
    BrowserSession Save(IBrowserContext context, IPage page);
    BrowserSession? Get(Guid sessionId);
    BrowserSession? GetByDownloadId(string downloadId);
    List<BrowserSession> GetAll();
    Task Cleanup(Guid sessionId);
    Task Remove(Guid sessionId);
}

public class PlaywrightSessionService : IPlaywrightSessionService
{
    private readonly IBrowser? browser;
    private readonly ConcurrentDictionary<Guid, BrowserSession> sessions = [];

    private async Task<IBrowser> GetBrowser()
    {
        if (browser != null)
            return browser;

        var playwright = await Playwright.CreateAsync();

        return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            Args = ["--no-sandbox", "--disable-dev-shm-usage"]
        });
    }

    public async Task<IBrowserContext> CreateContext()
    {
        var browser = await GetBrowser();
        var context = await browser.NewContextAsync(new()
        {
            AcceptDownloads = true
        });

        return context;
    }

    public BrowserSession Save(IBrowserContext context, IPage page)
    {
        Guid sessionId = Guid.NewGuid();
        var session = new BrowserSession(sessionId, context, page);
        sessions.TryAdd(sessionId, session);
        return session;
    }

    public BrowserSession? Get(Guid sessionId)
    {
        if (sessions.TryGetValue(sessionId, out var session))
            session.LastUsed = DateTime.UtcNow;

        return session;
    }

    public BrowserSession? GetByDownloadId(string downloadId)
    {
        return sessions.Values.FirstOrDefault(s => s.DownloadId == downloadId);
    }

    public List<BrowserSession> GetAll()
    {
        return [.. sessions.Values];
    }

    public async Task Cleanup(Guid sessionId)
    {
        if (sessions.TryGetValue(sessionId, out var session))
        {
            await session.Context.CloseAsync();
        }
    }

    public async Task Remove(Guid sessionId)
    {
        if (sessions.TryRemove(sessionId, out var session))
        {
            try
            {
                await session.Context.CloseAsync();
            }
            catch
            {
                // Ignore exceptions during cleanup
            }
        }
    }
}
