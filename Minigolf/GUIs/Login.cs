using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Minigolf
{
    public partial class Login : Form
    {
        SHA1 sha = new SHA1CryptoServiceProvider();
        System.Text.ASCIIEncoding ASCII_Encoder = new System.Text.ASCIIEncoding();
        public Login()
        {
            InitializeComponent();
            
            txt_user.Focus();            
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            lbl_repeatPassword.Visible = false;
            lbl_email.Visible = false;
            txt_repeatPassword.Visible = false;
            txt_email.Visible = false;

            string pwFromDB = RootThingy.sql.query("SELECT passwordHash FROM player WHERE userName='" + txt_user.Text.Trim()+"';"); //get the pw-hash for this user

            Console.WriteLine("PW Eingabe: " + string_to_SHA1_hash(txt_password.Text));
            Console.WriteLine("PW from DB: " + pwFromDB);

            if (string_to_SHA1_hash(txt_password.Text) == pwFromDB)
            {
                this.BackColor = System.Drawing.Color.Green;
                RootThingy.loggedIn = true;
                int.TryParse(RootThingy.sql.query("SELECT id FROM player WHERE userName='" + txt_user.Text.Trim() + "';"), out RootThingy.LocalPlayerId);    //save the userId
                this.Close();
            }
            else
                this.BackColor = System.Drawing.Color.Red;
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            lbl_repeatPassword.Visible = true;
            lbl_email.Visible = true;
            txt_repeatPassword.Visible = true;
            txt_email.Visible = true;


            if(txt_password.Text != "" && txt_repeatPassword.Text != "" && txt_password.Text==txt_repeatPassword.Text)   //password-confirmation against typos
            {
                string newPW = string_to_SHA1_hash(txt_password.Text);

                if (RootThingy.sql.query("SELECT id FROM player WHERE userName='" + txt_user.Text.Trim() + "';") == "")   //check if the username already exists
                {
                    if (RootThingy.sql.query("SELECT id FROM player WHERE email='" + txt_email.Text.Trim() + "';") == "") //Check if the emailaddres is already used
                    {
                        RootThingy.sql.query(       //Now the user can be inserted into the DB
                            "INSERT into player(userName,email,registerDate,passwordHash) "+
                            "VALUES('" + txt_user.Text.Trim() + "','" + txt_email.Text.Trim() + "',NOW(),'" + newPW + "');"
                            );
                    }
                }
            }
        }

        private void chbox_showPassword_CheckedChanged(object sender, EventArgs e)
        {            
                txt_password.UseSystemPasswordChar = !chbox_showPassword.Checked;
                txt_repeatPassword.UseSystemPasswordChar = !chbox_showPassword.Checked;
        }



        private string string_to_SHA1_hash(string string_in, string delimiter="#")
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] input = Encoding.ASCII.GetBytes(string_in);

            string result = "";
            input = sha.ComputeHash(input);
            foreach (byte b in input)
            {
                result += b + delimiter;
            }
            result = result.Substring(0, result.Length - 1);

            return result;
        }


       
    }
}
