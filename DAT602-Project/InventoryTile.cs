﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    internal class InventoryTile : Tile
    {
        private Inventory _inventory;
        public InventoryTile(int id, int x, int y, string tile_type, Inventory inventory) : base(id, x, y, tile_type)
        {
            Inventory = inventory;
        }

        public Inventory Inventory { get => _inventory; set => _inventory = value; }

        public override void Tile_Click(object sender, EventArgs e)
        {
            
        }
    }
}
