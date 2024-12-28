using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector_graphics_editor
{
    public class MyMatrix
    {
        private float[,] matrix;

        public MyMatrix()
        {
            Reset();
        }

        public void Reset() //Standard matrix
        {
            matrix = new float[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 }
            };
        }

        public void Translate(float dx, float dy)
        {
            var translationMatrix = new float[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { dx, dy, 1 }
            };
            Multiply(translationMatrix);
        }

        public void Scale(float sx, float sy)
        {
            var scaleMatrix = new float[,]
            {
                { sx, 0, 0 },
                { 0, sy, 0 },
                { 0, 0, 1 }
            };
            Multiply(scaleMatrix);
        }

        public void Rotate(float angle)
        {
            float radians = angle * (float)Math.PI / 180;
            var rotationMatrix = new float[,]
            {
                { (float)Math.Cos(radians), (float)Math.Sin(radians), 0 },
                { (float)-Math.Sin(radians), (float)Math.Cos(radians), 0 },
                { 0, 0, 1 }
            };
            Multiply(rotationMatrix);
        }

        public void ReflectYAxis()
        {
            var reflectionMatrix = new float[,]
            {
                { -1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 }
            };
            Multiply(reflectionMatrix);
        }

        public void TransformPoints(List<PointF> points)
        {
            for (int i = 0; i < points.Count; i++)
            {
                float x = points[i].X;
                float y = points[i].Y;

                points[i] = new PointF(
                    x * matrix[0, 0] + y * matrix[1, 0] + matrix[2, 0],
                    x * matrix[0, 1] + y * matrix[1, 1] + matrix[2, 1]
                );
            }
        }

        private void Multiply(float[,] other)
        {
            float[,] result = new float[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < 3; k++)
                    {
                        result[i, j] += matrix[i, k] * other[k, j];
                    }
                }
            }

            matrix = result;
        }
    }
}