using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vector_graphics_editor
{
    enum OperationType
    {
        Drawing,
        Selecting
    }

    enum PrimitiveType
    {
        StraightLine,
        Spline,
        PerfectPolygon,
        FreePolygon,
        Arrow
    }

    enum GeomOperetaionType
    {
        Standart,
        MirrorVertical,
        RotateAroundPoint
    }

    enum STOType
    {
        Merge,
        Intersection,
        SymmericDifference,
        DifferenceAB,
        DifferenceBA
    }

    public struct CustomPointData
    {
        public int X { get; set; }
        public int DeltaQ { get; set; }

        public CustomPointData(int x, int deltaQ)
        {
            X = x;
            DeltaQ = deltaQ;
        }
    }
    public partial class Form1 : Form
    {
        // Bitmap for drawing
        Bitmap bitmap;
        // Object for drawing on the Bitmap
        Graphics graphics;
        // Context for buffered graphics
        BufferedGraphicsContext context;
        // Buffered graphics object for increased performance
        BufferedGraphics bufferedGraphics;
        Pen drawPen = new Pen(Color.Black, 3);
        Pen borderPen = new Pen(Color.Black, 3);
        Pen dotPen = new Pen(Color.Black, 1);
        private Cursor verticalLineCursor;
        OperationType operationType = OperationType.Drawing;
        PrimitiveType primitiveType = PrimitiveType.Spline;
        // Type of geometric operation
        GeomOperetaionType geomOperetaionType = GeomOperetaionType.Standart;
        // Type of boolean operation
        STOType sTOType = STOType.Merge;
        Point currentMousePosition;
        // List of points for drawing
        List<PointF> drawingPoints = new List<PointF>();
        Queue<IGeomPrimitive> selectedPrimitives = new Queue<IGeomPrimitive>();
        // Maximum number of selected primitives
        private int maxSelected = 2;
        List<IGeomPrimitive> allPrimitives = new List<IGeomPrimitive>();
        // Maximum screen width
        ushort maxWidth = (ushort)Screen.PrimaryScreen.Bounds.Width;
        // Maximum screen height
        ushort maxHeight = (ushort)Screen.PrimaryScreen.Bounds.Height;
        // Radius of a point
        const int DOT_RADIUS = 4;
        // Flag indicating dragging
        bool isDragging = false;
        // Flag indicating rotation
        bool isRotating = false;
        // Flag indicating line drawing
        bool drawLines = false;
        // Step for spline
        double dt = 0.01;
        int pointsAmount = 3;

        public Form1()
        {
            InitializeComponent();

            colorCombobox.Items.AddRange(new object[] {
                Color.Magenta,
                Color.DarkSlateBlue,
                Color.LimeGreen,
                Color.DarkOrange
            });
            // Setting the drawing mode for the combobox items
            colorCombobox.DrawMode = DrawMode.OwnerDrawFixed;
            // Adding an event handler to draw the combobox items
            colorCombobox.DrawItem += colorComboBox_DrawItem;
            // Setting the selected index in the method combobox
            methodCombobox.SelectedIndex = (int)operationType;
            // Setting the selected index in the primitive type combobox
            primitiveTypeComboBox.SelectedIndex = (int)primitiveType;
            // Setting the selected index in the geometric operation type combobox
            geomOperationTypeComboBox.SelectedIndex = (int)geomOperetaionType;
            // Setting the selected index in the boolean operation type combobox
            STOTypeСomboBox.SelectedIndex = (int)sTOType;
            // Setting the selected color index
            colorCombobox.SelectedIndex = 1;
            // Creating a Bitmap for the cursor
            Bitmap bmp = new Bitmap(16, 16); // Cursor size
            // Creating a Graphics object to draw on the Bitmap
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Drawing a vertical line in the middle
                g.DrawLine(Pens.Black, 8, 0, 8, 16); // Drawing a vertical line in the middle
            }
            // Creating a cursor from the Bitmap
            verticalLineCursor = new Cursor(bmp.GetHicon());
            // Adding an event handler for the mouse wheel
            mainPictureBox.MouseWheel += mainPictureBox_MouseWheel;
            // Adding an event handler for drawing
            mainPictureBox.Paint += new PaintEventHandler(mainPictureBox_Paint);

            InitializeGraphics();
        }

        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            bufferedGraphics.Render(e.Graphics);
        }

        private void InitializeGraphics() 
        {
            bitmap = new Bitmap(maxWidth, maxHeight); //Fullscrean bitmap
            mainPictureBox.Image = bitmap;
            graphics = mainPictureBox.CreateGraphics();
            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(bitmap.Width + 1, bitmap.Height + 1);
            bufferedGraphics = context.Allocate(graphics, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
            RedrawScene();
        }


        private void methodCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            operationType = (OperationType)methodCombobox.SelectedIndex;
            UpdateText();
        }


        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = drawPen.Color; 
            if (MyDialog.ShowDialog() == DialogResult.OK)
                drawPen.Color = MyDialog.Color;
            colorTextBox.BackColor = drawPen.Color;
            colorTextBox.Text = drawPen.Color.Name;
        }

        private void colorCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (colorCombobox.SelectedItem is Color selectedColor)
                drawPen = new Pen(selectedColor);
            colorTextBox.BackColor = drawPen.Color;
            colorTextBox.Text = drawPen.Color.Name;
        }

        private void drawLinesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            drawLines = drawLinesCheckBox.Checked;
            RedrawScene();
        }

        private void STOTypeСomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            sTOType = (STOType)STOTypeСomboBox.SelectedIndex;
            UpdateText();
        }

        private void geomOperationTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            geomOperetaionType = (GeomOperetaionType)geomOperationTypeComboBox.SelectedIndex;
            if (geomOperetaionType == GeomOperetaionType.RotateAroundPoint) mainPictureBox.Cursor = Cursors.Cross;
            else if (geomOperetaionType == GeomOperetaionType.MirrorVertical) mainPictureBox.Cursor = verticalLineCursor;
            else mainPictureBox.Cursor = Cursors.Default;
            UpdateText();
        }

        private void primitiveTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            primitiveType = (PrimitiveType)primitiveTypeComboBox.SelectedIndex;
            //Скрытие элементов настройки примитива в зависимости от выбранного режима
            if (primitiveType == PrimitiveType.PerfectPolygon)
            {
                dtLabel.Visible = false;
                dtValueLabel.Visible = false;
                dtTrackBar.Visible = false;
                dtTrackBar.Enabled = false;
                pointsAmountTextBox.Visible = true;
                pointsAmountTextBox.Enabled = true;
                pointAmountLabel.Visible = true;
            }
            else if (primitiveType == PrimitiveType.Spline)
            {
                dtLabel.Visible = true;
                dtValueLabel.Visible = true;
                dtTrackBar.Visible = true;
                dtTrackBar.Enabled = true;
                pointsAmountTextBox.Visible = false;
                pointsAmountTextBox.Enabled = false;
                pointAmountLabel.Visible = false;
            }
            else
            {
                dtLabel.Visible = false;
                dtValueLabel.Visible = false;
                dtTrackBar.Visible = false;
                dtTrackBar.Enabled = false;
                pointsAmountTextBox.Visible = false;
                pointsAmountTextBox.Enabled = false;
                pointAmountLabel.Visible = false;
            }
            UpdateText();
        }

        private void colorComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0)
            {
                var colorName = colorCombobox.GetItemText(colorCombobox.Items[e.Index]);
                var color = (Color)colorCombobox.Items[e.Index];

                // Circle paratmers calculation
                int diameter = Math.Min(e.Bounds.Height - 4, e.Bounds.Width - 4); //  -4 for offset
                int x = e.Bounds.Left + 2;
                int y = e.Bounds.Top  + 2 + (e.Bounds.Height - diameter - 4) / 2; // Vertical correction
                var r1 = new Rectangle(x, y, diameter, diameter);
                var r2 = Rectangle.FromLTRB(r1.Right + 2, e.Bounds.Top, e.Bounds.Right, e.Bounds.Bottom);

                using (var b = new SolidBrush(color))
                    e.Graphics.FillEllipse(b, r1); //Circle 
                e.Graphics.DrawEllipse(dotPen, r1); //Countour

                TextRenderer.DrawText(e.Graphics, colorName, colorCombobox.Font, r2,
                    colorCombobox.ForeColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
        }

        //Clearing drawing surface
        private void clearButton_Click(object sender, EventArgs e)
        {
            //Clear Queue
            selectedPrimitives.Clear();
            //Clear primitives list
            allPrimitives.Clear();
            //Clear points list
            drawingPoints.Clear();
            RedrawScene();
        }


        private void mainPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            currentMousePosition = e.Location;
            switch (operationType)
            {
                //Handler for drawing
                case OperationType.Drawing:
                    switch (primitiveType)
                    {
                        case PrimitiveType.StraightLine:
                            HandleStraightLine(e);
                            break;
                        case PrimitiveType.Spline:
                            HandleSplineMouseDown(e);
                            break;
                        case PrimitiveType.PerfectPolygon:
                            HandlePerfectPolygonMouseDown(e);
                            break;
                        case PrimitiveType.FreePolygon:
                            HandleFreePolygonMouseDown(e);
                            break;
                        case PrimitiveType.Arrow:
                            HandleArrowMouseDown(e);
                            break;
                    }
                    break;
                //Handler for selecting
                case OperationType.Selecting:
                    HandleSelectingMouseDown(e);
                    break;
            }
            mainPictureBox.Refresh();
        }

        private void HandleStraightLine(MouseEventArgs e)
        {
            bufferedGraphics.Graphics.FillEllipse(dotPen.Brush, e.X - DOT_RADIUS, e.Y - DOT_RADIUS, DOT_RADIUS * 2, DOT_RADIUS * 2);
            drawingPoints.Add(new PointF(e.X, e.Y));
            if (drawingPoints.Count == 2)
                AddPrimitive(new StraightLine(drawingPoints, drawPen.Color));
        }

        private void HandleSplineMouseDown(MouseEventArgs e)
        {
            drawingPoints.Add(new PointF(e.X, e.Y));
            bufferedGraphics.Graphics.FillEllipse(dotPen.Brush, e.X - DOT_RADIUS, e.Y - DOT_RADIUS, DOT_RADIUS * 2, DOT_RADIUS * 2);
            var pointsCount = drawingPoints.Count;
            switch (pointsCount)
            {

                case 2: // First vector
                    if (drawLines)
                        DrawVector(drawPen, drawingPoints[0], drawingPoints[1]);
                    break;
                case 4: // Second vector
                    if (drawLines)
                        DrawVector(drawPen, drawingPoints[2], drawingPoints[3]);

                    AddPrimitive(new CubeSpline(drawingPoints, drawPen.Color, dt));
                    break;
                default:
                    break;
            }
        }

        private void HandlePerfectPolygonMouseDown(MouseEventArgs e)
        {
            isDragging = true;
            AddPrimitive(new PerfectPolygon(new PointF(e.X, e.Y), drawPen.Color, pointsAmount));
        }

        private void HandleFreePolygonMouseDown (MouseEventArgs e)
        {
            drawingPoints.Add(new PointF(e.X, e.Y));
            bufferedGraphics.Graphics.FillEllipse(dotPen.Brush, e.X - DOT_RADIUS, e.Y - DOT_RADIUS, DOT_RADIUS * 2, DOT_RADIUS * 2);
            if (e.Button == MouseButtons.Right)
                AddPrimitive(new FreePolygon(drawPen.Color, drawingPoints));
        }

        private void HandleArrowMouseDown(MouseEventArgs e)
        {
            drawingPoints.Add(new PointF(e.X, e.Y));
            bufferedGraphics.Graphics.FillEllipse(dotPen.Brush, e.X - DOT_RADIUS, e.Y - DOT_RADIUS, DOT_RADIUS * 2, DOT_RADIUS * 2);
            if (drawingPoints.Count == 2)
                AddPrimitive(new Arrow(drawingPoints, drawPen.Color));
        }

        private void HandleSelectingMouseDown(MouseEventArgs e)
        {
            //Selecting primitive if point of click is "inside" primitive.
            var primitive = allPrimitives.LastOrDefault(x => x.PointInsidePrimitive(e.Location));
            if (primitive != null)
            {
                //Selected primitive goes to queue
                selectedPrimitives.Enqueue(primitive);
                //First primitive goes out of queue if there are to many
                if (selectedPrimitives.Count > maxSelected) selectedPrimitives.Dequeue();
                if (e.Button == MouseButtons.Left)
                    isDragging = true;
                else if (e.Button == MouseButtons.Right)
                    isRotating = true;
            }
            //Mirror primitive (depends on operation mode)
            if (selectedPrimitives != null && geomOperetaionType == GeomOperetaionType.MirrorVertical)
            {
                selectedPrimitives.Last().MirrorVertical(e.X);
                RedrawScene();
            }

            if (selectedPrimitives != null && geomOperetaionType == GeomOperetaionType.RotateAroundPoint && e.Button == MouseButtons.Right) isRotating = true;
        }

        private void AddPrimitive(Primitive primitive)
        {
            //Adding to the queue
            selectedPrimitives.Enqueue(primitive);
            //Checking for queue overflow
            if (selectedPrimitives.Count > maxSelected)
                selectedPrimitives.Dequeue();

            //Drawing the added figure
            selectedPrimitives.Last().Draw(bufferedGraphics.Graphics, drawPen);
            //Adding to the list of all figures
            allPrimitives.Add(selectedPrimitives.Last());
            //Clearing the list of drawing points
            drawingPoints.Clear();

            RedrawScene();
        }

        //Draw vector for spline
        private void DrawVector(Pen pen, PointF p1, PointF p2)
        {
            bufferedGraphics.Graphics.DrawLine(drawPen, p1, p2);
            float angle = GetArrowAngle(p1, p2);
            bufferedGraphics.Graphics.TranslateTransform(p2.X, p2.Y);
            bufferedGraphics.Graphics.RotateTransform(angle);
            bufferedGraphics.Graphics.RotateTransform(135);
            bufferedGraphics.Graphics.DrawLine(drawPen, new Point(0, 0), new Point(8, 0));
            bufferedGraphics.Graphics.RotateTransform(-270);
            bufferedGraphics.Graphics.DrawLine(drawPen, new Point(0, 0), new Point(8, 0));
            bufferedGraphics.Graphics.ResetTransform();
        }

        private float GetArrowAngle(PointF p1, PointF p2)
        {
            float deltaX = p2.X - p1.X;
            float deltaY = p2.Y - p1.Y;
            return (float)(Math.Atan2(deltaY, deltaX) * 180.0 / Math.PI);
        }
        private void dtTrackBar_ValueChanged(object sender, EventArgs e)
        {
            UpdateTrackBar();
        }

        private void UpdateTrackBar()
        {
            if (dtTrackBar.Value == 0)
                dt = 0.01;
            else dt = dtTrackBar.Value / 100.0;
            dtValueLabel.Text = dt.ToString();
        }

        private void mainPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (primitiveType == PrimitiveType.PerfectPolygon && operationType == OperationType.Drawing)
            {
                //Calculating points of n-gon based on its size
                if (isDragging && selectedPrimitives.Last() is PerfectPolygon perfectPerfectPolygon)
                {
                    perfectPerfectPolygon.AdjustPerfectPolygon(e.X, e.Y);
                    RedrawScene();
                }
                currentMousePosition = e.Location;
            }

            //Moving and rotating
            if (operationType == OperationType.Selecting && selectedPrimitives != null && geomOperetaionType == GeomOperetaionType.Standart)
            {
                if (isDragging)
                {
                    //Moving coordinates
                    int dx = e.X - currentMousePosition.X;
                    int dy = e.Y - currentMousePosition.Y;
                    selectedPrimitives.Last().Move(dx, dy);
                    RedrawScene();
                }
                else if (isRotating)
                {
                    int dx = e.X - currentMousePosition.X;
                    //Rotating angle
                    float angle = dx > 0 ? 1 : -1;
                    selectedPrimitives.Last().Rotate(angle);
                    RedrawScene();
                }
                currentMousePosition = e.Location;
            }


            //Rotating around poibt
            if (operationType == OperationType.Selecting && geomOperetaionType == GeomOperetaionType.RotateAroundPoint)
            {
                if (isRotating)
                {
                    Cursor.Position = this.PointToScreen(currentMousePosition);
                    int dx = e.X - currentMousePosition.X;
                    float angle = dx > 0 ? 1 : -1;
                    selectedPrimitives.Last().RotateAroundPoint(angle, currentMousePosition);
                    RedrawScene();
                }
            }

        }

        private void mainPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
                isDragging = false;
            else if (e.Button == MouseButtons.Right)
                isRotating = false;
        }

        private void mainPictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (operationType == OperationType.Selecting && selectedPrimitives != null)
            {
                float scaleFactor = e.Delta > 0 ? 1.1f : 0.9f;
                var selectedPrimitive = selectedPrimitives.Last();
                if (selectedPrimitive is Primitive simple) simple.Scale(scaleFactor);
                else if (selectedPrimitive is ComplexPrimitive complex) complex.MultiScale(scaleFactor);
                RedrawScene();
            }
        }


        private void delteButton_Click(object sender, EventArgs e)
        {
            if (selectedPrimitives.Count > 0)
            DeletePrimitive(selectedPrimitives.Last());
        }

        private void DeletePrimitive(IGeomPrimitive primitive)
        {
            if (selectedPrimitives != null && selectedPrimitives.Count > 0)
            {
                //Deleting first primitive in queue
                var primitiveToDelete = selectedPrimitives.FirstOrDefault(p => p == primitive);

                if (primitiveToDelete != null)
                {
                    //Getting index of deleted primitive
                    int index = allPrimitives.IndexOf(primitiveToDelete);
                    if (index != -1)
                    {
                        //Removing primitive from list
                        allPrimitives.RemoveAt(index);
                        //Clearing queue
                        selectedPrimitives.Clear();
                        RedrawScene();
                    }
                }
            }
        }

        private void RedrawScene()
        {
            //Clearing drawing surface
            bufferedGraphics.Graphics.Clear(mainPictureBox.BackColor);
            //Redrawing all primitives
            foreach (var primitive in allPrimitives)
                primitive.Draw(bufferedGraphics.Graphics, drawPen);

            // Drawing contour for selected primitives
            if (drawLines)
            {
                foreach (var primitive in selectedPrimitives)
                    primitive.DrawContour(bufferedGraphics.Graphics, borderPen);
            }
            mainPictureBox.Refresh();
        }


        private void pointsAmountTextBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(pointsAmountTextBox.Text, out int points))
            {
                int newPointsAmount = points;
                if (newPointsAmount < 3)
                    MessageBox.Show("Недопустимое число вершин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else pointsAmount = newPointsAmount;
            }
        }

        //Method for executing STO (Set-Theoretic Operation)
        private void execSTOButton_Click(object sender, EventArgs e)
        {
            if (selectedPrimitives.Count > 1)
            {
                //Creating a new complex figure from two figures in the queue
                var complexPrimitive = new ComplexPrimitive(selectedPrimitives.Last(), selectedPrimitives.First(), sTOType);
                //Removing figures from the queue
                var deleted1 = selectedPrimitives.Dequeue();
                var deleted2 = selectedPrimitives.Dequeue();
                //Calculating the center of the complex figure
                complexPrimitive.UpdateOrigin();
                //Saving the color of the complex figure
                complexPrimitive.mainColorBrush = drawPen.Brush;
                //Adding the complex figure to the queue
                selectedPrimitives.Enqueue(complexPrimitive);
                //Removing the component parts of the complex figure from the overall list
                allPrimitives.Remove(deleted1);
                allPrimitives.Remove(deleted2);
                //Adding the complex figure to the overall list
                allPrimitives.Add(complexPrimitive);
                RedrawScene();
            }
        }

        //Help text
        private void UpdateText()
        {
            //Depends on operation mode, type, primitive type
            if (operationType == OperationType.Drawing)
            {
                helpTypeLabel.Text = "Primitive:";
                helpMethodLabel.Text = "Drawing";
                switch (primitiveType)
                {
                    case PrimitiveType.StraightLine:
                        helpPrimitiveLabel.Text = "Straight Line";
                        helpMouseLabel.Text = "Defined by 2 points. " +
                                            "To create a point, simply click any mouse button on the drawing surface.";
                        break;
                    case PrimitiveType.Spline:
                        helpPrimitiveLabel.Text = "Cubic Spline";
                        helpMouseLabel.Text = "Defined by 4 points. " +
                                            "To create a point, simply click any mouse button on the drawing surface.";
                        break;
                    case PrimitiveType.PerfectPolygon:
                        helpPrimitiveLabel.Text = "Regular n-gon";
                        helpMouseLabel.Text = "Clicking the mouse button sets the center of the PerfectPolygon, and the number of vertices is entered manually in the corresponding field." +
                                            "When clicking the mouse button, it must be held down. The position of the cursor will define a point on the circle described around the PerfectPolygon. " +
                                            "This is how the size of the PerfectPolygon is defined.";
                        break;
                    case PrimitiveType.FreePolygon:
                        helpPrimitiveLabel.Text = "Free n-gon";
                        helpMouseLabel.Text = "Clicking the left mouse button adds a new point to polygon." +
                                            "Clicking the right mouse button adds last point to polygon. After that polygon is drawed and filled.";
                        break;
                    case PrimitiveType.Arrow:
                        helpPrimitiveLabel.Text = "Arrow";
                        helpMouseLabel.Text = "Defined by 2 points: the lower left and upper right corners of the rectangle described around the arrow." +
                                            "To create a point, simply click any mouse button on the drawing surface.";
                        break;
                }
            }
            if (operationType == OperationType.Selecting)
            {
                helpMethodLabel.Text = "Working with Shape";
                helpTypeLabel.Text = "Type of transformations:";
                switch (geomOperetaionType)
                {
                    case GeomOperetaionType.Standart:
                        helpPrimitiveLabel.Text = "Standard";
                        helpMouseLabel.Text = "Planar translation is performed by pressing and holding the left mouse button. " +
                                        "Rotation around the center of the figure is done by pressing and holding the right mouse button. " +
                                        "The angle is determined by moving the mouse left or right. " +
                                        "Scaling relative to the center of the figure is done by rotating the mouse wheel.";
                        break;
                    case GeomOperetaionType.RotateAroundPoint:
                        helpPrimitiveLabel.Text = "Rotation around a specified point";
                        helpMouseLabel.Text = "The right mouse button defines the point around which the rotation is performed. " +
                                        "The angle is determined by moving the mouse left or right.";
                        break;
                    case GeomOperetaionType.MirrorVertical:
                        helpPrimitiveLabel.Text = "Reflection about a vertical line";
                        helpMouseLabel.Text = "Clicking the mouse button defines the X coordinate of the vertical line about which the reflection is performed." +
                                        "The reflection is also performed at this moment.";
                        break;

                }
                    
            }

        }
    }
}