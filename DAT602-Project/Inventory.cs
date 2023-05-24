﻿using Battlespire;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public abstract class Inventory
    {

        private List<InventoryTile> _tiles;
        private List<Item> _items;
        private InventoryForm _inventory_form;
        public Inventory(int entity_id)
        {
            GameDAO db_connection = new();
            Tiles = db_connection.GetEntityInventoryTiles(entity_id);
            Items = db_connection.GetEntityInventory(entity_id);
        }

        public List<InventoryTile> Tiles { get => _tiles; set => _tiles = value; }
        public List<Item> Items { get => _items; set => _items = value; }
        public InventoryForm InventoryForm { get => _inventory_form; set => _inventory_form = value; }

        //public virtual void MoveItem()
        //{
        //    // extract these out to board
        //    InventoryTile target = InventoryForm.Target_tile;
        //    InventoryTile origin = InventoryForm.Initial_tile;

        //    // check if the tile has an item on it.
        //    var query = from item in Item_list
        //                join tile in Tile_list on item.Tile_id equals tile.Id
        //                where tile.Id == origin.Id
        //                select new { item.Entity_id };


        //    var query1 = from item in Item_list
        //                join tile in Tile_list on item.Tile_id equals tile.Id
        //                where tile.Id == target.Id
        //                select new { item.Entity_id };

        //    GameDAO db_connection = new();

        //    var zip = query.Zip(query1);
        //    foreach (var pair in zip)
        //    {
        //        if (pair.First != null)
        //        {
        //            db_connection.MoveInventoryItem(pair.First.Entity_id, origin.Id, target.Id);
        //        }
        //        if (pair.Second != null)
        //        {

        //            db_connection.MoveInventoryItem(pair.Second.Entity_id, target.Id, origin.Id);
        //        }
        //    }
        //    foreach (var item in query)
        //    {
        //        if (item != null)
        //        {
        //            db_connection.MoveInventoryItem(item.Entity_id, origin.Id, target.Id);
        //        }
        //    }

        //    Item_list = db_connection.GetEntityInventory(Owner_id);
        //    InventoryForm.Target_tile = null;
        //    InventoryForm.Initial_tile = null;
        //}

        //public void EquipItem(Tile clicked_tile)
        //{
        //    if (clicked_tile != null)
        //    {
        //        GameDAO db_connection = new();

        //        var query = from item in Item_list
        //                    join tile in Tile_list on item.Tile_id equals tile.Id
        //                    where tile.Id == clicked_tile.Id
        //                    select new { item.Entity_id };

        //        foreach (var item in query)
        //        {
        //            if (item != null)
        //            {
        //                db_connection.EquipItem(Owner_id, item.Entity_id);
        //            }
        //        }
        //        Item_list = db_connection.GetEntityInventory(Owner_id);
        //    }
        //}
    }
}
