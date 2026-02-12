using Microsoft.Playwright;
using System.Collections.Concurrent;

namespace SquidWTF.Qobuz.DownloadAPI;

public interface IPlaywrightSessionService
{
    Task<IBrowserContext> CreateContext();
    BrowserSession Save(IBrowserContext context, IPage page);
    BrowserSession? Get(Guid sessionId);
    bool TryGet(Guid sessionId, out BrowserSession? session);
    BrowserSession? GetByDownloadId(string downloadId);
    List<BrowserSession> GetAll();
    Task Cleanup(Guid sessionId);
    Task Remove(Guid sessionId);
    Task Reset();
}

public class PlaywrightSessionService : IPlaywrightSessionService
{
    private IBrowser? browser;
    private readonly ConcurrentDictionary<Guid, BrowserSession> sessions = [];

    private async Task<IBrowser> GetBrowser()
    {
        if (browser != null)
            return browser;

        var playwright = await Playwright.CreateAsync();

        browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            Args = ["--no-sandbox", "--disable-dev-shm-usage"]
        });

        return browser;
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

    public bool TryGet(Guid sessionId, out BrowserSession? session)
    {
        session = Get(sessionId);
        return session is not null;
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

    public async Task Reset()
    {
        var localBrowser = browser;
        browser = null;

        if (localBrowser != null)
        {
            try
            {
                await localBrowser.CloseAsync();
                await localBrowser.DisposeAsync();
            }
            catch { }
        }
    }
}
