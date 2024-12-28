using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_graphics_editor
{
    class Arrow : Primitive
    {

        public Arrow(List<PointF> points, Color polygonColor) : base() 
        {
            mainColorBrush = new SolidBrush(polygonColor);
            CalculateArrowVertices(points);
            CalculateCenter();
        }

        //Calculating arrow's points
        private void CalculateArrowVertices(List<PointF> points)
        {
            float dx = (points[1].X - points[0].X) / 4;
            float dy = (points[1].Y - points[0].Y) /4;
            VertexList.Add(new PointF(points[0].X, points[0].Y + dy));  //1
            VertexList.Add(new PointF(VertexList[0].X, VertexList[0].Y + 2*dy));  //2
            VertexList.Add(new PointF(VertexList[1].X + 3*dx, VertexList[1].Y));  //3
            VertexList.Add(new PointF(VertexList[2].X, VertexList[2].Y + dy));  //4
            VertexList.Add(new PointF(VertexList[3].X + dx, VertexList[3].Y - 2*dy));  //5
            VertexList.Add(new PointF(VertexList[2].X, VertexList[1].Y - 3*dy));  //6
            VertexList.Add(new PointF(VertexList[5].X, VertexList[5].Y + dy));  //7
   
        }
    }
}
