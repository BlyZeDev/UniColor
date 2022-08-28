namespace UniColor.Extensions;

/// <summary>
/// Extension class for <see cref="UniColor"/>
/// </summary>
public static class UniColorAndroidExtensions
{
    /// <summary>
    /// Returns a new instance of <see cref="Android.Graphics.Color"/>
    /// </summary>
    public static Android.Graphics.Color ToAndroid(in this UniColor c)
        => Android.Graphics.Color.Argb(c.A, c.R, c.G, c.B);

    /// <summary>
    /// Returns a new instance of <see cref="UniColor"/>
    /// </summary>
    public static UniColor ToUniColor(this Android.Graphics.Color c)
        => new(c.R, c.G, c.B, c.A);
}
