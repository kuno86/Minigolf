using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Minigolf
{
    class Player 
    {
        private Vector2d pos;
        private Vector2d vel;

        public bool myTurn {get; set; }
        public System.Drawing.Color color { get; set; }
        public byte trackhits { get; set; }
        public bool trackFinished { get; set; }
        public bool hasHitBall { get; set; }
        public String name { get; set; }
        public string ip { get; set; }
        public double r { get; set; }
        public double scaleR { get; set; }  //this is only optical and dosen't touch the radius
        public Vector2d Pos { get { return this.pos; } set { this.pos = value; } }
        
        public Vector2d Vel 
        { 
            get 
            { return this.vel; } 
            set 
            {
                this.vel = value;
                if (vel.X > 7) vel.X = 7;
                if (vel.X < -7) vel.X = -7;
                if (vel.Y > 7) vel.Y = 7;
                if (vel.X < -7) vel.Y = -7;
            } 
        }

        public Vector2d pos2 { get; set; }
        public Vector2d vel2 { get; set; }

        private static Vector2d[] polyPoints = Wall.positions_of_circlepart(6.5, 0, 360, new Vector2d(7.5, 7.5));
        private bool sinkingIn = false;
        


        public Player(Vector2d pos, string name = "Test", string ip = "0.0.0.0", System.Drawing.Color color = default(System.Drawing.Color) )
        {
            myTurn = false;
            this.Pos = pos;
            this.Vel = Vector2d.Zero;
            this.ip = ip;
            if (color == default(System.Drawing.Color))
                this.color = System.Drawing.Color.White;
            else
                this.color = color;

            this.trackFinished = false;
            this.hasHitBall = false;
            this.trackhits = 0;
            this.name = name;
            r = 6.5;
            scaleR = 1;
        }

        public void render()
        {
            GL.Color4(this.color);   
         
            GL.Begin(PrimitiveType.Polygon);
            foreach(Vector2d vec in polyPoints)
                GL.Vertex2(((pos - new Vector2d(7.5, 7.5)) + vec * scaleR ) );
            GL.End();

            
        }

        public void tick()
        {
            if (RootThingy.keyboard[Key.A])
                vel.X -= 0.05;
            if (RootThingy.keyboard[Key.D])
                vel.X += 0.05;
            if (RootThingy.keyboard[Key.W])
                vel.Y -= 0.05;
            if (RootThingy.keyboard[Key.S])
                vel.Y += 0.05;

            if (RootThingy.keyboard[Key.R])
                reset();
            if (RootThingy.keyboard[Key.Delete])
                sinkIn();

            if (myTurn && RootThingy.mouse[MouseButton.Left])
            {
                GL.Color4(this.color);
                MyImage.endDraw2D();
                GL.Begin(PrimitiveType.Lines);  //Draw the hit-line
                GL.Vertex2(RootThingy.m1);
                GL.Vertex2(RootThingy.m2);
                GL.End();
                MyImage.beginDraw2D();
            }

            if (!myTurn && hasHitBall)
                hasHitBall = false;

            if (vel.X < 0.01 && vel.X > -0.01)
                vel.X = 0;
            if (vel.Y < 0.01 && vel.Y > -0.01)
                vel.Y = 0;


            bool FFDone = false;    //Friction / Force done ?
            bool BDone = false;     //Bouncing done ?

            if (!trackFinished && !sinkingIn)
                pos += vel;
              
            if (sinkingIn)
                sinkIn();
            
            if (vel.X > 7) vel.X = 7;
            if (vel.X < -7) vel.X = -7;
            if (vel.Y > 7) vel.Y = 7;
            if (vel.X < -7) vel.Y = -7;

            Vector2d depth;
            for (short yy = Math.Max((short)((pos.Y - r * 2) / 15), (short)0); yy < Math.Min((short)Math.Ceiling((pos.Y + r * 2) / 15), (short)Map.map.GetLength(0)); yy++)
            {
                for (short xx = Math.Max((short)((pos.X - r * 2)/15), (short)0); xx < Math.Min((short)Math.Ceiling((pos.X + r * 2)/15), (short)Map.map.GetLength(1)); xx++)
                {
                    if ((Map.map[yy, xx].W.ColType1.collide(new ColCircle(pos + vel, r), out depth) || (Map.map[yy, xx].W.ColType2.collide(new ColCircle(pos, r), out depth))))
                    {
                        if (!BDone)
                        {
                            vel *= Map.map[yy, xx].G.Bounce;
                            BDone = true;
                        }
                        vel += depth;

                    }

                    if (vel == Vector2d.Zero && Map.map[yy, xx].G.SinkIn && (pos.X >= xx * 15 && pos.X <= xx * 15 + 15 && pos.Y >= yy * 15 && pos.Y <= yy * 15 + 15))
                        sinkIn();

                    if (!FFDone && BaseObj.col_circleRect(this.pos, 1, new ColRect(new Vector2d(xx * 15, yy * 15), new Vector2d(15, 15), new Vector2d(0, 0))))
                    {
                        vel += Map.map[yy, xx].G.Force;
                        vel *= Map.map[yy, xx].G.Friction;
                        FFDone = true;
                    }

                    //if (Map.map[yy, xx].O != null && Map.map[yy, xx].O.ColType != null && Map.map[yy, xx].O.ColType.collide(new ColCircle(pos, r), out depth))
                    //    Map.map[yy, xx].O.tick();
                }
            }
            
        }


        public void sinkIn()
        {
            short sinkSpeed = 600;
            scaleR -= (r / sinkSpeed);
            pos += new Vector2d((r / sinkSpeed) * 7.5, (r / sinkSpeed) * 7.5);
            sinkingIn = true;
            if (scaleR <= 0)
            {
                scaleR = 1;
                reset();
                sinkingIn = false;
            }
        }
               

        public void reset()
        {
            r = 6.5;
            vel = Vector2d.Zero;
            scaleR = 1;
            trackFinished = false;
            foreach (MapTile mt in Map.map)
            {
                if (mt.O.id == 1)
                    this.pos = new Vector2d(mt.O.x + 7.5, mt.O.y + 7.5);    //send the ball back to start-position (id3)
            }
        }

    }
}
