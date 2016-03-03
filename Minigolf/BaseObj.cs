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
    class BaseObj
    {       
        public short id { get; set; }

        public double x { get; set; }
        public double y { get; set; }
        public Byte w { get; set; }
        public Byte h { get; set; }
                
        public ColRect colRect;

        protected int texture;


        public virtual void tick()
        { ;}

        public virtual void tick(ref Player p)
        { ;}

        public bool getCol2Obj(ColRect obj1, ColRect obj2)
        {
            if ((obj1.Pos.X + obj1.Dim.X >= obj2.Pos.X && obj1.Pos.X <= obj2.Pos.X + obj2.Dim.X) && (obj1.Pos.Y + obj1.Dim.Y >= obj2.Pos.Y && obj1.Pos.Y <= obj2.Pos.Y + obj2.Dim.Y))
                return true;
            else
                return false;
        }

        public static bool colRectRect(ColRect obj1, ColRect obj2)
        {
            if ((obj1.Pos.X + obj1.Dim.X >= obj2.Pos.X && obj1.Pos.X <= obj2.Pos.X + obj2.Dim.X) && (obj1.Pos.Y + obj1.Dim.Y >= obj2.Pos.Y && obj1.Pos.Y <= obj2.Pos.Y + obj2.Dim.Y))
                return true;
            else
                return false;
        }

        public void refreshColRect()
        {
            colRect.Pos = new Vector2d((short)(x + colRect.OffSet.X), (short)(y + colRect.OffSet.Y));
        }

        public static double distance2D(Vector2d p1, Vector2d p2)
        {
            double x = Math.Abs(p1.X - p2.X);
            double y = Math.Abs(p1.Y - p2.Y);
            return Math.Sqrt((x * x) + (y * y));
        }

        public static double angle2D(Vector2d p1, Vector2d p2)
        {
            double x = Math.Abs(p1.X - p2.X);
            double y = Math.Abs(p1.Y - p2.Y);
            return Math.Sqrt((x * x) + (y * y));
        }

        public static bool col_circleLine2D(Vector2d c, double r, Vector2d l1, Vector2d l2)
        {
            if (c.X + r > Math.Min(l1.X, l2.X) && c.X - r < Math.Max(l1.X, l2.X))
            {
                Vector2d p1 = l1;
                Vector2d p2 = l2;

                // Normalize points
                p1.X -= c.X;
                p1.Y -= c.Y;
                p2.X -= c.X;
                p2.Y -= c.Y;

                double dx = p2.X - p1.X;
                double dy = p2.Y - p1.Y;
                double dr = ((dx * dx) + (dy * dy));
                double D = (p1.X * p2.Y) - (p2.X * p1.Y);

                double di = (r * r) * ((dr * dr)*(dr * dr)) - (D * D);
                Console.WriteLine("Distance: " + di);

                if (di < 0)
                    return false;
                else
                    return true;
            }
            return false;
        }

        public static bool col_circleTriangle2D(Vector2d c, double r, Vector2d t1, Vector2d t2, Vector2d t3)
        {
            if (col_circleLine2D(c, r, t1, t2) || col_circleLine2D(c, r, t2, t3) || col_circleLine2D(c, r, t3, t1))
                return true;
            else
                return false;
        }

        public static bool col_circleCircle2D(Vector2d p1, double r1, Vector2d p2, double r2)
        {
            double x = Math.Abs(p1.X - p2.X);
            double y = Math.Abs(p1.Y - p2.Y);
            //Console.WriteLine("r1: " + r1 + " r2: " + r2 + " x: " + x + " y: " + y);
            return ((x * x) + (y * y) <= (r1 + r2) * (r1 + r2));
        }

        public static bool col_circleRect(Vector2d circle, double circleR, ColRect rect_)
        {
            ColRect rect = new ColRect(new Vector2d(rect_.Pos.X + rect_.Dim.X / 2, rect_.Pos.Y + rect_.Dim.Y / 2),rect_.Dim,rect_.OffSet);

            Vector2d circleDistance = new Vector2d();
            circleDistance.X = Math.Abs(circle.X - rect.Pos.X);
            circleDistance.Y = Math.Abs(circle.Y - rect.Pos.Y);

            if (circleDistance.X > (rect.Dim.X / 2 + circleR)) { return false; }
            if (circleDistance.Y > (rect.Dim.Y / 2 + circleR)) { return false; }

            if (circleDistance.X <= (rect.Dim.X / 2)) { return true; }
            if (circleDistance.Y <= (rect.Dim.Y / 2)) { return true; }

            double cornerDistance_sq = ((circleDistance.X - rect.Dim.X / 2) * (circleDistance.X - rect.Dim.X / 2)) +
                                        ((circleDistance.Y - rect.Dim.Y / 2) * (circleDistance.Y - rect.Dim.Y / 2));

            return (cornerDistance_sq <= (circleR * circleR));            
        }










        private static void ClosestPointFromCircle(Vector2d c, double r, Vector2d l1, Vector2d l2, out Vector2d closest)
        {
            closest = Vector2d.Zero;
            Vector2d segmentVector = l2 - l1;
            if (segmentVector.LengthSquared <= 0)
                segmentVector = l1 - l2;

            Vector2d circleRelative = c - l1;

            if (segmentVector.LengthSquared <= 0)
                throw new ApplicationException("Length of line segment must be greater than 0");

            Vector2d segmentUnit;
            Vector2d.Normalize(ref segmentVector, out segmentUnit);

            double projection = Vector2d.Dot(circleRelative, segmentUnit);
            if (projection <= 0)
            {
                closest = l1;
                return;
            }
            if ((projection * projection) >= segmentVector.LengthSquared)
            {
                closest = l2;
                return;
            }

            Vector2d projectionVector = segmentUnit * projection;
            closest = projectionVector + l1;
        }

        public static void SolveCircleSegmentCollision(Vector2d c, double r, Vector2d l1, Vector2d l2, out Vector2d depth)
        {
            depth = Vector2d.Zero;

            Vector2d closest;
            ClosestPointFromCircle(c, r, l1, l2, out closest);

            Vector2d distanceVector = c - closest;
            if (distanceVector.LengthSquared > (r * r))
            {
                return;
            }
            if (distanceVector.LengthSquared <= 0)
            {
                throw new ApplicationException("Circle center is exactly on line segment");
            }

            Vector2d unitDist;
            Vector2d.Normalize(ref distanceVector, out unitDist);
            depth = unitDist * (r - distanceVector.Length);
        }










    }
}
