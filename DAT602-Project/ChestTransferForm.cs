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

namespace Battlespire
{
    public partial class ChestTransferForm : Form
    {
        private Chest _chest;

        public Chest Chest { get => _chest; set => _chest = value; }
        public Panel Board { get => inventory_board; }
        public ChestTransferForm(Chest chest)
        {
            InitializeComponent();
            Chest = chest;
        }
        public void UpdateBoard()
        {
            Chest.Inventory.Items = Chest.Inventory.GetItems();
            Chest.Inventory.Tiles = Chest.Inventory.GetTiles();

            Game.UpdateInventoryBoard(Board, Chest.Inventory.Tiles, Chest.Inventory.Items);
        }


        public void GenerateBoard(List<Tile> tiles, int chestId, int xStart, int yStart, int xEnd, int yEnd)
        {

            Game.GenerateBoard(inventory_board, tiles, chestId, xStart, yStart, xEnd, yEnd);
        }

        private void ChestTransferForm_Load(object sender, EventArgs e)
        {
            if (Chest != null)
            {

                GenerateBoard(Chest.Inventory.Tiles, Chest.EntityId, 0, 0, 8, 4);
                UpdateBoard();
            }
        }

        private void take_button_Click(object sender, EventArgs e)
        {

            Item item = (Item)Chest.Inventory.Items.Single(item => item.TileId == Game.InitialTile.Id);
            Game.TransferItem(item);
            UpdateBoard();
        }
    }
}
