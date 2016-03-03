namespace Minigolf
{
    partial class Lobby
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_setupCourse = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.combo_NumTracks = new System.Windows.Forms.ComboBox();
            this.combo_maxPlayers = new System.Windows.Forms.ComboBox();
            this.btn_create = new System.Windows.Forms.Button();
            this.chk_ballCollisions = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.combo_ballInWater = new System.Windows.Forms.ComboBox();
            this.combo_maxHitsTrack = new System.Windows.Forms.ComboBox();
            this.combo_hitTimelimit = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_gamesList = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_join = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btn_ballColor = new System.Windows.Forms.Button();
            this.lbl_myIp = new System.Windows.Forms.Label();
            this.txt_myIP = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_gamesList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_setupCourse);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.combo_NumTracks);
            this.groupBox1.Controls.Add(this.combo_maxPlayers);
            this.groupBox1.Controls.Add(this.btn_create);
            this.groupBox1.Controls.Add(this.chk_ballCollisions);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.combo_ballInWater);
            this.groupBox1.Controls.Add(this.combo_maxHitsTrack);
            this.groupBox1.Controls.Add(this.combo_hitTimelimit);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_password);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(11, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(253, 361);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create Game";
            // 
            // btn_setupCourse
            // 
            this.btn_setupCourse.Location = new System.Drawing.Point(102, 106);
            this.btn_setupCourse.Name = "btn_setupCourse";
            this.btn_setupCourse.Size = new System.Drawing.Size(116, 39);
            this.btn_setupCourse.TabIndex = 19;
            this.btn_setupCourse.Text = "Setup course-tracks";
            this.btn_setupCourse.UseVisualStyleBackColor = true;
            this.btn_setupCourse.Click += new System.EventHandler(this.btn_setupCourse_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(171, 247);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "seconds";
            // 
            // combo_NumTracks
            // 
            this.combo_NumTracks.FormattingEnabled = true;
            this.combo_NumTracks.Location = new System.Drawing.Point(102, 79);
            this.combo_NumTracks.Name = "combo_NumTracks";
            this.combo_NumTracks.Size = new System.Drawing.Size(71, 21);
            this.combo_NumTracks.TabIndex = 17;
            this.combo_NumTracks.SelectedIndexChanged += new System.EventHandler(this.combo_NumTracks_SelectedIndexChanged);
            // 
            // combo_maxPlayers
            // 
            this.combo_maxPlayers.FormattingEnabled = true;
            this.combo_maxPlayers.Location = new System.Drawing.Point(101, 26);
            this.combo_maxPlayers.Name = "combo_maxPlayers";
            this.combo_maxPlayers.Size = new System.Drawing.Size(72, 21);
            this.combo_maxPlayers.TabIndex = 16;
            this.combo_maxPlayers.SelectedIndexChanged += new System.EventHandler(this.combo_maxPlayers_SelectedIndexChanged);
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(45, 325);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(158, 23);
            this.btn_create.TabIndex = 14;
            this.btn_create.Text = "Create!";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_createGame_Click);
            // 
            // chk_ballCollisions
            // 
            this.chk_ballCollisions.AutoSize = true;
            this.chk_ballCollisions.Location = new System.Drawing.Point(100, 298);
            this.chk_ballCollisions.Name = "chk_ballCollisions";
            this.chk_ballCollisions.Size = new System.Drawing.Size(15, 14);
            this.chk_ballCollisions.TabIndex = 13;
            this.chk_ballCollisions.UseVisualStyleBackColor = true;
            this.chk_ballCollisions.CheckedChanged += new System.EventHandler(this.chk_ballCollisions_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 298);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Ball collisions:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // combo_ballInWater
            // 
            this.combo_ballInWater.FormattingEnabled = true;
            this.combo_ballInWater.Location = new System.Drawing.Point(99, 271);
            this.combo_ballInWater.Name = "combo_ballInWater";
            this.combo_ballInWater.Size = new System.Drawing.Size(119, 21);
            this.combo_ballInWater.TabIndex = 11;
            this.combo_ballInWater.SelectedIndexChanged += new System.EventHandler(this.combo_ballInWater_SelectedIndexChanged);
            // 
            // combo_maxHitsTrack
            // 
            this.combo_maxHitsTrack.FormattingEnabled = true;
            this.combo_maxHitsTrack.Location = new System.Drawing.Point(100, 217);
            this.combo_maxHitsTrack.Name = "combo_maxHitsTrack";
            this.combo_maxHitsTrack.Size = new System.Drawing.Size(65, 21);
            this.combo_maxHitsTrack.TabIndex = 10;
            this.combo_maxHitsTrack.SelectedIndexChanged += new System.EventHandler(this.combo_maxHitsTrack_SelectedIndexChanged);
            // 
            // combo_hitTimelimit
            // 
            this.combo_hitTimelimit.FormattingEnabled = true;
            this.combo_hitTimelimit.Location = new System.Drawing.Point(100, 244);
            this.combo_hitTimelimit.Name = "combo_hitTimelimit";
            this.combo_hitTimelimit.Size = new System.Drawing.Size(65, 21);
            this.combo_hitTimelimit.TabIndex = 9;
            this.combo_hitTimelimit.SelectedIndexChanged += new System.EventHandler(this.combo_hitTimelimit_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 274);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Ball in water:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 247);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Timelimit per hit:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Max. hits/Track:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(101, 53);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(72, 20);
            this.txt_password.TabIndex = 5;
            this.txt_password.TextChanged += new System.EventHandler(this.txt_password_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Game Password:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Numer of Tracks:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max Players:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgv_gamesList
            // 
            this.dgv_gamesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_gamesList.Location = new System.Drawing.Point(280, 41);
            this.dgv_gamesList.Name = "dgv_gamesList";
            this.dgv_gamesList.Size = new System.Drawing.Size(380, 150);
            this.dgv_gamesList.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_join
            // 
            this.btn_join.Location = new System.Drawing.Point(376, 366);
            this.btn_join.Name = "btn_join";
            this.btn_join.Size = new System.Drawing.Size(158, 23);
            this.btn_join.TabIndex = 2;
            this.btn_join.Text = "Join Game";
            this.btn_join.UseVisualStyleBackColor = true;
            this.btn_join.Click += new System.EventHandler(this.btn_join_Click);
            // 
            // btn_ballColor
            // 
            this.btn_ballColor.Location = new System.Drawing.Point(396, 285);
            this.btn_ballColor.Name = "btn_ballColor";
            this.btn_ballColor.Size = new System.Drawing.Size(116, 39);
            this.btn_ballColor.TabIndex = 20;
            this.btn_ballColor.Text = "choose Ball-Color";
            this.btn_ballColor.UseVisualStyleBackColor = true;
            this.btn_ballColor.Click += new System.EventHandler(this.btn_ballColor_Click);
            // 
            // lbl_myIp
            // 
            this.lbl_myIp.AutoSize = true;
            this.lbl_myIp.Location = new System.Drawing.Point(290, 213);
            this.lbl_myIp.Name = "lbl_myIp";
            this.lbl_myIp.Size = new System.Drawing.Size(37, 13);
            this.lbl_myIp.TabIndex = 21;
            this.lbl_myIp.Text = "My IP:";
            // 
            // txt_myIP
            // 
            this.txt_myIP.Location = new System.Drawing.Point(333, 210);
            this.txt_myIP.Name = "txt_myIP";
            this.txt_myIP.Size = new System.Drawing.Size(106, 20);
            this.txt_myIP.TabIndex = 22;
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 414);
            this.Controls.Add(this.txt_myIP);
            this.Controls.Add(this.lbl_myIp);
            this.Controls.Add(this.btn_ballColor);
            this.Controls.Add(this.btn_join);
            this.Controls.Add(this.dgv_gamesList);
            this.Controls.Add(this.groupBox1);
            this.Name = "Lobby";
            this.Text = "Lobby";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_gamesList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.CheckBox chk_ballCollisions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox combo_ballInWater;
        private System.Windows.Forms.ComboBox combo_maxHitsTrack;
        private System.Windows.Forms.ComboBox combo_hitTimelimit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox combo_maxPlayers;
        private System.Windows.Forms.ComboBox combo_NumTracks;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_setupCourse;
        private System.Windows.Forms.DataGridView dgv_gamesList;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_join;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btn_ballColor;
        private System.Windows.Forms.Label lbl_myIp;
        private System.Windows.Forms.TextBox txt_myIP;
    }
}