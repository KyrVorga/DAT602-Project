using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Battlespire
{
    public partial class InventoryForm : Form
    {
        private Player _player;

        public Player Player { get => _player; set => _player = value; }
        public Panel Board { get => inventory_board; }
        public InventoryForm(Player player)
        {
            InitializeComponent();
            Player = player;
        }

        public void UpdateBoard()
        {
            try
            {
                Game.UpdateInventoryBoard(Board, Player.Inventory.Tiles, Player.Inventory.Items);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
            Player.Inventory.Items = Player.Inventory.GetItems();
            Player.Inventory.Tiles = Player.Inventory.GetTiles();
        }

        public void GenerateBoard(List<Tile> tiles, int playerId, int xStart, int yStart, int xEnd, int yEnd)
        {
            try
            {
                Game.GenerateBoard(inventory_board, tiles, playerId, xStart, yStart, xEnd, yEnd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        public void UpdateHealthLabel(decimal value)
        {
            player_health_label.Text = string.Format("Health: {0}", value);
        }
        public void UpdateCurrentHealthLabel(decimal value)
        {
            player_current_health_label.Text = string.Format("Current Health: {0}", value);
        }
        public void UpdateAttackLabel(decimal value)
        {
            player_attack_label.Text = string.Format("Attack: {0}", value);
        }
        public void UpdateDefenseLabel(decimal value)
        {
            player_defense_label.Text = string.Format("Defense: {0}", value);
        }
        public void UpdateHealingLabel(decimal value)
        {
            player_healing_label.Text = string.Format("Healing: {0}", value);
        }



        private void equip_button_Click(object sender, EventArgs e)
        {
            try
            {
                Player.Inventory.EquipItem(Game.InitialTile);
                Game.InitialTile = null;
                Player.CalculateStats();
                Player.UpdateStats();
                UpdateBoard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (Player != null)
                {
                    player_name_label.Text = Game.PlayerName;
                    Player.CalculateStats();
                    Player.UpdateStats();

                    GenerateBoard(Player.Inventory.Tiles, Player.EntityId, 0, 0, 8, 4);
                    UpdateBoard();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }
    }
}
