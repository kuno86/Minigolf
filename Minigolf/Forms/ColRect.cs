using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minigolf
{
    class ColRect : ColType
    {
        private Vector2d dim = new Vector2d(15, 15);
        private Vector2d pos = new Vector2d(0, 0);
        private Vector2d offSet = new Vector2d(0, 0);
        
        public Vector2d Dim { get { return this.dim; } set { this.dim = value; } }
        public Vector2d Pos { get { return this.pos; } set { this.pos = value; } }
        public Vector2d OffSet { get { return this.offSet; } set { this.offSet = value; } }
        
        public ColRect(short x, short y, Byte w = 15, Byte h = 15, Byte offsetX = 0, Byte offsetY = 0)
        {
            this.pos.X = x;
            this.pos.Y = y;
            this.dim.X = w;
            this.dim.Y = h;
            this.offSet.X = offsetX;
            this.offSet.Y = offsetY;
        }

        public ColRect(Vector2d pos, Vector2d dim, Vector2d offSet)
        {
            this.pos = pos;
            this.dim = dim;
            this.offSet = offSet;
        }

        public override void render()
        {
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(pos);
            GL.Vertex2(pos.X + dim.X, pos.Y);
            GL.Vertex2(pos.X + dim.X, pos.Y + dim.Y);
            GL.Vertex2(pos.X, pos.Y + dim.Y);
            GL.End();
        }

        public override bool collide(ColCircle ball, out Vector2d depth)
        {
            depth = new Vector2d(0, 0);
            return BaseObj.col_circleRect(ball.Pos, ball.R, new ColRect(this.Pos, this.Dim, new Vector2d(0, 0)));
        }

    }
}
