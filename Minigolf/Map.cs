using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Minigolf
{
    class Map
    {
        public static double mouseX;
        public static double mouseY;
        public static int mouseWheelLast, mouseWheel;

        public static MapTile[,] map = new MapTile[38, 51]; //y,X !!!

        public static Scoreboard scores;

        private static byte[] selectLimits = new byte[3] { 15, 41, 24 };
        private static sbyte[] currentIDs = new sbyte[3] { 0, 0, 0 };
        private static byte selector = 0;




        private static byte timeMSec;
        private static short timeSec;
        public static short timeSecPerTurn;

        public static bool BallBallCollision;
        public static bool waterTouchResetsBall;

        public static byte currentPlayerId = 0;
        public static Player[] players = new Player[255];

        public static byte currentMap;
        public static List<String> mapList = new List<string>();




        public static List<Vector2d> lineLooop = new List<Vector2d>();

        string uncodetest = "";
        byte cnt = 0;

        public Map()
        {
            timeSecPerTurn = 15;



            players[0] = new Player(new Vector2d(100, 100), "Player1");
            players[1] = new Player(new Vector2d(120, 100), "Player2", "0.0.0.0", Color.Aqua);
            players[2] = new Player(new Vector2d(140, 100), "Player3", "0.0.0.0", Color.Brown);
            players[0].myTurn = true;

            scores = new Scoreboard(players, 4);

            Random rnd = new Random();

            for (short yy = 0; yy < map.GetLength(0); yy++)
            {
                for (short xx = 0; xx < map.GetLength(1); xx++)
                {
                    map[yy, xx] = new MapTile(
                        (short)(xx * 15), (short)(yy * 15),
                        new Ground(0, (short)(xx * 15), (short)(yy * 15)),
                        new Wall(0, (short)(xx * 15), (short)(yy * 15)),
                        new Objectt(0, (short)(xx * 15), (short)(yy * 15))
                        );
                }
            }

            //map[16, 16].O = new Objectt(1, 16 * 15, 16 * 15);

            //map[2, 2].O = new Objectt(2, 2 * 15, 2 * 15); 
            //map[10, 10].O = new Objectt(13, 10 * 15, 10 * 15);
            //map[11, 20].O = new Objectt(14, 20 * 15, 11 * 15);

            //map[8, 13].O = new Objectt(15, 13 * 15, 8 * 15);
            //map[8, 16].O = new Objectt(17, 16 * 15, 8 * 15);

            //map[2, 3].O = new Objectt(19, 3 * 15, 2 * 15);
            //map[2, 5].O = new Objectt(20, 5 * 15, 2 * 15);
            //map[2, 7].O = new Objectt(21, 7 * 15, 2 * 15);
            //map[2, 9].O = new Objectt(22, 9 * 15, 2 * 15);


            string temp = "";
            for (short i = 0; i < short.MaxValue-1; i++)
            {
                //Console.Write((char)i);
                temp += (char)i;
            }
            File.WriteAllText("Unicode.txt", temp);

            for (int i = 0; i < 65534; i++)
                uncodetest+= (char)i;
        }





        public void process(int mausX, int mausY)
        {

            GL.Color4(1.0f, 1.0f, 1.0f, 1.0f);
            for (short yy = 0; yy < map.GetLength(0); yy++)
            {
                for (short xx = 0; xx < map.GetLength(1); xx++)
                {
                    if (map[yy, xx] != null)
                    {
                        if (map[yy, xx].G != null)
                            map[yy, xx].G.tick();

                        if (map[yy, xx].W != null)
                            map[yy, xx].W.tick();

                        if (map[yy, xx].O != null && players[currentPlayerId] != null)
                            map[yy, xx].O.tick(ref players[currentPlayerId]);
                    }

                }
            }




            if (RootThingy.debugInfo)
            {
                MyImage.endDraw2D();
                GL.Color4(1.0f, 0.0f, 0.0f, 1.0f);
                for (short yy = 0; yy < map.GetLength(0); yy++)
                {
                    for (short xx = 0; xx < map.GetLength(1); xx++)
                    {
                        if (map[yy, xx] != null)
                        {
                            if (map[yy, xx].W.ColType1 != null)
                            {
                                map[yy, xx].W.ColType1.render();
                            }

                            if (map[yy, xx].W.ColType2 != null)
                            {
                                map[yy, xx].W.ColType2.render();
                            }

                            if (map[yy, xx].O != null && map[yy, xx].O.ColType != null)
                            {
                                map[yy, xx].O.ColType.render();
                            }
                        }
                    }
                }
                MyImage.beginDraw2D();
            }

            timeMSec++;
            if (timeMSec >= 60)
            {
                timeMSec = 0;
                timeSec++;
                if (players[currentPlayerId] != null)
                    Console.WriteLine(players[currentPlayerId].name + " has " + (timeSecPerTurn - timeSec) + "s left.  HHB: " + players[currentPlayerId].hasHitBall);

                if (players[currentPlayerId] != null && (
                    (timeSec >= timeSecPerTurn && !players[currentPlayerId].hasHitBall) ||
                    (players[currentPlayerId].hasHitBall && players[currentPlayerId].Vel == Vector2d.Zero) ||
                    players[currentPlayerId].trackFinished))
                {
                    players[currentPlayerId].myTurn = false;
                    currentPlayerId++;
                    timeSec = 0;

                    if (players[currentPlayerId] == null || currentPlayerId > players.Length)
                    {
                        currentPlayerId = 0;
                        nextMap();
                    }
                    if (players[currentPlayerId].trackFinished)
                        currentPlayerId++;
                    if (players[currentPlayerId] != null)
                    {
                        Console.WriteLine("It's now " + players[currentPlayerId].name + "´s turn !");
                        players[currentPlayerId].myTurn = true;
                    }
                }
            }

            scores.render();

            MyImage.endDraw2D();
            foreach (Player p in players)
            {
                if (p != null)
                {
                    p.tick();
                    p.render();
                    //MyImage.drawText(p.name, p.Pos.X - ((p.name.Length * 8) / 2), p.Pos.Y - p.r - 14, p.color, Texture.ASCII);
                }
            }
            MyImage.beginDraw2D();

            cnt++;
            if (cnt > 20)
            {
                cnt = 0;
                uncodetest = "";
                UInt16 j;
                for (int i = 0; i < 50; i++)
                {
                    do{
                    j = (UInt16)(RootThingy.rnd.Next(0, 65534));
                    }while(RootThingy.unicodeData[j].id == -1);
                    uncodetest += ((char)(j)).ToString();
                }
            }
            MyImage.drawText("►►► "+uncodetest, 20, 500, Color.White, Texture.Unicode);



            

            ////////////////////////Editor begin

            if (RootThingy.editorMode)
            {
                if (RootThingy.keyboard[Key.M])
                {
                    selector++;
                    if (selector > 2)
                        selector = 0;
                    if (mouseWheel > selectLimits[selector])
                        mouseWheel = selectLimits[selector];
                    Thread.Sleep(250);
                }

                var maus = Mouse.GetState();
                mouseWheelLast = mouseWheel;
                mouseWheel = maus.Wheel;
                int mouseWheelDelta = mouseWheel - mouseWheelLast;

                if (currentIDs[selector] < selectLimits[selector] && mouseWheelDelta > 0)
                    currentIDs[selector] += (sbyte)mouseWheelDelta;
                if (currentIDs[selector] > 0 && mouseWheelDelta < 0)
                    currentIDs[selector] += (sbyte)mouseWheelDelta;

                //Console.WriteLine("Mausrad: " + mouseWheel);

                if (RootThingy.keyboard[Key.F1])
                    saveToString("", true);
                if (RootThingy.keyboard[Key.F2])
                    loadFromString("", true);

                MyImage.beginDraw2D();

                MyImage.drawText("Editor-Mode", 770, 10, Color.Red, Texture.ASCII);
                MyImage.drawText("F1 -> Save Map", 770, 10 + 12, Color.White, Texture.ASCII);
                MyImage.drawText("F2 -> Load Map", 770, 10 + 24, Color.White, Texture.ASCII);

                MyImage.drawText("F11 -> Toggle Editor-Mode", 770, 10 + 48, Color.White, Texture.ASCII);
                MyImage.drawText("F12 -> Toggle Hitboxes", 770, 10 + 60, Color.White, Texture.ASCII);
                MyImage.drawText("M   -> Place-Mode:", 770, 10 + 72, Color.White, Texture.ASCII);
                switch (selector)
                {
                    case 0: MyImage.drawText(" Grounds", 770 + (18 * 8), 10 + 72, Color.White, Texture.ASCII); break;
                    case 1: MyImage.drawText(" Walls", 770 + (18 * 8), 10 + 72, Color.White, Texture.ASCII); break;
                    case 2: MyImage.drawText(" Objects", 770 + (18 * 8), 10 + 72, Color.White, Texture.ASCII); break;
                }


                switch (selector)
                {
                    case 0: MyImage.drawTileFromID(Texture.grounds, currentIDs[selector], 15, ((short)(mausX / 15) * 15), ((short)(mausY / 15) * 15)); break;
                    case 1: MyImage.drawTileFromID(Texture.walls, currentIDs[selector], 15, ((short)(mausX / 15) * 15), ((short)(mausY / 15) * 15)); break;
                    case 2: MyImage.drawTileFromID(Texture.objects, currentIDs[selector], 15, ((short)(mausX / 15) * 15), ((short)(mausY / 15) * 15)); break;
                }
                MyImage.endDraw2D();

                if (mausX > 0 && mausX < RootThingy.sceneX && mausY > 0 && mausY < RootThingy.sceneY)
                {
                    if (maus[MouseButton.Left])
                    {
                        switch (selector)
                        {
                            case 0: map[(short)(mausY / 15), (short)(mausX / 15)].G = new Ground((byte)currentIDs[selector], (short)((short)(mausX / 15) * 15), (short)((short)(mausY / 15) * 15)); break;
                            case 1: map[(short)(mausY / 15), (short)(mausX / 15)].W = new Wall((byte)currentIDs[selector], (short)((short)(mausX / 15) * 15), (short)((short)(mausY / 15) * 15)); break;
                            case 2: map[(short)(mausY / 15), (short)(mausX / 15)].O = new Objectt((byte)currentIDs[selector], (short)((short)(mausX / 15) * 15), (short)((short)(mausY / 15) * 15)); break;
                        }

                    }
                    if (maus[MouseButton.Right])
                    {
                        switch (selector)
                        {
                            case 0: map[(short)(mausY / 15), (short)(mausX / 15)].G = new Ground(0, (short)((short)(mausX / 15) * 15), (short)((short)(mausY / 15) * 15)); break;
                            case 1: map[(short)(mausY / 15), (short)(mausX / 15)].W = new Wall(0, (short)((short)(mausX / 15) * 15), (short)((short)(mausY / 15) * 15)); break;
                            case 2: map[(short)(mausY / 15), (short)(mausX / 15)].O = new Objectt(0, (short)((short)(mausX / 15) * 15), (short)((short)(mausY / 15) * 15)); break;
                        }

                    }
                }
            }
            ////////////////////////End of Editor
        }


        public string saveToString(string filename = "", bool useFile = false)
        {
            if (filename == "")
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Minigolf-Mapfiles (*.mgolf)|*.mgolf|All Files (*.*)|*.*";
                save.FilterIndex = 1;
                save.RestoreDirectory = true;
                save.InitialDirectory = RootThingy.exePath + @"\maps\";
                if (save.ShowDialog() == DialogResult.OK)
                    filename = save.FileName;
                Console.WriteLine("Save-Path: " + filename);
                Console.ReadKey();
            }
            else
                filename = RootThingy.exePath + @"\maps\" + filename + ".mgolf";

            string data = "";
            data += (map.GetLength(1) - 1) + "\n";    //Map-Width
            data += (map.GetLength(0) - 1) + "\n";    //Map-Heigth
            for (short yy = 0; yy < map.GetLength(0); yy++)
            {
                for (short xx = 0; xx < map.GetLength(1); xx++)
                {
                    data += map[yy, xx].G.id + "|" + map[yy, xx].W.id + "|" + map[yy, xx].O.id + ",";
                }
                data += "\n";
            }
            data = data.Substring(0, data.Length - 2);

            if (useFile)
                File.WriteAllText(filename, data);

            return data;
        }


        public static void loadFromString(string data = "", bool useFile = false)
        {
            if (useFile)
            {
                if (data == "")
                {
                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "Minigolf-Mapfiles (*.mgolf)|*.mgolf|All Files (*.*)|*.*";
                    open.FilterIndex = 1;
                    if (open.ShowDialog() == DialogResult.OK)
                        data = File.ReadAllText(open.FileName);
                }
                else
                {
                    if (File.Exists(data))
                        data = File.ReadAllText(data);
                    else if (File.Exists(data + ".mgolf"))
                        data = File.ReadAllText(data + ".mgolf");
                    else if (File.Exists(RootThingy.exePath + @"\maps\" + data))
                        data = File.ReadAllText(RootThingy.exePath + @"\maps\" + data);
                    else if (File.Exists(RootThingy.exePath + @"\maps\" + data + ".mgolf"))
                        data = File.ReadAllText(RootThingy.exePath + @"\maps\" + data + ".mgolf");
                }
            }
            int w;
            int h;
            int pos = 0;
            short xx = 0;
            short yy = 0;
            string[] tmp = data.Split('\n');
            int.TryParse(tmp[0], out w);
            int.TryParse(tmp[1], out h);

            string[] mapTiles = data.Split(',');
            foreach (string str in mapTiles)
            {
                string[] tile = str.Split('|');
                byte ground;
                byte.TryParse(tile[0], out ground);
                byte wall;
                byte.TryParse(tile[1], out wall);
                byte objekt;
                byte.TryParse(tile[2], out objekt);
                map[yy, xx] = new MapTile((short)(xx * 15), (short)(yy * 15),
                    new Ground(ground, (short)(xx * 15), (short)(yy * 15)),
                    new Wall(wall, (short)(xx * 15), (short)(yy * 15)),
                    new Objectt(objekt, (short)(xx * 15), (short)(yy * 15))
                    );

                xx++;
                if (xx > w)
                {
                    xx = 0;
                    yy++;
                    if (yy > h)
                        break;
                }
            }
        }


        public static void nextMap()
        {
            currentMap++;
            if (currentMap > mapList.Count - 1)
                CourseDone();
            else
            {
                loadFromString(mapList[currentMap]);
                currentPlayerId = 0;
                players[0].myTurn = true;
            }
        }



        public static void CourseDone()
        { ;}



    }
}
