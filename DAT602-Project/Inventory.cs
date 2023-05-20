using Battlespire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    public class Inventory
    {

        private List<Tile> _tile_list;
        private List<Item> _items;
        private InventoryForm _inventory_form;

        public Inventory(int entity_id)
        {

            GameDAO db_connection = new();
            Tile_list = db_connection.GetEntityInventoryTiles(entity_id, this);
            Items = db_connection.GetEntityInventory(entity_id);
            _inventory_form = new InventoryForm(this);
        }

        public List<Tile> Tile_list { get => _tile_list; set => _tile_list = value; }
        internal List<Item> Items { get => _items; set => _items = value; }
        public InventoryForm InventoryForm { get => _inventory_form; set => _inventory_form = value; }


    }
}
