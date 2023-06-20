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
    public partial class SettingsAdmin : Form
    {
        private Mainform _game;

        public SettingsAdmin(Mainform game)
        {
            _game = game;
            InitializeComponent();
        }


        private void UpdateListbox(ListBox listbox, List<String> list)
        {
            try
            {
                listbox.Items.Clear();

                list.ForEach(item =>
                {
                    listbox.Items.Add(item);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        private void SettingsAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                _game.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        private void resetGameButton_Click(object sender, EventArgs e)
        {
            try
            {
                AdminDAO db_connection = new();
                db_connection.ResetGame();
                Game.Mainform.ReloadGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        private void SettingsAdmin_Load(object sender, EventArgs e)
        {
            try
            {
                AdminDAO db_connection = new();
                UpdateListbox(player_box, db_connection.GetAllPlayers());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        private void moveHomeButton_Click(object sender, EventArgs e)
        {
            try
            {
                AdminDAO db_connection = new();
                db_connection.MoveToHome(player_box.SelectedItem.ToString());
                Game.Mainform.ReloadGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        private void regenerateMapButton_Click(object sender, EventArgs e)
        {
            try
            {
                AdminDAO db_connection = new();
                db_connection.RegenerateMap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }
    }
}
