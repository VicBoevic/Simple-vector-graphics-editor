using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Efremov_Kurs
{
    public class Polygon
    {
        Matrix transformationMatrix;
        public PointF[] VertexList { get; private set; }

        Brush mainColorBrush = null;

        PointF origin = new PointF();

        public Polygon(PointF[] points, Color polygonColor)
        {
            transformationMatrix = new Matrix();
            VertexList = points;
            mainColorBrush = new SolidBrush(polygonColor);
            CalculateCenter();
        }

        public Polygon(PointF center, Color polygonColor, int n)
        {
            transformationMatrix = new Matrix();
            origin = center;
            VertexList = new PointF[n];
            mainColorBrush = new SolidBrush(polygonColor);
        }

        public void Fill(Graphics g, Pen borderPen, Pen dotPen, ushort dotRadius)
        {
            g.FillPolygon(mainColorBrush, VertexList);
           // DrawPolygon(g, borderPen);
           // DrawDots(g, dotPen, dotRadius);
        }

        public void DrawPerfectPolygon(Graphics g, float rX, float rY)
        {
            double angle = 2 * Math.PI / VertexList.Length;
            float radius = (float)Math.Sqrt(Math.Pow((rX - origin.X), 2) + Math.Pow((rY - origin.Y), 2));
            for (int i = 0; i < VertexList.Length; i++)
            {
                VertexList[i] = new Point((int)(origin.X + radius * Math.Cos(angle * i)), (int)(origin.Y + radius * Math.Sin(angle * i)));
            }
            g.FillPolygon(mainColorBrush, VertexList);
        }

        public void DrawPolygon(Graphics g, Pen borderPen)
        {
            g.DrawPolygon(borderPen, VertexList);

        }

        public void DrawDots(Graphics g, Pen dotPen, ushort dotRadius)
        {
            foreach (PointF point in VertexList)
                g.FillEllipse(dotPen.Brush, point.X - dotRadius, point.Y - dotRadius, dotRadius * 2, dotRadius * 2);
        }

        public bool PointInsidePolygon(PointF point)
        {
            int intersections = 0;
            for (int i = 0, j = VertexList.Length - 1; i < VertexList.Length; j = i++)
                if ((VertexList[i].Y > point.Y) != (VertexList[j].Y > point.Y) &&
                    (point.X < (VertexList[j].X - VertexList[i].X) * (point.Y - VertexList[i].Y) / (VertexList[j].Y - VertexList[i].Y) + VertexList[i].X))
                    intersections++;

            return intersections % 2 == 1;
        }

        public void Move(int dx, int dy)
        {
            transformationMatrix.Reset();
            transformationMatrix.Translate(dx, dy);
            transformationMatrix.TransformPoints(VertexList);
        }

        public void Scale(float scaleFactor)
        {
            transformationMatrix.Reset();
            transformationMatrix.Translate(origin.X, origin.Y);
            transformationMatrix.Scale(scaleFactor, scaleFactor);
            transformationMatrix.Translate(-origin.X, -origin.Y);
            transformationMatrix.TransformPoints(VertexList);
        }

        public void Rotate(float angle)
        {
            transformationMatrix.Reset();
            transformationMatrix.Translate(origin.X, origin.Y);
            transformationMatrix.Rotate(angle);
            transformationMatrix.Translate(-origin.X, -origin.Y);
            transformationMatrix.TransformPoints(VertexList);
        }

        public void CalculateCenter()
        {
            float x = 0, y = 0;
            foreach (var point in VertexList)
            {
                x += point.X;
                y += point.Y;
            }

            origin.X = x / VertexList.Length;
            origin.Y = y / VertexList.Length;
        }
    }

}