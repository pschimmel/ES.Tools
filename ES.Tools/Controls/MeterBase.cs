﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ES.Tools.Controls
{
  /// <summary>
  /// Base class for meter controls
  /// </summary>
  [TemplatePart(Name = BackgroundTemplateName, Type = typeof(FrameworkElement))]
  [TemplatePart(Name = IndicatorTemplateName, Type = typeof(FrameworkElement))]
  [TemplatePart(Name = ErrorTemplateName, Type = typeof(FrameworkElement))]
  [TemplatePart(Name = WarningTemplateName, Type = typeof(FrameworkElement))]
  public abstract class MeterBase : Control
  {
    #region Fields

    private const string IndicatorTemplateName = "PART_Indicator";
    private const string BackgroundTemplateName = "PART_Background";
    private const string ErrorTemplateName = "PART_Error";
    private const string WarningTemplateName = "PART_Warning";
    protected FrameworkElement _background;
    protected FrameworkElement _indicator;
    protected FrameworkElement _warning;
    protected FrameworkElement _error;

    #endregion

    #region Dependency Properties

    #region Value

    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof(Value), typeof(double), typeof(MeterBase), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged, CoerceValue));

    public double Value
    {
      get => (double)GetValue(ValueProperty);
      set => SetValue(ValueProperty, value);
    }

    private static object CoerceValue(DependencyObject d, object baseValue)
    {
      return ValidateValue((MeterBase)d, (double)baseValue);
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var meter = (MeterBase)d;
      meter.UpdateVisual();
    }

    #endregion

    #region Min Value

    public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(nameof(MinValue), typeof(double), typeof(MeterBase), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender, OnMinValueChanged));

    public double MinValue
    {
      get => (double)GetValue(MinValueProperty);
      set => SetValue(MinValueProperty, value);
    }

    private static void OnMinValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var meter = (MeterBase)d;
      meter.Value = ValidateValue(meter, meter.Value);
      meter.UpdateVisual();
    }

    #endregion

    #region Max Value

    public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(nameof(MaxValue), typeof(double), typeof(MeterBase), new FrameworkPropertyMetadata(100d, FrameworkPropertyMetadataOptions.AffectsRender, OnMaxValueChanged));

    public double MaxValue
    {
      get => (double)GetValue(MaxValueProperty);
      set => SetValue(MaxValueProperty, value);
    }

    private static void OnMaxValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var meter = (MeterBase)d;
      meter.Value = ValidateValue(meter, meter.Value);
      meter.UpdateVisual();
    }

    #endregion

    #region Warning Value

    public static readonly DependencyProperty WarningValueProperty = DependencyProperty.Register(nameof(WarningValue), typeof(double), typeof(MeterBase), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender, OnWarningValueChanged));

    private static void OnWarningValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var meter = (MeterBase)d;
      meter.UpdateVisual();
    }

    public double WarningValue
    {
      get => (double)GetValue(WarningValueProperty);
      set => SetValue(WarningValueProperty, value);
    }

    #endregion

    #region Warning Brush

    public static readonly DependencyProperty WarningBrushProperty = DependencyProperty.Register(nameof(WarningBrush), typeof(Brush), typeof(MeterBase), new FrameworkPropertyMetadata(Brushes.Yellow, FrameworkPropertyMetadataOptions.AffectsRender));

    public Brush WarningBrush
    {
      get => (Brush)GetValue(WarningBrushProperty);
      set => SetValue(WarningBrushProperty, value);
    }

    #endregion

    #region Error Value

    public static readonly DependencyProperty ErrorValueProperty = DependencyProperty.Register(nameof(ErrorValue), typeof(double), typeof(MeterBase), new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.AffectsRender, OnErrorValueChanged));

    private static void OnErrorValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var meter = (MeterBase)d;
      meter.UpdateVisual();
    }

    public double ErrorValue
    {
      get => (double)GetValue(ErrorValueProperty);
      set => SetValue(ErrorValueProperty, value);
    }

    #endregion

    #region Error Brush

    public static readonly DependencyProperty ErrorBrushProperty = DependencyProperty.Register(nameof(ErrorBrush), typeof(Brush), typeof(MeterBase), new FrameworkPropertyMetadata(Brushes.Red, FrameworkPropertyMetadataOptions.AffectsRender));

    public Brush ErrorBrush
    {
      get => (Brush)GetValue(ErrorBrushProperty);
      set => SetValue(ErrorBrushProperty, value);
    }

    #endregion

    #endregion

    #region Overwritten Methods

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      if (_background != null)
      {
        _background.SizeChanged -= OnContentSizeChanged;
      }

      _background = GetTemplateChild(BackgroundTemplateName) as FrameworkElement;
      _indicator = GetTemplateChild(IndicatorTemplateName) as FrameworkElement;
      _warning = GetTemplateChild(WarningTemplateName) as FrameworkElement;
      _error = GetTemplateChild(ErrorTemplateName) as FrameworkElement;

      if (_background != null)
      {
        _background.SizeChanged += OnContentSizeChanged;
      }
    }

    #endregion

    #region Protected Methods

    protected abstract void UpdateVisual();

    #endregion

    #region Private Methods

    private void OnContentSizeChanged(object sender, SizeChangedEventArgs e)
    {
      UpdateVisual();
    }

    private static double ValidateValue(MeterBase meter, double value)
    {
      double current = Math.Max(meter.MinValue, value);
      current = Math.Min(meter.MaxValue, current);
      return current;
    }

    #endregion
  }
}
