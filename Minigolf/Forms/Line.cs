using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minigolf
{
    class Line : ColType
    {
        private Vector2d pos1 = new Vector2d(0, 0);
        private Vector2d pos2 = new Vector2d(0, 0);

        public Vector2d Pos1 { get { return this.pos1; } set { this.pos1 = value; } }
        public Vector2d Pos2 { get { return this.pos2; } set { this.pos2 = value; } }

        public Line(short x1, short y1, short x2, short y2)
        {
            this.pos1.X = x1;
            this.pos1.Y = y1;
            this.pos2.X = x2;
            this.pos2.Y = y2;
        }

        public Line(Vector2d pos1, Vector2d pos2)
        {
            this.pos1 = pos1;
            this.pos2 = pos2;
        }

        public override void render()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(pos1);
            GL.Vertex2(pos2);
            GL.End();
        }

        public override bool collide(ColCircle ball, out Vector2d depth)
        {
            //return BaseObj.col_circleLine2D(ball.Pos, ball.R, this.pos1,this.pos2);
            depth = Vector2d.Zero;
            BaseObj.SolveCircleSegmentCollision(ball.Pos, ball.R, this.pos1, this.pos2, out depth);
            if(depth==Vector2d.Zero)
                return false;
            else
            {
                
                return true;
            }
        }



    }
}

