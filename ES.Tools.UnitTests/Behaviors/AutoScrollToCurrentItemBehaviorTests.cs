using System.Linq;
using System.Threading;
using System.Windows.Controls;
using ES.Tools.Behaviors;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Behaviors
{
  /// <summary>
  /// Unit tests testing the <see cref="AutoScrollToCurrentItemBehavior"/>
  /// </summary>
  public class AutoScrollToCurrentItemBehaviorTests
  {
    private ListBox _listBox;
    private ListView _listView;
    private DataGrid _dataGrid;
    private readonly string[] items = new string[] { "a", "b", "c", "d", "e", "f" };

    [SetUp]
    public void Setup()
    {
      _listBox = new ListBox { ItemsSource = items.ToList() };
      _listView = new ListView { ItemsSource = items.ToList() };
      _dataGrid = new DataGrid { ItemsSource = items.ToList() };
      AutoScrollToCurrentItemBehavior.SetAutoScroll(_listBox, true);
      AutoScrollToCurrentItemBehavior.SetAutoScroll(_listView, true);
      AutoScrollToCurrentItemBehavior.SetAutoScroll(_dataGrid, true);
    }

    /// <summary>
    /// Basic tests for <see cref="AutoScrollToCurrentItemBehavior"/>.
    /// </summary>
    [Test, Apartment(ApartmentState.STA)]
    public void AutoScrollToCurrentItemBehaviorBasicTest()
    {
      // Testing the scrolling is quite hard. So for now we only test if the attached propery is set correctly.
      Assert.IsTrue(AutoScrollToCurrentItemBehavior.GetAutoScroll(_listBox));
      Assert.IsTrue(AutoScrollToCurrentItemBehavior.GetAutoScroll(_listView));
      Assert.IsTrue(AutoScrollToCurrentItemBehavior.GetAutoScroll(_dataGrid));
    }
  }
}