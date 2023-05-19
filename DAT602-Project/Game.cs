using DAT602_Project;
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
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Battlespire
{
    public partial class Game : Form
    {
        private Login _login_form;
        private SettingsAdmin _settings_admin;
        private SettingsUser _settings_user;
        private String _username;
        private static List<Entity> entitiy_list;
        private static List<Tile> tile_list;
        private static Player current_player;
        private static Board board;

        public static List<Entity> Entitiy_list { get => entitiy_list; set => entitiy_list = value; }
        public static List<Tile> Tile_list { get => tile_list; set => tile_list = value; }
        public static Player Current_player { get => current_player; set => current_player = value; }
        public string Username { get => _username; set => _username = value; }
        public static Board Board { get => board; set => board = value; }

        public Game(Login login, String username)
        {
            InitializeComponent();
            _login_form = login;
            Username = username;

            Board = new Board(this);

            Board.GenerateBoard();
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
            Boolean admin_result = db_connection.checkIsAdmin(Username);
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
    }
}
