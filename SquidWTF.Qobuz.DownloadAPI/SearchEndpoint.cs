using Microsoft.Playwright;

namespace SquidWTF.Qobuz.DownloadAPI;

public static class SearchEndpoint
{
    public static void MapSearchEndpoint(this WebApplication app)
    {
        app.MapGet("/search", async (string query, string type, IPlaywrightSessionService sessions, IConfiguration configuration) =>
        {
            IBrowserContext? context = null;

            try
            {
                context = await sessions.CreateContext();

                var page = await context.NewPageAsync();
                await page.GotoAsync(configuration["SquidWTF:QobuzUrl"] ?? throw new ArgumentException("SquidWTF QobuzUrl is not set"));

                var responseTask = page.WaitForResponseAsync(r => r.Url.Contains("/api/get-music") && r.Status == 200);

                await page.FillAsync("#search", query);
                await page.Locator("svg.lucide-arrow-right").Locator("xpath=..").ClickAsync();

                var response = await responseTask;
                var result = await response.JsonAsync<SearchJsonModel>();

                SearchEndpointResponseItemModel[] items = [];
                if (type == "album")
                {
                    items = [.. result.data.albums.items.Select(x => new SearchEndpointResponseItemModel(x.id, x.artist.name, x.title, DateTime.Parse(x.release_date_original), x.parental_warning, x.tracks_count, x.duration, x.url, type))];
                }

                var session = sessions.Save(context, page);
                session.SearchResult = result;

                return Results.Ok(new SearchEndpointResponseModel(session.Id, items));
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
