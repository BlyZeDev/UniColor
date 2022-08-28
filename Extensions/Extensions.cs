namespace UniColor.Extensions;

/// <summary>
/// Extension class for <see cref="UniColor"/>
/// </summary>
public static class UniColorSharpExtensions
{
    /// <summary>
    /// Returns a new instance of <see cref="SixLabors.ImageSharp.Color"/>
    /// </summary>
    public static SixLabors.ImageSharp.Color ToSharp(in this UniColor c)
        => SixLabors.ImageSharp.Color.FromRgba(c.R, c.G, c.B, c.A);

    /// <summary>
    /// Returns a new instance of <see cref="UniColor"/>
    /// </summary>
    public static UniColor ToUniColor(in this SixLabors.ImageSharp.Color c)
        => UniColor.FromHex(c.ToHex(), HexType.RRGGBBAA);
}

/// <summary>
/// Extension class for <see cref="UniColor"/>
/// </summary>
public static class UniColorSkiaExtensions
{
    /// <summary>
    /// Returns a new instance of <see cref="SkiaSharp.SKColor"/>
    /// </summary>
    public static SkiaSharp.SKColor ToSkia(in this UniColor c)
        => new(c.R, c.G, c.B, c.A);

    /// <summary>
    /// Returns a new instance of <see cref="SkiaSharp.SKColorF"/>
    /// </summary>
    public static SkiaSharp.SKColorF ToSkiaF(in this UniColor c)
    {
        var (r, g, b, a) = c.GetRgbaFractions();

        return new((float)r, (float)g, (float)b, (float)a);
    }

    /// <summary>
    /// Returns a new instance of <see cref="UniColor"/>
    /// </summary>
    public static UniColor ToUniColor(in this SkiaSharp.SKColor c)
        => UniColor.FromRgba(c.Red, c.Green, c.Blue, c.Alpha);

    /// <summary>
    /// Returns a new instance of <see cref="UniColor"/>
    /// </summary>
    public static UniColor ToUniColor(in this SkiaSharp.SKColorF c)
        => UniColor.FromRgba(c.Red, c.Green, c.Blue, c.Alpha);
}
