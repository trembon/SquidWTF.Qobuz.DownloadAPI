using System.Text;
using System.Text.RegularExpressions;

namespace SquidWTF.Qobuz.DownloadAPI;

public static partial class DownloadEndpoint
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
            folderName = SanitizeFolderName(folderName);

            session.DownloadFolder = Path.Combine(configuration["Downloads:RootFolder"] ?? throw new ArgumentException("Download root folder is not set"), folderName);
            session.TracksToDownload = albumData.tracks_count;

            DownloadBackgroundService.DownloadQueue.Enqueue(downloadId);
            return Results.Ok(new DownloadEndpointResponseModel(downloadId, folderName));
        });
    }

    private static string SanitizeFolderName(string name)
    {
        // Normalize unicode
        name = name.Normalize(NormalizationForm.FormKC);

        // Remove null characters
        name = name.Replace("\0", "");

        // Remove slashes (path separator)
        name = name.Replace("/", "_");

        // Remove control characters
        name = RegexControlCharachters().Replace(name, "");

        // Remove path traversal patterns
        name = name.Replace("..", "");

        // Trim whitespace
        name = name.Trim();

        // Optional: limit length (Linux usually allows 255 bytes)
        const int maxLength = 100;
        if (name.Length > maxLength)
            name = name[..maxLength];

        return name;
    }

    [GeneratedRegex(@"[\u0000-\u001F\u007F]")]
    private static partial Regex RegexControlCharachters();
}

public record DownloadEndpointResponseModel(string DownloadId, string DownloadFolder);
