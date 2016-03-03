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
    public partial class SPorMP : Form
    {
        public SPorMP()
        {
            InitializeComponent();
        }

        private void btn_SP_Click(object sender, EventArgs e)
        {
            RootThingy.loggedIn = true;
            this.Close();
        }

        private void btn_MP_Click(object sender, EventArgs e)
        {            
            Login login = new Login();
            login.Focus();
            login.Show();
            this.Hide();
        }

        
    }
}
