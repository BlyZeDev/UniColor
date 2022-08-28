# UniColor
A platform-independent color system that supports Json Serialization
## How to use
### Ways to initialize a new UniColor instance
```
var color = new UniColor(200, 100, 25, 255);
var colorFraction = new UniColor(0.34, 0.78, 1, 1);

var fromHex = UniColor.FromHex("#ABCDEF", HexType.RRGGBB);

var fromRgb = UniColor.FromRgb(69, 200, 33);
var fromRgba = UniColor.FromRgba(0.2, 0.5, 1, 1);
var fromArgb = UniColor.FromArgb(-123456);

var fromYCbCr = UniColor.FromYCbCr(2.65, 22.6, 43.7);
var fromHsl = UniColor.FromHsl(180, 1.4445, 1.69);

var random = UniColor.GetRandom(true);
```
### Get information about the color
```
float brightness = color.GetBrightness();
float saturation = color.GetSaturation();
float hue = color.GetHue();

var (r, g, b) = color.GetRgb();
var (r, g, b, a) = color.GetRgba();
var (r, g, b) = color.GetRgbFractions();
var (r, g, b, a) = color.GetRgbaFractions();

byte r = color.R;
byte g = color.G;
byte b = color.B;
byte a = color.A;
bool isEmpty = color.IsEmpty;
```
### Convert the color to other systems
```
int rgb = color.ToRgb();
int rgba = color.ToRgba();
int argb = color.ToArgb();

string hex = color.ToHex(HexType.RRGGBBAA);

var (h, s, l) = color.ToHsl();
var (y, cb, cr) = color.ToYCbCr();

string colorString = color.ToString();
```
### Work with the colors
```
var addedColor = color + anotherColor;
var subtractedColor = color - anotherColor;

var areEqual = color == anotherColor;
var anotherAreEqual = color.Equals(anotherColor);

var invertedColor = color.Invert(true);
```
### There are useful extensions for the UniColor struct
```
System.Drawing.Color systemColor = color.ToSystem();
System.ConsoleColor nearestCC = color.ToNearestConsoleColor();
UniColor uniColor = System.Drawing.Color.LightBlue.ToUniColor();

Xamarin.Forms.Color xamarinColor = color.ToXamarin();
UniColor uniColor = Xamarin.Forms.Color.DodgerBlue.ToUniColor();

Microsoft.Maui.Graphics.Color mauiColor = color.ToMaui();
UniColor uniColor = new Microsoft.Maui.Graphics.Color(10, 20, 30, 50).ToUniColor();

SixLabors.ImageSharp.Color sharpColor = color.ToSharp();
UniColor uniColor = SixLabors.ImageSharp.Color.DodgerBlue.ToUniColor();

SkiaSharp.SKColor skColor = color.ToSkia();
SkiaSharp.SKColorF skColorF = color.ToSkiaF();
UniColor uniColor = new SkiaSharp.SKColor(10, 20, 30, 50).ToUniColor();
UniColor uniColor = new SkiaSharp.SKColorF(0.1f, 0.2f, 0.3f, 0.5f).ToUniColor();

Android.Graphics.Color androidColor = color.ToAndroid();
UniColor uniColor = Android.Graphics.Color.DodgerBlue.ToUniColor();
```
