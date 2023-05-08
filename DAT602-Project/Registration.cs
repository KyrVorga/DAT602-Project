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

        private Login _login_form;
        public Registration()
        {
            InitializeComponent();
        }
        public Registration(Login? login)
            : this()
        {
            _login_form = login;
            InitializeComponent();
        }

        private void register_button_Click(object sender, EventArgs e)
        {
            string username = username_input.Text;
            string password = password_input.Text;
            string email = email_input.Text;

            LoginAndRegistrationDAO db_connection = new();


            if (db_connection.RegisterUser(username, password, email) == "Error")
            {
                MessageBox.Show("Account creation failed, please try again.");
            }
            else
            {
                MessageBox.Show("Account created successfully.");
                this.Hide();
                Login login = new Login(this);
                login.Show();
            }
        }

        private void redirect_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login login = new Login(this);
            login.Show();
        }
    }
}
