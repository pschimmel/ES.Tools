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
  [TemplatePart(Name = MainGridTemplateName, Type = typeof(FrameworkElement))]
  public class Gauge : MeterBase
  {
    public enum IndicatorType { Default, Thin }

    #region Fields

    private const string MainGridTemplateName = "PART_MainGrid";
    private static readonly double _outerDistance = 20.0;
    private static readonly double _indicatorPinSize = 2.5;
    private static readonly double _indicatorAngle = 300;
    private bool _resetTicks = true;
    private Shape _indicatorControl;
    //private Ellipse _indicatorCover;
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
      get => GetValue(HeaderProperty);
      set => SetValue(HeaderProperty, value);
    }

    #endregion

    #region Content Property

    public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof(Content), typeof(object), typeof(Gauge), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Content shown in the lower center of the gauge.
    /// </summary>
    public object Content
    {
      get => GetValue(ContentProperty);
      set => SetValue(ContentProperty, value);
    }

    #endregion

    #region Indicator Property

    /// <summary>
    /// Type of the Indicator.
    /// </summary>
    public static readonly DependencyProperty IndicatorProperty = DependencyProperty.Register(nameof(Indicator), typeof(IndicatorType), typeof(Gauge), new FrameworkPropertyMetadata(IndicatorType.Default, FrameworkPropertyMetadataOptions.AffectsRender, IndicatorChanged));

    public IndicatorType Indicator
    {
      get => (IndicatorType)GetValue(IndicatorProperty);
      set => SetValue(IndicatorProperty, value);
    }

    private static void IndicatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var gauge = (Gauge)d;
      gauge.UpdateVisual();
    }

    #endregion

    #region Indicator Width Property

    /// <summary>
    /// Type of the Indicator.
    /// </summary>
    public static readonly DependencyProperty IndicatorWidthProperty = DependencyProperty.Register(nameof(IndicatorWidth), typeof(double), typeof(Gauge), new FrameworkPropertyMetadata(6.0, FrameworkPropertyMetadataOptions.AffectsRender, IndicatorWidthChanged, CoerceIndicatorWidth));

    public double IndicatorWidth
    {
      get => (double)GetValue(IndicatorWidthProperty);
      set => SetValue(IndicatorWidthProperty, value);
    }

    private static void IndicatorWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var gauge = (Gauge)d;
      gauge.UpdateVisual();
    }

    private static object CoerceIndicatorWidth(DependencyObject d, object baseValue)
    {
      double value = (double)baseValue;
      return Math.Min(20.0, Math.Max(3.0, value));
    }

    #endregion

    #region TotalTicks Property

    public static readonly DependencyProperty TotalTicksProperty = DependencyProperty.Register(nameof(TotalTicks), typeof(int), typeof(Gauge), new FrameworkPropertyMetadata(20, FrameworkPropertyMetadataOptions.AffectsRender, TotalTicksChanged, CoerceTotalTicks));

    /// <summary>
    /// Total amount of the tick marks.
    /// </summary>
    public int TotalTicks
    {
      get => (int)GetValue(TotalTicksProperty);
      set => SetValue(TotalTicksProperty, value);
    }

    private static void TotalTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var gauge = (Gauge)d;
      gauge._resetTicks = true;
      gauge.UpdateVisual();
    }

    private static object CoerceTotalTicks(DependencyObject d, object baseValue)
    {
      int value = (int)baseValue;
      return Math.Min(100, Math.Max(0, value));
    }

    #endregion

    #region SubTicks Property

    /// <summary>
    /// Sub ticks between each main tick
    /// </summary>
    public static readonly DependencyProperty SubTicksProperty = DependencyProperty.Register(nameof(SubTicks), typeof(int), typeof(Gauge), new FrameworkPropertyMetadata(4, FrameworkPropertyMetadataOptions.AffectsRender, SubTicksChanged, CoerceSubTicks));

    public int SubTicks
    {
      get => (int)GetValue(SubTicksProperty);
      set => SetValue(SubTicksProperty, value);
    }

    private static void SubTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var gauge = (Gauge)d;
      gauge._resetTicks = true;
      gauge.UpdateVisual();
    }

    private static object CoerceSubTicks(DependencyObject d, object baseValue)
    {
      int value = (int)baseValue;
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
          _indicatorControl = Indicator == IndicatorType.Default
            ? new Indicator { StrokeThickness = 0, StrokeLineJoin = PenLineJoin.Round }
            : new ThinIndicator { StrokeThickness = 0, StrokeLineJoin = PenLineJoin.Round };

          _indicatorControl.Fill = Foreground;
          indicatorCanvas.Children.Add(_indicatorControl);

          //_indicatorCover = new Ellipse { StrokeThickness = 0 };
          //_indicatorCover.Fill = Foreground;
          //indicatorCanvas.Children.Add(_indicatorCover);

          _indicatorPin = new Ellipse { StrokeThickness = 0 };
          _indicatorPin.Fill = Brushes.Black;
          indicatorCanvas.Children.Add(_indicatorPin);
        }

        if (_background != null)
        {
          double angle = GetAngle(Value);
          double maxAngle = _indicatorAngle - _indicatorAngle / 2;

          indicatorCanvas.RenderTransform = new RotateTransform(angle, indicatorCanvas.ActualWidth / 2, indicatorCanvas.ActualHeight / 2);

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

          //double indicatorCoverSize = IndicatorWidth + 2;

          if (_indicatorControl != null)
          {
            if (Indicator == IndicatorType.Default)
            {
              _indicatorControl.Width = IndicatorWidth;
              _indicatorControl.Height = Math.Min(_background.ActualHeight, _background.ActualWidth) / 2.0 + IndicatorWidth / 2.0 - _outerDistance;
              Canvas.SetLeft(_indicatorControl, (_background.ActualWidth - IndicatorWidth) / 2.0);
              Canvas.SetTop(_indicatorControl, _outerDistance + Math.Abs(_background.ActualHeight - _background.ActualWidth) / 2);
            }
            else
            {
              _indicatorControl.Width = IndicatorWidth;
              _indicatorControl.Height = (Math.Min(_background.ActualHeight, _background.ActualWidth) / 2.0 + IndicatorWidth / 2.0 - _outerDistance) * 4 / 3;
              Canvas.SetLeft(_indicatorControl, (_background.ActualWidth - IndicatorWidth) / 2.0);
              Canvas.SetTop(_indicatorControl, _outerDistance + Math.Abs(_background.ActualHeight - _background.ActualWidth) / 2.0 - IndicatorWidth / 2.0);
            }
          }

          //if (_indicatorCover != null)
          //{
          //  _indicatorCover.Width = indicatorCoverSize;
          //  _indicatorCover.Height = indicatorCoverSize;
          //  Canvas.SetLeft(_indicatorCover, (_background.ActualWidth - indicatorCoverSize) / 2.0);
          //  Canvas.SetTop(_indicatorCover, (_background.ActualHeight - indicatorCoverSize) / 2.0);
          //}

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
      double percent = max <= min ? 0.0 : (value - min) / (max - min);
      return percent * _indicatorAngle - _indicatorAngle / 2;
    }

    #endregion
  }
}
