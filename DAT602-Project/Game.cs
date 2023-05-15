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

        public static List<Entity> Entitiy_list { get => entitiy_list; set => entitiy_list = value; }
        public static List<Tile> Tile_list { get => tile_list; set => tile_list = value; }
        public static Player Current_player { get => current_player; set => current_player = value; }
        public string Username { get => _username; set => _username = value; }

        public Game(Login login, String username)
        {
            _login_form = login;
            Username = username;

            InitializeComponent();
            GameDAO db_connection = new();
            Current_player = db_connection.LoadPlayer(Username);
            Entitiy_list = db_connection.LoadEntities();
            Tile_list = db_connection.GetTilesByPlayer(Current_player.Entity_id);
            GenerateBoard(Tile_list);
            UpdateBoard();
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

        private void GenerateBoard(List<Tile> viewport_tiles)
        {
            //for (int row = 10; row < 10; row++)
            //{
            //    for (int col = 10; col < 10; col++)
            //    {
            //        PictureBox pictureBox = new PictureBox();
            //        pictureBox.BackColor = Color.Gray;
            //        pictureBox.Width = 50;
            //        pictureBox.Height = 50;

            //        pictureBox.Location = new Point(100 + (row * pictureBox.Height + 1), 100 + (col * pictureBox.Width + 1));
            //    }
            //}
            var tiles_accross = 11;
            var tile_border = 1 * tiles_accross;
            var board_width = this.board_panel.Width - tile_border;
            var board_height = this.board_panel.Height - tile_border;
            var tile_width = board_width / tiles_accross;
            var tile_height = board_height / tiles_accross;

            viewport_tiles.ForEach(tile =>
            {
                Console.WriteLine(tile.ToString());
                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = tile.Id.ToString();
                pictureBox.BackColor = Color.Gray;
                pictureBox.Width = tile_width;
                pictureBox.Height = tile_height;
                pictureBox.Location = new Point(board_width / 2 + tile.X * (pictureBox.Height + 1), board_height / 2 + tile.Y * (pictureBox.Width + 1));
                pictureBox.Click += new EventHandler(tile.tile_Click);

                board_panel.Controls.Add(pictureBox);
            });

        }


        public static void UpdateBoard() {
            Game game = 
            var query = from entity in Entitiy_list
                        join tile in Tile_list on entity.Tile_id equals tile.Id
                        select new { entity.Entity_id, entity.Tile_id };
            foreach (var entity in query)
            {
                board_panel.Controls[entity.Tile_id.ToString()].BackColor = Color.Blue;
            }
        }

    }
}
