namespace UniColor.Extensions;

public static class UniColorMauiExtensions
{
    public static Microsoft.Maui.Graphics.Color ToMaui(in this UniColor c)
        => Microsoft.Maui.Graphics.Color.FromRgba(c.R, c.G, c.B, c.A);

    public static UniColor ToUniColor(this Microsoft.Maui.Graphics.Color c)
        => UniColor.FromRgba(c.Red, c.Green, c.Blue, c.Alpha);
}
