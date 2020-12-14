using SharpGL;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGL_Ex
{
    class Line : Shape
    {
        Point point1, point2; 

        public Line(Point point1, Point point2, Color color)
        {
            this.point1 = point1;
            this.point2 = point2;

            this.color = color;
        }

        public override void Draw(OpenGL gl)
        {
            gl.Color(color.R, color.G, color.B);
            gl.Begin(OpenGL.GL_LINES);
            gl.Vertex(point1.X, gl.RenderContextProvider.Height - point1.Y);
            gl.Vertex(point2.X, gl.RenderContextProvider.Height - point2.Y);
            gl.End();
        }
    }
}
