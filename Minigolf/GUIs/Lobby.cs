using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;

namespace Minigolf
{
    public partial class Lobby : Form
    {
        private byte maxPlayers;
        private string password;
        private string myIP;
        private byte numTracks;
        private byte maxHitsPerTrackId;
        private byte maxTimePerHitId;
        private byte waterTouchResetsBall;
        private byte courseTrackId;     //1 or 0
        private byte ballBallCollision; //1 or 0

        int maxGameId;


        public Lobby()
        {            
            InitializeComponent();

            myIP = getExternalIp();
            txt_myIP.Text = myIP;           

            for (int i = 2; i <= 64; i++)
                combo_maxPlayers.Items.Add(i);
            combo_maxPlayers.SelectedIndex = 0;
            
            for (int i = 1; i <= 20; i++)
                combo_NumTracks.Items.Add(i);
            combo_NumTracks.SelectedIndex = 0;


            string[] tmp = (RootThingy.sql.query("SELECT maxHits FROM maxhitspertrack;","☺").Split('☺'));
            foreach (string str in tmp)
                combo_maxHitsTrack.Items.Add(str);
            combo_maxHitsTrack.SelectedIndex = 0;
            tmp = null;

            tmp = (RootThingy.sql.query("SELECT maxTimeSec FROM maxtimeperhit;", "☺").Split('☺'));
            foreach (string str in tmp)
                combo_hitTimelimit.Items.Add(str);
            combo_hitTimelimit.SelectedIndex = 0;

            combo_ballInWater.Items.Add("goes back to land");
            combo_ballInWater.Items.Add("goes back to start");
            combo_ballInWater.SelectedIndex = 0;

            dgv_gamesList.Columns.Add("password", "PW");
            dgv_gamesList.Columns["password"].Width = 40;

            dgv_gamesList.Columns.Add("courseName", "Game");
            dgv_gamesList.Columns["courseName"].Width = 180;
            
            dgv_gamesList.Columns.Add("tracksNum", "Tracks");
            dgv_gamesList.Columns["tracksNum"].Width = 60;

            dgv_gamesList.Columns.Add("players", "Player");
            dgv_gamesList.Columns["players"].Width = 60;

            //timer1.Interval = 5000;
            //timer1.Enabled = true;
            //timer1.Start();            
        }

        
        private void combo_maxPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte.TryParse(combo_maxPlayers.SelectedItem.ToString(), out maxPlayers);
        }               

        private void txt_password_TextChanged(object sender, EventArgs e)
        {
            password = txt_password.Text;
        }

        private void combo_NumTracks_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte.TryParse(combo_NumTracks.SelectedItem.ToString(), out numTracks);
        }

        private void combo_maxHitsTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            maxHitsPerTrackId=(byte)combo_maxHitsTrack.SelectedIndex;
        }

        private void combo_hitTimelimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte.TryParse(combo_hitTimelimit.SelectedItem.ToString(), out maxTimePerHitId);
        }

        private void combo_ballInWater_SelectedIndexChanged(object sender, EventArgs e)
        {
            waterTouchResetsBall = (byte)combo_ballInWater.SelectedIndex;
        }

        private void chk_ballCollisions_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ballCollisions.Checked)
                ballBallCollision = 1;
            else
                ballBallCollision = 0;
        }


        private void btn_setupCourse_Click(object sender, EventArgs e)
        {
            byte nt;
            byte.TryParse(combo_NumTracks.SelectedItem.ToString(), out nt);
            CourseSetup courseSetup = new CourseSetup(nt, 1);
            courseSetup.Show();
        }


        private void btn_createGame_Click(object sender, EventArgs e)
        {
            if (btn_create.Text == "Create!")
            {
                btn_create.Text = "Retract!";
                
                int.TryParse(RootThingy.sql.query("SELECT MAX(id)+1 FROM waitinggame;"), out maxGameId);  //get the next unused free Id

                RootThingy.sql.query(
                    "INSERT INTO TABLE waitinggame (id, maxPlayers, password, courseTrackId, maxHitsPerTrackId, maxTimePerHitId, waterTouchResetsBall, ballBallCollision) " +
                    "VALUES ('"+ maxGameId+ "','" + maxPlayers + "','" + password + "','" + 999 + "','" + maxHitsPerTrackId + "','" + maxTimePerHitId + "','" + waterTouchResetsBall + "','" + ballBallCollision + "');");

            }
            else
            {
                btn_create.Text = "Create!";
                RootThingy.sql.query("DELETE FROM waitinggame WHERE id='"+maxGameId+"';");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int nrGames; 
            int.TryParse(RootThingy.sql.query("SELECT count(*) FROM waitinggame;"), out nrGames);
            
            int id=0;
            RootThingy.sql.query(
                "SELECT waitinggame.id, course.name, count(playerwaitinggame.playerId), waitinggame.maxPlayers "+
                "FROM waitinggame, playerwaitinggame, course, coursetrack "+
                "WHERE waitiggame.id="+id+" AND waitinggame.courseTrackId=course.id;");
        }

        private void btn_join_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }


        private string getExternalIp()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return externalIP;
            }
            catch { return null; }
        }

        private void btn_ballColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            System.Drawing.Color color = colorDialog1.Color;
            btn_ballColor.BackColor = color;
            RootThingy.color = color;
        }
        

        

        

    }
}
