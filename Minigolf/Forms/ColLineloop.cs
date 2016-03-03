using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minigolf
{
    class ColLineloop : ColType
    {
        private Vector2d[] pos;

        public Vector2d[] Pos { get { return this.pos; } set { this.pos = value; } }
                

        public ColLineloop(Vector2d[] pos)
        {
            this.pos = pos;
        }

        public override void render()
        {
            GL.Begin(PrimitiveType.LineStrip);
            foreach (Vector2d vec in pos)
            {
                GL.Vertex2(vec);
            }
            //GL.Vertex2(pos[0]);
            GL.End();
        }

        public override bool collide(ColCircle ball, out Vector2d depth)
        {
            //return BaseObj.col_circleLine2D(ball.Pos, ball.R, this.pos1,this.pos2);
            depth = Vector2d.Zero;
            if (pos.Length > 1)     //only 1 Vector isn't enough for a line
            {
                for (int i = 0; i < pos.Length - 1; i++)
                {
                    if (pos[i] != null)
                        BaseObj.SolveCircleSegmentCollision(ball.Pos+ball.Vel, ball.R, this.pos[i], this.pos[i + 1], out depth);                        

                    if (depth != Vector2d.Zero)
                    {
                        
                        return true;
                    }
                }
                
            }
            return false;
        }



    }
}


