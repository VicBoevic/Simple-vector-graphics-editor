using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Vector_graphics_editor
{
    public class Primitive : IGeomPrimitive
    {
        public MyMatrix TransformationMatrix { get; set; }
        public List<PointF> VertexList { get; set; }
        public Brush mainColorBrush { get; set ; }
        public PointF Origin { get; set; }

        public Primitive()
        {
            TransformationMatrix = new MyMatrix();
            VertexList = new List<PointF>();
            mainColorBrush = new SolidBrush(Color.Black);
            Origin = new PointF();
        }

        public Primitive(List<PointF> points, Color polygonColor)
        {
            TransformationMatrix = new MyMatrix();
            VertexList = new List<PointF>(points);
            mainColorBrush = new SolidBrush(polygonColor);
            CalculateCenter();
        }

        public virtual void Draw(Graphics g, Pen drawPen)
        {
            drawPen = new Pen(mainColorBrush); //Saving color
            LineByLineMethod(g, drawPen, VertexList); //Fill figure 
        }

        public virtual void DrawContour(Graphics g, Pen countourPen)
        {
            for (int i = 0; i < VertexList.Count - 1; i++) //Cycle in all points execpt last
                g.DrawLine(countourPen, VertexList[i], VertexList[i + 1]); //Drawing line between two neighboor dots
            g.DrawLine(countourPen, VertexList[0], VertexList.LastOrDefault<PointF>()); //Drawing line between first and last point
        }

        public virtual bool PointInsidePrimitive(PointF point)
        {
            int intersections = 0;
            //Ray casting algorithm for determining if a point is inside a polygon
            //Two conditions at each iteration: whether the ray intersects the edge and whether the point is to the left of the edge
            for (int i = 0, j = VertexList.Count - 1; i < VertexList.Count; j = i++)
                if ((VertexList[i].Y > point.Y) != (VertexList[j].Y > point.Y) &&
                    (point.X < (VertexList[j].X - VertexList[i].X) * (point.Y - VertexList[i].Y) / (VertexList[j].Y - VertexList[i].Y) + VertexList[i].X))
                    intersections++;

            return intersections % 2 == 1; //If intersections amaount is odd - point is outside of primitive
        }

        public void LineByLineMethod(Graphics g, Pen drawPen, List<PointF> vertexList) //Filling polygon method
        {
            //Borders Ymin and Ymax
            float Ymin = vertexList[0].Y;
            float Ymax = vertexList[0].Y;
            foreach (PointF p in vertexList)
            {
                Ymin = Math.Min(Ymin, p.Y);
                Ymax = Math.Max(Ymax, p.Y);
            }

            //Loop through the rows Y
            for (float Y = Ymin; Y <= Ymax; Y++)
            {

                //Clearing list of intersections Xb
                List<float> Xb = GetIntersection(VertexList, Y);

                // Filling segments of row Y
                for (int j = 0; j < Xb.Count; j += 2)
                {
                    // Check for a pair of points
                    if (j + 1 < Xb.Count)
                    {
                        // Drawing segment
                        g.DrawLine(drawPen, Xb[j], Y, Xb[j + 1], Y);
                    }
                }
            }
        }

        //Finding the x coordinates of the intersections between the figure and the row
        private List<float> GetIntersection(List<PointF> polygon, float y)
        {
            List<float> intersections = new List<float>();

            for (int i = 0; i < polygon.Count; i++) //Loop through the points
            {
                PointF currentPoint = polygon[i]; //Current point
                PointF nextPoint = polygon[(i + 1) % polygon.Count]; //Next point

                //Check for a change in the y coordinate between the current and next points
                if ((currentPoint.Y <= y && nextPoint.Y > y) || (currentPoint.Y > y && nextPoint.Y <= y))
                {
                    //Calculate the x coordinate of the intersection border
                    float intersectionX = (currentPoint.X + (y - currentPoint.Y) / (nextPoint.Y - currentPoint.Y) * (nextPoint.X - currentPoint.X));
                    intersections.Add(intersectionX);
                }
            }
            //Sorting the intersection borders
            intersections.Sort();

            //Borders of the polygon's cross-section by the line
            return intersections;
        }

        public void Move(int dx, int dy) 
        {
            TransformationMatrix.Reset(); //Reset matrix to standart
            TransformationMatrix.Translate(dx, dy); //Move by dx, dy
            TransformationMatrix.TransformPoints(VertexList); //Changing points coordinates 
            CalculateCenter();
        }

        public void Scale(float scaleFactor)
        {
            TransformationMatrix.Reset(); //Reset matrix to standart
            TransformationMatrix.Translate(-Origin.X, -Origin.Y); //Move to primitive center
            TransformationMatrix.Scale(scaleFactor, scaleFactor);
            TransformationMatrix.Translate(Origin.X, Origin.Y); //Move backwards
            TransformationMatrix.TransformPoints(VertexList); //Changing points coordinates 
            CalculateCenter();
        }

        public void Rotate(float angle)
        {
            TransformationMatrix.Reset(); //Reset matrix to standart
            TransformationMatrix.Translate(-Origin.X, -Origin.Y); //Move to primitive center
            TransformationMatrix.Rotate(angle);
            TransformationMatrix.Translate(Origin.X, Origin.Y); //Move backwards
            TransformationMatrix.TransformPoints(VertexList); //Changing points coordinates 
            CalculateCenter();
        }

        public void MirrorVertical(float dx)
        {
           TransformationMatrix.Reset(); //Reset matrix to standart
           TransformationMatrix.Translate(-dx, 0); //Move to mirror line
           TransformationMatrix.ReflectYAxis();
           TransformationMatrix.Translate(dx, 0); //Move backwards
           TransformationMatrix.TransformPoints(VertexList); //Changing points coordinates 
            CalculateCenter(); //Changing points coordinates 
        }

        public void RotateAroundPoint (float angle, PointF center)
        {
            TransformationMatrix.Reset(); //Reset matrix to standart
            TransformationMatrix.Translate(-center.X, -center.Y); //Move to rotate point
            TransformationMatrix.Rotate(angle);
            TransformationMatrix.Translate(center.X, center.Y); //Move backwards
            TransformationMatrix.TransformPoints(VertexList); //Changing points coordinates
            CalculateCenter(); //Changing points coordinates 
        }


        public void CalculateCenter()
        {
            float x = 0, y = 0;
            foreach (var point in VertexList)
            {
                x += point.X;
                y += point.Y;
            }
            Origin = new PointF(x / VertexList.Count, y / VertexList.Count);
        }
    }

}