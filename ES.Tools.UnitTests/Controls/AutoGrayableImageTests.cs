using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ES.Tools.Controls;
using ES.Tools.Effects;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Controls
{
  /// <summary>
  /// Unit tests testing the <see cref="AutoGrayableImage"/>
  /// </summary>
  public class AutoGrayableImageTests : TestBase
  {
    private ImageSource _imageSource;

    [SetUp]
    public void Setup()
    {
      base.PackURIFix();
      _imageSource = GetTestImageSource();
    }

    private ImageSource GetTestImageSource()
    {
      var sourceStream = new MemoryStream(Properties.Resources.TestImage);
      var decoder = BitmapDecoder.Create(sourceStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
      _imageSource = decoder.Frames[0];
      _imageSource.Freeze();
      return _imageSource;
    }

    [Test, Apartment(ApartmentState.STA)]
    public void AutoGrayableImageTest()
    {
      var image = new AutoGrayableImage { Source = _imageSource };
      var bitmapImage = (BitmapFrame)_imageSource;
      Assert.AreEqual(null, image.Effect);

      image.Measure(new Size(bitmapImage.PixelWidth, bitmapImage.PixelHeight));
      image.Arrange(new Rect(0, 0, bitmapImage.PixelWidth, bitmapImage.PixelHeight));

      var enabledColors = GetMap(image, bitmapImage);

      // Check saturation
      CheckColors(enabledColors, c => Assert.Greater(GetSaturation(c), 0));

      image.IsEnabled = false;
      var disabledColors = GetMap(image, bitmapImage);

      Assert.AreEqual(typeof(GrayscaleEffect), image.Effect?.GetType());

      // Check saturation
      CheckColors(disabledColors, c => Assert.AreEqual(0, GetSaturation(c), 0.001));
      CheckColors(disabledColors, c => Assert.AreEqual(0, GetHue(c), 0.001));
      CheckColors(enabledColors, disabledColors, (c1, c2) => Assert.GreaterOrEqual(GetBrightness(c1), GetBrightness(c2)));
    }

    private static void CheckColors(Color[,] colors, Action<Color> check)
    {
      for (int i = 0; i < colors.GetLength(0); i++)
      {
        for (int j = 0; j < colors.GetLength(1); j++)
        {
          check(colors[i, j]);
        }
      }
    }

    private static void CheckColors(Color[,] enabledColors, Color[,] disabledColors, Action<Color, Color> check)
    {
      for (int i = 0; i < enabledColors.GetLength(0); i++)
      {
        for (int j = 0; j < enabledColors.GetLength(1); j++)
        {
          check(enabledColors[i, j], disabledColors[i, j]);
        }
      }
    }

    private static Color[,] GetMap(AutoGrayableImage image, BitmapFrame bitmapImage)
    {
      var colors = new Color[bitmapImage.PixelWidth, bitmapImage.PixelHeight];

      for (int i = 0; i < bitmapImage.PixelWidth; i++)
      {
        for (int j = 0; j < bitmapImage.PixelHeight; j++)
        {
          colors[i, j] = GetColor(image, new Point(i, j));
        }
      }

      return colors;
    }

    private static float GetSaturation(Color c)
    {
      return System.Drawing.Color.FromArgb(c.R, c.G, c.B).GetSaturation();
    }

    private static float GetBrightness(Color c)
    {
      return System.Drawing.Color.FromArgb(c.R, c.G, c.B).GetBrightness();
    }

    private static float GetHue(Color c)
    {
      return System.Drawing.Color.FromArgb(c.R, c.G, c.B).GetHue();
    }

    private static Color GetColor(Image image, Point point)
    {
      // Use RenderTargetBitmap to get the visual, in case the image has been transformed.
      var renderTargetBitmap = new RenderTargetBitmap((int)image.ActualWidth, (int)image.ActualHeight, 96, 96, PixelFormats.Default);
      renderTargetBitmap.Render(image);

      // Make sure that the point is within the dimensions of the image.
      if (point.X <= renderTargetBitmap.PixelWidth && point.Y <= renderTargetBitmap.PixelHeight)
      {
        // Create a cropped image at the supplied point coordinates.
        var croppedBitmap = new CroppedBitmap(renderTargetBitmap, new Int32Rect((int)point.X, (int)point.Y, 1, 1));

        // Copy the sampled pixel to a byte array.
        byte[] pixels = new byte[4];
        croppedBitmap.CopyPixels(pixels, 4, 0);

        // Get the sampled color.
        return Color.FromArgb(255, pixels[2], pixels[1], pixels[0]);
      }

      return Colors.Transparent;
    }
  }
}