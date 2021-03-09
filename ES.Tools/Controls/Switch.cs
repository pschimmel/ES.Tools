using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ES.Tools.Controls
{
  [TemplatePart(Name = Switch.BackgroundTemplateName, Type = typeof(FrameworkElement))]
  [TemplatePart(Name = Switch.SwitchTemplateName, Type = typeof(FrameworkElement))]
  public class Switch : ToggleButton
  {
    #region Fields

    private const string BackgroundTemplateName = "PART_Background";
    private const string SwitchTemplateName = "PART_Switch";
    protected FrameworkElement _background;
    protected FrameworkElement _switch;

    #endregion

    #region Constructor

    static Switch()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Switch), new FrameworkPropertyMetadata(typeof(Switch)));
    }

    #endregion

    #region Dependency Properties

    #region Corner Radius

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(Switch), new FrameworkPropertyMetadata(new CornerRadius(5), FrameworkPropertyMetadataOptions.AffectsRender));

    public CornerRadius CornerRadius
    {
      get { return (CornerRadius)GetValue(CornerRadiusProperty); }
      set { SetValue(CornerRadiusProperty, value); }
    }

    #endregion

    #region On Content Property

    public static readonly DependencyProperty OnContentProperty = DependencyProperty.Register(nameof(OnContent), typeof(object), typeof(Switch), new FrameworkPropertyMetadata("On", FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Content that is shown on the switch when it is on
    /// </summary>
    public object OnContent
    {
      get { return (object)GetValue(OnContentProperty); }
      set { SetValue(OnContentProperty, value); }
    }

    #endregion

    #region Off Content Property

    public static readonly DependencyProperty OffContentProperty = DependencyProperty.Register(nameof(OffContent), typeof(object), typeof(Switch), new FrameworkPropertyMetadata("Off", FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Content that is shown on the switch when it is on
    /// </summary>
    public object OffContent
    {
      get { return (object)GetValue(OffContentProperty); }
      set { SetValue(OffContentProperty, value); }
    }

    #endregion

    #region Switch Width Property

    public static readonly DependencyProperty SwitchWidthProperty = DependencyProperty.Register(nameof(SwitchWidth), typeof(double), typeof(Switch), new FrameworkPropertyMetadata(20d, FrameworkPropertyMetadataOptions.AffectsRender, null, CoerceSwitchWidth));

    /// <summary>
    /// Width of the switch button.
    /// </summary>
    public double SwitchWidth
    {
      get { return (double)GetValue(SwitchWidthProperty); }
      set { SetValue(SwitchWidthProperty, value); }
    }

    private static object CoerceSwitchWidth(DependencyObject d, object baseValue)
    {
      var value = (double)baseValue;
      return Math.Min(Math.Max(value, 10), 50);
    }

    #endregion

    #region Switch Brush Property

    public static readonly DependencyProperty SwitchBrushProperty = DependencyProperty.Register(nameof(SwitchBrush), typeof(Brush), typeof(Switch), new FrameworkPropertyMetadata(Brushes.DarkGray, FrameworkPropertyMetadataOptions.AffectsRender));

    /// <summary>
    /// Brush of the switch button.
    /// </summary>
    public Brush SwitchBrush
    {
      get { return (Brush)GetValue(SwitchBrushProperty); }
      set { SetValue(SwitchBrushProperty, value); }
    }

    #endregion

    #endregion

    #region Overwritten Methods

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      _background = GetTemplateChild(BackgroundTemplateName) as FrameworkElement;
      _switch = GetTemplateChild(SwitchTemplateName) as FrameworkElement;
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
      base.OnPreviewKeyDown(e);

      if (!IsKeyboardFocusWithin)
        return;

      switch (e.Key)
      {
        case Key.Left:
          if (IsChecked == false)
            IsChecked = IsThreeState ? null : true;
          else if (IsChecked == null)
            IsChecked = true;
          e.Handled = true;
          break;
        case Key.Right:
          if (IsChecked == true)
            IsChecked = IsThreeState ? null : false;
          else if (IsChecked == null)
            IsChecked = false;
          e.Handled = true;
          break;
        case Key.Return:
        case Key.Space:
          if (IsChecked == true)
          {
            IsChecked = IsThreeState ? null : false;
          }
          else if (IsChecked == null)
          {
            IsChecked = false;
          }
          else if (IsChecked == false)
          {
            IsChecked = true;
          }
          e.Handled = true;
          break;
      }
    }

    #endregion
  }
}
