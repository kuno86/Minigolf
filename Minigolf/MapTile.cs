using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minigolf
{
    class MapTile
    {
        public Ground G    //1=None  &   0=100%
        { get; set; }

        public Wall W
        { get; set; }

        public Objectt O
        { get; set; }


        public MapTile(short x, short y, Ground g, Wall w, Objectt o)
        {
            if (g == null)
                g = new Ground(0, x, y);
            this.G = g;
            this.W = w;
            this.O = o;
        }
                

    }
}
