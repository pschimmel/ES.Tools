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
    private TreeView _treeView;
    private readonly string[] items = new string[] { "a", "b", "c", "d", "e", "f" };

    [SetUp]
    public void Setup()
    {
      _listBox = new ListBox { ItemsSource = items.ToList() };
      _listView = new ListView { ItemsSource = items.ToList() };
      _dataGrid = new DataGrid { ItemsSource = items.ToList() };
      _treeView = new TreeView { ItemsSource = items.ToList().ConvertAll(x => new TreeViewItem { Header = x }) };
      AutoScrollToCurrentItemBehavior.SetAutoScrollToCurrentItem(_listBox, true);
      AutoScrollToCurrentItemBehavior.SetAutoScrollToCurrentItem(_listView, true);
      AutoScrollToCurrentItemBehavior.SetAutoScrollToCurrentItem(_dataGrid, true);
      foreach (var treeViewItem in _treeView.Items.OfType<TreeViewItem>())
      {
        AutoScrollToCurrentItemBehavior.SetAutoBringIntoView(treeViewItem, true);
      }
    }

    /// <summary>
    /// Basic tests for <see cref="AutoScrollToCurrentItemBehavior"/>.
    /// </summary>
    [Test, Apartment(ApartmentState.STA)]
    public void AutoScrollToCurrentItemBehaviorBasicTest()
    {
      // Testing the scrolling is quite hard. So for now we only test if the attached propery is set correctly.
      Assert.IsTrue(AutoScrollToCurrentItemBehavior.GetAutoScrollToCurrentItem(_listBox));
      Assert.IsTrue(AutoScrollToCurrentItemBehavior.GetAutoScrollToCurrentItem(_listView));
      Assert.IsTrue(AutoScrollToCurrentItemBehavior.GetAutoScrollToCurrentItem(_dataGrid));
      foreach (var treeViewItem in _treeView.Items.OfType<TreeViewItem>())
      {
        Assert.IsTrue(AutoScrollToCurrentItemBehavior.GetAutoBringIntoView(treeViewItem));
      }
    }
  }
}