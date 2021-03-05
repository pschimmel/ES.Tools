using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ES.Tools.Controls
{
  public class TickMark : Shape
  {
    protected override Geometry DefiningGeometry
    {
      get
      {
        return new LineGeometry(new Point(RenderSize.Width / 2.0, 0), new Point(RenderSize.Width / 2.0, 10));
      }
    }
  }
}
