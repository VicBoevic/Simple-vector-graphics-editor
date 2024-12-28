using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Vector_graphics_editor
{
    public interface IGeomPrimitive
    {
        MyMatrix TransformationMatrix { get; set; }
        Brush mainColorBrush { get; set; }
        PointF Origin { get; set; } //Figure's center

        void Draw(Graphics g, Pen drawPen);
        bool PointInsidePrimitive(PointF point); //Cheking point (is point incide figure)
        void Move(int dx, int dy);
        void Scale(float scaleFactor);
        void Rotate(float angle);
        void MirrorVertical(float dx);
        void RotateAroundPoint(float angle, PointF center);
        void DrawContour(Graphics graphics, Pen borderPen);
    }
}