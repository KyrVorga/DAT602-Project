using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Battlespire
{
    public partial class Registration : Form
    {

        public Registration()
        {
            InitializeComponent();
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            try
            {
                string username = username_input.Text;
                string password = password_input.Text;
                string email = email_input.Text;

                LoginAndRegistrationDAO db_connection = new();

                string accountCreationResult = db_connection.RegisterUser(username, email, password);
                if (accountCreationResult.StartsWith("Success"))
                {
                    MessageBox.Show("Account created successfully.");
                    this.Hide();
                    Login login = new Login();
                    login.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        private void redirect_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Hide();
                Login login = new Login();
                login.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }
    }
}
