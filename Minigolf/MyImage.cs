﻿using System;
using System.IO;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Minigolf
{
    public class MyImage : GameWindow
    {
        public static int activeTexture;
        public static bool baseColor_is_fixedWhite = true;  //true=always true to original colors,,, false=tinted with last set GL.Color()
        private static int ASCII;
        private static bool draw2D;
        

        public MyImage()
            : base(800, 600, GraphicsMode.Default, "Hoard of Upgrades")
        {
            //GL.ClearColor(0, 0.1f, 0.4f, 1);
            //ASCII = LoadTexture(RootThingy.exePath + @"\gfx\tilesets\smb1nes.bmp");
        }

        public struct Point
        {
            public double x;
            public double y;
        }

        public static int LoadTexture(string file)
        {

            if (!File.Exists(file))
            {
                Console.WriteLine("Texture '" + file + "' could not be loaded!");
                Console.ReadKey();
                return 0;
            }
            
            Bitmap bitmap = new Bitmap(file);
            bitmap.MakeTransparent(Color.Magenta);
            int tex;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);



            return tex;
        }


        public static int LoadTexture(Bitmap bitmap)
        {
            
            bitmap.MakeTransparent(Color.Magenta);
            int tex;
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            GL.GenTextures(1, out tex);
            GL.BindTexture(TextureTarget.Texture2D, tex);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            return tex;
        }


        public static Bitmap BMPfromTextureID(int id)
        {
            int w, h;
            GL.Color4(1, 1, 1, 1.0f);
            GL.BindTexture(TextureTarget.Texture2D, id);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out w);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out h);
            if (w == 0 || h == 0)   //Don't try empty / not existing textures
                return null;

            Bitmap bmp = new Bitmap(w, h);
              
            System.Drawing.Imaging.BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(0, 0, w, h), 
            ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            
            GL.GetTexImage(TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgr, PixelType.UnsignedByte, data.Scan0);
            
            bmp.UnlockBits(data);
            //bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            //bmp.Save(@"C:\test\"+id+".bmp");
            return bmp;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// BEGIN DRAW 2D
        public static void beginDraw2D()
        {
            if (!draw2D)
            {
                GL.Disable(EnableCap.Lighting);
                GL.Enable(EnableCap.Texture2D);
                draw2D = true;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// END DRAW 2D
        public static void endDraw2D()
        {
            if (draw2D)
            {
                GL.Disable(EnableCap.Texture2D);
                draw2D = false;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// DRAW IMAGE WITH ROTATION
        public static void drawImageRot(int image, double x, double y, double angle = 0, double z = 0)
        {
            int w;
            int h;
            Point p1 = new Point();
            if(baseColor_is_fixedWhite)
                GL.Color4(1, 1, 1, 1.0f);

            if (activeTexture != image)
                GL.BindTexture(TextureTarget.Texture2D, image);
            activeTexture = image;

            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out w);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out h);
            
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0); 
            p1=rotate(-w/2, -h/2, angle);
            GL.Vertex3(p1.x + x, p1.y + y, z);

            GL.TexCoord2(1, 0);
            p1=rotate(w/2, -h/2, angle);
            GL.Vertex3(p1.x + x, p1.y + y, z);

            GL.TexCoord2(1, 1);
            p1=rotate(w/2, h/2, angle);
            GL.Vertex3(p1.x + x, p1.y + y, z);

            GL.TexCoord2(0, 1);
            p1 = rotate(-w / 2,h/2, angle);
            GL.Vertex3(p1.x + x, p1.y + y, z);
            GL.End();

            
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// DRAW IMAGE
        public static void drawImage(int image, double x, double y, bool flipV = false, bool flipH = false, double z = 0)
        {
            int w;
            int h;
            if(baseColor_is_fixedWhite)
                GL.Color4(1, 1, 1, 1.0f);

            if (activeTexture != image)
                GL.BindTexture(TextureTarget.Texture2D, image);
            activeTexture = image;

            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out w);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out h);

            GL.Begin(PrimitiveType.Quads);
            if(!flipV && !flipH)    //No flip
            {
                GL.TexCoord2(0, 0);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(1, 0);
                GL.Vertex3(x + w, y, z);
                GL.TexCoord2(1, 1);
                GL.Vertex3(x + w, y + h, z);
                GL.TexCoord2(0, 1);
                GL.Vertex3(x, y + h, z);
            }
            else if (flipV && !flipH)   //Vertical(X) flip 
            {
                GL.TexCoord2(1, 0);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(0, 0);
                GL.Vertex3(x + w, y, z);
                GL.TexCoord2(0, 1);
                GL.Vertex3(x + w, y + h, z);
                GL.TexCoord2(1, 1);
                GL.Vertex3(x, y + h, z);
            }
            else if (!flipV && flipH)    //Horizontal(y) flip
            {
                GL.TexCoord2(0, 1);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(1, 1);
                GL.Vertex3(x + w, y, z);
                GL.TexCoord2(1, 0);
                GL.Vertex3(x + w, y + h, z);
                GL.TexCoord2(0, 0);
                GL.Vertex3(x, y + h, z);
            }
            else if (flipV && flipH)    //Vertical(x) + Horizontal(y) flip
            {
                GL.TexCoord2(1, 1);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(0, 1);
                GL.Vertex3(x + w, y, z);
                GL.TexCoord2(0, 0);
                GL.Vertex3(x + w, y + h, z);
                GL.TexCoord2(1, 0);
                GL.Vertex3(x, y + h, z);
            }
            GL.End();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// DRAW TILE AT POSITION FROM A TILESET
        public static void drawTileFromXY(int image, int tileX, int tileY, int tileSize, double x, double y, bool flipV = false, bool flipH = false, double z = 0)
        {
            int w;
            int h;
            if (activeTexture != image)
                GL.BindTexture(TextureTarget.Texture2D, image);
            activeTexture = image;
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out w);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out h);

            double tileXX = 0;
            double tileYY = 0;
            if (tileX * tileSize <= w)
                tileXX = tileX * tileSize;

            if (tileY * tileSize <= h)
                tileYY = (tileY * tileSize);

            GL.Begin(PrimitiveType.Quads);
            if(baseColor_is_fixedWhite)
                GL.Color4(1, 1, 1, 1.0f);

            if (!flipV && !flipH)    //No flip
            {
                GL.TexCoord2(tileXX / w, tileYY / h);
                GL.Vertex3(x, y, z);
                GL.TexCoord2((tileXX + tileSize) / w, tileYY / h);
                GL.Vertex3(x + tileSize, y, z);
                GL.TexCoord2((tileXX + tileSize) / w, (tileYY + tileSize) / h);
                GL.Vertex3(x + tileSize, y + tileSize, z);
                GL.TexCoord2(tileXX / w, (tileYY + tileSize) / h);
                GL.Vertex3(x, y + tileSize, z);
            }
            else if (flipV && !flipH)   //Vertical(X) flip 
            {
                GL.TexCoord2((tileXX + tileSize) / w, tileYY / h);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(tileXX / w, tileYY / h);
                GL.Vertex3(x + tileSize, y, z);
                GL.TexCoord2(tileXX / w, (tileYY + tileSize) / h);
                GL.Vertex3(x + tileSize, y + tileSize, z);
                GL.TexCoord2((tileXX + tileSize) / w, (tileYY + tileSize) / h);
                GL.Vertex3(x, y + tileSize, z);
            }
            else if (!flipV && flipH)    //Horizontal(y) flip
            {
                GL.TexCoord2(tileXX / w, (tileYY + tileSize) / h);
                GL.Vertex3(x, y, z);
                GL.TexCoord2((tileXX + tileSize) / w, (tileYY + tileSize) / h);
                GL.Vertex3(x + tileSize, y, z);
                GL.TexCoord2((tileXX + tileSize) / w, tileYY / h);
                GL.Vertex3(x + tileSize, y + tileSize, z);
                GL.TexCoord2(tileXX / w, tileYY / h);
                GL.Vertex3(x, y + tileSize, z);
            }
            else if (flipV && flipH)    //Vertical(x) + Horizontal(y) flip
            {
                GL.TexCoord2((tileXX + tileSize) / w, (tileYY + tileSize) / h);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(tileXX / w, (tileYY + tileSize) / h);
                GL.Vertex3(x + tileSize, y, z);
                GL.TexCoord2(tileXX / w, tileYY / h);
                GL.Vertex3(x + tileSize, y + tileSize, z);
                GL.TexCoord2((tileXX + tileSize) / w, tileYY / h);
                GL.Vertex3(x, y + tileSize, z);
            }
            GL.End();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// DRAW TILE-ID FROM A TILESET
        public static void drawTileFromID(int image, int tileID, int tileSize, double x, double y, bool flipV = false, bool flipH = false, double z = 0)
        {
            int w;
            int h;
            if (activeTexture != image)
                GL.BindTexture(TextureTarget.Texture2D, image);
            activeTexture = image;
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out w);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out h);

            double tileX = 0;
            double tileY = 0;
            if (tileID * tileSize <= w * h)
                tileX = tileID * tileSize;
            tileY = 0;
            if (tileID * tileSize >= w)
            {
                tileX = (tileID * tileSize) - ((tileID * tileSize) / w) * w;
                tileY = ((tileID * tileSize) / w) * tileSize;
            }

            GL.Begin(PrimitiveType.Quads);
            if(baseColor_is_fixedWhite)
                GL.Color4(1, 1, 1, 1.0f);

            if (!flipV && !flipH)    //No flip
            {
                GL.TexCoord2(tileX / w, tileY / h);
                GL.Vertex3(x, y, z);
                GL.TexCoord2((tileX + tileSize) / w, tileY / h);
                GL.Vertex3(x + tileSize, y, z);
                GL.TexCoord2((tileX + tileSize) / w, (tileY + tileSize) / h);
                GL.Vertex3(x + tileSize, y + tileSize, z);
                GL.TexCoord2(tileX / w, (tileY + tileSize) / h);
                GL.Vertex3(x, y + tileSize, z);
            }
            else if (flipV && !flipH)   //Vertical(X) flip 
            {
                GL.TexCoord2((tileX + tileSize) / w, tileY / h);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(tileX / w, tileY / h);
                GL.Vertex3(x + tileSize, y, z);
                GL.TexCoord2(tileX / w, (tileY + tileSize) / h);
                GL.Vertex3(x + tileSize, y + tileSize, z);
                GL.TexCoord2((tileX + tileSize) / w, (tileY + tileSize) / h);
                GL.Vertex3(x, y + tileSize, z);
            }
            else if (!flipV && flipH)    //Horizontal(y) flip
            {
                GL.TexCoord2(tileX / w, (tileY + tileSize) / h);
                GL.Vertex3(x, y, z);
                GL.TexCoord2((tileX + tileSize) / w, (tileY + tileSize) / h);
                GL.Vertex3(x + tileSize, y, z);
                GL.TexCoord2((tileX + tileSize) / w, tileY / h);
                GL.Vertex3(x + tileSize, y + tileSize, z);
                GL.TexCoord2(tileX / w, tileY / h);
                GL.Vertex3(x, y + tileSize, z);
            }
            else if (flipV && flipH)    //Vertical(x) + Horizontal(y) flip
            {
                GL.TexCoord2((tileX + tileSize) / w, (tileY + tileSize) / h);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(tileX / w, (tileY + tileSize) / h);
                GL.Vertex3(x + tileSize, y, z);
                GL.TexCoord2(tileX / w, tileY / h);
                GL.Vertex3(x + tileSize, y + tileSize, z);
                GL.TexCoord2((tileX + tileSize) / w, tileY / h);
                GL.Vertex3(x, y + tileSize, z);
            }
            GL.End();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// DRAW TILE
        public static void drawTileFrame(int image, int frameID, int frameCount, double x, double y, bool flipV = false, bool flipH = false, double z = 0)
        {
            int w;
            int h;
            if (activeTexture != image)
                GL.BindTexture(TextureTarget.Texture2D, image);
            activeTexture = image;
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out w);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out h);

            while (frameID > frameCount - 1)
            {
                frameID -= frameCount;
            }
            GL.Begin(PrimitiveType.Quads);
            if(baseColor_is_fixedWhite)
                GL.Color4(1, 1, 1, 1.0f);

            if (!flipV && !flipH)    //No flip
            {
                GL.TexCoord2((frameID) / (double)frameCount, 0);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(((frameID + 1) / (double)frameCount), 0);
                GL.Vertex3(x + (w / frameCount), y, z);
                GL.TexCoord2(((frameID + 1) / (double)frameCount), 1);
                GL.Vertex3(x + (w / frameCount), y + h, z);
                GL.TexCoord2((frameID) / (double)frameCount, 1);
                GL.Vertex3(x, y + h, z);
            }
            else if (flipV && !flipH)   //Vertical(X) flip 
            {
                GL.TexCoord2((frameID+1) / (double)frameCount, 0);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(((frameID) / (double)frameCount), 0);
                GL.Vertex3(x + (w / frameCount), y, z);
                GL.TexCoord2(((frameID) / (double)frameCount), 1);
                GL.Vertex3(x + (w / frameCount), y + h, z);
                GL.TexCoord2((frameID+1) / (double)frameCount, 1);
                GL.Vertex3(x, y + h, z);
            }
            else if (!flipV && flipH)    //Horizontal(y) flip
            {
                GL.TexCoord2((frameID) / (double)frameCount, 1);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(((frameID + 1) / (double)frameCount), 1);
                GL.Vertex3(x + (w / frameCount), y, z);
                GL.TexCoord2(((frameID + 1) / (double)frameCount), 0);
                GL.Vertex3(x + (w / frameCount), y + h, z);
                GL.TexCoord2((frameID) / (double)frameCount, 0);
                GL.Vertex3(x, y + h, z);
            }
            else if (flipV && flipH)    //Vertical(x) + Horizontal(y) flip
            {
                GL.TexCoord2((frameID + 1) / (double)frameCount, 1);
                GL.Vertex3(x, y, z);
                GL.TexCoord2(((frameID) / (double)frameCount), 1);
                GL.Vertex3(x + (w / frameCount), y, z);
                GL.TexCoord2(((frameID) / (double)frameCount), 0);
                GL.Vertex3(x + (w / frameCount), y + h, z);
                GL.TexCoord2((frameID + 1) / (double)frameCount, 0);
                GL.Vertex3(x, y + h, z);
            }
            GL.End();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// DRAW TILE
        public static void drawImagePart(int image, int x, int y,int PartX, int PartY, int PartW, int PartH, double z = 0)
        {
            int w;
            int h;
            if (activeTexture != image)
                GL.BindTexture(TextureTarget.Texture2D, image);
            activeTexture = image;
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out w);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out h);

            if (baseColor_is_fixedWhite)
                GL.Color4(1, 1, 1, 1.0f);

            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2((PartX) / (double)w, (PartY) / (double)h);
            GL.Vertex3(x, y, z);

            GL.TexCoord2((PartX + PartW) / (double)w, (PartY) / (double)h);
            GL.Vertex3(x + PartW, y, z);

            GL.TexCoord2((PartX + PartW) / (double)w, (PartY + PartH) / (double)h);
            GL.Vertex3(x + PartW, y + PartH, z);

            GL.TexCoord2((PartX) / (double)w, (PartY + PartH) / (double)h);
            GL.Vertex3(x, y + PartH, z);

            GL.End();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////// DRAW TEXT
        public static void drawText(string text, double x, double y, Color textColor, int image, double z=0)
        {
            char[] textArr = new char[text.Length];
            textArr = text.ToCharArray();

            int w;
            int h;
            GL.Color4(textColor);

            if (activeTexture != image)
                GL.BindTexture(TextureTarget.Texture2D, image);
            activeTexture = image;
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureWidth, out w);
            GL.GetTexLevelParameter(TextureTarget.Texture2D, 0, GetTextureParameter.TextureHeight, out h);

            if (image == Texture.ASCII)
            {
                GL.Begin(PrimitiveType.Quads);
                foreach (char character in textArr)
                {
                    GL.TexCoord2(((int)character) / 256.0, 0);
                    GL.Vertex2(x, y);
                    GL.TexCoord2((((int)character + 1) / 256.0), 0);
                    GL.Vertex2(x + 8, y);
                    GL.TexCoord2((((int)character + 1) / 256.0), 1);
                    GL.Vertex2(x + 8, y + h);
                    GL.TexCoord2(((int)character) / 256.0, 1);
                    GL.Vertex2(x, y + h);
                    x += 8;
                }
                GL.End();
            }

            if (image == Texture.Unicode)
            {
                int PartX, PartY, PartW, PartH;
                GL.Begin(PrimitiveType.Quads);
                foreach (char character in textArr)
                {
                    PartX = RootThingy.unicodeData[(UInt16)character].x;
                    PartY = RootThingy.unicodeData[(UInt16)character].y;
                    PartW = RootThingy.unicodeData[(UInt16)character].w;
                    PartH = RootThingy.unicodeData[(UInt16)character].h;

                    GL.TexCoord2((PartX) / (double)w, (PartY) / (double)h);
                    GL.Vertex3(x, y, z);
                    GL.TexCoord2((PartX + PartW) / (double)w, (PartY) / (double)h);
                    GL.Vertex3(x + PartW, y, z);
                    GL.TexCoord2((PartX + PartW) / (double)w, (PartY + PartH) / (double)h);
                    GL.Vertex3(x + PartW, y + PartH, z);
                    GL.TexCoord2((PartX) / (double)w, (PartY + PartH) / (double)h);
                    GL.Vertex3(x, y + PartH, z);
                    
                    x += RootThingy.unicodeData[(int)character].xAdvance;
                }
                GL.End();
            }
        }



        public static Point rotate(Point p, double angle)  //Rotate a point to the specified angle
        {
            angle = angle * 0.0174532925;
            Point rotated = new Point();
            rotated.x = p.x * Math.Cos(angle) + p.y * Math.Sin(angle);
            rotated.y = p.y * Math.Cos(angle) - p.x * Math.Sin(angle);
            return rotated;
        }

        public static Point rotate(double x, double y, double angle)   //Rotate a xy-coordinate to the specified angle
        {
            angle = angle * 0.0174532925;
            Point rotated = new Point();
            rotated.x = x * Math.Cos(angle) + y * Math.Sin(angle);
            rotated.y = y * Math.Cos(angle) - x * Math.Sin(angle);
            return rotated;
        }


    }

}
