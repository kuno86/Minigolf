using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Minigolf
{
    public partial class CourseSetup : Form
    {
        string[] parts = RootThingy.sql.query("SELECT track.id, track.title, track.description, player.userName FROM track, player WHERE track.author_id=player.id;", "☺").Split('☺');

        Label[] labels1;
        ComboBox[] combos;    //comboboxes with all track-titles
        RichTextBox trackInfo = new RichTextBox();
        int courseId;

        public CourseSetup(byte numTracks, int courseIdExt)
        {
            Console.WriteLine("Setup CourseTrack");
            labels1 = new Label[numTracks];
            combos = new ComboBox[numTracks];
            this.courseId = courseIdExt;

            InitializeComponent();
            
            Label l1 = new Label();
            l1.SetBounds(50, 10, 64, 14);
            l1.Text = "Tracks:";
            this.Controls.Add(l1);

            Label l2 = new Label();
            l2.SetBounds(265, 10, 64, 14);
            l2.Text = "Track-Info:";
            this.Controls.Add(l2);

            for (int i = 0; i < numTracks; i++)
            {
                labels1[i]= new Label();
                labels1[i].Text = "Track " + (i + 1);
                this.Controls.Add(labels1[i]);
                labels1[i].SetBounds(15, 30 + (i * 20), 50, 14);

                combos[i] = new ComboBox();
                this.Controls.Add(combos[i]);
                combos[i].SetBounds(66, 30 + (i * 20), 128, 14);
                combos[i].SelectedIndexChanged += new System.EventHandler(this.combo_changed);  //Add the event for changes ind the comboboxe


                for(int j=0; j < (parts.Length); j+=4)
                {
                    combos[i].Items.Add(parts[j + 1]);  //offset 1 = Title
                }
            }

            trackInfo.SetBounds(210, 30, 200, 200);
            trackInfo.ReadOnly = true;
            this.Controls.Add(trackInfo);

            Button btn_random = new Button();
            btn_random.SetBounds(210, 240, 200, 30);
            btn_random.Text = "Randomize all Tracks!";
            btn_random.Click += new System.EventHandler(this.btn_random_Click);
            this.Controls.Add(btn_random);

            Button btn_done = new Button();
            btn_done.SetBounds(210, 280, 200, 30);
            btn_done.Text = "Done!";
            btn_done.Click += new System.EventHandler(this.btn_done_Click);
            this.Controls.Add(btn_done);

            this.Width = 440;
            if (numTracks > 14)
                this.Height = 30 + ((numTracks - 14) * combos[0].Height) + 330;
            else
                this.Height = 360;
            
        }

        

        private void combo_changed(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            trackInfo.Text =
                "ID: " + parts[(4 * combo.SelectedIndex) + 0] + "\n" +
                "\n" +
                "Title:  " + parts[(4 * combo.SelectedIndex) + 1] + "\n" +
                "\n" +
                "Author:  " + parts[(4 * combo.SelectedIndex) + 3] + "\n" +
                "\n" +
                "Description:\n" + parts[(4 * combo.SelectedIndex) + 2] + "\n";
        }

        private void btn_random_Click(object sender, EventArgs e)
        {
            foreach (ComboBox cb in combos)
                cb.SelectedIndex = RootThingy.rnd.Next(0, cb.Items.Count);
        }

        private void btn_done_Click(object sender, EventArgs e)
        {
            int lastId=0;

            if (RootThingy.sql.query("SELECT * FROM coursetrack;") != "")   //not empty
            {
                int.TryParse(RootThingy.sql.query("SELECT MAX(id) FROM coursetrack;"), out lastId);

            }
            else
            {
            }
            string sqlString="INSERT INTO coursetrack (id, courseId, trackId) VALUES ";
            for (int i = 0; i < combos.Length; i++)
                {
                    sqlString += "('" + lastId + "','" + courseId + "','" + parts[(4 * combos[i].SelectedIndex) + 0] + "'),";
                }
            sqlString = sqlString.Substring(0,sqlString.Length - 1);
            sqlString+= ';';
            Console.WriteLine("CourseTrack SQL: "+sqlString);

        }

    }
}
