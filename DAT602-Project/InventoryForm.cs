﻿using Battlespire;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAT602_Project
{
    public partial class InventoryForm : Form
    {
        private Inventory _inventory;
        public InventoryForm(Inventory inventory)
        {
            InitializeComponent();
            Inventory = inventory;
        }

        public Inventory Inventory { get => _inventory; set => _inventory = value; }

        private void InventoryWindow_Load(object sender, EventArgs e)
        {
            Player player = Board.Current_player;
            if (player != null)
            {
                player_health_label.Text = string.Format("Health: {0}", player.Health);
                player_attack_label.Text = string.Format("Attack: {0}", player.Attack);
                player_defense_label.Text = string.Format("Defense: {0}", player.Defense);
                player_healing_label.Text = string.Format("Healing: {0}", player.Healing);
                player_name_label.Text = Game.Username;

                GenerateBoard();
            }

        }

        public void GenerateBoard()
        {
            int tiles_accross = 9;
            int tiles_vertical = 5;
            int board_width = inventory_board.Width;
            int tile_dimension = board_width / tiles_accross;

            inventory_board.Controls.Clear();

            for (int i = 0; i < tiles_accross; i++)
            {
                for (int j = 0; j < tiles_vertical; j++)
                {
                    PictureBox pictureBox = new();
                    pictureBox.BackColor = Color.Gray;
                    pictureBox.Width = tile_dimension;
                    pictureBox.Height = tile_dimension;
                    pictureBox.Location = new Point(i * (pictureBox.Height + 1), j * (pictureBox.Width + 1));
                    inventory_board.Controls.Add(pictureBox);
                }
            }
            UpdateBoard();
        }

        public void UpdateBoard()
        {
            List<Tile> Tile_list = Inventory.Tile_list;
            List<Item> Item_list = Inventory.Items;

            for (int i = 0; i < inventory_board.Controls.Count; i++)
            {
                Tile tile = Inventory.Tile_list[i];
                Control box = inventory_board.Controls[i];

                box.Name = tile.Id.ToString();
                box.BackColor = Color.Gray;
                // remove previous Tile_click event handler


                box.Click += tile.Tile_Click;
            }




            var query = from entity in Item_list
                        join tile in Tile_list on entity.Tile_id equals tile.Id
                        select new { entity.Entity_id, entity.Tile_id, entity.Name};

            foreach (var entity in query)
            {
                if (entity.Name.StartsWith("Amulet"))
                {
                    inventory_board.Controls[entity.Tile_id.ToString()].BackColor = Color.Green;
                }
                else if (entity.Name.StartsWith("Sword"))
                {
                    inventory_board.Controls[entity.Tile_id.ToString()].BackColor = Color.Orange;
                }
                else if (entity.Name.StartsWith("Armour"))
                {
                    inventory_board.Controls[entity.Tile_id.ToString()].BackColor = Color.Red;
                }
                else if (entity.Name.StartsWith("Shield"))
                {
                    inventory_board.Controls[entity.Tile_id.ToString()].BackColor = Color.Blue;
                }
            }
        }
    }
}
