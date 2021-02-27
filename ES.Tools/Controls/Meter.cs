using System.Windows;
using System.Windows.Controls;

namespace ES.Tools.Controls
{
  /// <summary>
  /// Horizontal or vertical meter control
  /// </summary>
  public class Meter : MeterBase
  {
    public enum MeterType { Column, Bar }

    #region Constructor

    static Meter()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Meter), new FrameworkPropertyMetadata(typeof(Meter)));
    }

    #endregion

    #region Dependency Properties

    #region Type

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(nameof(Type), typeof(MeterType), typeof(Meter), new FrameworkPropertyMetadata(
        MeterType.Column,
        FrameworkPropertyMetadataOptions.AffectsRender,
        new PropertyChangedCallback(OnTypeChanged)));

    private static void OnTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Meter meter = (Meter)d;
      meter.UpdateVisual();
    }

    public MeterType Type
    {
      get { return (MeterType)GetValue(TypeProperty); }
      set { SetValue(TypeProperty, value); }
    }

    #endregion

    #region Orientation

    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(Meter), new FrameworkPropertyMetadata(
        Orientation.Vertical,
        FrameworkPropertyMetadataOptions.AffectsMeasure,
        OnOrientationChanged));

    private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Meter meter = (Meter)d;
      meter.UpdateVisual();
    }

    public Orientation Orientation
    {
      get { return (Orientation)GetValue(OrientationProperty); }
      set { SetValue(OrientationProperty, value); }
    }

    #endregion

    #endregion

    #region Overwritten Methods

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
    }

    protected override void UpdateVisual()
    {
      SetProgressBarIndicatorLength();
    }

    #endregion

    #region Private Methods

    private void SetProgressBarIndicatorLength()
    {
      if (_indicator != null && _background != null)
      {
        _indicator.Width = GetLength(Value);
      }

      if (_error != null && _warning != null)
      {
        if (Type == MeterType.Column)
        {
          _warning.Visibility = Value >= WarningValue ? Visibility.Visible : Visibility.Collapsed;
          _error.Visibility = Value >= ErrorValue ? Visibility.Visible : Visibility.Collapsed;
        }
        else if (Type == MeterType.Bar)
        {
          _error.Width = _background.ActualWidth - GetLength(ErrorValue);
          _warning.Width = _background.ActualWidth - GetLength(WarningValue);
        }
      }
    }

    private double GetLength(double value)
    {
      double min = MinValue;
      double max = MaxValue;
      double percent = max <= min ? 0.0 : (value - min) / (max - min);
      return percent * _background.ActualWidth;
    }

    #endregion
  }
}
