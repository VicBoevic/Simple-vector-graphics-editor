using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_graphics_editor
{
    class StraightLine : Primitive
    {
        public StraightLine(List<PointF> points, Color polygonColor) : base(points, polygonColor) { }

        public override void Draw(Graphics g, Pen drawPen)
        {
            drawPen.Brush = mainColorBrush;
            g.DrawLine(drawPen, VertexList[0], VertexList[1]);
        }
        
        public override void DrawContour(Graphics g, Pen countourPen)
        {
            g.FillEllipse(countourPen.Brush, VertexList[0].X - 4, VertexList[0].Y - 4, 8, 8);
            g.FillEllipse(countourPen.Brush, VertexList[1].X - 4, VertexList[1].Y - 4, 8, 8);
        }

        public override bool PointInsidePrimitive(PointF point)
        {
            float distance = DistancePointToLine(point, VertexList[0], VertexList[1]);
            return distance <= 10;
        }

       
        private float DistancePointToLine(PointF point, PointF a, PointF b)
        {
            float area = Math.Abs((b.X - a.X) * (point.Y - a.Y) - (point.X - a.X) * (b.Y - a.Y));
            float lineLength = (float)Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
            return area / lineLength;
        }
    }
}
