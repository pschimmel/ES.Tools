using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ES.Tools.Controls
{
  [TemplatePart(Name = "PART_Header", Type = typeof(FrameworkElement))]
  [TemplatePart(Name = "PART_Content", Type = typeof(FrameworkElement))]
  public class AccordionItem : HeaderedContentControl
  {
    #region Fields

    //private bool _settingFocus = false;
    //private bool _setFocusOnContent = false;
    private FrameworkElement _headerHost;
    private FrameworkElement _contentHost;

    #endregion

    #region Constructors

    static AccordionItem()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(AccordionItem), new FrameworkPropertyMetadata(typeof(AccordionItem)));
      KeyboardNavigation.DirectionalNavigationProperty.OverrideMetadata(typeof(AccordionItem), new FrameworkPropertyMetadata(KeyboardNavigationMode.Contained));
      KeyboardNavigation.TabNavigationProperty.OverrideMetadata(typeof(AccordionItem), new FrameworkPropertyMetadata(KeyboardNavigationMode.Local));
    }

    #endregion

    #region Dependency Properties

    //#region Header Property

    //public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(nameof(Header), typeof(object), typeof(AccordionItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

    ///// <summary>
    ///// Content shown in the lower center of the gauge.
    ///// </summary>
    //public object Header
    //{
    //  get => GetValue(HeaderProperty);
    //  set => SetValue(HeaderProperty, value);
    //}

    //#endregion

    #region IsSelected Property

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
        accordionItem.AccordionItemParent.DeselectOthers(accordionItem);
      }

      //var parentTabControl = accordionItem.AccordionItemParent;
      //if (parentTabControl != null)
      //{
      //  //parentTabControl.RaiseIsSelectedChangedAutomationEvent(accordionItem, isSelected);
      //}

      //if (isSelected)
      //{
      //  accordionItem.OnSelected(new RoutedEventArgs(Selector.SelectedEvent, accordionItem));
      //}
      //else
      //{
      //  accordionItem.OnUnselected(new RoutedEventArgs(Selector.UnselectedEvent, accordionItem));
      //}

      //accordionItem.UpdateVisualState();
    }

    #endregion

    //private void UpdateVisualState()
    //{
    //  //throw new NotImplementedException();
    //}

    ///// <summary>
    /////     Event indicating that the IsSelected property is now true.
    ///// </summary>
    ///// <param name="e">Event arguments</param>
    //protected virtual void OnSelected(RoutedEventArgs e)
    //{
    //  HandleIsSelectedChanged(true, e);
    //}

    ///// <summary>
    /////     Event indicating that the IsSelected property is now false.
    ///// </summary>
    ///// <param name="e">Event arguments</param>
    //protected virtual void OnUnselected(RoutedEventArgs e)
    //{
    //  HandleIsSelectedChanged(false, e);
    //}

    //private void HandleIsSelectedChanged(bool newValue, RoutedEventArgs e)
    //{
    //  RaiseEvent(e);
    //}

    #endregion

    #region Overwritten Protected Methods

    /// <inheritdoc />
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      _headerHost = GetTemplateChild("PART_Header") as FrameworkElement;
      _contentHost = GetTemplateChild("PART_Content") as FrameworkElement;
    }

    /// <inheritdoc />
    protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
    {
      if (ReferenceEquals(e.Source, this))
      {
        IsSelected = !IsSelected;
      }
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

    ///// <summary>
    ///// Focus event handler
    ///// </summary>
    //protected override void OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
    //{
    //  base.OnPreviewGotKeyboardFocus(e);
    //  if (!e.Handled && e.NewFocus == this)
    //  {
    //    if (!IsSelected && AccordionItemParent != null)
    //    {
    //      IsSelected = true;

    //      // If focus moved in result of selection - handle the event to prevent setting focus back on the new item
    //      if (e.OldFocus != Keyboard.FocusedElement)
    //      {
    //        e.Handled = true;
    //      }
    //    }

    //    if (!e.Handled && _setFocusOnContent)
    //    {
    //      var parentTabControl = AccordionItemParent;
    //      if (parentTabControl != null)
    //      {
    //        // Save the parent and check for null to make sure that SetCurrentValue didn't have a change handler
    //        // that removed the AccordionItem from the tree.
    //        var selectedContentPresenter = parentTabControl.SelectedContentPresenter;
    //        if (selectedContentPresenter != null)
    //        {
    //          parentTabControl.UpdateLayout(); // Wait for layout
    //          bool success = selectedContentPresenter.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));

    //          // If we successfully move focus inside the content then don't set focus to the header
    //          if (success)
    //          {
    //            e.Handled = true;
    //          }
    //        }
    //      }
    //    }
    //  }
    //}

    //internal bool SetFocus()
    //{
    //  bool returnValue = false;

    //  if (!_settingFocus)
    //  {
    //    var currentFocus = Keyboard.FocusedElement as AccordionItem;

    //    // If current focus was another AccordionItem in the same TabControl - dont set focus on content
    //    bool setFocusOnContent = !IsKeyboardFocusWithin && (currentFocus == this || currentFocus == null || currentFocus.AccordionItemParent != AccordionItemParent);
    //    _settingFocus = true;
    //    _setFocusOnContent = setFocusOnContent;

    //    try
    //    {
    //      returnValue = Focus() || setFocusOnContent;
    //    }
    //    finally
    //    {
    //      _settingFocus = false;
    //      _setFocusOnContent = false;
    //    }
    //  }

    //  return returnValue;
    //}

    protected Accordion AccordionItemParent => ItemsControl.ItemsControlFromItemContainer(this) as Accordion;

    #endregion
  }
}