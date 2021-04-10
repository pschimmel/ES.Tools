using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ES.Tools.Controls
{
  /// <summary>
  /// A standard progress bar that animates the values between the steps.
  /// </summary>
  public class AnimatedProgressBar
    : ProgressBar
  {
    /// <summary>
    /// AnimatedValue property
    /// </summary>
    public static readonly DependencyProperty AnimatedValueProperty = DependencyProperty.Register(nameof(AnimatedValue), typeof(double), typeof(AnimatedProgressBar), new FrameworkPropertyMetadata(0.0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnAnimatedValueChanged), new CoerceValueCallback(ConstrainToRange)), new ValidateValueCallback(IsValidDoubleValue));

    /// <summary>
    /// AnimatedValue
    /// </summary>
    [Bindable(true), Category("Behavior")]
    public double AnimatedValue
    {
      get => (double)GetValue(AnimatedValueProperty);
      set => SetValue(AnimatedValueProperty, value);
    }

    private static object ConstrainToRange(DependencyObject d, object value)
    {
      var progressBar = (AnimatedProgressBar)d;
      double min = progressBar.Minimum;
      double v = (double)value;
      if (v < min)
      {
        return min;
      }

      double max = progressBar.Maximum;
      return v > max ? max : value;
    }

    private static void OnAnimatedValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var progressBar = (AnimatedProgressBar)d;
      progressBar.OnAnimatedValueChanged((double)e.NewValue);
    }

    /// <summary>
    /// Validate input value in RangeBase (Minimum, Maximum, and Value).
    /// </summary>
    /// <returns>Returns False if value is NaN or NegativeInfinity or PositiveInfinity. Otherwise, returns True.</returns>
    private static bool IsValidDoubleValue(object value)
    {
      double d = (double)value;
      return !(double.IsNaN(d) || double.IsInfinity(d));
    }

    /// <summary>
    /// This method is invoked when the AnimatedValue property changes.
    /// </summary>
    private void OnAnimatedValueChanged(double newValue)
    {
      // Always animate starting from the current value.
      var doubleAnimation = new DoubleAnimation(Value, newValue, Duration, FillBehavior.HoldEnd);
      doubleAnimation.EasingFunction = EasingFunction;
      BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
    }

    /// <summary>
    /// Duration Property
    /// </summary>
    public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(nameof(Duration), typeof(TimeSpan), typeof(AnimatedProgressBar), new FrameworkPropertyMetadata(TimeSpan.FromSeconds(1),  FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    /// <summary>
    /// Duration of the animation
    /// </summary>
    public TimeSpan Duration
    {
      get => (TimeSpan)GetValue(DurationProperty);
      set => SetValue(DurationProperty, value);
    }

    /// <summary>
    /// EasingFunctionProperty
    /// </summary>
    public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(nameof(EasingFunction), typeof(IEasingFunction), typeof(AnimatedProgressBar), new FrameworkPropertyMetadata(null,  FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    /// <summary>
    /// EasingFunction
    /// </summary>
    public IEasingFunction EasingFunction
    {
      get => (IEasingFunction)GetValue(EasingFunctionProperty);
      set => SetValue(EasingFunctionProperty, value);
    }
  }
}