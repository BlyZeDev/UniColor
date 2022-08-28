namespace UniColor.Extensions;

/// <summary>
/// Extension class for <see cref="UniColor"/>
/// </summary>
public static class UniColorSystemExtensions
{
    private static IDictionary<UniColor, ConsoleColor> colors = new Dictionary<UniColor, ConsoleColor>()
    {
        { UniColor.FromRgb(0, 0, 0), ConsoleColor.Black },
        { UniColor.FromRgb(0, 0, 128), ConsoleColor.DarkBlue },
        { UniColor.FromRgb(0, 128, 0), ConsoleColor.DarkGreen },
        { UniColor.FromRgb(0, 128, 128), ConsoleColor.DarkCyan },
        { UniColor.FromRgb(128, 0, 0), ConsoleColor.DarkRed },
        { UniColor.FromRgb(128, 0, 128), ConsoleColor.DarkMagenta },
        { UniColor.FromRgb(128, 128, 0), ConsoleColor.DarkYellow },
        { UniColor.FromRgb(192, 192, 192), ConsoleColor.Gray },
        { UniColor.FromRgb(128, 128, 128), ConsoleColor.DarkGray },
        { UniColor.FromRgb(0, 0, 255), ConsoleColor.Blue },
        { UniColor.FromRgb(0, 255, 0), ConsoleColor.Green },
        { UniColor.FromRgb(0, 255, 255), ConsoleColor.Cyan },
        { UniColor.FromRgb(255, 0, 0), ConsoleColor.Red },
        { UniColor.FromRgb(255, 0, 255), ConsoleColor.Magenta },
        { UniColor.FromRgb(255, 255, 0), ConsoleColor.Yellow },
        { UniColor.FromRgb(255, 255, 255), ConsoleColor.White }
    };

    /// <summary>
    /// Returns a new instance of <see cref="System.Drawing.Color"/>
    /// </summary>
    public static System.Drawing.Color ToSystem(in this UniColor c)
        => System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);

    /// <summary>
    /// Returns a new instance of <see cref="UniColor"/>
    /// </summary>
    public static UniColor ToUniColor(in this System.Drawing.Color c)
        => UniColor.FromRgba(c.R, c.G, c.B, c.A);

    /// <summary>
    /// Returns the nearest <see cref="ConsoleColor"/>
    /// </summary>
    public static ConsoleColor ToNearestConsoleColor(this UniColor c)
    {
        var minDiff = colors.Keys.MinBy(x => GetDiff(x, c));
        colors.TryGetValue(minDiff, out var nearestColor);

        return nearestColor;
    }

    private static int GetDiff(in UniColor color, in UniColor toMatch)
    {
        var (redDiff, greenDiff, blueDiff) =
            (color.R - toMatch.R, color.G - toMatch.G, color.B - toMatch.B);

        return redDiff * redDiff + greenDiff * greenDiff + blueDiff * blueDiff;
    }
}
