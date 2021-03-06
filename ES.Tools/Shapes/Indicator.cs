﻿using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ES.Tools.Shapes
{
  public class Indicator : Shape
  {
    protected override Geometry DefiningGeometry
    {
      get
      {
        StreamGeometry geometry = new StreamGeometry();
        using (StreamGeometryContext geometryContext = geometry.Open())
        {
          geometryContext.BeginFigure(new Point(RenderSize.Width / 2.0, 0), true, true);
          geometryContext.LineTo(new Point(0, RenderSize.Height), true, true);
          geometryContext.ArcTo(new Point(RenderSize.Width, RenderSize.Height), new Size(RenderSize.Width / 2.0, RenderSize.Width / 2.0), 0.0, false, SweepDirection.Counterclockwise, true, false);
        }

        return geometry;
      }
    }
  }
}
