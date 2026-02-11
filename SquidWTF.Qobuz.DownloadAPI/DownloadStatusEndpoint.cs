namespace SquidWTF.Qobuz.DownloadAPI;

public static class DownloadStatusEndpoint
{
    public static void MapDownloadStatusEndpoint(this WebApplication app)
    {
        app.MapGet("/download/status", async (string downloadId, IPlaywrightSessionService sessions) =>
        {
            var session = sessions.GetByDownloadId(downloadId);
            if (session is null || !session.IsDownloading)
                return Results.NotFound();

            session.LastUsed = DateTime.UtcNow;

            return Results.Ok(new DownloadStatusEndpointResponseModel(session.DownloadedTracks == session.TracksToDownload, session.DownloadedTracks, session.TracksToDownload));
        });
    }
}

public record DownloadStatusEndpointResponseModel(bool Complete, int DownloadedItems, int ItemsToDownload);
