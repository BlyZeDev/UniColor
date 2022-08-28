namespace UniColor;

using Newtonsoft.Json;

/// <summary>
/// A platform-independent color system that supports Json Serialization
/// </summary>
[Serializable]
public readonly struct UniColor : IEquatable<UniColor>
{
    /// <summary>
    /// Returns an empty color
    /// </summary>
    public static UniColor Empty { get; } = new UniColor(true);

    /// <summary>
    /// Returns <see langword="true"/> if the color is empty, otherwise <see langword="false"/>
    /// </summary>
    [JsonRequired]
    public bool IsEmpty { get; init; }

    /// <summary>
    /// Returns the Red component of the color
    /// </summary>
    [JsonRequired]
    public byte R { get; init; }

    /// <summary>
    /// Returns the Green component of the color
    /// </summary>
    [JsonRequired]
    public byte G { get; init; }

    /// <summary>
    /// Returns the B component of the color
    /// </summary>
    [JsonRequired]
    public byte B { get; init; }

    /// <summary>
    /// Returns the Alpha component of the color
    /// </summary>
    [JsonRequired]
    public byte A { get; init; }

    private UniColor(bool isEmpty)
    {
        IsEmpty = isEmpty;
        R = byte.MinValue;
        G = byte.MinValue;
        B = byte.MinValue;
        A = byte.MinValue;
    }

    [JsonConstructor]
    private UniColor(byte red, byte green, byte blue, byte alpha, bool isEmpty)
    {
        IsEmpty = isEmpty;
        R = red;
        G = green;
        B = blue;
        A = alpha;
    }

    /// <summary>
    /// Instantiates an empty color
    /// </summary>
    public UniColor()
    {
        IsEmpty = true;
        R = byte.MinValue;
        G = byte.MinValue;
        B = byte.MinValue;
        A = byte.MinValue;
    }

    /// <summary>
    /// Instantiates a color with the given component values [0-255]
    /// </summary>
    /// <param name="red">Red component</param>
    /// <param name="green">Green component</param>
    /// <param name="blue">Blue component</param>
    public UniColor(byte red, byte green, byte blue)
    {
        IsEmpty = false;
        R = red;
        G = green;
        B = blue;
        A = byte.MaxValue;
    }

    /// <summary>
    /// Instantiates a color with the given component values [0-255]
    /// </summary>
    /// <param name="red">Red component</param>
    /// <param name="green">Green component</param>
    /// <param name="blue">Blue component</param>
    /// <param name="alpha">Alpha component</param>
    public UniColor(byte red, byte green, byte blue, byte alpha)
    {
        IsEmpty = false;
        R = red;
        G = green;
        B = blue;
        A = alpha;
    }

    /// <summary>
    /// Instantiates a color with the given component fractions [0-1]
    /// <para>Component values are clamped to [0-1]</para>
    /// </summary>
    /// <param name="red">Red fraction</param>
    /// <param name="green">Green fraction</param>
    /// <param name="blue">Blue fraction</param>
    public UniColor(double red, double green, double blue)
    {
        this = FromRgb(red, green, blue);
    }

    /// <summary>
    /// Instantiates a color with the given component fractions [0-1]
    /// <para>Component values are clamped to [0-1]</para>
    /// </summary>
    /// <param name="red">Red fraction</param>
    /// <param name="green">Green fraction</param>
    /// <param name="blue">Blue fraction</param>
    /// <param name="alpha">Alpha fraction</param>
    public UniColor(double red, double green, double blue, double alpha)
    {
        this = FromRgba(red, green, blue, alpha);
    }

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>; <see cref="Empty"/> if HEX is invalid
    /// </summary>
    /// <param name="hex">Color HEX code</param>
    /// <param name="hexType">The formatting of the HEX code</param>
    public static UniColor FromHex(string hex, HexType hexType)
    {
        if (hex is null || hex.Length == 0) return Empty;
        if (hex[0] == '#') hex = hex[1..];

        try
        {
            if (hexType is HexType.RGB)
            {
                string r = char.ToString(hex[0]);
                string g = char.ToString(hex[1]);
                string b = char.ToString(hex[2]);

                return FromRgb(
                    Convert.ToByte(r + r, 16),
                    Convert.ToByte(g + g, 16),
                    Convert.ToByte(b + b, 16));
            }

            if (hexType is HexType.RGBA)
            {
                string r = char.ToString(hex[0]);
                string g = char.ToString(hex[1]);
                string b = char.ToString(hex[2]);
                string a = char.ToString(hex[3]);

                return FromRgba(
                    Convert.ToByte(r + r, 16),
                    Convert.ToByte(g + g, 16),
                    Convert.ToByte(b + b, 16),
                    Convert.ToByte(a + a, 16));
            }

            if (hexType is HexType.ARGB)
            {
                string a = char.ToString(hex[0]);
                string r = char.ToString(hex[1]);
                string g = char.ToString(hex[2]);
                string b = char.ToString(hex[3]);

                return FromRgba(
                    Convert.ToByte(r + r, 16),
                    Convert.ToByte(g + g, 16),
                    Convert.ToByte(b + b, 16),
                    Convert.ToByte(a + a, 16));
            }

            if (hexType is HexType.RRGGBB)
            {
                return FromRgb(
                    Convert.ToByte(hex[0..2], 16),
                    Convert.ToByte(hex[2..4], 16),
                    Convert.ToByte(hex[4..6], 16));
            }

            if (hexType is HexType.RRGGBBAA)
            {
                return FromRgba(
                    Convert.ToByte(hex[0..2], 16),
                    Convert.ToByte(hex[2..4], 16),
                    Convert.ToByte(hex[4..6], 16),
                    Convert.ToByte(hex[6..8], 16));
            }

            if (hexType is HexType.AARRGGBB)
            {
                return FromRgba(
                    Convert.ToByte(hex[2..4], 16),
                    Convert.ToByte(hex[4..6], 16),
                    Convert.ToByte(hex[6..8], 16),
                    Convert.ToByte(hex[0..2], 16));
            }
        }
        catch (Exception) { }

        return Empty;
    }

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// </summary>
    /// <param name="red">Red component</param>
    /// <param name="green">Green component</param>
    /// <param name="blue">Blue component</param>
    public static UniColor FromRgb(byte red, byte green, byte blue) => new(red, green, blue);

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// <para>Component values are clamped to [0-1]</para>
    /// </summary>
    /// <param name="red">Red fraction</param>
    /// <param name="green">Green fraction</param>
    /// <param name="blue">Blue fraction</param>
    public static UniColor FromRgb(double red, double green, double blue)
        => new(
            (byte)(Clamp(red, 0, 1) * byte.MaxValue),
            (byte)(Clamp(green, 0, 1) * byte.MaxValue),
            (byte)(Clamp(blue, 0, 1) * byte.MaxValue));

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// </summary>
    /// <param name="rgb">RGB value</param>
    public static UniColor FromRgb(int rgb)
        => new((byte)(rgb >> 24 & 0xFF),
            (byte)(rgb >> 16 & 0xFF),
            (byte)(rgb >> 8 & 0xFF));

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// </summary>
    /// <param name="red">Red component</param>
    /// <param name="green">Green component</param>
    /// <param name="blue">Blue component</param>
    /// <param name="alpha">Alpha component</param>
    public static UniColor FromRgba(byte red, byte green, byte blue, byte alpha) => new(red, green, blue, alpha);

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// <para>Component values are clamped to [0-1]</para>
    /// </summary>
    /// <param name="red">Red fraction</param>
    /// <param name="green">Green fraction</param>
    /// <param name="blue">Blue fraction</param>
    /// <param name="alpha">Alpha fraction</param>
    public static UniColor FromRgba(double red, double green, double blue, double alpha)
        => new(
            (byte)(Clamp(red, 0, 1) * byte.MaxValue),
            (byte)(Clamp(green, 0, 1) * byte.MaxValue),
            (byte)(Clamp(blue, 0, 1) * byte.MaxValue),
            (byte)(Clamp(alpha, 0, 1) * byte.MaxValue));

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// </summary>
    /// <param name="rgba">RGBA value</param>
    public static UniColor FromRgba(int rgba)
        => new((byte)(rgba >> 24 & 0xFF),
            (byte)(rgba >> 16 & 0xFF),
            (byte)(rgba >> 8 & 0xFF),
            (byte)(rgba & 0xFF));

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// </summary>
    /// <param name="argb">ARGB value</param>
    public static UniColor FromArgb(int argb)
        => new((byte)(argb >> 16 & 0xFF),
            (byte)(argb >> 8 & 0xFF),
            (byte)(argb & 0xFF),
            (byte)(argb >> 24 & 0xFF));

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// </summary>
    /// <param name="y">Luminance value</param>
    /// <param name="cb">Chrominance blue</param>
    /// <param name="cr">Chrominance red</param>
    public static UniColor FromYCbCr(double y, double cb, double cr)
    {
        var r = Math.Max(0.0, Math.Min(1.0, y + 0.0000 * cb + 1.4022 * cr));
        var g = Math.Max(0.0, Math.Min(1.0, y - 0.3456 * cb - 0.7145 * cr));
        var b = Math.Max(0.0, Math.Min(1.0, y + 1.7710 * cb + 0.0000 * cr));

        return new UniColor((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
    }

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// </summary>
    /// <param name="h">Hue value</param>
    /// <param name="s">Saturation value</param>
    /// <param name="l">Lightness value</param>
    public static UniColor FromHsl(int h, double s, double l)
    {
        byte r;
        byte g;
        byte b;

        if (s == 0)
        {
            r = g = b = (byte)(l * 255);
        }
        else
        {
            double v1, v2;
            var hue = (double)h / 360;

            v2 = (l < 0.5) ? (l * (1 + s)) : (l + s - (l * s));
            v1 = 2 * l - v2;

            r = (byte)(255 * HueToRGB(v1, v2, hue + (1.0 / 3)));
            g = (byte)(255 * HueToRGB(v1, v2, hue));
            b = (byte)(255 * HueToRGB(v1, v2, hue - (1.0 / 3)));
        }

        return new UniColor(r, g, b);
    }

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// </summary>
    /// <param name="randomAlpha">If <see langword="true"/> alpha component is also random, otherwise <see cref="byte.MaxValue"/></param>
    public static UniColor GetRandom(bool randomAlpha = false)
        => GetRandom(new Random(), randomAlpha);

    /// <summary>
    /// Returns an instance of <see cref="UniColor"/>
    /// </summary>
    /// <param name="seed">Seed for the color randomizer</param>
    /// <param name="randomAlpha">If <see langword="true"/> alpha component is also random, otherwise <see cref="byte.MaxValue"/></param>
    public static UniColor GetRandom(int seed, bool randomAlpha = false)
        => GetRandom(new Random(seed), randomAlpha);

    /// <summary>
    /// Returns a <see cref="Tuple{T1, T2, T3}"/> of the color components (<see cref="byte"/> red, <see cref="byte"/> green, <see cref="byte"/> blue)
    /// </summary>
    public (byte r, byte g, byte b) GetRgb() => (R, G, B);

    /// <summary>
    /// Returns a <see cref="Tuple{T1, T2, T3}"/> of the color fractions (<see cref="double"/> red, <see cref="double"/> green, <see cref="double"/> blue)
    /// </summary>
    public (double r, double g, double b) GetRgbFractions()
        => (R / (double)byte.MaxValue, G / (double)byte.MaxValue, B / (double)byte.MaxValue);

    /// <summary>
    /// Returns a <see cref="Tuple{T1, T2, T3, T4}"/> of the color components (<see cref="byte"/> red, <see cref="byte"/> green, <see cref="byte"/> blue, <see cref="byte"/> alpha)
    /// </summary>
    public (byte r, byte g, byte b, byte a) GetRgba() => (R, G, B, A);

    /// <summary>
    /// Returns a <see cref="Tuple{T1, T2, T3, T4}"/> of the color fractions (<see cref="double"/> red, <see cref="double"/> green, <see cref="double"/> blue, <see cref="double"/> alpha)
    /// </summary>
    public (double r, double g, double b, double a) GetRgbaFractions()
        => (R / (double)byte.MaxValue, G / (double)byte.MaxValue, B / (double)byte.MaxValue, A / (double)byte.MaxValue);

    /// <summary>
    /// Returns a HEX color <see cref="string"/> from the <see cref="UniColor"/>
    /// </summary>
    /// <param name="type">The formatting of the HEX code</param>
    public string ToHex(HexType type)
    {
        string r = R.ToString("X2");
        string g = G.ToString("X2");
        string b = B.ToString("X2");

        if (type is HexType.RGB) return r[0] == r[1] && g[0] == g[1] && b[0] == b[1] ? $"#{r[0]}{g[0]}{b[0]}" : "";

        if (type is HexType.RGBA)
        {
            string a = A.ToString("X2");

            return r[0] == r[1] && g[0] == g[1] && b[0] == b[1] && a[0] == a[1] ? $"#{r[0]}{g[0]}{b[0]}{a[0]}" : "";
        }

        if (type is HexType.ARGB)
        {
            string a = A.ToString("X2");

            return r[0] == r[1] && g[0] == g[1] && b[0] == b[1] && a[0] == a[1] ? $"#{a[0]}{r[0]}{g[0]}{b[0]}" : "";
        }

        if (type is HexType.RRGGBB) return $"#{r}{g}{b}";

        if (type is HexType.RRGGBBAA) return $"#{r}{g}{b}{A:X2}";

        if (type is HexType.AARRGGBB) return $"#{A:X2}{r}{g}{b}";

        return "";
    }

    /// <summary>
    /// Returns a RGB color <see cref="int"/> from the <see cref="UniColor"/>
    /// </summary>
    public int ToRgb()
        => unchecked((R << 24) + (G << 16) + (B << 8));

    /// <summary>
    /// Returns a RGBA color <see cref="int"/> from the <see cref="UniColor"/>
    /// </summary>
    public int ToRgba()
        => unchecked((R << 24) + (G << 16) + (B << 8) + A);

    /// <summary>
    /// Returns a ARGB color <see cref="int"/> from the <see cref="UniColor"/>
    /// </summary>
    public int ToArgb()
        => unchecked((A << 24) + (R << 16) + (G << 8) + B);

    /// <summary>
    /// Returns a YCbCr color <see cref="Tuple{T1, T2, T3}"/> (<see cref="double"/> luminance, <see cref="double"/> chrominance blue, <see cref="double"/> chrominance red)
    /// </summary>
    public (double y, double cb, double cr) ToYCbCr()
    {
        var (r, g, b) = GetRgbFractions();

        var y = 0.2989 * r + 0.5866 * g + 0.1145 * b;
        var cb = -0.1687 * r - 0.3313 * g + 0.5000 * b;
        var cr = 0.5000 * r - 0.4184 * g - 0.0816 * b;

        return (y, cb, cr);
    }

    /// <summary>
    /// Returns a HSL color <see cref="Tuple{T1, T2, T3}"/> (<see cref="int"/> hue, <see cref="double"/> saturation, <see cref="double"/> lightness)
    /// </summary>
    public (int h, double s, double l) ToHsl()
    {
        int h;
        double s;
        double l;

        double r = R / 255d;
        double g = G / 255d;
        double b = B / 255d;

        var min = Math.Min(Math.Min(r, g), b);
        var max = Math.Max(Math.Max(r, g), b);
        var delta = max - min;

        l = (max + min) / 2;

        if (delta == 0)
        {
            h = 0;
            s = 0;
        }
        else
        {
            s = (l <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));

            double hue;

            if (r == max)
            {
                hue = ((g - b) / 6) / delta;
            }
            else if (g == max)
            {
                hue = (1.0f / 3) + ((b - r) / 6) / delta;
            }
            else
            {
                hue = (2.0f / 3) + ((r - g) / 6) / delta;
            }

            if (hue < 0)
                hue += 1;
            if (hue > 1)
                hue -= 1;

            h = (int)Math.Round(hue * 360, 0, MidpointRounding.ToEven);
        }

        return (h, s, l);
    }

    /// <summary>
    /// Returns the inverted color
    /// </summary>
    public UniColor Invert()
        => IsEmpty ?
        this :
        new((byte)(byte.MaxValue - R),
            (byte)(byte.MaxValue - G),
            (byte)(byte.MaxValue - B));

    /// <summary>
    /// Returns the inverted color
    /// </summary>
    /// <param name="invertAlpha">If <see langword="true"/> the alpha value will get inverted too, otherwise alpha value will not be changed</param>
    public UniColor Invert(bool invertAlpha)
    {
        return IsEmpty ?
            this :
            new((byte)(byte.MaxValue - R),
            (byte)(byte.MaxValue - G),
            (byte)(byte.MaxValue - B),
            invertAlpha ? (byte)(byte.MaxValue - A) : A);
    }

    /// <summary>
    /// Returns the Brightness value of the color
    /// </summary>
    public float GetBrightness()
    {
        MinMaxRgb(out var min, out var max, R, G, B);

        return (max + min) / (byte.MaxValue * 2f);
    }

    /// <summary>
    /// Returns the Saturation value of the color
    /// </summary>
    public float GetSaturation()
    {
        if (R == G && G == B) return 0f;

        MinMaxRgb(out var min, out var max, R, G, B);

        int div = max + min;

        if (div > byte.MaxValue)
            div = byte.MaxValue * 2 - max - min;

        return (max - min) / (float)div;
    }

    /// <summary>
    /// Returns the Hue value of the color
    /// </summary>
    public float GetHue()
    {
        if (R == G && G == B) return 0f;

        MinMaxRgb(out var min, out var max, R, G, B);

        float delta = max - min;
        float hue;

        if (R == max) hue = (G - B) / delta;
        else if (G == max) hue = (B - R) / delta + 2f;
        else hue = (R - G) / delta + 4f;

        hue *= 60f;

        if (hue < 0f) hue += 360f;

        return hue;
    }

    private static void MinMaxRgb(out byte min, out byte max, byte r, byte g, byte b)
    {
        if (r > g)
        {
            max = r;
            min = g;
        }
        else
        {
            max = g;
            min = r;
        }
        if (b > max)
        {
            max = b;
        }
        else if (b < min)
        {
            min = b;
        }
    }

    private static double HueToRGB(double v1, double v2, double vH)
    {
        if (vH < 0)
            vH += 1;

        if (vH > 1)
            vH -= 1;

        if ((6 * vH) < 1)
            return (v1 + (v2 - v1) * 6 * vH);

        if ((2 * vH) < 1)
            return v2;

        if ((3 * vH) < 2)
            return (v1 + (v2 - v1) * ((2.0f / 3) - vH) * 6);

        return v1;
    }

    private static UniColor GetRandom(Random r, bool randomAlpha)
    {
        return new UniColor(
            (byte)r.Next(byte.MinValue, byte.MaxValue + 1),
            (byte)r.Next(byte.MinValue, byte.MaxValue + 1),
            (byte)r.Next(byte.MinValue, byte.MaxValue + 1),
            (byte)(randomAlpha ? r.Next(byte.MinValue, byte.MaxValue + 1) : byte.MaxValue));
    }

    private static double Clamp(double number, double min, double max)
    {
        if (max < min)
        {
            return max;
        }
        else if (number < min)
        {
            return min;
        }
        else if (number > max)
        {
            return max;
        }

        return number;
    }

    /// <summary>
    /// Adds the RGBA values
    /// </summary>
    public static UniColor operator +(in UniColor left, in UniColor right)
    {
        var (lr, lg, lb, la) = left.GetRgba();
        var (rr, rg, rb, ra) = right.GetRgba();

        return new UniColor(
            (byte)(rr + lr > byte.MaxValue ? byte.MaxValue : rr + lr),
            (byte)(rg + lg > byte.MaxValue ? byte.MaxValue : rg + lg),
            (byte)(rb + lb > byte.MaxValue ? byte.MaxValue : rb + lb),
            (byte)(ra + la > byte.MaxValue ? byte.MaxValue : ra + la));
    }

    /// <summary>
    /// Subtracts the RGBA values
    /// </summary>
    public static UniColor operator -(in UniColor left, in UniColor right)
    {
        var (lr, lg, lb, la) = left.GetRgba();
        var (rr, rg, rb, ra) = right.GetRgba();

        return new UniColor(
            (byte)(lr - rr < byte.MinValue ? byte.MinValue : lr - rr),
            (byte)(lg - rg < byte.MinValue ? byte.MinValue : lg - rg),
            (byte)(lb - rb < byte.MinValue ? byte.MinValue : lb - rb),
            (byte)(la - ra < byte.MinValue ? byte.MinValue : la - ra));
    }

    /// <summary>
    /// Compares the RGBA values
    /// </summary>
    public static bool operator ==(in UniColor left, in UniColor right)
        => left.IsEmpty == right.IsEmpty
        && left.R == right.R
        && left.G == right.G
        && left.B == right.B
        && left.A == right.A;

    /// <summary>
    /// Compares the RGBA values
    /// </summary>
    public static bool operator !=(in UniColor left, in UniColor right) => !(left == right);

    public bool Equals(UniColor other) => this == other;

    public override bool Equals(object? obj) => obj is UniColor other && Equals(other);

    /// <summary>
    /// Returns the hash code of this color
    /// </summary>
    public override int GetHashCode()
        => HashCode.Combine(R.GetHashCode(), G.GetHashCode(), B.GetHashCode(), A.GetHashCode());

    /// <summary>
    /// Returns a <see cref="string"/> representation of the color
    /// </summary>
    public override string ToString()
        => IsEmpty ? $"{nameof(UniColor)} [Empty]" : $"{nameof(UniColor)} [R={R}, G={G}, B={B}, A={A}]";
}

/// <summary>
/// The format of the color HEX
/// </summary>
public enum HexType
{
    /// <summary>
    /// A hex formatted like #RGB
    /// </summary>
    RGB,
    /// <summary>
    /// A hex formatted like #RGBA
    /// </summary>
    RGBA,
    /// <summary>
    /// A hex formatted like #ARGB
    /// </summary>
    ARGB,
    /// <summary>
    /// A hex formatted like #RRGGBB
    /// </summary>
    RRGGBB,
    /// <summary>
    /// A hex formatted like #RRGGBBAA
    /// </summary>
    RRGGBBAA,
    /// <summary>
    /// A hex formatted like #AARRGGBB
    /// </summary>
    AARRGGBB
}
