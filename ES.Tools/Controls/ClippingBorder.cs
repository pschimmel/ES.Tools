using System;
using System.Windows;
using System.Windows.Controls;

namespace ES.Tools.Controls
{
  /// <summary>
  /// Border that clips its content, e.g. when using round corners
  /// </summary>
  [TemplatePart(Name = ClippingBorder.BackgroundTemplateName, Type = typeof(FrameworkElement))]
  [TemplatePart(Name = ClippingBorder.InnerBorderTemplateName, Type = typeof(Border))]
  public class ClippingBorder : ContentControl
  {
    #region Fields

    private const string BackgroundTemplateName = "PART_Background";
    private const string InnerBorderTemplateName = "PART_InnerBorder";
    protected FrameworkElement _background;
    protected Border _innerBorder;

    #endregion

    #region Constructor

    static ClippingBorder()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(ClippingBorder), new FrameworkPropertyMetadata(typeof(ClippingBorder)));
    }

    #endregion

    #region Dependency Properties

    #region Corner Radius

    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ClippingBorder), new FrameworkPropertyMetadata(new CornerRadius(), FrameworkPropertyMetadataOptions.AffectsRender, CornerRadiusChanged));

    public CornerRadius CornerRadius
    {
      get { return (CornerRadius)GetValue(CornerRadiusProperty); }
      set { SetValue(CornerRadiusProperty, value); }
    }

    private static void CornerRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((ClippingBorder)d).AdaptInnerBorderCornerRadius();
    }

    #endregion

    #endregion

    #region Overwritten Methods

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      _background = GetTemplateChild(BackgroundTemplateName) as FrameworkElement;
      _innerBorder = GetTemplateChild(InnerBorderTemplateName) as Border;
      AdaptInnerBorderCornerRadius();
    }

    #endregion

    #region Private Methods

    private void AdaptInnerBorderCornerRadius()
    {
      if (_innerBorder != null)
      {
        double topLeft = Math.Max(CornerRadius.TopLeft - Math.Min(BorderThickness.Top, BorderThickness.Left) - Math.Min(Padding.Top, Padding.Left), 0);
        double topRight = Math.Max(CornerRadius.TopRight - Math.Min(BorderThickness.Top, BorderThickness.Right) - Math.Min(Padding.Top, Padding.Right), 0);
        double bottomLeft = Math.Max(CornerRadius.BottomLeft - Math.Min(BorderThickness.Bottom, BorderThickness.Left) - Math.Min(Padding.Bottom, Padding.Left), 0);
        double bottomRight = Math.Max(CornerRadius.BottomRight - Math.Min(BorderThickness.Bottom, BorderThickness.Right) - Math.Min(Padding.Bottom, Padding.Right), 0);
        _innerBorder.CornerRadius = new CornerRadius(topLeft, topRight, bottomRight, bottomLeft);
      }
    }

    #endregion
  }
}
