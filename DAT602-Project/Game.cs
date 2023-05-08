using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Battlespire
{
    public partial class Game : Form
    {
        private Login _login_form;
        private SettingsAdmin _settings_admin;
        private SettingsUser _settings_user;
        private String _username;
        private int _form_height;
        private int _form_width;

        public Game(Login login, String username)
        {
            _login_form = login;
            _username = username;
            _form_height = this.Height;
            _form_width = this.Width;

            InitializeComponent();
            GenerateBoard();
        }

        private void update_chat_button_Click(object sender, EventArgs e)
        {
            GameDAO db_connection = new();
            UpdateListbox(chat_box, db_connection.GetChat());
        }

        private void update_leaderboard_button_Click(object sender, EventArgs e)
        {
            GameDAO db_connection = new();
            UpdateListbox(leaderboard_box, db_connection.GetLeaderboard());
        }

        private void UpdateListbox(ListBox listbox, List<String> list)
        {
            listbox.Items.Clear();

            list.ForEach(item =>
            {
                listbox.Items.Add(item);
            });
        }

        private void settings_button_Click(object sender, EventArgs e)
        {
            GameDAO db_connection = new();
            Boolean admin_result = db_connection.checkIsAdmin(_username);
            if (admin_result)
            {
                this.Hide();
                SettingsAdmin admin = new SettingsAdmin(this);
                admin.Show();
            }
            else
            {
                this.Hide();
                SettingsUser user = new SettingsUser(this);
                user.Show();
            }
        }

        private void GenerateBoard()
        {
            PictureBox pictureBox;

            for (int row = 10; row < 10; row++)
            {
                for (int col = 10; col < 10; col++)
                {
                    pictureBox = new PictureBox();
                    pictureBox.BackColor = Color.Gray;
                    pictureBox.Width = 50;
                    pictureBox.Height = 50;

                    pictureBox.Location = new Point(100 + (row * pictureBox.Height + 1), 100 + (col * pictureBox.Width + 1));
                }
            }
        }

        private void GetTilesTest_Click(object sender, EventArgs e)
        {
            GameDAO db_connection = new();
            //db_connection.GetTilesByPlayer(82);
            UpdateListbox(Tiles, db_connection.GetTilesByPlayer(82));
        }
    }
}
