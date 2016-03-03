namespace Minigolf
{
    partial class SPorMP
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
            this.btn_SP = new System.Windows.Forms.Button();
            this.btn_MP = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_SP
            // 
            this.btn_SP.Location = new System.Drawing.Point(99, 67);
            this.btn_SP.Name = "btn_SP";
            this.btn_SP.Size = new System.Drawing.Size(75, 23);
            this.btn_SP.TabIndex = 0;
            this.btn_SP.Text = "Singleplayer";
            this.btn_SP.UseVisualStyleBackColor = true;
            this.btn_SP.Click += new System.EventHandler(this.btn_SP_Click);
            // 
            // btn_MP
            // 
            this.btn_MP.Location = new System.Drawing.Point(99, 152);
            this.btn_MP.Name = "btn_MP";
            this.btn_MP.Size = new System.Drawing.Size(75, 23);
            this.btn_MP.TabIndex = 1;
            this.btn_MP.Text = "Multiplayer";
            this.btn_MP.UseVisualStyleBackColor = true;
            this.btn_MP.Click += new System.EventHandler(this.btn_MP_Click);
            // 
            // SPorMP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btn_MP);
            this.Controls.Add(this.btn_SP);
            this.Name = "SPorMP";
            this.Text = "SPorMP";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_SP;
        private System.Windows.Forms.Button btn_MP;
    }
}