using System;
using System.Threading;
using System.Windows;
using System.Windows.Shapes;
using ES.Tools.Adorners;
using ES.Tools.Shapes;
using NUnit.Framework;

namespace ES.Tools.UnitTests.Shapes
{
  /// <summary>
  /// Unit tests testing the <see cref="ControlAdorner"/>.
  /// </summary>
  public class ShapesTests
  {
    private Window _window;

    [SetUp]
    public void Setup()
    {
      _window = new Window();
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

    [Apartment(ApartmentState.STA)]
    [TestCase(typeof(DonutSegment))]
    [TestCase(typeof(Indicator))]
    [TestCase(typeof(PieSegment))]
    [TestCase(typeof(ThinIndicator))]
    public void ControlAdornerInitTest(Type type)
    {
      var shape = (Shape)Activator.CreateInstance(type);
      _window.Content = shape;
      _window.Show();

      shape.Width = 50;
      shape.Height = 50;
      shape.Stroke = System.Windows.Media.Brushes.Black;
      shape.StrokeThickness = 1;
      shape.Fill = System.Windows.Media.Brushes.Yellow;
      shape.UpdateLayout();
      Assert.IsNotNull(shape.RenderedGeometry);
      Assert.IsFalse(shape.RenderedGeometry.Bounds == Rect.Empty);
    }
  }
}
