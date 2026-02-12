namespace SquidWTF.Qobuz.DownloadAPI;

public static class DownloadEndpoint
{
    public static void MapDownloadEndpoint(this WebApplication app)
    {
        app.MapPost("/download", async (Guid sessionId, string downloadId, IPlaywrightSessionService sessions, IConfiguration configuration) =>
        {
            var session = sessions.Get(sessionId);
            if (session is null || session.IsDownloading)
                return Results.NotFound();

            session.LastUsed = DateTime.UtcNow;

            var albumData = session.SearchResult?.data.albums.items.FirstOrDefault(x => x.id == downloadId);
            if (albumData is null)
                return Results.NotFound();

            var coverImage = session.Page.Locator($"img[src*='{downloadId}']");
            if (await coverImage.CountAsync() == 0)
                return Results.NotFound();

            session.IsDownloading = true;
            session.DownloadId = downloadId;

            string folderName = $"{albumData.artist.name} - {albumData.title} ({albumData.release_date_original[..4]}){(albumData.parental_warning ? " E" : "")}";
            session.DownloadFolder = Path.Combine(configuration["Downloads:RootFolder"] ?? throw new ArgumentException("Download root folder is not set"), folderName);
            session.TracksToDownload = albumData.tracks_count;

            DownloadBackgroundService.DownloadQueue.Enqueue(downloadId);
            return Results.Ok(new DownloadEndpointResponseModel(downloadId, folderName));
        });
    }
}

public record DownloadEndpointResponseModel(string DownloadId, string DownloadFolder);
