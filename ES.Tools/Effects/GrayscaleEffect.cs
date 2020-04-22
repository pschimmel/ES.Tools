using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace ES.Tools.Effects
{
  /// <summary>
  /// An effect that turns the input into shades of gray.
  /// </summary>
  public class GrayscaleEffect : ShaderEffect
  {
    public GrayscaleEffect()
    {
      PixelShader = CreatePixelShader();
      UpdateShaderValue(InputProperty);
      UpdateShaderValue(SaturationFactorProperty);
    }

    /// <summary>
    /// Dependency property for Input
    /// </summary>
    public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(nameof(Input), typeof(GrayscaleEffect), 0);

    /// <summary>
    /// Implicit input
    /// </summary>
    public Brush Input
    {
      get => (Brush)GetValue(InputProperty);
      set => SetValue(InputProperty, value);
    }

    /// <summary>
    /// Dependency property for saturation factor
    /// </summary>
    public static readonly DependencyProperty SaturationFactorProperty = DependencyProperty.Register(nameof(SaturationFactor), typeof(double), typeof(GrayscaleEffect), new UIPropertyMetadata(0.0, PixelShaderConstantCallback(0), CoerceSaturationFactor));

    public double SaturationFactor
    {
      get => (double)GetValue(SaturationFactorProperty);
      set => SetValue(SaturationFactorProperty, value);
    }

    private static object CoerceSaturationFactor(DependencyObject d, object value)
    {
      var effect = (GrayscaleEffect)d;
      double newFactor = (double)value;

      return newFactor < 0.0 || newFactor > 1.0 ? effect.SaturationFactor : (object)newFactor;
    }

    private PixelShader CreatePixelShader()
    {
      var pixelShader = new PixelShader();

      if (DesignerProperties.GetIsInDesignMode(this) == false)
      {
        pixelShader.UriSource = new Uri("pack://application:,,,/ES.Tools;component/Effects/Grayscale.ps", UriKind.Absolute);
      }

      return pixelShader;
    }
  }
}
