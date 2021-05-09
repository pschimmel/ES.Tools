using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ES.Tools.UI;

namespace ES.Tools.Controls
{
  public class Accordion : ItemsControl
  {
    #region Constructors

    static Accordion()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(Accordion), new FrameworkPropertyMetadata(typeof(Accordion)));
    }

    public Accordion()
    {
      Focusable = false;
    }

    #endregion

    #region Dependency Properties

    #region Orientation

    public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(Accordion), new FrameworkPropertyMetadata(Orientation.Vertical, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public Orientation Orientation
    {
      get => (Orientation)GetValue(OrientationProperty);
      set => SetValue(OrientationProperty, value);
    }

    #endregion

    #region Selection Mode

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

    public AccordionSelectionMode SelectionMode
    {
      get => (AccordionSelectionMode)GetValue(SelectionModeProperty);
      set => SetValue(SelectionModeProperty, value);
    }

    #endregion

    #region Is Animated

    /// <summary>
    /// Animate the <see cref="AccordionItem"/>s.
    /// </summary>
    public static readonly DependencyProperty IsAnimatedProperty = DependencyProperty.Register(nameof(IsAnimated), typeof(bool), typeof(Accordion), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsMeasure));

    public bool IsAnimated
    {
      get => (bool)GetValue(IsAnimatedProperty);
      set => SetValue(IsAnimatedProperty, value);
    }

    #endregion

    #endregion


    #region Internal Methods

    internal void DeselectOthers(AccordionItem selectedItem)
    {
      if (SelectionMode == AccordionSelectionMode.Single)
      {
        foreach (object item in Items)
        {
          if (item is not AccordionItem accordionItem)
          {
            // If the accordionItem is not a direct child, we try to find it using the visual tree.
            var cp = ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
            accordionItem = cp.GetVisualChild<AccordionItem>();
          }

          if (accordionItem != null && accordionItem != selectedItem)
          {
            accordionItem.IsSelected = false;
          }

        }
      }
    }

    #endregion

    public enum AccordionSelectionMode { Single, Multiple }
  }
}
