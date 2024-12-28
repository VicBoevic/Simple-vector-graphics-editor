using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Vector_graphics_editor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.methodCombobox = new System.Windows.Forms.ComboBox();
            this.colorCombobox = new System.Windows.Forms.ComboBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.leftPanel = new System.Windows.Forms.Panel();
            this.ClearingGroupBox = new System.Windows.Forms.GroupBox();
            this.clearLabel = new System.Windows.Forms.Label();
            this.deleteLabel = new System.Windows.Forms.Label();
            this.deleteButton = new System.Windows.Forms.Button();
            this.STOGroupBox = new System.Windows.Forms.GroupBox();
            this.STOTypeLabel = new System.Windows.Forms.Label();
            this.execSTOButton = new System.Windows.Forms.Button();
            this.STOTypeСomboBox = new System.Windows.Forms.ComboBox();
            this.geomOpGroupBox = new System.Windows.Forms.GroupBox();
            this.geomOpTypeLabel = new System.Windows.Forms.Label();
            this.methodLabel = new System.Windows.Forms.Label();
            this.geomOperationTypeComboBox = new System.Windows.Forms.ComboBox();
            this.primitiveGroupBox = new System.Windows.Forms.GroupBox();
            this.colorTextBox = new System.Windows.Forms.TextBox();
            this.colorButton = new System.Windows.Forms.Button();
            this.colorLabel = new System.Windows.Forms.Label();
            this.PrimitiveOptionsLabel = new System.Windows.Forms.Label();
            this.PrimitiveTypeLabel = new System.Windows.Forms.Label();
            this.drawLinesCheckBox = new System.Windows.Forms.CheckBox();
            this.primitiveTypeComboBox = new System.Windows.Forms.ComboBox();
            this.pointAmountLabel = new System.Windows.Forms.Label();
            this.pointsAmountTextBox = new System.Windows.Forms.TextBox();
            this.dtTrackBar = new System.Windows.Forms.TrackBar();
            this.dtLabel = new System.Windows.Forms.Label();
            this.dtValueLabel = new System.Windows.Forms.Label();
            this.lowPanel = new System.Windows.Forms.Panel();
            this.HelpGroupBox = new System.Windows.Forms.GroupBox();
            this.helpPrimitiveLabel = new System.Windows.Forms.Label();
            this.helpTypeLabel = new System.Windows.Forms.Label();
            this.helpMouseLabel = new System.Windows.Forms.Label();
            this.helpMethodLabel = new System.Windows.Forms.Label();
            this.modeLabel = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.leftPanel.SuspendLayout();
            this.ClearingGroupBox.SuspendLayout();
            this.STOGroupBox.SuspendLayout();
            this.geomOpGroupBox.SuspendLayout();
            this.primitiveGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTrackBar)).BeginInit();
            this.lowPanel.SuspendLayout();
            this.HelpGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(1398, 673);
            this.mainPictureBox.TabIndex = 0;
            this.mainPictureBox.TabStop = false;
            this.mainPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseDown);
            this.mainPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseMove);
            this.mainPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainPictureBox_MouseUp);
            // 
            // methodCombobox
            // 
            this.methodCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.methodCombobox.BackColor = System.Drawing.SystemColors.Menu;
            this.methodCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.methodCombobox.DropDownWidth = 137;
            this.methodCombobox.Items.AddRange(new object[] {
            "Drawing",
            "Selecting"});
            this.methodCombobox.Location = new System.Drawing.Point(6, 28);
            this.methodCombobox.Name = "methodCombobox";
            this.methodCombobox.Size = new System.Drawing.Size(137, 24);
            this.methodCombobox.TabIndex = 1;
            this.methodCombobox.SelectedIndexChanged += new System.EventHandler(this.methodCombobox_SelectedIndexChanged);
            // 
            // colorCombobox
            // 
            this.colorCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.colorCombobox.BackColor = System.Drawing.SystemColors.Menu;
            this.colorCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorCombobox.FormattingEnabled = true;
            this.colorCombobox.Location = new System.Drawing.Point(4, 115);
            this.colorCombobox.Name = "colorCombobox";
            this.colorCombobox.Size = new System.Drawing.Size(137, 24);
            this.colorCombobox.TabIndex = 2;
            this.colorCombobox.SelectedIndexChanged += new System.EventHandler(this.colorCombobox_SelectedIndexChanged);
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.BackColor = System.Drawing.SystemColors.Menu;
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearButton.Location = new System.Drawing.Point(6, 64);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(97, 39);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.leftPanel);
            this.panel1.Controls.Add(this.lowPanel);
            this.panel1.Controls.Add(this.mainPictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1398, 673);
            this.panel1.TabIndex = 7;
            // 
            // leftPanel
            // 
            this.leftPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.leftPanel.Controls.Add(this.ClearingGroupBox);
            this.leftPanel.Controls.Add(this.STOGroupBox);
            this.leftPanel.Controls.Add(this.geomOpGroupBox);
            this.leftPanel.Controls.Add(this.primitiveGroupBox);
            this.leftPanel.Location = new System.Drawing.Point(0, 0);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(303, 673);
            this.leftPanel.TabIndex = 20;
            // 
            // ClearingGroupBox
            // 
            this.ClearingGroupBox.Controls.Add(this.clearLabel);
            this.ClearingGroupBox.Controls.Add(this.deleteLabel);
            this.ClearingGroupBox.Controls.Add(this.clearButton);
            this.ClearingGroupBox.Controls.Add(this.deleteButton);
            this.ClearingGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.ClearingGroupBox.Location = new System.Drawing.Point(3, 457);
            this.ClearingGroupBox.Name = "ClearingGroupBox";
            this.ClearingGroupBox.Size = new System.Drawing.Size(297, 120);
            this.ClearingGroupBox.TabIndex = 3;
            this.ClearingGroupBox.TabStop = false;
            this.ClearingGroupBox.Text = "Clearing";
            // 
            // clearLabel
            // 
            this.clearLabel.AutoSize = true;
            this.clearLabel.Location = new System.Drawing.Point(233, 75);
            this.clearLabel.MaximumSize = new System.Drawing.Size(150, 0);
            this.clearLabel.Name = "clearLabel";
            this.clearLabel.Size = new System.Drawing.Size(57, 16);
            this.clearLabel.TabIndex = 27;
            this.clearLabel.Text = "Clear all";
            // 
            // deleteLabel
            // 
            this.deleteLabel.AutoSize = true;
            this.deleteLabel.Location = new System.Drawing.Point(203, 30);
            this.deleteLabel.MaximumSize = new System.Drawing.Size(120, 0);
            this.deleteLabel.Name = "deleteLabel";
            this.deleteLabel.Size = new System.Drawing.Size(88, 16);
            this.deleteLabel.TabIndex = 26;
            this.deleteLabel.Text = "Delete object";
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.BackColor = System.Drawing.SystemColors.Menu;
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deleteButton.Location = new System.Drawing.Point(6, 19);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(97, 39);
            this.deleteButton.TabIndex = 16;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.delteButton_Click);
            // 
            // STOGroupBox
            // 
            this.STOGroupBox.Controls.Add(this.STOTypeLabel);
            this.STOGroupBox.Controls.Add(this.execSTOButton);
            this.STOGroupBox.Controls.Add(this.STOTypeСomboBox);
            this.STOGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.STOGroupBox.Location = new System.Drawing.Point(3, 324);
            this.STOGroupBox.Name = "STOGroupBox";
            this.STOGroupBox.Size = new System.Drawing.Size(297, 119);
            this.STOGroupBox.TabIndex = 2;
            this.STOGroupBox.TabStop = false;
            this.STOGroupBox.Text = "STO";
            // 
            // STOTypeLabel
            // 
            this.STOTypeLabel.AutoSize = true;
            this.STOTypeLabel.Location = new System.Drawing.Point(225, 30);
            this.STOTypeLabel.MaximumSize = new System.Drawing.Size(120, 0);
            this.STOTypeLabel.Name = "STOTypeLabel";
            this.STOTypeLabel.Size = new System.Drawing.Size(65, 16);
            this.STOTypeLabel.TabIndex = 25;
            this.STOTypeLabel.Text = "STO type";
            // 
            // execSTOButton
            // 
            this.execSTOButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.execSTOButton.BackColor = System.Drawing.SystemColors.Menu;
            this.execSTOButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.execSTOButton.Location = new System.Drawing.Point(13, 57);
            this.execSTOButton.Name = "execSTOButton";
            this.execSTOButton.Size = new System.Drawing.Size(97, 51);
            this.execSTOButton.TabIndex = 18;
            this.execSTOButton.Text = "Compute STO";
            this.execSTOButton.UseVisualStyleBackColor = false;
            this.execSTOButton.Click += new System.EventHandler(this.execSTOButton_Click);
            // 
            // STOTypeСomboBox
            // 
            this.STOTypeСomboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.STOTypeСomboBox.BackColor = System.Drawing.SystemColors.Menu;
            this.STOTypeСomboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.STOTypeСomboBox.DropDownWidth = 200;
            this.STOTypeСomboBox.FormattingEnabled = true;
            this.STOTypeСomboBox.Items.AddRange(new object[] {
            "Merge",
            "Intersection",
            "Symmeric difference",
            "Difference A\\B",
            "Difference B\\A"});
            this.STOTypeСomboBox.Location = new System.Drawing.Point(6, 27);
            this.STOTypeСomboBox.Name = "STOTypeСomboBox";
            this.STOTypeСomboBox.Size = new System.Drawing.Size(137, 24);
            this.STOTypeСomboBox.TabIndex = 17;
            this.STOTypeСomboBox.SelectedIndexChanged += new System.EventHandler(this.STOTypeСomboBox_SelectedIndexChanged);
            // 
            // geomOpGroupBox
            // 
            this.geomOpGroupBox.Controls.Add(this.geomOpTypeLabel);
            this.geomOpGroupBox.Controls.Add(this.methodLabel);
            this.geomOpGroupBox.Controls.Add(this.methodCombobox);
            this.geomOpGroupBox.Controls.Add(this.geomOperationTypeComboBox);
            this.geomOpGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.geomOpGroupBox.Location = new System.Drawing.Point(3, 202);
            this.geomOpGroupBox.Name = "geomOpGroupBox";
            this.geomOpGroupBox.Size = new System.Drawing.Size(297, 107);
            this.geomOpGroupBox.TabIndex = 1;
            this.geomOpGroupBox.TabStop = false;
            this.geomOpGroupBox.Text = "Operation mode";
            // 
            // geomOpTypeLabel
            // 
            this.geomOpTypeLabel.AutoSize = true;
            this.geomOpTypeLabel.Location = new System.Drawing.Point(195, 70);
            this.geomOpTypeLabel.MaximumSize = new System.Drawing.Size(120, 0);
            this.geomOpTypeLabel.Name = "geomOpTypeLabel";
            this.geomOpTypeLabel.Size = new System.Drawing.Size(96, 16);
            this.geomOpTypeLabel.TabIndex = 24;
            this.geomOpTypeLabel.Text = "Operation type";
            this.geomOpTypeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // methodLabel
            // 
            this.methodLabel.AutoSize = true;
            this.methodLabel.Location = new System.Drawing.Point(189, 31);
            this.methodLabel.MaximumSize = new System.Drawing.Size(120, 0);
            this.methodLabel.Name = "methodLabel";
            this.methodLabel.Size = new System.Drawing.Size(105, 16);
            this.methodLabel.TabIndex = 23;
            this.methodLabel.Text = "Operation mode";
            // 
            // geomOperationTypeComboBox
            // 
            this.geomOperationTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.geomOperationTypeComboBox.BackColor = System.Drawing.SystemColors.Menu;
            this.geomOperationTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.geomOperationTypeComboBox.DropDownWidth = 350;
            this.geomOperationTypeComboBox.FormattingEnabled = true;
            this.geomOperationTypeComboBox.Items.AddRange(new object[] {
            "Standart",
            "Vertical mirror",
            "Rotate around point"});
            this.geomOperationTypeComboBox.Location = new System.Drawing.Point(6, 67);
            this.geomOperationTypeComboBox.MaximumSize = new System.Drawing.Size(500, 0);
            this.geomOperationTypeComboBox.Name = "geomOperationTypeComboBox";
            this.geomOperationTypeComboBox.Size = new System.Drawing.Size(137, 24);
            this.geomOperationTypeComboBox.TabIndex = 15;
            this.geomOperationTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.geomOperationTypeComboBox_SelectedIndexChanged);
            // 
            // primitiveGroupBox
            // 
            this.primitiveGroupBox.Controls.Add(this.colorTextBox);
            this.primitiveGroupBox.Controls.Add(this.colorCombobox);
            this.primitiveGroupBox.Controls.Add(this.colorButton);
            this.primitiveGroupBox.Controls.Add(this.colorLabel);
            this.primitiveGroupBox.Controls.Add(this.PrimitiveOptionsLabel);
            this.primitiveGroupBox.Controls.Add(this.PrimitiveTypeLabel);
            this.primitiveGroupBox.Controls.Add(this.drawLinesCheckBox);
            this.primitiveGroupBox.Controls.Add(this.primitiveTypeComboBox);
            this.primitiveGroupBox.Controls.Add(this.pointAmountLabel);
            this.primitiveGroupBox.Controls.Add(this.pointsAmountTextBox);
            this.primitiveGroupBox.Controls.Add(this.dtTrackBar);
            this.primitiveGroupBox.Controls.Add(this.dtLabel);
            this.primitiveGroupBox.Controls.Add(this.dtValueLabel);
            this.primitiveGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.primitiveGroupBox.Location = new System.Drawing.Point(3, 3);
            this.primitiveGroupBox.Name = "primitiveGroupBox";
            this.primitiveGroupBox.Size = new System.Drawing.Size(297, 193);
            this.primitiveGroupBox.TabIndex = 0;
            this.primitiveGroupBox.TabStop = false;
            this.primitiveGroupBox.Text = "Primitive";
            // 
            // colorTextBox
            // 
            this.colorTextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.colorTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.colorTextBox.Location = new System.Drawing.Point(4, 145);
            this.colorTextBox.Name = "colorTextBox";
            this.colorTextBox.Size = new System.Drawing.Size(134, 22);
            this.colorTextBox.TabIndex = 27;
            // 
            // colorButton
            // 
            this.colorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.colorButton.BackColor = System.Drawing.SystemColors.Menu;
            this.colorButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.colorButton.Location = new System.Drawing.Point(6, 84);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(97, 25);
            this.colorButton.TabIndex = 26;
            this.colorButton.Text = "Color";
            this.colorButton.UseVisualStyleBackColor = false;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // colorLabel
            // 
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new System.Drawing.Point(199, 118);
            this.colorLabel.MaximumSize = new System.Drawing.Size(100, 0);
            this.colorLabel.Name = "colorLabel";
            this.colorLabel.Size = new System.Drawing.Size(83, 16);
            this.colorLabel.TabIndex = 22;
            this.colorLabel.Text = "Color choise";
            // 
            // PrimitiveOptionsLabel
            // 
            this.PrimitiveOptionsLabel.AutoSize = true;
            this.PrimitiveOptionsLabel.Location = new System.Drawing.Point(209, 51);
            this.PrimitiveOptionsLabel.MaximumSize = new System.Drawing.Size(100, 0);
            this.PrimitiveOptionsLabel.Name = "PrimitiveOptionsLabel";
            this.PrimitiveOptionsLabel.Size = new System.Drawing.Size(62, 32);
            this.PrimitiveOptionsLabel.TabIndex = 21;
            this.PrimitiveOptionsLabel.Text = "Primitive params";
            // 
            // PrimitiveTypeLabel
            // 
            this.PrimitiveTypeLabel.AutoSize = true;
            this.PrimitiveTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.PrimitiveTypeLabel.Location = new System.Drawing.Point(183, 24);
            this.PrimitiveTypeLabel.Name = "PrimitiveTypeLabel";
            this.PrimitiveTypeLabel.Size = new System.Drawing.Size(88, 16);
            this.PrimitiveTypeLabel.TabIndex = 20;
            this.PrimitiveTypeLabel.Text = "Primitive type";
            // 
            // drawLinesCheckBox
            // 
            this.drawLinesCheckBox.AutoSize = true;
            this.drawLinesCheckBox.Location = new System.Drawing.Point(6, 167);
            this.drawLinesCheckBox.Name = "drawLinesCheckBox";
            this.drawLinesCheckBox.Size = new System.Drawing.Size(120, 20);
            this.drawLinesCheckBox.TabIndex = 19;
            this.drawLinesCheckBox.Text = "Display contour";
            this.drawLinesCheckBox.UseVisualStyleBackColor = true;
            this.drawLinesCheckBox.CheckedChanged += new System.EventHandler(this.drawLinesCheckBox_CheckedChanged);
            // 
            // primitiveTypeComboBox
            // 
            this.primitiveTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.primitiveTypeComboBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.primitiveTypeComboBox.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.primitiveTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.primitiveTypeComboBox.FormattingEnabled = true;
            this.primitiveTypeComboBox.Items.AddRange(new object[] {
            "Straight line",
            "Cube spline",
            "Polygon",
            "Free polygon",
            "Arrow"});
            this.primitiveTypeComboBox.Location = new System.Drawing.Point(4, 18);
            this.primitiveTypeComboBox.Name = "primitiveTypeComboBox";
            this.primitiveTypeComboBox.Size = new System.Drawing.Size(173, 24);
            this.primitiveTypeComboBox.TabIndex = 12;
            this.primitiveTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.primitiveTypeComboBox_SelectedIndexChanged);
            // 
            // pointAmountLabel
            // 
            this.pointAmountLabel.AutoSize = true;
            this.pointAmountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.pointAmountLabel.Location = new System.Drawing.Point(6, 51);
            this.pointAmountLabel.Name = "pointAmountLabel";
            this.pointAmountLabel.Size = new System.Drawing.Size(98, 16);
            this.pointAmountLabel.TabIndex = 13;
            this.pointAmountLabel.Text = "Points amount: ";
            // 
            // pointsAmountTextBox
            // 
            this.pointsAmountTextBox.BackColor = System.Drawing.SystemColors.Menu;
            this.pointsAmountTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.pointsAmountTextBox.Location = new System.Drawing.Point(113, 48);
            this.pointsAmountTextBox.Name = "pointsAmountTextBox";
            this.pointsAmountTextBox.Size = new System.Drawing.Size(66, 22);
            this.pointsAmountTextBox.TabIndex = 14;
            this.pointsAmountTextBox.Text = "3";
            this.pointsAmountTextBox.TextChanged += new System.EventHandler(this.pointsAmountTextBox_TextChanged);
            // 
            // dtTrackBar
            // 
            this.dtTrackBar.Location = new System.Drawing.Point(75, 45);
            this.dtTrackBar.Name = "dtTrackBar";
            this.dtTrackBar.Size = new System.Drawing.Size(104, 45);
            this.dtTrackBar.TabIndex = 5;
            this.dtTrackBar.ValueChanged += new System.EventHandler(this.dtTrackBar_ValueChanged);
            // 
            // dtLabel
            // 
            this.dtLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtLabel.AutoSize = true;
            this.dtLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtLabel.Location = new System.Drawing.Point(6, 51);
            this.dtLabel.Name = "dtLabel";
            this.dtLabel.Size = new System.Drawing.Size(22, 16);
            this.dtLabel.TabIndex = 10;
            this.dtLabel.Text = "dt:";
            // 
            // dtValueLabel
            // 
            this.dtValueLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dtValueLabel.AutoSize = true;
            this.dtValueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtValueLabel.Location = new System.Drawing.Point(26, 51);
            this.dtValueLabel.Name = "dtValueLabel";
            this.dtValueLabel.Size = new System.Drawing.Size(46, 16);
            this.dtValueLabel.TabIndex = 11;
            this.dtValueLabel.Text = "0.0005";
            // 
            // lowPanel
            // 
            this.lowPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lowPanel.Controls.Add(this.HelpGroupBox);
            this.lowPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lowPanel.Location = new System.Drawing.Point(0, 583);
            this.lowPanel.Name = "lowPanel";
            this.lowPanel.Size = new System.Drawing.Size(1398, 90);
            this.lowPanel.TabIndex = 8;
            // 
            // HelpGroupBox
            // 
            this.HelpGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.HelpGroupBox.Controls.Add(this.helpPrimitiveLabel);
            this.HelpGroupBox.Controls.Add(this.helpTypeLabel);
            this.HelpGroupBox.Controls.Add(this.helpMouseLabel);
            this.HelpGroupBox.Controls.Add(this.helpMethodLabel);
            this.HelpGroupBox.Controls.Add(this.modeLabel);
            this.HelpGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.HelpGroupBox.Location = new System.Drawing.Point(327, 3);
            this.HelpGroupBox.Name = "HelpGroupBox";
            this.HelpGroupBox.Size = new System.Drawing.Size(1066, 82);
            this.HelpGroupBox.TabIndex = 24;
            this.HelpGroupBox.TabStop = false;
            this.HelpGroupBox.Text = "Help";
            // 
            // helpPrimitiveLabel
            // 
            this.helpPrimitiveLabel.BackColor = System.Drawing.Color.Transparent;
            this.helpPrimitiveLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.helpPrimitiveLabel.Location = new System.Drawing.Point(134, 43);
            this.helpPrimitiveLabel.MinimumSize = new System.Drawing.Size(100, 0);
            this.helpPrimitiveLabel.Name = "helpPrimitiveLabel";
            this.helpPrimitiveLabel.Size = new System.Drawing.Size(193, 34);
            this.helpPrimitiveLabel.TabIndex = 27;
            this.helpPrimitiveLabel.Text = "Straight line";
            // 
            // helpTypeLabel
            // 
            this.helpTypeLabel.AutoSize = true;
            this.helpTypeLabel.BackColor = System.Drawing.Color.Transparent;
            this.helpTypeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.helpTypeLabel.Location = new System.Drawing.Point(134, 18);
            this.helpTypeLabel.Name = "helpTypeLabel";
            this.helpTypeLabel.Size = new System.Drawing.Size(62, 16);
            this.helpTypeLabel.TabIndex = 26;
            this.helpTypeLabel.Text = "Primitive:";
            // 
            // helpMouseLabel
            // 
            this.helpMouseLabel.AutoSize = true;
            this.helpMouseLabel.BackColor = System.Drawing.Color.Transparent;
            this.helpMouseLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.helpMouseLabel.Location = new System.Drawing.Point(325, 18);
            this.helpMouseLabel.MaximumSize = new System.Drawing.Size(740, 0);
            this.helpMouseLabel.Name = "helpMouseLabel";
            this.helpMouseLabel.Size = new System.Drawing.Size(57, 16);
            this.helpMouseLabel.TabIndex = 25;
            this.helpMouseLabel.Text = "Drawing";
            // 
            // helpMethodLabel
            // 
            this.helpMethodLabel.AutoSize = true;
            this.helpMethodLabel.BackColor = System.Drawing.Color.Transparent;
            this.helpMethodLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.helpMethodLabel.Location = new System.Drawing.Point(9, 43);
            this.helpMethodLabel.Name = "helpMethodLabel";
            this.helpMethodLabel.Size = new System.Drawing.Size(57, 16);
            this.helpMethodLabel.TabIndex = 24;
            this.helpMethodLabel.Text = "Drawing";
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.BackColor = System.Drawing.Color.Transparent;
            this.modeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.modeLabel.Location = new System.Drawing.Point(9, 18);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(46, 16);
            this.modeLabel.TabIndex = 23;
            this.modeLabel.Text = "Mode:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(1398, 673);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(453, 455);
            this.Name = "Form1";
            this.Text = "Vector Graphics Editor";
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.leftPanel.ResumeLayout(false);
            this.ClearingGroupBox.ResumeLayout(false);
            this.ClearingGroupBox.PerformLayout();
            this.STOGroupBox.ResumeLayout(false);
            this.STOGroupBox.PerformLayout();
            this.geomOpGroupBox.ResumeLayout(false);
            this.geomOpGroupBox.PerformLayout();
            this.primitiveGroupBox.ResumeLayout(false);
            this.primitiveGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTrackBar)).EndInit();
            this.lowPanel.ResumeLayout(false);
            this.HelpGroupBox.ResumeLayout(false);
            this.HelpGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.ComboBox methodCombobox;
        private System.Windows.Forms.ComboBox colorCombobox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel lowPanel;
        private TrackBar dtTrackBar;
        private Label dtValueLabel;
        private Label dtLabel;
        private ComboBox primitiveTypeComboBox;
        private TextBox pointsAmountTextBox;
        private Label pointAmountLabel;
        private ComboBox geomOperationTypeComboBox;
        private Button deleteButton;
        private ComboBox STOTypeСomboBox;
        private Button execSTOButton;
        private CheckBox drawLinesCheckBox;
        private Panel leftPanel;
        private GroupBox primitiveGroupBox;
        private GroupBox geomOpGroupBox;
        private GroupBox STOGroupBox;
        private GroupBox ClearingGroupBox;
        private Label colorLabel;
        private Label PrimitiveOptionsLabel;
        private Label PrimitiveTypeLabel;
        private Label methodLabel;
        private Label geomOpTypeLabel;
        private Label STOTypeLabel;
        private Label clearLabel;
        private Label deleteLabel;
        private GroupBox HelpGroupBox;
        private Label modeLabel;
        private Label helpMethodLabel;
        private TextBox colorTextBox;
        private Button colorButton;
        private ColorDialog colorDialog1;
        private Label helpPrimitiveLabel;
        private Label helpTypeLabel;
        private Label helpMouseLabel;
    }
}

