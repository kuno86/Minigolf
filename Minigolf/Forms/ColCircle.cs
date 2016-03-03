using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minigolf
{
    class ColCircle : ColType
    {        
        private double r = 15;
        private Vector2d pos = new Vector2d(0, 0);
        private static Vector2d[] circleparts = calcCircleParts();

        public double R { get { return this.r; } set { this.r = value; } }
        public Vector2d Pos { get { return this.pos; } set { this.pos = value; } }

        public ColCircle(short x, short y, double r)
        {
            this.pos.X = x;
            this.pos.Y = y;
            this.r = r;
        }

        public ColCircle(Vector2d pos, double r)
        {
            this.pos = pos;
            this.r = r;
        }

        public override void render()
        {
            GL.Begin(PrimitiveType.LineLoop);
            foreach (Vector2d v in circleparts)
                GL.Vertex2(pos.X + 0 + v.X * r, pos.Y +  v.Y * r);
            GL.End();
        }

        public override bool collide(ColCircle ball, out Vector2d depth)
        {
            depth = new Vector2d(0, 0);
            bool col = BaseObj.col_circleCircle2D(ball.Pos, ball.R, this.Pos, this.R);
            if(col)
                depth=new Vector2d(Math.Abs(ball.pos.X - this.pos.X),Math.Abs(ball.pos.Y - this.pos.Y));
            return col;
        }

        private static Vector2d[] calcCircleParts()
        {
            List<Vector2d> tmp = new List<Vector2d>();
            for (double i = 0; i < 2 * Math.PI; i += Math.PI / 120)
                tmp.Add(new Vector2d(Math.Cos(i), Math.Sin(i)));
            Console.Write('.');
            return tmp.ToArray();
        }

    }
}

