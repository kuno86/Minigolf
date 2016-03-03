using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Minigolf
{
    class Scoreboard
    {
        private byte[,] scores;

        private string[] IndicatorFrames = new string[4] { " " + (char)(16) + (char)(16) + (char)(16), "" + (char)(16) + " " + (char)(16) + (char)(16), "" + (char)(16) + (char)(16) + " " + (char)(16), "" + (char)(16) + (char)(16) + (char)(16) + " " };
        private byte count = 0;
        private byte frame = 0;

        int x = 770;
        int y = 400;

        public Scoreboard(Player[] players, byte numtracks)     // X 770 - 1020
        {
            scores = new byte[numtracks,players.Length];
        }







        public void render()
        {
            count++;
            if (count > 30)
            {
                count = 0;
                frame++;
                if (frame > 3)
                    frame = 0;

            }
            foreach (Player p in Map.players)
            {
                if (p != null)
                {
                    if (p.myTurn)
                        MyImage.drawText(IndicatorFrames[frame], x, y, Color.Red, Texture.ASCII);
                    MyImage.drawText(p.name, x + 32, y, p.color, Texture.ASCII);

                    y += 14;
                }
            }
            y = 400;
        }

        public void setScore(byte playerId, byte trackId, byte scoreValue)
        {
            if(playerId <= scores.GetLength(0) && trackId <= scores.GetLength(1))
                scores[trackId, playerId] = scoreValue;
        }

        public void IncrementScore(byte playerId, byte trackId)
        {
            if (playerId <= scores.GetLength(0) && trackId <= scores.GetLength(1) && scores[trackId, playerId] < 255)
                scores[trackId, playerId] ++;
        }

    }
}
