using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_graphics_editor
{
    class PerfectPolygon : Primitive
    {
        public PerfectPolygon() : base()
        {
        }

        public PerfectPolygon(PointF center, Color polygonColor, int n)
        {
            TransformationMatrix = new MyMatrix();
            Origin = center;
            VertexList = new List<PointF>();
            mainColorBrush = new SolidBrush(polygonColor);

            for (int i = 0; i < n; i++)
                VertexList.Add(new PointF());
        }

        //Calculating point's coordinates based on circumcircle
        public void AdjustPerfectPolygon(float rX, float rY)
        {
            double angle = 2 * Math.PI / VertexList.Count;
            float radius = (float)Math.Sqrt(Math.Pow((rX - Origin.X), 2) + Math.Pow((rY - Origin.Y), 2));
            for (int i = 0; i < VertexList.Count; i++) 
                VertexList[i] = new PointF((float)(Origin.X + radius * Math.Cos(angle * i)), (float)(Origin.Y + radius * Math.Sin(angle * i)));
        }
    }
}
