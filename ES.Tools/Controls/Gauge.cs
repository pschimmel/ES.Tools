using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using ES.Tools.Shapes;

namespace ES.Tools.Controls
{
  /// <summary>
  /// Horizontal or vertical meter control
  /// </summary>
  [ContentProperty("Content")]
  [TemplatePart(Name = Gauge.MainGridTemplateName, Type = typeof(FrameworkElement))]
  public class Gauge : MeterBase
  {
    #region Fields

    private const string MainGridTemplateName = "PART_MainGrid";
    private static double _outerDistance = 5.0;
    private static double _indicatorWidth = 5.0;
    private static double _indicatorCoverSize = _indicatorWidth + 2;
    private static double _indicatorPinSize = 2.5;
    private static double _indicatorAngle = 300;
    private bool _resetTicks = true;
    private Indicator _indicatorControl;
    private Ellipse _indicatorCover;
    private Ellipse _indicatorPin;
    private Grid _mainGrid;

    #endregion

    #region Constructor

    static Gauge()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Gauge), new FrameworkPropertyMetadata(typeof(Gauge)));
    }

    #endregion

    #region Header Property

    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(object), typeof(Gauge), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Header shown in the upper center of the gauge.
    /// </summary>
    public object Header
    {
      get { return (object)GetValue(HeaderProperty); }
      set { SetValue(HeaderProperty, value); }
    }

    #endregion

    #region Content Property

    public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(Object), typeof(Gauge), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Content shown in the lower center of the gauge.
    /// </summary>
    public Object Content
    {
      get { return (Object)GetValue(ContentProperty); }
      set { SetValue(ContentProperty, value); }
    }

    #endregion

    #region TotalTicks Property

    public static readonly DependencyProperty TotalTicksProperty = DependencyProperty.Register(nameof(TotalTicks), typeof(int), typeof(Gauge), new FrameworkPropertyMetadata(20, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, TotalTicksChanged, CoerceTotalTicks));

    /// <summary>
    /// Total amount of the tick marks.
    /// </summary>
    public int TotalTicks
    {
      get { return (int)GetValue(TotalTicksProperty); }
      set { SetValue(TotalTicksProperty, value); }
    }

    private static void TotalTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Gauge gauge = (Gauge)d;
      gauge._resetTicks = true;
      gauge.UpdateVisual();
    }

    private static object CoerceTotalTicks(DependencyObject d, object baseValue)
    {
      var value = (int)baseValue;
      return Math.Min(100, Math.Max(0, value));
    }

    #endregion

    #region SubTicks Property

    /// <summary>
    /// Sub ticks between each main tick
    /// </summary>
    public static readonly DependencyProperty SubTicksProperty = DependencyProperty.Register(nameof(SubTicks), typeof(int), typeof(Gauge), new FrameworkPropertyMetadata(4, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SubTicksChanged, CoerceSubTicks));

    public int SubTicks
    {
      get { return (int)GetValue(SubTicksProperty); }
      set { SetValue(SubTicksProperty, value); }
    }

    private static void SubTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Gauge gauge = (Gauge)d;
      gauge._resetTicks = true;
      gauge.UpdateVisual();
    }

    private static object CoerceSubTicks(DependencyObject d, object baseValue)
    {
      var value = (int)baseValue;
      return Math.Min(10, Math.Max(0, value));
    }

    #endregion

    #region Overwritten Methods

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      _mainGrid = GetTemplateChild(MainGridTemplateName) as Grid;
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
          double angle = GetAngle(Value);
          double maxAngle = _indicatorAngle - _indicatorAngle / 2;

          indicatorCanvas.RenderTransform = new RotateTransform(angle, indicatorCanvas.ActualWidth / 2, indicatorCanvas.ActualHeight / 2);
          double backgroundSize = Math.Min(_background.ActualWidth, _background.ActualHeight);

          AddTickMarks();

          if (_warning is DonutSegment warning)
          {
            warning.StartAngle = GetAngle(WarningValue);
            warning.EndAngle = maxAngle;
          }

          if (_error is DonutSegment error)
          {
            error.StartAngle = GetAngle(ErrorValue);
            error.EndAngle = maxAngle;
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

    private void AddTickMarks()
    {
      if (_resetTicks)
      {
        _mainGrid.Children.OfType<TickMark>().ToList().ForEach(x => _mainGrid.Children.Remove(x));
        _resetTicks = false;
      }

      if (_mainGrid != null && !_mainGrid.Children.OfType<TickMark>().Any() && TotalTicks > 0)
      {
        double minAngle = -_indicatorAngle / 2;
        double maxAngle = _indicatorAngle - _indicatorAngle / 2;

        int i = 0;
        for (double angle = minAngle; angle <= maxAngle; angle += (maxAngle - minAngle) / TotalTicks)
        {
          AddTickMark(angle, SubTicks == 0 || i % SubTicks == 0 ? 2 : 1);
          i++;
        }
      }
    }

    private void AddTickMark(double angle, double width)
    {
      var minTickMark = new TickMark
      {
        HorizontalAlignment = HorizontalAlignment.Stretch,
        VerticalAlignment = VerticalAlignment.Stretch,
        Stroke = Brushes.Black,
        StrokeEndLineCap = PenLineCap.Flat,
        StrokeStartLineCap = PenLineCap.Flat,
        StrokeThickness = width,
        Margin = new Thickness(5)
      };
      _mainGrid.Children.Add(minTickMark);
      minTickMark.RenderTransformOrigin = new Point(0.5, 0.5);
      minTickMark.RenderTransform = new RotateTransform(angle, 0.5, 0.5);
    }

    private double GetAngle(double value)
    {
      double min = MinValue;
      double max = MaxValue;
      var percent = max <= min ? 0.0 : (value - min) / (max - min);
      return percent * _indicatorAngle - _indicatorAngle / 2;
    }

    #endregion
  }
}
