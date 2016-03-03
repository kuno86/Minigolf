using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minigolf
{
    class Ground : BaseObj
    {
        public double Friction    //1=None  &   0=100%
        { get; set; }

        public double Bounce
        { get; set; }

        public Vector2d Force
        { get; set; }

        public bool SinkIn { get; set; }

        private double hillForce = 0.04;
                
        
        public Ground(byte id, short x, short y)
        {            
            this.x = x;
            this.y = y;
            this.w = 15;
            this.h = 15;
            
            this.texture = Texture.grounds;

            SinkIn = false;
            this.id = id;
            switch (id)
            {
                default:
                case 0:     //grass
                    Friction = 0.988;
                    Bounce = 1;
                    Force = new Vector2d(0, 0);
                    break;
                case 1:     //N-Hill (^^)
                    Friction = 0.988;
                    Bounce = 1;
                    Force = new Vector2d(0, -hillForce);
                    break;
                case 2:     //NE-Hill (^>)
                    Friction = 0.988;
                    Bounce = 1;
                    Force = new Vector2d(hillForce, -hillForce);
                    break;
                case 3:     //E-Hill (>>)
                    Friction = 0.988;
                    Bounce = 1;
                    Force = new Vector2d(hillForce, 0);
                    break;
                case 4:     //SE-Hill (V>)
                    Friction = 0.988;
                    Bounce = 1;
                    Force = new Vector2d(hillForce, hillForce);
                    break;
                case 5:     //S-Hill (vv)
                    Friction = 0.988;
                    Bounce = 1;
                    Force = new Vector2d(0, hillForce);
                    break;
                case 6:     //SW-Hill (v<)
                    Friction = 0.988;
                    Bounce = 1;
                    Force = new Vector2d(-hillForce, hillForce);
                    break;
                case 7:     //W-Hill (<<)
                    Friction = 0.988;
                    Bounce = 1;
                    Force = new Vector2d(-hillForce, 0);
                    break;
                case 8:     //NW-Hill (^<)
                    Friction = 0.988;
                    Bounce = 1;
                    Force = new Vector2d(-hillForce, -hillForce);
                    break;
                case 9:     //water
                    Friction = 0.4;
                    Bounce = 1;
                    Force = new Vector2d(0, 0);
                    SinkIn = true;
                    break;
                case 10:     //quicksand
                    Friction = 0.4;
                    Bounce = 1;
                    Force = new Vector2d(0, 0);
                    SinkIn = true;
                    break;
                case 11:     //ice
                    Friction = 0.99;
                    Bounce = 1;
                    Force = new Vector2d(0, 0);
                    break;
                case 12:     //dirt  (light-brown)
                    Friction = 0.9;
                    Bounce = 1;
                    Force = new Vector2d(0, 0);
                    break;
                case 13:     //mud (dark-brown)
                    Friction = 0.75;
                    Bounce = 1;
                    Force = new Vector2d(0, 0);
                    break;
                case 14:     //flummy
                    Friction = 0.988;
                    Bounce = 2;
                    Force = new Vector2d(0, 0);
                    break;
                case 15:     //anti-flummy
                    Friction = 0.95;
                    Bounce = 0.5;
                    Force = new Vector2d(0, 0);
                    break;
            }


        }

        public override void tick()
        {
            MyImage.drawTileFromID(this.texture, id, 15, x, y);
        }


    }
}
