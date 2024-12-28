using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Vector_graphics_editor
{
    class FreePolygon : Primitive
    {
        public FreePolygon() : base()
        {
        }

        public FreePolygon(Color polygonColor, List<PointF> points)
        {
            TransformationMatrix = new MyMatrix();
            VertexList = new List<PointF>();
            mainColorBrush = new SolidBrush(polygonColor);

            for (int i = 0; i < points.Count; i++)
                VertexList.Add(points[i]);
            CalculateCenter();
        }
    }
}
