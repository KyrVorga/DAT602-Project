using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battlespire
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;

            LoginAndRegistrationDAO db_connection = new();


            if (db_connection.LoginUser(username, password))
            {
                // MessageBox.Show("Logged in successfully.");
                this.Hide();
                Mainform game = new Mainform(this, username);
                game.Show();
            }
            else
            {
                MessageBox.Show("Login failed, please try again.");
            }
        }

        private void redirect_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Registration register = new Registration();
            register.Show();
        }
    }
}
