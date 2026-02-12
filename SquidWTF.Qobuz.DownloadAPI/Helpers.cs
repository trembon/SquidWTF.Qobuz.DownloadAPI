using System.Text;
using System.Text.RegularExpressions;

namespace SquidWTF.Qobuz.DownloadAPI;

public static partial class Helpers
{
    public static string SanitizeNameForFileSystem(string name)
    {
        // Normalize unicode
        name = name.Normalize(NormalizationForm.FormKC);

        // Remove null characters
        name = name.Replace("\0", "");

        // Remove invalid Windows + problematic characters
        name = RegexProblematicCharacters().Replace(name, "_");

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
    [GeneratedRegex(@"[<>:""/\\|?*]")]
    private static partial Regex RegexProblematicCharacters();
}
