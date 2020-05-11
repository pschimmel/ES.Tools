using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using ES.Tools.Effects;

namespace ES.Tools.Controls
{
  /// <summary>
  /// Image control that is automatically grayed out when disabled.
  /// </summary>
  public class AutoGrayableImage : Image
  {
    private double _storedOpacity = 1.0;
    private static readonly Effect _grayscaleEffect = new GrayscaleEffect();

    /// <summary>
    /// Overwritten to handle changes of IsEnabled, Source and OpacityMask properties
    /// </summary>
    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
      base.OnPropertyChanged(e);

      if (e.Property.Name == nameof(IsEnabled))
      {
        if (IsEnabled)
        {
          Opacity = _storedOpacity;
          Effect = null;
        }
        else
        {
          _storedOpacity = Opacity;
          Opacity = 0.5;
          Effect = _grayscaleEffect;
        }
      }
    }
  }
}
