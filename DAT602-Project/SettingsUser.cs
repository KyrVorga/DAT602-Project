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
    public partial class SettingsUser : Form
    {
        private Mainform _game;

        public SettingsUser(Mainform game)
        {
            _game = game;
            InitializeComponent();
        }

        private void SettingsUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            _game.Show();
        }

        private void deleteAccountButton_Click(object sender, EventArgs e)
        {

            AdminDAO db_connection = new();
            db_connection.DeleteAccount();
        }
    }
}
