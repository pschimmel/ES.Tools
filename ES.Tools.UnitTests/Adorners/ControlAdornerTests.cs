using System.Threading;
using System.Windows.Controls;
using System.Windows.Documents;
using ES.Tools.Adorners;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Adorners
{
  public class ControlAdornerTests
  {
    private Button _button;
    private AdornerDecorator _decorator;

    [SetUp]
    public void Setup()
    {
      _button = new Button { Content = "Test" };
      _button.UpdateLayout();

      // Surround control with adorner layer
      _decorator = new AdornerDecorator { Child = _button };
    }

    [Test, Apartment(ApartmentState.STA)]
    public void ControlAdornerInitTest()
    {
      var checkBox = new CheckBox();
      using var adorner = new ControlAdorner(_button, checkBox);

      // The child must be the same instance as the control provided in constructor.
      Assert.AreSame(checkBox, adorner.Child);

      var adornerLayer= AdornerLayer.GetAdornerLayer(_button);
      var adornersOfButton = adornerLayer.GetAdorners(_button);

      // Check if adorner is there.
      Assert.IsNotNull(adornersOfButton);
      Assert.AreEqual(1, adornersOfButton.Length);
      Assert.AreSame(adorner, adornersOfButton[0]);
    }
  }
}
