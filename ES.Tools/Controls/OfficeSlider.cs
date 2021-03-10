using System.Windows;
using System.Windows.Controls;

namespace ES.Tools.Controls
{
  /// <summary>
  /// Slider that looks more like the zoom slider from office.
  /// </summary>
  public class OfficeSlider : Slider
  {
    #region Constructor

    static OfficeSlider()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(OfficeSlider), new FrameworkPropertyMetadata(typeof(OfficeSlider)));
    }

    #endregion

    #region Dependency Properties

    #region ShowValue

    public static readonly DependencyProperty ShowValueProperty = DependencyProperty.Register(nameof(ShowValue), typeof(bool), typeof(OfficeSlider), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

    public bool ShowValue
    {
      get { return (bool)GetValue(ShowValueProperty); }
      set { SetValue(ShowValueProperty, value); }
    }

    #endregion

    #region ValueStringFormat

    public static readonly DependencyProperty ValueStringFormatProperty = DependencyProperty.Register(nameof(ValueStringFormat), typeof(string), typeof(OfficeSlider), new FrameworkPropertyMetadata("P0", FrameworkPropertyMetadataOptions.AffectsMeasure));

    public string ValueStringFormat
    {
      get { return (string)GetValue(ValueStringFormatProperty); }
      set { SetValue(ValueStringFormatProperty, value); }
    }

    #endregion

    #endregion
  }
}