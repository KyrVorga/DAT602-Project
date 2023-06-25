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
            try
            {
                string username = username_input.Text;
                string password = password_input.Text;

                LoginAndRegistrationDAO db_connection = new();

                string procedureResult = db_connection.LoginUser(username, password);

                if (procedureResult.StartsWith("Success:"))
                {
                    this.Hide();
                    Mainform game = new Mainform(this, username);
                    game.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
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
