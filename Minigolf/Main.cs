using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;

namespace Minigolf
{
    class RootThingy
    {
        public static int windowX = 1024;
        public static int windowY = 576;
        public static string exePath = Environment.CurrentDirectory;
        public static int sceneX = 768; // (51 * 38 tiles)
        public static int sceneY = 570; // Actual draw-space for the playfield        
        private static bool fullscreen = false;

        public static bool debugInfo = true;
        public static bool editorMode = false;

        public static Color color = Color.White;

        public static bool loggedIn = false;
        public static int LocalPlayerId = -1;
        
        private static double zoom = 1;
        private static double camX;
        private static double camY;

        
        public static KeyboardState keyboard;
        public static MouseState mouse;
        public static Vector2d m1 = Vector2d.Zero;
        public static Vector2d m2 = Vector2d.Zero;


        private static bool mouseOverWindow = false;


        public static MySql sql = new MySql();
        
        //
        //Texture;

        public static Random rnd = new Random();

        public struct Point
        {
            public double x;
            public double y;
        }

        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        public struct unicode  //format to be used with BMFont (http://www.angelcode.com/products/bmfont/)
        {
            public int id;
            public UInt16 x;
            public UInt16 y;
            public UInt16 w;
            public UInt16 h;
            public UInt16 xOffset;  //Unused
            public UInt16 yOffset;  //Unused
            public UInt16 xAdvance;     //distance to next characters origin
            public UInt16 page;     //Unused
            public UInt16 chnl;    //Unused
        }

        public static unicode[] unicodeData = new unicode[65535]; 


        [STAThread]
        public static void Main()
        {

            OpenTK.DisplayDevice display = DisplayDevice.GetDisplay(0);



            System.Diagnostics.Process.GetCurrentProcess().ProcessorAffinity = (System.IntPtr)(System.Environment.ProcessorCount); //Assign the process to the last CPU-core of the System
            Console.WriteLine("We have " + System.Environment.ProcessorCount + " CPU-Cores.");

            

            Application.EnableVisualStyles();

            //CourseSetup CS = new CourseSetup(25, 2);
            //Application.Run(CS);

            //SPorMP choose = new SPorMP();
            //Application.Run(choose);

            //Lobby lobby = new Lobby();
            //Application.Run(lobby);

            loggedIn = true;    //remove this later            

            while (true)
            {
                //login.Invalidate();
                if (loggedIn)
                {
                    GameWindow game = new GameWindow();
                    {



                        game.Load += (sender, e) =>
                        {
                            // setup settings, load textures, sounds

                            unicode emptyChar;
                            emptyChar.id = -1;
                            emptyChar.x = 2040;
                            emptyChar.y = 1020;
                            emptyChar.w = 8;
                            emptyChar.h = 4;
                            emptyChar.xAdvance = 9;
                            emptyChar.xOffset = 0;
                            emptyChar.yOffset = 0;
                            emptyChar.page = 0;
                            emptyChar.chnl = 15;

                            for (int i = 0; i < unicodeData.Length; i++)
                                unicodeData[i] = emptyChar;

                            string uniCodeFile = "";
                            if (File.Exists(RootThingy.exePath + @"\gfx\Arial.txt") && File.Exists(RootThingy.exePath + @"\gfx\Arial.bmp"))
                                uniCodeFile = File.ReadAllText(RootThingy.exePath + @"\gfx\Arial.txt");
                            string[] uniCodeChars1 = uniCodeFile.Split(new char[]{'\n'});
                            List<string> uniCodeChars = new List<string>();
                            foreach (string str in uniCodeChars1)
                            {
                                if (!string.IsNullOrWhiteSpace(str))
                                    uniCodeChars.Add(str);
                            }

                            foreach (string line in uniCodeChars)
                            {
                                List<string> lineParts1 = line.Split('=').ToList();
                                List<string> lineParts = new List<string>();
                                foreach (string str in lineParts1)
                                {
                                    int tmp=0;
                                    //if(!string.IsNullOrWhiteSpace(str))     
                                    //    lineParts.Add(str);
                                    if (int.TryParse(str.Split(' ')[0], out tmp))
                                        lineParts.Add(tmp.ToString());
                                }

                                UInt16[] values = new UInt16[10];
                                for (int i = 0; i < 10; i++)
                                {
                                    UInt16.TryParse(lineParts[i].Split(' ').First(), out values[i]);
                                }
                                unicode dataSet = new unicode();
                                dataSet.id = values[0];
                                dataSet.x = values[1];
                                dataSet.y = values[2];
                                dataSet.w = values[3];
                                dataSet.h = values[4];
                                dataSet.xOffset = values[5];
                                dataSet.yOffset = values[6];
                                dataSet.xAdvance = values[7];
                                dataSet.page = values[8];
                                dataSet.chnl = values[9];
                                unicodeData[dataSet.id] = dataSet;
                            }


                            game.X = 32;
                            game.Y = 16;
                            game.VSync = VSyncMode.Off;
                            game.Width = windowX;
                            game.Height = windowY;
                            game.WindowBorder = WindowBorder.Fixed; //Disables the resizable windowframe
                            GL.Enable(EnableCap.Blend);                                                     //These lines
                            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);  //enable transparency using alpha-channel
                            game.TargetRenderFrequency = 60;
                        };

                        Map karte = new Map();


                        game.Resize += (sender, e) =>
                        {
                            //sceneX = game.Height;
                            //sceneY = game.Width;
                            GL.Viewport(0, 0, windowX, windowY);  //unZoomed
                        };

                        game.MouseEnter += (sender, e) =>
                        { mouseOverWindow = true; };

                        game.MouseLeave += (sender, e) =>
                        { mouseOverWindow = false; };
                                                
                        game.Mouse.ButtonDown += (sender, e) =>
                            {
                                m1 = new Vector2d(game.Mouse.X, game.Mouse.Y);
                            };
                        

                        game.Mouse.ButtonUp += (sender, e) =>
                            {
                                if (!game.Mouse[MouseButton.Left] && 
                                    Map.players[Map.currentPlayerId] != null && 
                                    Map.players[Map.currentPlayerId].Vel == Vector2d.Zero && 
                                    !Map.players[Map.currentPlayerId].hasHitBall)
                                {
                                    m2 = new Vector2d(game.Mouse.X, game.Mouse.Y);
                                    Map.players[Map.currentPlayerId].Vel = Math.Min((m2 - m1).LengthSquared, 200 * 200) * (m2 - m1).Normalized() / 900 * -1;
                                    Map.players[Map.currentPlayerId].hasHitBall = true;
                                    Map.scores.IncrementScore(Map.currentPlayerId, Map.currentMap);
                                }
                            };
                        
                        
                        game.UpdateFrame += (sender, e) =>
                        {
                            // add game logic, input handling
                            if (Map.players[Map.currentPlayerId] != null)
                            {
                                if (mouseOverWindow)
                                    mouse = Mouse.GetState();

                                m1 = Map.players[Map.currentPlayerId].Pos;

                                if (game.Mouse[MouseButton.Left])
                                {
                                    m2 = new Vector2d(game.Mouse.X, game.Mouse.Y);
                                    //MyImage.endDraw2D();
                                    //GL.Begin(PrimitiveType.Lines);
                                    //GL.Vertex2(m1);
                                    //GL.Vertex2(m1 + Map.players[Map.currentPlayerId].Vel);
                                    //GL.End();
                                    //MyImage.beginDraw2D();
                                }
                            }



                            keyboard = Keyboard.GetState();

                            if (keyboard[Key.Escape])
                            {
                                game.Exit();
                            }

                            if (keyboard[Key.F12])
                            {
                                debugInfo = !debugInfo;
                                Thread.Sleep(200);
                            }

                            if (keyboard[Key.F11])
                            {
                                editorMode = !editorMode;
                                Thread.Sleep(200);
                            }


                            if (keyboard[Key.KeypadAdd])
                                zoom += 0.01;
                            if (keyboard[Key.KeypadMinus])
                                zoom -= 0.01;
                            if (keyboard[Key.Keypad4])
                                camX -= 01;
                            if (keyboard[Key.Keypad6])
                                camX += 01;
                            if (keyboard[Key.Keypad8])
                                camY -= 01;
                            if (keyboard[Key.Keypad5])
                                camY += 01;


                            if (keyboard[Key.F])
                            {
                                fullscreen = !fullscreen;
                                Thread.Sleep(200);

                                if (fullscreen)
                                {
                                    game.WindowState = WindowState.Fullscreen;

                                    display.ChangeResolution(windowX, windowY, 32, 60);
                                }
                                else
                                {
                                    game.WindowState = WindowState.Normal;
                                    display.RestoreResolution();
                                }
                            }


                            game.Title = ("Minigolf - FPS: " + (int)(game.RenderFrequency) + " Mouse[" + game.Mouse.X + " ; " + game.Mouse.Y + "]");
                        };

                        game.RenderFrame += (sender, e) =>
                        {
                            // render graphics
                            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                            //GL.Scale(Map.temp, Map.temp, 0);
                            GL.MatrixMode(MatrixMode.Projection);
                            //GL.MatrixMode(MatrixMode.Modelview);
                            GL.LoadIdentity();

                            //Console.WriteLine("Cam (X:" + camX + " |Y:" + camY + ")");


                            GL.Viewport((int)(0), (int)(0), windowX, windowY);
                            //GL.LineWidth((float)zoom);
                            GL.LineWidth(1);
                            game.Width = windowX;
                            game.Height = windowY;
                            Vector2d mouseVector = new Vector2d(game.Mouse.X, game.Mouse.Y);


                            GL.Ortho((int)0, (int)windowX, (int)windowY, (int)0, -1000, 1000);  //Render  distant objects smaller
                            GL.MatrixMode(MatrixMode.Modelview);
                            GL.PopMatrix();
                            GL.PushMatrix();

                            MyImage.beginDraw2D();

                            GL.Translate(-camX, -camY, 0);  //glTranslate (add offset) the zoomed scene
                            GL.Scale(zoom, zoom, 1);        //glZomm the scene


                            karte.process(game.Mouse.X, game.Mouse.Y);

                            //GL.ColorMask(false,false,false,false);  //SETS if RGBA-Parts are drawn to frameBuffer or not

                            //GL.ColorMask(true, true, true, true);   
                            //GL.Enable(EnableCap.StencilTest);
                            //GL.StencilFunc(StencilFunction.Equal, 1, 1);
                            //GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);


                            

                            MyImage.endDraw2D();

                            game.SwapBuffers();
                        };

                        // Run the game at 60 updates per second
                        game.Run(60);
                    }
                }
            }
        }
        
        //End of Root =============================================================






    }
}
