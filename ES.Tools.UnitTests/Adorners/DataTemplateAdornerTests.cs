using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using ES.Tools.Adorners;
using ES.Tools.Helpers;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Adorners
{
  public class DataTemplateAdornerTests
  {
    private Button _button;
    private DataTemplate _dataTemplate;
#pragma warning disable IDE0052 // Remove unread private members
    private AdornerDecorator _decorator;
#pragma warning restore IDE0052 // Remove unread private members

    [SetUp]
    public void Setup()
    {
      _button = new Button { Content = "Test" };
      _button.UpdateLayout();

      // Surround control with adorner layer
      _decorator = new AdornerDecorator { Child = _button };

      _dataTemplate = new DataTemplate();

      // Set up the grid
      var gridFactory = new FrameworkElementFactory(typeof(Grid));
      gridFactory.Name = "gridFactory";
      gridFactory.SetValue(FrameworkElement.NameProperty, "grid");

      //set up the card holder textblock
      var textBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
      textBlockFactory.Name = "textBlockFactory";
      textBlockFactory.SetValue(FrameworkElement.NameProperty, "textBlock");
      textBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Text"));
      gridFactory.AppendChild(textBlockFactory);

      _dataTemplate.VisualTree = gridFactory;
    }

    [Test, Apartment(ApartmentState.STA)]
    public void DataTemplateAdornerInitTest()
    {
      var vm = new TestVM { Text = "Blah" };
      using var adorner = new DataTemplateAdorner(vm, _dataTemplate, _button);

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
    }

    internal class TestVM
    {
      public string Text { get; set; }
    }
  }
}
