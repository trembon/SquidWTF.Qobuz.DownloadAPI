using Microsoft.Playwright;
using System.Collections.Concurrent;

namespace SquidWTF.Qobuz.DownloadAPI;

public static class SearchEndpoint
{
    private static readonly ConcurrentDictionary<string, Guid> searchCache = new();

    public static void MapSearchEndpoint(this WebApplication app)
    {
        app.MapGet("/search", async (string query, string type, IPlaywrightSessionService sessions, IConfiguration configuration) =>
        {
            if (string.IsNullOrWhiteSpace(query))
                return Results.BadRequest("Invalid query.Cannot be empty.");

            if (type != "album")
                return Results.BadRequest("Invalid type. Only 'album' is supported.");

            IBrowserContext? context = null;

            try
            {
                SearchJsonModel result;
                Guid sessionId;
                if (searchCache.TryGetValue($"{type}|{query}", out var cachedSessionId) && sessions.TryGet(cachedSessionId, out BrowserSession? cachedSession) && cachedSession != null && !cachedSession.IsDownloading)
                {
                    sessionId = cachedSessionId;
                    result = cachedSession.SearchResult!;
                }
                else
                {
                    context = await sessions.CreateContext();

                    var page = await context.NewPageAsync();
                    await page.GotoAsync(configuration["SquidWTF:QobuzUrl"] ?? throw new ArgumentException("SquidWTF QobuzUrl is not set"));

                    var responseTask = page.WaitForResponseAsync(r => r.Url.Contains("/api/get-music") && r.Status == 200);

                    await page.FillAsync("#search", query);
                    await page.Locator("svg.lucide-arrow-right").Locator("xpath=..").ClickAsync();

                    var response = await responseTask;
                    result = await response.JsonAsync<SearchJsonModel>();

                    var session = sessions.Save(context, page);
                    sessionId = session.Id;
                    session.SearchResult = result;

                    searchCache.TryAdd($"{type}|{query}", session.Id);
                }

                SearchEndpointResponseItemModel[] items = [];
                if (type == "album")
                {
                    items = [.. result.data.albums.items.Select(x => new SearchEndpointResponseItemModel(x.id, x.artist.name, x.title, DateTime.Parse(x.release_date_original), x.parental_warning, x.tracks_count, x.duration, x.url.Replace("/fr-fr/", "/us-en/"), type))];
                }

                return Results.Ok(new SearchEndpointResponseModel(sessionId, items));
            }
            catch (Exception ex)
            {
                try
                {
                    if (context is not null)
                        await context.CloseAsync();
                }
                catch { }

                return Results.InternalServerError(ex.Message);
            }
        });
    }
}

public record SearchEndpointResponseModel(Guid SessionId, SearchEndpointResponseItemModel[] Items);
public record SearchEndpointResponseItemModel(string Id, string Artist, string Album, DateTime ReleaseDate, bool Explicit, int TrackCount, int Duration, string InfoUrl, string Type);
