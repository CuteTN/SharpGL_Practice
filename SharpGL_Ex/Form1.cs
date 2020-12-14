using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpGL_Ex
{
    // using this type to know what user is currently drawing
    enum FormMode
    {
        None,
        DrawLine,
        DrawCircle,
        DrawRectangle,
        DrawEllipse,
        DrawEqTriangle,
        DrawEqPentagon,
        DrawEqHexagon,
    }

    public partial class FormMain : Form
    {
        #region Utils stuff
        List<Shape> shapes = new List<Shape>();

        FormMode formMode = FormMode.None;

        Point mouseDownPosition = new Point(-1,-1);
        Point mouseUpPosition = new Point(-1,-1);
        bool flagMouseIsDown = false;

        Color chosenColor = Color.LightPink; // dont get and set directly to this
        Color ChosenColor
        {
            get => chosenColor;
            set
            {
                chosenColor = value;

                button_Color.BackColor = value;

                // if this is a bright color
                if(value.R + value.G + value.B >= 128*3)
                    button_Color.ForeColor = Color.Black;
                else
                    button_Color.ForeColor = Color.White;
            }
        }
        #endregion

        #region Init stuff
        private void CustomInit()
        {
            comboBox_Shapes.SelectedIndex = 0;
            ChosenColor = Color.LightPink;
        }

        public FormMain()
        {
            InitializeComponent();
            CustomInit();
        }
        #endregion

        #region Template stuff
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the clear color.
            gl.ClearColor(0, 0, 0, 0);
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
        }

        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            // Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            // Clear the color and depth buffer.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);



            // Vẽ vời chỗ này. Ví dụ:
            //gl.Begin(OpenGL.GL_TRIANGLES); // Chọn chế độ vẽ tam giác
            //gl.Vertex2sv(new short[] { 0, 0 }); // Đỉnh thứ 1 tọa độ 0,0
            //gl.Vertex2sv(new short[] { 100, 100 }); // Đỉnh thứ 2 tọa độ 100, 100
            //gl.Vertex2sv(new short[] { 200, 0 }); // Đỉnh thứ 3 tọa độ 200, 0
            //gl.End();
            //gl.Flush(); // Thực hiện lệnh vẽ ngay lập tức thay vì đợi sau 1 khoảng thời gian

            // CuteTN note: add new shapes cases here
            foreach(var shape in shapes)
            {
                shape.Draw(gl);
            }

            gl.Flush();
        }

        #endregion

        #region Buttons Handling stuff
        private void button_Color_Click(object sender, EventArgs e)
        {
            // open color picker form
            ColorDialog dlg = new ColorDialog();
            var dlgRes = dlg.ShowDialog();

            if(dlgRes == DialogResult.OK)
            {
                ChosenColor = dlg.Color;
            }
        }
        #endregion

        #region Mouse handling stuff
        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPosition = e.Location;
            mouseUpPosition = e.Location;
            flagMouseIsDown = true;
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            if(flagMouseIsDown)
                mouseUpPosition = e.Location;
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            flagMouseIsDown = false;

            switch(formMode)
            {
                case FormMode.DrawLine:
                    shapes.Add(new Line(mouseDownPosition, mouseUpPosition, ChosenColor));
                    break;
            }
            // CuteTN note: add Shape to shape collection here
        }
        #endregion

        #region Shapes drawing stuff
        // CuteTN note: we gonna need some better OOP stuff and polymorphism pla pla here :)
        // CuteTN note: add drawing logic to the functions below
        #endregion

        #region Mode changing stuff
        private void comboBox_Shapes_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Dirty code here

            /*
                None
                Line
                Circle
                Rectangle
                Ellipse
                Equilateral triangle
                Equilateral pentagon
                Equilateral hexagon
             */

            switch (comboBox_Shapes.SelectedItem as string)
            {
                case "None":
                    formMode = FormMode.None;
                    break;

                case "Line":
                    formMode = FormMode.DrawLine;
                    break;

                case "Circle":
                    formMode = FormMode.DrawCircle;
                    break;

                case "Rectangle":
                    formMode = FormMode.DrawRectangle;
                    break;

                case "Ellipse":
                    formMode = FormMode.DrawEllipse;
                    break;

                case "Equilateral triangle":
                    formMode = FormMode.DrawEqTriangle;
                    break;

                case "Equilateral pentagon":
                    formMode = FormMode.DrawEqPentagon;
                    break;

                case "Equilateral hexagon":
                    formMode = FormMode.DrawEqHexagon;
                    break;
            }
        }
        #endregion
    }
}
