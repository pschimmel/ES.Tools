﻿using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using ES.Tools.Adorners;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Adorners
{
  /// <summary>
  /// Unit tests testing the <see cref="ControlAdorner"/>.
  /// </summary>
  public class ControlAdornerTests
  {
    private Button _button;
    private Window _window;

    [SetUp]
    public void Setup()
    {
      _button = new Button { Content = "Test" };
      _window = new Window { Content = _button };
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

      _button = null;
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
      Assert.AreSame(_button, adornersOfButton[0].AdornedElement);

      adorner.UpdateLayout();
      Assert.AreNotEqual(0, adorner.ActualWidth);
      Assert.AreNotEqual(0, adorner.ActualHeight);
      Assert.AreEqual(checkBox.ActualWidth, adorner.ActualWidth);
      Assert.AreEqual(checkBox.ActualHeight, adorner.ActualHeight);
    }
  }
}
