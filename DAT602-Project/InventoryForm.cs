using Battlespire;
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
    public partial class InventoryWindow : Form
    {

        private List<Tile> _tile_list;
        private List<Item> _items;

        public Inventory(int entity_id)
        {

            InitializeComponent();
            GameDAO db_connection = new();
            Tile_list = db_connection.GetEntityInventoryTiles(entity_id, this);
            Items = db_connection.GetEntityInventory(entity_id);
        }

        public List<Tile> Tile_list { get => _tile_list; set => _tile_list = value; }
        internal List<Item> Items { get => _items; set => _items = value; }
        public static InventoryWindow InventoryWindow { get => _inventory_window; set => _inventory_window = value; }



    }
}
