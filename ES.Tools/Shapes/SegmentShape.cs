using System;
using System.Windows;
using System.Windows.Shapes;

namespace ES.Tools.Shapes
{
  public abstract class SegmentShape : Shape
  {
    #region Start Angle Property

    public static readonly DependencyProperty StartAngleProperty = DependencyProperty.Register(nameof(StartAngle), typeof(double), typeof(SegmentShape), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender, null, CoerceAngle));

    public double StartAngle
    {
      get { return (double)GetValue(StartAngleProperty); }
      set { SetValue(StartAngleProperty, value); }
    }

    #endregion

    #region End Angle Property

    public static readonly DependencyProperty EndAngleProperty = DependencyProperty.Register(nameof(EndAngle), typeof(double), typeof(SegmentShape), new FrameworkPropertyMetadata(90.0, FrameworkPropertyMetadataOptions.AffectsRender, null, CoerceAngle));

    public double EndAngle
    {
      get { return (double)GetValue(EndAngleProperty); }
      set { SetValue(EndAngleProperty, value); }
    }

    #endregion

    #region Private Members

    private static object CoerceAngle(DependencyObject d, object value)
    {
      double angle = (double)value;
      angle = Math.Min(angle, 359.9);
      angle = Math.Max(angle, -359.9);
      return angle;
    }

    #endregion
  }
}
