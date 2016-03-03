using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Minigolf  //Only purpose is to hold all the textures
{
    class Texture
    {
        public static int gfx =     MyImage.LoadTexture(RootThingy.exePath + @"\gfx\gfx.bmp");
        public static int grounds = MyImage.LoadTexture(RootThingy.exePath + @"\gfx\grounds.bmp");
        public static int objects = MyImage.LoadTexture(RootThingy.exePath + @"\gfx\objects.bmp");
        public static int player =  MyImage.LoadTexture(RootThingy.exePath + @"\gfx\player_greyscale.bmp");
        public static int walls =   MyImage.LoadTexture(RootThingy.exePath + @"\gfx\walls.bmp");
        public static int ASCII =   MyImage.LoadTexture(RootThingy.exePath + @"\gfx\ASCII-Characters_8x12.bmp");
        public static int Unicode = MyImage.LoadTexture(RootThingy.exePath + @"\gfx\Arial.bmp");
    }
}

