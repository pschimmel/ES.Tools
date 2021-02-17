using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ES.Tools.UI;
using NUnit.Framework;

namespace ES.Tools.UnitTests.UI
{
  /// <summary>
  /// Unit tests testing the <see cref="TreeHelper"/> class.
  /// </summary>
  public class TreeHelperTests : TestBase
  {
    private UITestWindow _window;

    [Flags]
    private enum TestScope
    {
      None = 0,
      VisualTree = 1,
      LogicalTree = 2,
      All = 3
    }

    [SetUp]
    public void Setup()
    {
      _window = new UITestWindow();
      _window.Show();
    }

    [TearDown]
    public void Cleanup()
    {
      if (_window != null)
      {
        _window.Close();
        _window = null;
      }
    }

    [Test, Apartment(ApartmentState.STA)]
    public void TreeHelperGetParentTest()
    {
      FrameworkElement child = _window.yellowRectangle;
      TestGetParent<Canvas>(child, "canvas");
      var border2 = TestGetParent<Border>(child, "border2");
      TestGetParent<Grid>(child, "grid");
      var border0 = TestGetParent<Border>(border2, "border0");
      TestGetParent<Window>(border0, "");

      child = _window.blueBorder;
      TestGetParent<Canvas>(child, "canvas");
      TestGetParent<Border>(child, "border2");
      TestGetParent<Grid>(child, "grid");
    }

    [Test, Apartment(ApartmentState.STA)]
    public void TreeHelperFailingGetParentTest()
    {
      FrameworkElement child = _window.yellowRectangle;
      var parent = child.GetParent<Button>();
      Assert.IsNull(parent);
    }

    [Test, Apartment(ApartmentState.STA)]
    public void TreeHelperGetParentWithPredicateTest()
    {
      FrameworkElement child = _window.yellowRectangle;
      var parent = child.GetParent<Border>(x => x.Name == "border0");
      Assert.IsNotNull(parent);
    }

    [Test, Apartment(ApartmentState.STA)]
    public void TreeHelperFailingGetParentWithPredicateTest()
    {
      FrameworkElement child = _window.yellowRectangle;
      var parent = child.GetParent<Border>(x => x.Name == "borderX");
      Assert.IsNull(parent);
    }

    [Test, Apartment(ApartmentState.STA)]
    public void TreeHelperGetChildrenTest()
    {
      FrameworkElement parent = _window.grid;
      var child = parent.GetVisualChild<Border>();
      Assert.IsNotNull(child);
      Assert.AreEqual("border1", child.Name);

      var children = parent.GetVisualChildren<Border>();
      Assert.IsNotNull(children);
      Assert.Contains(_window.border1, children.ToArray());
      Assert.Contains(_window.border2, children.ToArray());

      parent = _window.canvas;
      var children2 = parent.GetVisualChildren<FrameworkElement>();
      Assert.IsNotNull(children2);
      Assert.Contains(_window.yellowRectangle, children2.ToArray());
      Assert.Contains(_window.blueBorder, children2.ToArray());

      parent = _window.lastBorder;
      var childButton = parent.GetVisualChild<Button>();
      Assert.NotNull(childButton);
      var childTextBlock = childButton.GetVisualChild<TextBlock>();
      Assert.NotNull(childTextBlock);
      Assert.AreEqual("Custom Button", childTextBlock.Text);
    }

    [Test, Apartment(ApartmentState.STA)]
    public void TreeHelperFailingGetChildrenTest()
    {
      FrameworkElement parent = _window.grid;
      var child = parent.GetVisualChild<ProgressBar>();
      Assert.IsNull(child);

      var children = parent.GetVisualChildren<ProgressBar>();
      Assert.IsNotNull(children);
      Assert.AreEqual(0, children.Count());
    }

    [Test, Apartment(ApartmentState.STA)]
    public void TreeHelperGetChildrenSpecialTest()
    {
      FrameworkElement parent = _window.itemsControl1;
      var children = parent.GetVisualChildren<Control>();
      Assert.IsNotNull(children);
      Assert.AreEqual(6, children.Count());

      foreach (var child in children)
      {
        Assert.IsInstanceOf(typeof(Button), child);
      }
    }

    [Test, Apartment(ApartmentState.STA)]
    public void TreeHelperGetWindowTest()
    {
      FrameworkElement control = _window.yellowRectangle;
      var window = control.GetWindow();
      Assert.AreSame(_window, window);
    }

    private static T TestGetParent<T>(DependencyObject dependencyObject, string name, TestScope scope = TestScope.All) where T : FrameworkElement
    {
      T parent = null;

      if (scope.HasFlag(TestScope.LogicalTree))
      {
        parent = dependencyObject.GetLogicalParent<T>();
        Assert.IsNotNull(parent);
        Assert.AreEqual(name, parent.Name);
      }
      if (scope.HasFlag(TestScope.VisualTree))
      {
        parent = dependencyObject.GetVisualParent<T>();
        Assert.IsNotNull(parent);
        Assert.AreEqual(name, parent.Name);
      }
      if ((scope & TestScope.All) != 0)
      {
        parent = dependencyObject.GetParent<T>();
        Assert.IsNotNull(parent);
        Assert.AreEqual(name, parent.Name);
      }

      return parent;
    }
  }
}