using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minigolf
{
    class ColTriangle : ColType
    {
        private Vector2d pos1 = new Vector2d(0, 0);
        private Vector2d pos2 = new Vector2d(0, 0);
        private Vector2d pos3 = new Vector2d(0, 0);

        public Vector2d Pos1 { get { return this.pos1; } set { this.pos1 = value; } }
        public Vector2d Pos2 { get { return this.pos2; } set { this.pos2 = value; } }
        public Vector2d Pos3 { get { return this.pos3; } set { this.pos3 = value; } }

        public ColTriangle(short x1, short y1, short x2, short y2, short x3, short y3)
        {
            this.pos1.X = x1;
            this.pos1.Y = y1;
            this.pos2.X = x2;
            this.pos2.Y = y2;
            this.pos3.X = x3;
            this.pos3.Y = y3;
        }

        public ColTriangle(Vector2d pos1, Vector2d pos2, Vector2d pos3)
        {
            this.pos1 = pos1;
            this.pos2 = pos2;
            this.pos3 = pos3;
        }

        public override void render()
        {
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(pos1);
            GL.Vertex2(pos2);
            GL.Vertex2(pos3);
            GL.End();
        }

        public override bool collide(ColCircle ball, out Vector2d depth)
        {
            depth = new Vector2d(0, 0);
            return BaseObj.col_circleTriangle2D(ball.Pos, ball.R, this.Pos1, this.Pos2, this.Pos3);
        }

        
    }
}
