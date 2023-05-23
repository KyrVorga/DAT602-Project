using Battlespire;
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
        private static String _username;
        private static Board board;

        public static string Username { get => _username; set => _username = value; }
        public static Board Board { get => board; set => board = value; }

        public Game(Login login, String username)
        {
            InitializeComponent();
            _login_form = login;
            Username = username;

            Board = new Board(this);

            Board.GenerateBoard();
        }
        private void UpdateChat()
        {
            GameDAO db_connection = new();
            UpdateListbox(chat_box, db_connection.GetChat());
        }
        private void UpdateLeaderboard()
        {
            GameDAO db_connection = new();
            UpdateListbox(leaderboard_box, db_connection.GetLeaderboard());
        }

        private void LoadEntities()
        {
            GameDAO db_connection = new();

            Board.Entitiy_list = db_connection.LoadEntities(Board.Current_player.Entity_id);

        }
        private void update_chat_button_Click(object sender, EventArgs e)
        {
            UpdateChat();
        }

        private void update_leaderboard_button_Click(object sender, EventArgs e)
        {
            UpdateLeaderboard();
        }

        private void UpdateListbox(ListBox listbox, List<String> list)
        {
            listbox.Items.Clear();

            list.ForEach(item =>
            {
                listbox.Items.Add(item);
            });
        }
        private void MoveNPCMonsters()
        {

            GameDAO db_connection = new();

            var query = from entity in Board.Entitiy_list
                        select new { entity.Entity_id, entity.Entity_type };

            foreach (var entity in query)
            {
                if (entity.Entity_type == "monster")
                {
                    db_connection.MoveMonster(entity.Entity_id);
                }
            }
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

        private void inventory_icon_Click(object sender, EventArgs e)
        {
            Inventory inventory = Board.Current_player.Inventory;
            InventoryForm inventoryForm = inventory.InventoryForm;
            inventoryForm.Show();
        }

        private void update_timer_Tick(object sender, EventArgs e)
        {
            Board.UpdateBoard();
            LoadEntities();
        }

        private void chat_refresh_Tick(object sender, EventArgs e)
        {
            UpdateChat();
        }

        private void leaderboard_refresh_Tick(object sender, EventArgs e)
        {
            UpdateLeaderboard();
        }

        private void Game_Load(object sender, EventArgs e)
        {

            UpdateLeaderboard();
            UpdateChat();
        }

        private void MonsterMove_Tick(object sender, EventArgs e)
        {
            MoveNPCMonsters();
        }
    }
}
