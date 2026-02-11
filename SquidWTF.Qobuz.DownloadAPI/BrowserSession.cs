using Microsoft.Playwright;

namespace SquidWTF.Qobuz.DownloadAPI;

public sealed class BrowserSession(Guid sessionId, IBrowserContext context, IPage page)
{
    public Guid Id { get; } = sessionId;
    public IBrowserContext Context { get; } = context;
    public IPage Page { get; } = page;
    public SearchJsonModel? SearchResult { get; set; }
    public DateTime LastUsed { get; set; } = DateTime.UtcNow;

    public bool IsDownloading { get; set; } = false;
    public bool HasStartedDownloading { get; set; } = false;
    public string? DownloadId { get; set; }
    public string? DownloadFolder { get; set; }
    public int TracksToDownload { get; set; } = 0;
    public int DownloadedTracks { get; set; } = 0;
}
