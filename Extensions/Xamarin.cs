namespace UniColor.Extensions;

/// <summary>
/// Extension class for <see cref="UniColor"/>
/// </summary>
public static class UniColorXamarinExtensions
{
    /// <summary>
    /// Returns a new instance of <see cref="Xamarin.Forms.Color"/>
    /// </summary>
    public static Xamarin.Forms.Color ToXamarin(in this UniColor c)
        => Xamarin.Forms.Color.FromRgba(c.R, c.G, c.B, c.A);

    /// <summary>
    /// Returns a new instance of <see cref="UniColor"/>
    /// </summary>
    public static UniColor ToUniColor(this Xamarin.Forms.Color c)
        => UniColor.FromRgba(c.R, c.G, c.B, c.A);
}
