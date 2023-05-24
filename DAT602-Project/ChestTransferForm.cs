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
    public partial class ChestTransferForm : InventoryForm // Form//
    {
        public ChestTransferForm(Inventory inventory, int chest_id) : base(inventory) // ) //
        {
            InitializeComponent();
            base.Inventory = new ChestInventory(chest_id);

        }
        //private void ChestTransferForm_Load(object sender, EventArgs e)
        //{
        //    Player player = Game.Current_player;
        //    if (player != null)
        //    {

        //        GenerateChestBoard();
        //    }

        //}
        //public void GenerateChestBoard()
        //{
        //    int tiles_accross = 9;
        //    int tiles_vertical = 5;
        //    int board_width = chest_inventory_board.Width;
        //    int tile_dimension = board_width / tiles_accross;
        //    List<InventoryTile> Tile_list = ChestInventory.Tile_list;

        //    //inventory_board.Controls.Clear();
        //    int index = 0;
        //    for (int i = 0; i < tiles_accross; i++)
        //    {
        //        for (int j = 0; j < tiles_vertical; j++)
        //        {
        //            PictureBox pictureBox = new();
        //            pictureBox.BackColor = Color.Gray;
        //            pictureBox.Width = tile_dimension;
        //            pictureBox.Height = tile_dimension;
        //            pictureBox.Location = new Point(i * (pictureBox.Height + 1), j * (pictureBox.Width + 1));

        //            pictureBox.Click += Tile_list[index].Tile_Click;
        //            chest_inventory_board.Controls.Add(pictureBox);
        //            index++;
        //        }
        //    }
        //    UpdateChestBoard();
        //}

        //public void UpdateChestBoard()
        //{
        //    List<InventoryTile> Tile_list = ChestInventory.Tile_list;
        //    List<Item> Item_list = ChestInventory.Item_list;

        //    for (int i = 0; i < chest_inventory_board.Controls.Count; i++)
        //    {
        //        Tile tile = ChestInventory.Tile_list[i];
        //        Control box = chest_inventory_board.Controls[i];

        //        box.Name = tile.Id.ToString();
        //        box.BackColor = Color.Gray;


        //    }




        //    var query = from entity in Item_list
        //                join tile in Tile_list on entity.Tile_id equals tile.Id
        //                select new { entity.Entity_id, entity.Tile_id, entity.Name, entity.Is_equipped };

        //    foreach (var entity in query)
        //    {
        //        if (entity.Name.StartsWith("Amulet"))
        //        {
        //            chest_inventory_board.Controls[entity.Tile_id.ToString()].BackColor = Color.Green;
        //        }
        //        else if (entity.Name.StartsWith("Sword"))
        //        {
        //            chest_inventory_board.Controls[entity.Tile_id.ToString()].BackColor = Color.Orange;
        //        }
        //        else if (entity.Name.StartsWith("Armour"))
        //        {
        //            chest_inventory_board.Controls[entity.Tile_id.ToString()].BackColor = Color.Red;
        //        }
        //        else if (entity.Name.StartsWith("Shield"))
        //        {
        //            chest_inventory_board.Controls[entity.Tile_id.ToString()].BackColor = Color.Blue;
        //        }
        //        if (entity.Is_equipped == true)
        //        {
        //            chest_inventory_board.Controls[entity.Tile_id.ToString()].BackColor = Color.Pink;
        //        }
        //    }
        //}
    }
}
