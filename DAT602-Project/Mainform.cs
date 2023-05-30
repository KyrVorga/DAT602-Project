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
    public partial class Mainform : Form
    {
        private Login _loginForm;
        private SettingsAdmin _settingsAdmin;
        private SettingsUser _settingsUser;
        private static Game _game;
        private string _username;

        public static Game Game { get => _game; set => _game = value; }
        public Login LoginForm { get => _loginForm; set => _loginForm = value; }
        public SettingsAdmin SettingsAdmin { get => _settingsAdmin; set => _settingsAdmin = value; }
        public SettingsUser SettingsUser { get => _settingsUser; set => _settingsUser = value; }
        public string Username { get => _username; set => _username = value; }

        public Mainform(Login login, string username)
        {
            InitializeComponent();
            LoginForm = login;
            Username = username;

            Game = new Game(this, Username);

        }
        private void UpdateChat()
        {
            UpdateListbox(chat_box, Game.DbConnection.GetChat());
        }
        private void UpdateLeaderboard()
        {
            UpdateListbox(leaderboard_box, Game.DbConnection.GetLeaderboard());
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
            Boolean admin_result = Game.DbConnection.CheckIsAdmin(Game.PlayerName);
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
            Game.CurrentPlayer.Inventory = new(Game.CurrentPlayer.EntityId, Game.CurrentPlayer);
            Game.CurrentPlayer.Inventory.InventoryForm.Show();
        }

        private void update_timer_Tick(object sender, EventArgs e)
        {
            Game.UpdateGameBoard(Game.Mainform.board_panel, Game.Tiles, Game.Entities);
            Game.Entities = Game.GetEntities();
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
            Game.MoveNPCMonsters();
        }

        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoginForm.Close();
        }

        public void ReloadGame()
        {
            Game = new(this, Username);
        }
    }
}
