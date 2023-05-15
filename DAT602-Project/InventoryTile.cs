using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    internal class InventoryTile : Tile
    {
        private string _owner_id;
        public InventoryTile(int id, int x, int y, string tile_type, string owner_id) : base(id, x, y, tile_type)
        {
            Owner_id = owner_id;
        }

        public string Owner_id { get => _owner_id; set => _owner_id = value; }
    }

}
