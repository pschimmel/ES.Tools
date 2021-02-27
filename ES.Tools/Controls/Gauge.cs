using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ES.Tools.Controls
{
  /// <summary>
  /// Horizontal or vertical meter control
  /// </summary>
  [ContentProperty("Content")]
  public class Gauge : MeterBase
  {
    #region Fields

    private static double _outerDistance = 5.0;
    private static double _indicatorWidth = 5.0;
    private static double _indicatorCoverSize = _indicatorWidth + 2;
    private static double _indicatorPinSize = 2.5;
    private static double _indicatorAngle = 300;
    private Indicator _indicatorControl;
    private Ellipse _indicatorCover;
    private Ellipse _indicatorPin;

    #endregion

    #region Constructor

    static Gauge()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Gauge), new FrameworkPropertyMetadata(typeof(Gauge)));
    }

    #endregion

    #region Content Property

    public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(Object), typeof(Gauge), new PropertyMetadata());

    public Object Content
    {
      get { return (Object)GetValue(ContentProperty); }
      set { SetValue(ContentProperty, value); }
    }

    #endregion Content Property

    #region Overwritten Methods

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
    }

    protected override void UpdateVisual()
    {
      SetIndicatorLocation();
    }

    #endregion

    #region Private Methods

    private void SetIndicatorLocation()
    {
      if (_indicator is Canvas indicatorCanvas)
      {
        if (_indicatorControl == null)
        {
          _indicatorControl = new Indicator { StrokeThickness = 0 };
          _indicatorControl.Fill = Foreground;
          indicatorCanvas.Children.Add(_indicatorControl);

          _indicatorCover = new Ellipse { StrokeThickness = 0 };
          _indicatorCover.Fill = Foreground;
          indicatorCanvas.Children.Add(_indicatorCover);

          _indicatorPin = new Ellipse { StrokeThickness = 0 };
          _indicatorPin.Fill = Brushes.Black;
          indicatorCanvas.Children.Add(_indicatorPin);
        }

        if (_background != null)
        {
          double val = Value;
          double min = MinValue;
          double max = MaxValue;
          double percent = max <= min ? 0.0 : (val - min) / (max - min);
          double angle = percent * _indicatorAngle - _indicatorAngle / 2;

          indicatorCanvas.RenderTransformOrigin = new Point(0.5, 0.5);
          indicatorCanvas.RenderTransform = new RotateTransform(angle);
          double backgroundSize = Math.Min(_background.ActualWidth, _background.ActualHeight);

          if (_warning is DonutSegment warning)
          {
            double warningPercent = max <= min ? 0.0 : (WarningValue - min) / (max - min);
            warning.StartAngle = warningPercent * _indicatorAngle - _indicatorAngle / 2;
            warning.EndAngle = _indicatorAngle - _indicatorAngle / 2;
          }

          if (_error is DonutSegment error)
          {
            double errorPercent = max <= min ? 0.0 : (ErrorValue - min) / (max - min);
            error.StartAngle = errorPercent * _indicatorAngle - _indicatorAngle / 2;
            error.EndAngle = _indicatorAngle - _indicatorAngle / 2;
          }

          if (_indicatorControl != null)
          {
            _indicatorControl.Width = _indicatorWidth;
            _indicatorControl.Height = Math.Min(_background.ActualHeight, _background.ActualWidth) / 2.0 - _outerDistance;
            Canvas.SetLeft(_indicatorControl, (_background.ActualWidth - _indicatorWidth) / 2.0);
            Canvas.SetTop(_indicatorControl, _outerDistance + Math.Abs(_background.ActualHeight - _background.ActualWidth) / 2);
          }

          if (_indicatorCover != null)
          {
            _indicatorCover.Width = _indicatorCoverSize;
            _indicatorCover.Height = _indicatorCoverSize;
            Canvas.SetLeft(_indicatorCover, (_background.ActualWidth - _indicatorCoverSize) / 2.0);
            Canvas.SetTop(_indicatorCover, (_background.ActualHeight - _indicatorCoverSize) / 2.0);
          }

          if (_indicatorPin != null)
          {
            _indicatorPin.Width = _indicatorPinSize;
            _indicatorPin.Height = _indicatorPinSize;
            Canvas.SetLeft(_indicatorPin, (_background.ActualWidth - _indicatorPinSize) / 2.0);
            Canvas.SetTop(_indicatorPin, (_background.ActualHeight - _indicatorPinSize) / 2.0);
          }
        }
      }
    }

    #endregion
  }
}
