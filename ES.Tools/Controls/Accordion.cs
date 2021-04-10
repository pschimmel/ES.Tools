using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ES.Tools.Controls
{
  [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(AccordionItem))]
  [TemplatePart(Name = "PART_SelectedContentHost", Type = typeof(ContentPresenter))]
  public class Accordion : ItemsControl
  {
    #region Fields

    // Part name used in the style. The class TemplatePartAttribute should use the same name
    private const string SelectedContentHostTemplateName = "PART_SelectedContentHost";

    #endregion

    #region Constructors

    static Accordion()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Accordion), new FrameworkPropertyMetadata(typeof(Accordion)));
    }

    #endregion

    #region Dependency Properties

    public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register(nameof(SelectionMode), typeof(AccordionSelectionMode), typeof(Accordion), new FrameworkPropertyMetadata(AccordionSelectionMode.Single, FrameworkPropertyMetadataOptions.AffectsMeasure, SelectionModeChanged));

    private static void SelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      var accordion = (Accordion)d;
      if ((AccordionSelectionMode)e.NewValue == AccordionSelectionMode.Single)
      {
        var accordionItem = accordion.Items.OfType<AccordionItem>().FirstOrDefault(x => x.IsSelected);
        // DeselectOthers can cope with null values
        accordion.DeselectOthers(accordionItem);
      }
    }

    internal void DeselectOthers(AccordionItem selectedItem)
    {
      foreach (var accordionItem in Items.OfType<AccordionItem>())
      {
        if (accordionItem != selectedItem)
        {
          accordionItem.IsSelected = false;
        }
      }
    }

    public AccordionSelectionMode SelectionMode
    {
      get => (AccordionSelectionMode)GetValue(SelectionModeProperty);
      set => SetValue(SelectionModeProperty, value);
    }

    /// <summary>
    /// ContentTemplate is the ContentTemplate to apply to <see cref="AccordionItem"/>s
    /// that do not have the ContentTemplate or ContentTemplateSelector properties
    /// defined
    /// </summary>
    public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register(nameof(ContentTemplate), typeof(DataTemplate), typeof(Accordion), new FrameworkPropertyMetadata((DataTemplate)null));

    public DataTemplate ContentTemplate
    {
      get => (DataTemplate)GetValue(ContentTemplateProperty);
      set => SetValue(ContentTemplateProperty, value);
    }

    /// <summary>
    /// ContentTemplateSelector allows the app writer to provide custom style selection logic.
    /// </summary>
    public static readonly DependencyProperty ContentTemplateSelectorProperty = DependencyProperty.Register(nameof(ContentTemplateSelector), typeof(DataTemplateSelector), typeof(Accordion), new FrameworkPropertyMetadata((DataTemplateSelector)null));

    public DataTemplateSelector ContentTemplateSelector
    {
      get => (DataTemplateSelector)GetValue(ContentTemplateSelectorProperty);
      set => SetValue(ContentTemplateSelectorProperty, value);
    }

    #endregion

    #region Overwritten Methods

    protected override void OnInitialized(EventArgs e)
    {
      base.OnInitialized(e);
      ItemContainerGenerator.StatusChanged += new EventHandler(OnGeneratorStatusChanged);
    }

    ///// <summary>
    ///// A virtual function that is called when the selection is changed. Default behavior
    ///// is to raise a SelectionChangedEvent
    ///// </summary>
    ///// <param name="e">The inputs for this event. Can be raised (default behavior) or processed
    /////   in some other way.</param>
    //protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    //{
    //  Change SelectedContent and focus before raising SelectionChanged.
    //  bool isKeyboardFocusWithin = IsKeyboardFocusWithin;

    //  UpdateSelectedContent();
    //  if (isKeyboardFocusWithin)
    //  {
    //    If keyboard focus is within the control, make sure it is going to the correct place
    //   var item = GetSelectedAccordionItem();
    //    if (item != null)
    //    {
    //      item.SetFocus();
    //    }
    //  }
    //  base.OnSelectionChanged(e);
    //}

    private void OnGeneratorStatusChanged(object sender, EventArgs e)
    {
      if (ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
      {
        //if (HasItems && _selectedItems.Count == 0)
        //{
        //  SelectedIndex = 0;
        //}

        //UpdateSelectedContent();
      }
    }

    private static readonly DependencyPropertyKey SelectedContentPropertyKey = DependencyProperty.RegisterReadOnly(nameof(SelectedContent), typeof(object), typeof(Accordion), new FrameworkPropertyMetadata((object)null));

    /// <summary>
    /// Content of current SelectedItem. This property is updated whenever the selection is changed.
    /// It always keeps a reference to active TabItem.Content Used for aliasing in default TabControl Style
    /// </summary>
    public static readonly DependencyProperty SelectedContentProperty = SelectedContentPropertyKey.DependencyProperty;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object SelectedContent
    {
      get => GetValue(SelectedContentProperty);
      internal set => SetValue(SelectedContentPropertyKey, value);
    }

    internal ContentPresenter SelectedContentPresenter => GetTemplateChild(SelectedContentHostTemplateName) as ContentPresenter;


    //// When selection is changed we need to copy the active AccordionItem content in SelectedContent property
    //// SelectedContent is aliased in the Accordion style
    //private void UpdateSelectedContent()
    //{
    //  if (SelectedIndex < 0)
    //  {
    //    SelectedContent = null;
    //    SelectedContentTemplate = null;
    //    SelectedContentTemplateSelector = null;
    //    return;
    //  }

    //  var accordionItem = GetSelectedAccordionItem();
    //  if (accordionItem != null)
    //  {
    //    if (VisualTreeHelper.GetParent(accordionItem) is FrameworkElement visualParent)
    //    {
    //      //KeyboardNavigation.SetTabOnceActiveElement(visualParent, accordionItem);
    //      //KeyboardNavigation.SetTabOnceActiveElement(this, visualParent);
    //    }

    //    SelectedContent = accordionItem.Content;
    //    var scp = SelectedContentPresenter;
    //    if (scp != null)
    //    {
    //      scp.HorizontalAlignment = accordionItem.HorizontalContentAlignment;
    //      scp.VerticalAlignment = accordionItem.VerticalContentAlignment;
    //    }

    //    // Use accordionItem's template or selector if specified, otherwise use Accordion's
    //    if (accordionItem.ContentTemplate != null || accordionItem.ContentTemplateSelector != null || accordionItem.ContentStringFormat != null)
    //    {
    //      SelectedContentTemplate = accordionItem.ContentTemplate;
    //      SelectedContentTemplateSelector = accordionItem.ContentTemplateSelector;
    //    }
    //    else
    //    {
    //      SelectedContentTemplate = ContentTemplate;
    //      SelectedContentTemplateSelector = ContentTemplateSelector;
    //    }
    //  }
    //}

    //private AccordionItem GetSelectedAccordionItem()
    //{
    //  object selectedItem = SelectedItem;
    //  if (selectedItem != null)
    //  {
    //    // Check if the selected item is a AccordionItem
    //    if (selectedItem is not AccordionItem tabItem)
    //    {
    //      // It is a data item, get its AccordionItem container
    //      tabItem = ItemContainerGenerator.ContainerFromIndex(SelectedIndex) as AccordionItem;

    //      if (tabItem == null || !Equals(selectedItem, ItemContainerGenerator.ItemFromContainer(tabItem)))
    //      {
    //        tabItem = ItemContainerGenerator.ContainerFromItem(selectedItem) as AccordionItem;
    //      }
    //    }

    //    return tabItem;
    //  }

    //  return null;
    //}

    private static readonly DependencyPropertyKey SelectedContentTemplatePropertyKey = DependencyProperty.RegisterReadOnly(nameof(SelectedContentTemplate), typeof(DataTemplate), typeof(Accordion), new FrameworkPropertyMetadata((DataTemplate)null));

    /// <summary>
    /// SelectedContentTemplate is the ContentTemplate of current SelectedItem. This property is updated whenever the selection is changed.
    /// It always keeps a reference to active AccordionItem.ContentTemplate. It is used for aliasing in default Accordion Style
    /// </summary>
    public static readonly DependencyProperty SelectedContentTemplateProperty = SelectedContentTemplatePropertyKey.DependencyProperty;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTemplate SelectedContentTemplate
    {
      get => (DataTemplate)GetValue(SelectedContentTemplateProperty);
      internal set => SetValue(SelectedContentTemplatePropertyKey, value);
    }

    private static readonly DependencyPropertyKey SelectedContentTemplateSelectorPropertyKey = DependencyProperty.RegisterReadOnly(nameof(SelectedContentTemplateSelector), typeof(DataTemplateSelector), typeof(Accordion), new FrameworkPropertyMetadata((DataTemplateSelector)null));

    /// <summary>
    /// SelectedContentTemplateSelector allows the app writer to provide custom style selection logic.
    /// </summary>
    public static readonly DependencyProperty SelectedContentTemplateSelectorProperty = SelectedContentTemplateSelectorPropertyKey.DependencyProperty;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTemplateSelector SelectedContentTemplateSelector
    {
      get => (DataTemplateSelector)GetValue(SelectedContentTemplateSelectorProperty);
      internal set => SetValue(SelectedContentTemplateSelectorPropertyKey, value);
    }

    /// <summary>
    /// Updates the current selection when Items has changed
    /// </summary>
    //protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
    //{
    //  base.OnItemsChanged(e);
    //  if (e.Action == NotifyCollectionChangedAction.Remove && SelectedIndex == -1)
    //  {
    //    // If we remove the selected item we should select the previous item
    //    int startIndex = e.OldStartingIndex + 1;
    //    if (startIndex > Items.Count)
    //    {
    //      startIndex = 0;
    //    }

    //    var nextTabItem = FindNextAccordionItem(startIndex, -1);
    //    if (nextTabItem != null)
    //    {
    //      nextTabItem.IsSelected = true;
    //    }
    //  }
    //}

    #endregion

    #region Private Methods

    private AccordionItem FindNextAccordionItem(int startIndex, int direction)
    {
      AccordionItem nextItem = null;
      if (direction != 0)
      {
        int index = startIndex;
        for (int i = 0; i < Items.Count; i++)
        {
          index += direction;
          if (index >= Items.Count)
          {
            index = 0;
          }
          else if (index < 0)
          {
            index = Items.Count - 1;
          }

          if (ItemContainerGenerator.ContainerFromIndex(index) is AccordionItem Item && Item.IsEnabled && Item.Visibility == Visibility.Visible)
          {
            nextItem = Item;
            break;
          }
        }
      }
      return nextItem;
    }

    #endregion

    public enum AccordionSelectionMode { Single, Multiple }
  }
}
