using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minigolf
{
    class ColEmpty : ColType
    {
        private Vector2d pos;

        public Vector2d Pos { get { return this.pos; } set { this.pos = value; } }


        public ColEmpty(Vector2d pos)
        {
            this.pos = pos;
        }

        public override void render()
        {
            ;
        }

        public override bool collide(ColCircle ball, out Vector2d depth)
        {
            depth = new Vector2d(0, 0);
            return false;
        }



    }
}



