using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vector_graphics_editor
{
    class ComplexPrimitive : IGeomPrimitive
    {
        //List of figures within the complex one
        public List<IGeomPrimitive> GeomPrimitivesList { get; private set; } = new List<IGeomPrimitive>();
        //List of applied STO types
        public List<STOType> STOtypes { get; private set; } = new List<STOType>();
        public Brush mainColorBrush { get; set; }
        public PointF Origin { get; set; }
        public MyMatrix TransformationMatrix { get; set; }

        public ComplexPrimitive(IGeomPrimitive p1, IGeomPrimitive p2, STOType type)
        {
            GeomPrimitivesList.Add(p1);
            GeomPrimitivesList.Add(p2);
            STOtypes.Add(type);
            TransformationMatrix = new MyMatrix();
        }

        //Override the draw method
        public void Draw(Graphics g, Pen drawPen)
        {
            drawPen = new Pen(mainColorBrush);
            // Get all points from all primitives
            var points = GetAllPoints().ToList();
            // Find the maximum and minimum Y coordinates
            float maxY = points.Max(pt => pt.Y);
            float minY = points.Min(pt => pt.Y);

            // Iterate through each row from minY to maxY
            for (int currentY = (int)minY; currentY <= maxY; currentY++)
            {
                // Compute intersections for the current row
                var intersections = ComputeIntersection(currentY);

                // If there are intersections, draw segments
                if (intersections.Count > 0)
                    DrawSTOSegments(g, drawPen, intersections, currentY);
            }
        }

        private void DrawSTOSegments(Graphics g, Pen drawPen, List<int> borders, int y)
        {
            // Drawing segments based on the list of borders
            for (int j = 0; j < borders.Count - 1; j += 2)
            {
                // Get pairs of X coordinates for drawing a segment
                int x1 = borders[j], x2 = borders[j + 1];
                // Draw the segment
                g.DrawLine(drawPen, x1, y, x2, y);
            }
        }

        //Intersections for the complex figure
        public List<int> ComputeIntersection(int yCoordinate)
        {
            // Creating a list to store all intersections from all primitives
            var allIntersections = new List<List<int>>();

            // Iterate over all primitives in the figure
            foreach (var component in GeomPrimitivesList)
            {
                switch (component)
                {
                    case Primitive simplePrimitive:
                        allIntersections.Add(CalculateLineIntersections(simplePrimitive.VertexList, yCoordinate));
                        break;
                    case ComplexPrimitive complexPrimitive:
                        //Recursive call for a complex figure within a complex figure
                        allIntersections.Add(complexPrimitive.ComputeIntersection(yCoordinate));
                        break;
                }
            }

            // Take the first list of intersections as the base result
            var resultIntersections = allIntersections.First();
            //Loop through all applied STO types
            for (int i = 0; i < STOtypes.Count; i++)
            {
                // Get the parameters of the operation (valid values of threshold function)
                int[] operationParameters = GetOperationParameters(STOtypes[i]);
                // Apply the operation to the current result and the next list of intersections
                resultIntersections = ApplyOperation(operationParameters, resultIntersections, allIntersections[i + 1]);
            }

            return resultIntersections;
        }

        //Threshold function values depending on the operation
        private int[] GetOperationParameters(STOType type)
        {
            switch (type)
            {
                case STOType.Merge: return new int[] { 1, 2, 3 }; // Union
                case STOType.Intersection: return new int[] { 3 }; // Intersection
                case STOType.SymmericDifference: return new int[] { 1, 2 }; // Symmetric Difference
                case STOType.DifferenceAB: return new int[] { 2 }; // Difference A \ B
                case STOType.DifferenceBA: return new int[] { 1 }; // Difference B \ A
                default: return null;
            }
        }


        private List<int> CalculateLineIntersections(List<PointF> polygon, int y)
        {
            // List to store the X coordinates of intersection points
            List<int> intersections = new List<int>();

            // Iterate over all edges of the polygon
            for (int i = 0; i < polygon.Count; i++)
            {
                // Current and next points of the edge
                PointF currentPoint = polygon[i];
                PointF nextPoint = polygon[(i + 1) % polygon.Count];

                // Check if the edge intersects the horizontal line
                if ((currentPoint.Y <= y && nextPoint.Y > y) || (currentPoint.Y > y && nextPoint.Y <= y))
                {
                    // Calculate the X coordinate of the intersection point
                    int intersectionX = (int)(currentPoint.X + (double)(y - currentPoint.Y) / (nextPoint.Y - currentPoint.Y) * (nextPoint.X - currentPoint.X));
                    // Add the X coordinate to the list
                    intersections.Add(intersectionX);
                }
            }

            // Sort intersection points by X coordinate
            intersections.Sort();

            // Return the list of X coordinates of intersection points
            return intersections;
        }

        //Method for selecting points that fall into the STO region
        public List<int> ApplyOperation(int[] setQ, List<int> listA, List<int> listB)
        {
            //List of all points in the format storing x-coordinate and deltaQ value
            List<CustomPointData> combinedPoints = new List<CustomPointData>();

            //Loop through points of figure A
            for (int i = 0; i < listA.Count; i++)
            {
                //If point is even - deltaQ = 2, otherwise deltaQ = -2
                int deltaQ = i % 2 == 0 ? 2 : -2;
                combinedPoints.Add(new CustomPointData(listA[i], deltaQ));
            }

            //Loop through points of figure B
            for (int i = 0; i < listB.Count; i++)
            {
                //If point is even - deltaQ = 1, otherwise deltaQ = -1
                int deltaQ = i % 2 == 0 ? 1 : -1;
                combinedPoints.Add(new CustomPointData(listB[i], deltaQ));
            }

            //Sort all points by x
            combinedPoints.Sort((a, b) => a.X.CompareTo(b.X));

            //Initial Q value
            int Q = 0;
            //Final list of points
            var result = new List<int>();

            //Loop through all points
            foreach (var point in combinedPoints)
            {
                //Q value for the next point
                int newQ = Q + point.DeltaQ;
                //Comparison of allowed threshold function values and Q
                //If the Q of the current point is in setQ and the Q of the next is not, the point is written to the result
                //If the Q of the current point is not in setQ and the Q of the next one is, the point is written to the result
                if (setQ.Contains(newQ) != setQ.Contains(Q))
                    result.Add(point.X);

                //Update Q
                Q = newQ;
            }

            //Return the resulting list of points - borders of segments of the resulting STO area
            return result;
        }

    
        private List<PointF> GetAllPoints()
        {
            List<PointF> allPoints = new List<PointF>();

            foreach (var geomPrimitive in GeomPrimitivesList)
            {
                if (geomPrimitive is Primitive primitive)
                    allPoints.AddRange(primitive.VertexList);
                else if (geomPrimitive is ComplexPrimitive complex)
                    allPoints.AddRange(complex.GetAllPoints());
            }

            return allPoints;
        }


        public List<PointF> CalculateBoundingBoxPoints()
        {
            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            foreach (var primitive in GeomPrimitivesList)
            {
                if (primitive is Primitive simplePrimitive)
                {
                    minX = Math.Min(minX, simplePrimitive.VertexList.Min(p => p.X));
                    minY = Math.Min(minY, simplePrimitive.VertexList.Min(p => p.Y));
                    maxX = Math.Max(maxX, simplePrimitive.VertexList.Max(p => p.X));
                    maxY = Math.Max(maxY, simplePrimitive.VertexList.Max(p => p.Y));
                }
                else if (primitive is ComplexPrimitive complexPrimitive)
                {
                    List<PointF> complexBounds = complexPrimitive.CalculateBoundingBoxPoints(); // Recursive method 
                    minX = Math.Min(minX, complexBounds[0].X);
                    minY = Math.Min(minY, complexBounds[0].Y);
                    maxX = Math.Max(maxX, complexBounds[1].X);
                    maxY = Math.Max(maxY, complexBounds[2].Y);
                }
            }

            if (minX == float.MaxValue) return new List<PointF>(); // Empty figure

            return new List<PointF>()
        {
            new PointF(minX, minY),
            new PointF(maxX, minY),
            new PointF(maxX, maxY),
            new PointF(minX, maxY),
            new PointF(minX, minY) 
        };
        }


        public void DrawContour(Graphics graphics, Pen countourPen)
        {
            List<PointF> complexCountour = CalculateBoundingBoxPoints();
            for (int i = 0; i < complexCountour.Count - 1; i++)
                graphics.DrawLine(countourPen, complexCountour[i], complexCountour[i + 1]);
            graphics.DrawLine(countourPen, complexCountour[0], complexCountour.LastOrDefault<PointF>());
        }

        public void UpdateOrigin()
        {
            Origin = CalculateCenter();
        }
        
        public PointF CalculateCenter()
        {
            float totalX = 0;
            float totalY = 0;
            int pointCount = 0;

            foreach (var primitive in GeomPrimitivesList)
            {
                if (primitive is Primitive simplePrimitive)
                {
                    foreach (var point in simplePrimitive.VertexList)
                    {
                        totalX += point.X;
                        totalY += point.Y;
                        pointCount++;
                    }
                }
                else if (primitive is ComplexPrimitive complexPrimitive)
                {
                    PointF complexCenter = complexPrimitive.CalculateCenter(); // Recursive method
                    totalX += complexCenter.X;
                    totalY += complexCenter.Y;
                    pointCount++;
                }
            }

            if (pointCount == 0) return new PointF(0, 0); // Emtpty case

            return new PointF(totalX / pointCount, totalY / pointCount);
        }


        public void MirrorVertical(float dx)
        {
            foreach (var primitive in GeomPrimitivesList)
                primitive.MirrorVertical(dx);
            UpdateOrigin();
        }


        public void Move(int dx, int dy)
        {
            foreach (var primitive in GeomPrimitivesList)
                primitive.Move(dx, dy);
            UpdateOrigin();
        }


        public bool PointInsidePrimitive(PointF point)
        {
            foreach (var primitive in GeomPrimitivesList)
                if (primitive.PointInsidePrimitive(point) == true) return true;

            return false;
        }


        public void Rotate(float angle)
        {
            //Rotate center - center of complex figure
            foreach (var primitive in GeomPrimitivesList)
                primitive.RotateAroundPoint(angle, Origin);
            UpdateOrigin();
        }

        public void RotateAroundPoint(float angle, PointF center)
        {
            foreach (var primitive in GeomPrimitivesList)
                primitive.RotateAroundPoint(angle, center);
            UpdateOrigin();
        }


        public void Scale(float scaleFactor)
        {
            PointF thisOrigin = new PointF(Origin.X, Origin.Y);
            foreach (var primitive in GeomPrimitivesList)
            {
                if (primitive is ComplexPrimitive complexPrimitive)
                    complexPrimitive.Scale(scaleFactor);
                else if (primitive is Primitive simplePrimitive)
                {
                    simplePrimitive.TransformationMatrix.Reset();
                    simplePrimitive.TransformationMatrix.Translate(-thisOrigin.X, -thisOrigin.Y);
                    simplePrimitive.TransformationMatrix.Scale(scaleFactor, scaleFactor);
                    simplePrimitive.TransformationMatrix.Translate(thisOrigin.X, thisOrigin.Y);
                    simplePrimitive.TransformationMatrix.TransformPoints(simplePrimitive.VertexList);
                    simplePrimitive.CalculateCenter();
                }
            }
            TransformPoints(TransformationMatrix);
            UpdateOrigin();
        }

        public void MultiScale(float scaleFactor, PointF origin = default)
        {
            if (origin == default) origin = Origin; //Origin for 1 level 

            foreach (var primitive in GeomPrimitivesList)
            {
                if (primitive is ComplexPrimitive complexPrimitive)
                {
                    complexPrimitive.MultiScale(scaleFactor, origin); 
                }
                else if (primitive is Primitive simplePrimitive)
                {
                    simplePrimitive.TransformationMatrix.Reset();
                    simplePrimitive.TransformationMatrix.Translate(-origin.X, -origin.Y); 
                    simplePrimitive.TransformationMatrix.Scale(scaleFactor, scaleFactor);
                    simplePrimitive.TransformationMatrix.Translate(origin.X, origin.Y);
                    simplePrimitive.TransformationMatrix.TransformPoints(simplePrimitive.VertexList);
                    simplePrimitive.CalculateCenter();
                }
            }
            UpdateOrigin(); 
        }

        private void TransformPoints(MyMatrix external)
        {
            foreach (var primitive in GeomPrimitivesList)
            {
                if (primitive is ComplexPrimitive complexPrimitive) complexPrimitive.TransformPoints(TransformationMatrix);
                else if (primitive is Primitive simplePrimitive)
                    simplePrimitive.TransformationMatrix.TransformPoints(simplePrimitive.VertexList);
            }
        }


    }
}

