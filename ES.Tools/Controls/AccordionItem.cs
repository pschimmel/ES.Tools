using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ES.Tools.UI;

namespace ES.Tools.Controls
{
  [TemplatePart(Name = "PART_Header", Type = typeof(FrameworkElement))]
  [TemplatePart(Name = "PART_Content", Type = typeof(FrameworkElement))]
  public class AccordionItem : HeaderedContentControl
  {
    #region Fields

    private FrameworkElement _header;
    private FrameworkElement _content;
    private const double _animationDuration = 0.5;

    #endregion

    #region Constructors

    static AccordionItem()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(AccordionItem), new FrameworkPropertyMetadata(typeof(AccordionItem)));
      KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(AccordionItem), new FrameworkPropertyMetadata(KeyboardNavigationMode.Contained));
      KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(AccordionItem), new FrameworkPropertyMetadata(KeyboardNavigationMode.Local));
    }

    #endregion

    #region Routed Events

    #region Expanded

    public static readonly RoutedEvent ExpandedEvent = EventManager.RegisterRoutedEvent(nameof(Expanded), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AccordionItem));

    public event RoutedEventHandler Expanded
    {
      add { AddHandler(ExpandedEvent, value); }
      remove { RemoveHandler(ExpandedEvent, value); }
    }

    private void RaiseExpandedEvent()
    {
      RaiseEvent(new RoutedEventArgs(AccordionItem.ExpandedEvent));
    }

    #endregion

    #region Collapsed

    public static readonly RoutedEvent CollapsedEvent = EventManager.RegisterRoutedEvent(nameof(Collapsed), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AccordionItem));

    public event RoutedEventHandler Collapsed
    {
      add { AddHandler(CollapsedEvent, value); }
      remove { RemoveHandler(CollapsedEvent, value); }
    }

    private void RaiseCollapsedEvent()
    {
      RaiseEvent(new RoutedEventArgs(AccordionItem.CollapsedEvent));
    }

    #endregion

    #endregion

    #region Dependency Properties

    #region IsSelected

    /// <summary>
    /// Indicates whether this AccordionItem is selected.
    /// </summary>
    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(AccordionItem), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsMeasure, OnIsSelectedChanged));

    public bool IsSelected
    {
      get => (bool)GetValue(IsSelectedProperty);
      set => SetValue(IsSelectedProperty, value);
    }

    private static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var accordionItem = d as AccordionItem;
      bool isSelected = (bool)e.NewValue;

      if (isSelected)
      {
        accordionItem.AccordionItemParent?.DeselectOthers(accordionItem);
        accordionItem.RaiseExpandedEvent();

        if (accordionItem._content != null && accordionItem.AccordionItemParent?.IsAnimated == true)
        {
          double currentScale = (accordionItem._content.LayoutTransform as ScaleTransform)?.ScaleY ?? 0.0;
          accordionItem._content.LayoutTransform = new ScaleTransform(1, 0);
          var animation = new DoubleAnimation(1, TimeSpan.FromSeconds(_animationDuration * (1 - currentScale)));
          accordionItem._content.LayoutTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
          animation.Completed += (s, e) => accordionItem._content.Visibility = Visibility.Collapsed;
        }
      }
      else
      {
        accordionItem.RaiseCollapsedEvent();

        if (accordionItem._content != null && accordionItem.AccordionItemParent?.IsAnimated == true)
        {
          accordionItem._content.Visibility = Visibility.Visible;
          double currentScale = (accordionItem._content.LayoutTransform as ScaleTransform)?.ScaleY ?? 1.0;
          accordionItem._content.LayoutTransform = new ScaleTransform(1, currentScale);
          var animation = new DoubleAnimation(0, TimeSpan.FromSeconds(_animationDuration * currentScale));
          accordionItem._content.LayoutTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }
      }
    }

    #endregion

    #endregion

    #region Overwritten Protected Methods

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      _header = GetTemplateChild("PART_Header") as FrameworkElement;
      _header.PreviewMouseLeftButtonUp += Header_PreviewMouseLeftButtonUp;
      _content = GetTemplateChild("PART_Content") as FrameworkElement;
    }

    private void Header_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      IsSelected = !IsSelected;
    }

    /// <inheritdoc />
    protected override void OnKeyUp(KeyEventArgs e)
    {
      if ((e.Key == Key.Space || e.Key == Key.Enter) && ReferenceEquals(e.Source, this))
      {
        IsSelected = !IsSelected;
      }
      else
      {
        base.OnKeyUp(e);
      }
    }

    #endregion

    #region Protected Members

    protected Accordion AccordionItemParent
    {
      get
      {
        var parent = ItemsControl.ItemsControlFromItemContainer(this);

        if (parent == null)
        {
          // If it is not a direct child, use the visual tree to get the parent.
          parent = this.GetParent<Accordion>();
        }

        return parent as Accordion;
      }
    }

    #endregion
  }
}