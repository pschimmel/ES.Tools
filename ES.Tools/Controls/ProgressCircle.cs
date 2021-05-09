using System.Windows;
using System.Windows.Controls;
using ES.Tools.Shapes;

namespace ES.Tools.Controls
{
  /// <summary>
  /// A circular progress bar.
  /// </summary>
  [TemplatePart(Name = BarTemplateName, Type = typeof(FrameworkElement))]
  public class ProgressCircle : ProgressBar
  {
    #region Fields

    private const string BarTemplateName = "PART_Bar";
    protected FrameworkElement _bar;

    #endregion

    #region Constructor

    static ProgressCircle()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressCircle), new FrameworkPropertyMetadata(typeof(ProgressCircle)));
    }

    #endregion

    #region Overwritten Methods

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      _bar = GetTemplateChild(BarTemplateName) as FrameworkElement;
    }

    protected override void OnValueChanged(double oldValue, double newValue)
    {
      base.OnValueChanged(oldValue, newValue);

      if (_bar is DonutSegment segment && !IsIndeterminate)
      {
        double min = Minimum;
        double max = Maximum;
        double percent = max <= min ? 0.0 : (newValue - min) / (max - min);
        segment.EndAngle = percent * 360;
      }
    }

    #endregion

    #region Dependency Properties

    #region Circle Width

    public static readonly DependencyProperty CircleWidthProperty = DependencyProperty.Register(nameof(CircleWidth), typeof(double), typeof(ProgressCircle), new FrameworkPropertyMetadata(10d, FrameworkPropertyMetadataOptions.AffectsRender));

    public double CircleWidth
    {
      get => (double)GetValue(CircleWidthProperty);
      set => SetValue(CircleWidthProperty, value);
    }

    #endregion

    #region Is Rotating

    public static readonly DependencyProperty IsRotatingProperty = DependencyProperty.Register(nameof(IsRotating), typeof(bool), typeof(ProgressCircle), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

    public bool IsRotating
    {
      get => (bool)GetValue(IsRotatingProperty);
      set => SetValue(IsRotatingProperty, value);
    }

    #endregion

    #endregion
  }
}