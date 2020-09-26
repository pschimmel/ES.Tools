using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using ES.Tools.Adorners;
using ES.Tools.UI;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Adorners
{
  /// <summary>
  /// Unit tests testing the <see cref="DataTemplateAdorner"/>.
  /// </summary>
  public class DataTemplateAdornerTests
  {
    private Button _button;
    private DataTemplate _dataTemplate;
    private Window _window;

    [SetUp]
    public void Setup()
    {
      _button = new Button { Content = "Test" };
      _window = new Window { Content = _button };
      _window.Show();

      // Set up the grid
      var gridFactory = new FrameworkElementFactory(typeof(Grid));
      gridFactory.Name = "gridFactory";
      gridFactory.SetValue(FrameworkElement.NameProperty, "grid");

      // Set up the textblock
      var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
      textBlockFactory.Name = "textBlockFactory";
      textBlockFactory.SetValue(FrameworkElement.NameProperty, "textBlock");
      textBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Text"));
      gridFactory.AppendChild(textBlockFactory);

      // Add everything to a DataTemplate
      _dataTemplate = new DataTemplate();
      _dataTemplate.VisualTree = gridFactory;
    }

    [TearDown]
    public void Cleanup()
    {
      if (_window != null)
      {
        _window.Close();
        _window = null;
      }

      _button = null;
      _dataTemplate = null;
    }

    [Test, Apartment(ApartmentState.STA)]
    public void DataTemplateAdornerInitTest()
    {
      var vm = new TestVM { Text = "Blah" };
      using var adorner = new DataTemplateAdorner(_button, vm, _dataTemplate);

      var adornerLayer= AdornerLayer.GetAdornerLayer(_button);
      var adornersOfButton = adornerLayer.GetAdorners(_button);

      // Check if adorner is there.
      Assert.IsNotNull(adornersOfButton);
      Assert.AreEqual(1, adornersOfButton.Length);
      Assert.AreSame(adorner, adornersOfButton[0]);
      Assert.AreSame(_button, adornersOfButton[0].AdornedElement);

      // Check content of adorner
      var presenter = adornersOfButton[0].GetVisualChild<ContentPresenter>();

      Assert.AreSame(_dataTemplate, presenter.ContentTemplate);
      Assert.AreSame(vm, presenter.Content);

      adorner.UpdateLayout();
      Assert.AreNotEqual(0, adorner.ActualWidth);
      Assert.AreNotEqual(0, adorner.ActualHeight);
    }

    internal class TestVM
    {
      public string Text { get; set; }
    }
  }
}
