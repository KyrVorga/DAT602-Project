using Battlespire;
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

        private List<Tile> _tiles;
        private List<Item> _items;
        private int ownerId;
        public Inventory(int entityId)
        {
            OwnerId = entityId;
            Items = GetItems();
        }

        public List<Tile> Tiles { get => _tiles; set => _tiles = value; }
        public List<Item> Items { get => _items; set => _items = value; }
        public int OwnerId { get => ownerId; set => ownerId = value; }

        public List<Item> GetItems()
        {
            return Game.DbConnection.GetEntityInventory(ownerId);
        }
        public void EquipItem(Tile clickedTile)
        {
            try
            {
                if (clickedTile != null)
                {

                    var query = from item in Items
                                join tile in Tiles on item.TileId equals tile.Id
                                where tile.Id == clickedTile.Id
                                select new { item.EntityId };

                    foreach (var item in query)
                    {
                        if (item != null)
                        {
                            Game.DbConnection.EquipItem(OwnerId, item.EntityId);
                        }
                    }
                    Items = GetItems();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
