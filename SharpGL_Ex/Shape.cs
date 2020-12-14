using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpGL_Ex
{
    abstract class Shape
    {
        protected Color color;
        public abstract void Draw(OpenGL gl);
    }
}
