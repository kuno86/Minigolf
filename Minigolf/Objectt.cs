using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Minigolf
{
    class Objectt : BaseObj
    {
        public ColType ColType { get; set; }
        private double r = 0;

        public Objectt(byte id, short x, short y)
        {
            this.x = x;
            this.y = y;
            this.id = id;

            this.texture = Texture.objects;
            Vector2d p = new Vector2d(x,y);

            switch (this.id)
            {
                case 00:    //Nothing
                    break;
                case 01: //Startpoint
                    foreach (MapTile mt in Map.map)
                    {
                        if (mt.O.id == 1)   //find duplicates only one target per color on a track
                            mt.O = new Objectt(0, (short)x, (short)y);
                    }
                    this.id = 1;
                    break;
                case 02: //Finish-Hole
                    this.ColType = new ColLineloop(Wall.positions_of_circlepart(7.5, 0, 360, new Vector2d(7.5, 7.5) + p));
                    this.r = 7.5;
                    break;
                case 03: //Quicksand
                    this.ColType = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    Map.map[y / 15, x / 15].G.SinkIn = true;
                    break;
                case 04: //Swamp
                    this.ColType = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    Map.map[y / 15, x / 15].G.SinkIn = true;                    
                    break;
                case 05: //Teleporter Start Blue
                    this.ColType = new ColLineloop(Wall.positions_of_circlepart(7.5, 0, 360, new Vector2d(7.5, 7.5) + new Vector2d(x, y)));
                    this.r = 7.5;
                    break;
                case 06: //Teleporter Target Blue    
                    foreach (MapTile mt in Map.map)
                    {
                        if (mt.O.id == 06)   //find duplicates only one target per color on a track
                            mt.O = new Objectt(0, (short)x, (short)y);
                    }
                    this.id = 06;
                    break;
                case 07: //Teleporter Start Yellow
                    this.ColType = new ColLineloop(Wall.positions_of_circlepart(7.5, 0, 360, new Vector2d(7.5, 7.5) + new Vector2d(x, y)));
                    this.r = 7.5;
                    break;
                case 08: //Teleporter Target Yellow
                    foreach (MapTile mt in Map.map)
                    {
                        if (mt.O.id == 08)   //find duplicates only one target per color on a track
                            mt.O = new Objectt(0, (short)x, (short)y);
                    }
                    this.id = 08;
                    break;
                case 09: //Teleporter Start Red
                    this.ColType = new ColLineloop(Wall.positions_of_circlepart(7.5, 0, 360, new Vector2d(7.5, 7.5) + new Vector2d(x, y)));
                    this.r = 7.5;
                    break;
                case 10: //Teleporter Target Red
                    foreach (MapTile mt in Map.map)
                    {
                        if (mt.O.id == 10)   //find duplicates only one target per color on a track
                            mt.O = new Objectt(0, (short)x, (short)y);
                    }
                    this.id = 10;
                    break;
                case 11: //Teleporter Start Green
                    this.ColType = new ColLineloop(Wall.positions_of_circlepart(7.5, 0, 360, new Vector2d(7.5, 7.5) + new Vector2d(x, y)));
                    this.r = 7.5;
                    break;
                case 12: //Teleporter Target Green
                    foreach (MapTile mt in Map.map)
                    {
                        if (mt.O.id == 12)   //find duplicates only one target per color on a track
                            mt.O = new Objectt(0, (short)x, (short)y);
                    }
                    this.id = 12;
                    break;
                case 13: //Gravity
                    //this.ColType = new ColLineloop(Wall.positions_of_circlepart(42.5, 0, 360, new Vector2d(7.5, 7.5) + new Vector2d(x, y)));
                    this.ColType = new ColCircle(new Vector2d(x + 7.5, y + 7.5), 42.5);
                    r = 42.5;
                    break;
                case 14: //Anti-Gravity
                    this.ColType = new ColCircle(new Vector2d(x + 7.5, y + 7.5), 42.5);
                    this.r = 42.5;
                    break;
                
                case 15: //Bomb small
                    this.ColType = new ColLineloop(Wall.positions_of_circlepart(6, 0, 360, new Vector2d(7.5, 7.5) + new Vector2d(x, y)));
                    this.r = 6;
                    break;
                case 16: //Bomb small exploded
                    break;
                case 17: //Bomb
                    this.ColType = new ColLineloop(Wall.positions_of_circlepart(6, 0, 360, new Vector2d(7.5, 7.5) + new Vector2d(x, y)));
                    this.r = 6;
                    break;
                case 18: //Bomb exploded
                    break;
                case 19: //Bricks 4hp
                    this.ColType = new ColLineloop(new Vector2d[] { new Vector2d(0, 1) + p, new Vector2d(15, 1) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 1) + p });
                    break;
                case 20: //Bricks 3hp
                    this.ColType = new ColLineloop(new Vector2d[] { 
                        new Vector2d(1, 1) + p, new Vector2d(5, 1) + p, new Vector2d(5, 4) + p, new Vector2d(11, 4) + p, new Vector2d(11, 1) + p, 
                        new Vector2d(15, 1) + p, new Vector2d(15, 6) + p, new Vector2d(12, 6) + p, new Vector2d(12, 10) + p, new Vector2d(15, 10) + p, 
                        new Vector2d(15, 12) + p, new Vector2d(9, 12) + p, new Vector2d(9, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 10) + p, 
                        new Vector2d(3, 10) + p, new Vector2d(3, 6) + p, new Vector2d(0, 6) + p, new Vector2d(0, 4) + p, new Vector2d(1, 4) + p, new Vector2d(1, 1) + p });
                    break;
                case 21: //Bricks 2hp
                    this.ColType = new ColLineloop(new Vector2d[] { 
                        new Vector2d(1, 1) + p, new Vector2d(5, 1) + p, new Vector2d(5, 4) + p, new Vector2d(11, 4) + p, new Vector2d(11, 1) + p, new Vector2d(15, 1) + p, new Vector2d(15, 3) + p, new Vector2d(11, 3) + p, new Vector2d(11, 7) + p, new Vector2d(12, 7) + p, 
                        new Vector2d(12, 10) + p, new Vector2d(13, 10) + p, new Vector2d(13, 12) + p, new Vector2d(9, 12) + p, new Vector2d(9, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 13) + p, new Vector2d(9, 13) + p, new Vector2d(9, 9) + p, new Vector2d(3, 9) + p,
                        new Vector2d(3, 7) + p, new Vector2d(7, 7) + p, new Vector2d(7, 3) + p, new Vector2d(1, 3) + p, new Vector2d(1, 1) + p});
                    break;
                case 22: //Bricks 1hp
                    this.ColType = new ColLineloop(new Vector2d[] { 
                        new Vector2d(1, 1) + p, new Vector2d(5, 1) + p, new Vector2d(5, 4) + p, new Vector2d(11, 4) + p, new Vector2d(11, 6) + p, new Vector2d(7, 6) + p, new Vector2d(7, 10) + p, new Vector2d(13, 10) + p,
                        new Vector2d(13, 12) + p, new Vector2d(9, 12) + p, new Vector2d(9, 15) + p, new Vector2d(5, 15) + p, new Vector2d(5, 13) + p, new Vector2d(9, 13) + p, new Vector2d(9, 10) + p, new Vector2d(7, 9) + p,
                        new Vector2d(3, 9) + p, new Vector2d(3, 7) + p, new Vector2d(7, 7) + p, new Vector2d(7, 4) + p, new Vector2d(5, 3) + p, new Vector2d(1, 3) + p, new Vector2d(1, 1) + p});
                    break;
                case 23:    //
                case 24:    //unused
                    break;
                default: //null
                    break;
            }
        }


        public override void tick(ref Player p)
        
        {
            
            Vector2d hix;   //not really used
            ColCircle ccP = new ColCircle(p.Pos, p.r);          

            
            if (this.ColType != null && ((this.ColType.collide(new ColCircle(p.Pos, p.r), out hix)) || ((this.ColType.GetType()==ccP.GetType()) && ((x+7.5 * x+7.5)+(y+7.5 * y+7.5)) <= (this.r * this.r))))
            {                                                                                                                                    //distance2D(p.Pos, new Vector2d(x+7.5, y+7.5))<=(this.r)
                double dist = 0;
                switch (this.id)
                {
                    case 00: //Nothing
                        break;
                    case 01: //Startpoint                    
                        break;
                    case 02: //Finish-Hole
                        dist = distance2D(new Vector2d(x + 7.5, y + 7.5), p.Pos);
                        if (dist > 0 && dist <= 7.5)
                        {
                            dist = Math.Abs(dist - 7.5);
                            //Console.WriteLine("# " + dist);
                            Vector2d hm = new Vector2d(p.Pos.X - (x + 7.5), p.Pos.Y - (y + 7.5)) * -0.4 * (dist / 8);
                            //Console.WriteLine("Vec: " + Math.Round(hm.X, 4) + " " + Math.Round(hm.Y, 4));
                            p.Vel += hm*0.3;
                        }
                        if ((p.Pos.X >= x + 6.5 && p.Pos.X <= x + 8.5) && (p.Pos.Y >= y + 6.5 && p.Pos.Y <= y + 8.5) && !p.trackFinished)
                        {
                            p.trackFinished = true;
                            Console.WriteLine("Player " + p.name + "has finished the track!");
                            p.Vel = new Vector2d(0,0);                           
                        }
                        break;
                    case 03: //Quicksand
                        break;
                    case 04: //Swamp
                        break;
                    case 05: //Teleporter Start Blue
                        foreach (MapTile mt in Map.map)
                        {
                            if (mt.O.id == 6)
                                p.Pos = new Vector2d(mt.G.x + 7.5, mt.G.y + 7.5);   //teleport to Target-positions center
                        }
                        break;
                    case 06: //Teleporter Target Blue
                        break;
                    case 07: //Teleporter Start Yellow
                        foreach (MapTile mt in Map.map)
                        {
                            if (mt.O.id == 8)
                                p.Pos = new Vector2d(mt.G.x + 7.5, mt.G.y + 7.5);   //teleport to Target-positions center
                        }
                        break;
                    case 08: //Teleporter Target Yellow
                        break;
                    case 09: //Teleporter Start Red
                        foreach (MapTile mt in Map.map)
                        {
                            if (mt.O.id == 10)
                                p.Pos = new Vector2d(mt.G.x + 7.5, mt.G.y + 7.5);   //teleport to Target-positions center
                        }
                        break;
                    case 10: //Teleporter Target Red
                        break;
                    case 11: //Teleporter Start Green
                        foreach (MapTile mt in Map.map)
                        {
                            if (mt.O.id == 12)
                                p.Pos = new Vector2d(mt.G.x + 7.5, mt.G.y + 7.5);   //teleport to Target-positions center
                        }
                        break;
                    case 12: //Teleporter Target Green
                        break;
                    case 13: //Gravity
                        {
                            dist = distance2D(new Vector2d(x + 7.5, y + 7.5), p.Pos);
                            if (dist > 0 && dist <= 42.5)
                            {
                                dist = Math.Abs(dist - 42.5);
                                //Console.WriteLine("# " + dist);
                                Vector2d hm = new Vector2d(p.Pos.X - (x + 7.5), p.Pos.Y - (y + 7.5)) * -0.002 * (dist / 16);
                                //Console.WriteLine("Vec: " + Math.Round(hm.X, 4) + " " + Math.Round(hm.Y, 4));
                                p.Vel += hm;
                            }
                        }
                        break;
                    case 14: //Anti-Gravity
                        dist = distance2D(new Vector2d(x + 7.5, y + 7.5), p.Pos);
                        if (dist > 0 && dist <= 42.5)
                        {
                            dist = Math.Abs(dist - 42.5);
                            //Console.WriteLine("# " + dist);
                            Vector2d hm = new Vector2d(p.Pos.X - (x + 7.5), p.Pos.Y - (y + 7.5)) * -0.002 * (dist / 16);
                            //Console.WriteLine("Vec: " + Math.Round(hm.X, 4) + " " + Math.Round(hm.Y, 4));
                            p.Vel -= hm;
                        }
                        break;
                    case 15: //Bomb small
                        p.Vel += p.Vel.Normalized() * -5;
                        Map.map[(short)y / 15, (short)x / 15].O = new Objectt(16, (short)x, (short)y);
                        break;
                    case 16: //Bomb small exploded
                        break;
                    case 17: //Bomb
                        //explode
                        p.Vel += p.Vel.Normalized() * -7.5;
                        Map.map[(short)y / 15, (short)x / 15].O = new Objectt(18, (short)x, (short)y);
                        break;
                    case 18: //Bomb exploded
                        break;
                    case 19: //Bricks 4hp
                        p.Vel += hix;
                        Map.map[(short)y/15, (short)x/15].O = new Objectt(20, (short)x, (short)y);
                        break;
                    case 20: //Bricks 3hp
                        p.Vel += hix;
                        Map.map[(short)y / 15, (short)x / 15].O = new Objectt(21, (short)x, (short)y);
                        break;
                    case 21: //Bricks 2hp
                        p.Vel += hix;
                        Map.map[(short)y / 15, (short)x / 15].O = new Objectt(22, (short)x, (short)y);
                        break;
                    case 22: //Bricks 1hp
                        p.Vel += hix;
                        Map.map[(short)y / 15, (short)x / 15].O = new Objectt(0, (short)x, (short)y);
                        break;
                    case 23:    //
                    case 24:    //unused
                    default: //null
                        break;
                }
            }
            MyImage.drawTileFromID(this.texture, id, 15, x, y);
        }

    }
}
