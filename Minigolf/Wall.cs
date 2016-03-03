using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace Minigolf
{
    class Wall : BaseObj
    {
        public ColType ColType1 { get; set; }  
        public ColType ColType2 { get; set; }  

        public Wall(Byte id, short x, short y)
        {
            this.x = x;
            this.y = y;
            Vector2d p = new Vector2d(x, y);

            this.texture = Texture.walls;
            
            switch (id)
            {
                default:
                case 0:     //Nothing
                    this.id = id;
                    this.ColType1 = new ColEmpty(new Vector2d(0, 0));
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0));
                    break;
                case 1:     //NW big circle region
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(15, -90, 0, new Vector2d(15, 15) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p });
                    break;
                case 2:     //NE big circle region
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(15, 0, 90, new Vector2d(0, 15) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(0, 15) + p, new Vector2d(15, 15) + p });
                    break;
                case 3:     //SE big circle region
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(15, 90, 180, new Vector2d(0, 0) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(0, 15) + p, new Vector2d(0, 0) + p, new Vector2d(15, 0) + p });
                    break;
                case 4:     //SW big circle region
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(15, 180, 270, new Vector2d(15, 0) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p });
                    break;
                case 5:     //NW big circle region negative
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(15, -90, 0, new Vector2d(15, 15) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(0, 15) + p, new Vector2d(0, 0) + p, new Vector2d(15, 0) + p }); 
                    break;
                case 6:     //NE big circle region negative
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(15, 0, 90, new Vector2d(0, 15) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p });
                    break;
                case 7:     //SE inner big circle region
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(15, 90, 180, new Vector2d(0, 0) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(0, 15) + p, new Vector2d(15, 15) + p, new Vector2d(15, 0) + p });
                    break;
                case 8:     //SW inner big circle region
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(15, 180, 270, new Vector2d(15, 0) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(0, 15) + p, new Vector2d(15, 15) + p });
                    break;
                case 9:     //NW triangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 15) + p, new Vector2d(15, 15) + p, new Vector2d(15, 0) + p, new Vector2d(0, 15) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 10:     //NE triangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 11:     //SE triangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(0, 15) + p, new Vector2d(15, 0) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 12:     //SW triangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 15) + p, new Vector2d(15, 0) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 13:     //W triangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(7.5, 7.5) + p, new Vector2d(15, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 14:     //N triangle
                    this.id = id;
                    //this.ColType1 = new ColTriangle(new Vector2d(0, 15) + pos, new Vector2d(15, 15) + pos, new Vector2d(7, 7) + pos);
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 15) + p, new Vector2d(15, 15) + p, new Vector2d(7.5, 7.5) + p, new Vector2d(0, 15) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 15:     //E Triangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 15) + p, new Vector2d(0, 0) + p, new Vector2d(7.5, 7.5) + p, new Vector2d(0, 15) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 16:     //S Triangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(7.5, 7.5) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 17:     //W Triangle negative
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(7.5, 7.5) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 18:     //N Triangle negative
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(7.5, 7.5) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 19:     //E Triangle negative
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(7.5, 7.5) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 20:     //S Triangle negative
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(7.5, 7.5) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 21:     //W rounded rectangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(7, 180, 360, new Vector2d(7, 7) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(7, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(7, 15) + p });
                    break;
                case 22:     //N rounded rectangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(7, -90, 90, new Vector2d(7, 7) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(0, 7) + p, new Vector2d(0, 15) + p, new Vector2d(15, 15) + p, new Vector2d(15, 7) + p });
                    break;
                case 23:     //E rounded rectangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(7.5, 0, 180, new Vector2d(7.5, 7.5) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(7, 0) + p, new Vector2d(0, 0) + p, new Vector2d(0, 15) + p, new Vector2d(7, 15) + p });
                    break;
                case 24:     //S rounded rectangle
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(7.5, 90, 270, new Vector2d(7.5, 7.5) + p));
                    this.ColType2 = new ColLineloop(new Vector2d[] { new Vector2d(0, 7) + p, new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 7) + p });
                    break;
                case 25:     //SE half angle
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 7) + p, new Vector2d(8, 7) + p, new Vector2d(8, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 7) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 26:     //SW half angle
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(7, 0) + p, new Vector2d(7, 7) + p, new Vector2d(15, 7) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 27:     //NW half angle 
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 6) + p, new Vector2d(8, 6) + p, new Vector2d(8, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 28:     //NE half angle 
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(8, 15) + p, new Vector2d(8, 6) + p, new Vector2d(0, 6) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 29:     //SE half corner
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(8, 7) + p, new Vector2d(15, 7) + p, new Vector2d(15, 15) + p, new Vector2d(8, 15) + p, new Vector2d(8, 7) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 30:     //SW half corner
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 7) + p, new Vector2d(7, 7) + p, new Vector2d(7, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 7) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 31:     //NW half corner
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(7, 0) + p, new Vector2d(7, 6) + p, new Vector2d(0, 6) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 32:     //NE half corner
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(8, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 6) + p, new Vector2d(8, 6) + p, new Vector2d(8, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 33:     //E half 
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(8, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(8, 15) + p, new Vector2d(8, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 34:     //S half 
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 7) + p, new Vector2d(15, 7) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 7) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 35:     //W half 
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(7, 0) + p, new Vector2d(7, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 36:     //N half 
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 6) + p, new Vector2d(0, 6) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 37:     //full square 1
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 38:     //full square 2
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 0) + p, new Vector2d(15, 0) + p, new Vector2d(15, 15) + p, new Vector2d(0, 15) + p, new Vector2d(0, 0) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 39:     //circle
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(7, 0, 360, new Vector2d(7, 7) + p));
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;      
                case 40:     //small circle
                    this.id = id;
                    this.ColType1 = new ColLineloop(positions_of_circlepart(5, 0, 360, new Vector2d(7.5, 7.5) + p));
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
                case 41:     //Diamond
                    this.id = id;
                    this.ColType1 = new ColLineloop(new Vector2d[] { new Vector2d(0, 7.5) + p, new Vector2d(7.5, 0) + p, new Vector2d(15, 7.5) + p, new Vector2d(7.5, 15) + p, new Vector2d(0, 7.5) + p });
                    this.ColType2 = new ColEmpty(new Vector2d(0, 0) + p);
                    break;
            }
            

        }

        public override void tick()
        {
            MyImage.drawTileFromID(this.texture, id, 15, x, y);
        }


        public static Vector2d[] positions_of_circlepart(double r, short angleStart, short angleEnd, Vector2d offset)
        {
            List<Vector2d> vectors = new List<Vector2d>();
            for (int i = angleStart; i <= angleEnd; i += 10)    //2
                vectors.Add(new Vector2d(offset.X + Math.Cos((i - 90) * 0.0174532925) * r, offset.Y + Math.Sin((i - 90) * 0.0174532925) * r));

            return vectors.ToArray();
        }

    }
}
