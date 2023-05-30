using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    internal class ChestInventory : Inventory
    {
        private Chest _chest;
        private ChestTransferForm _chestTransferForm;
        public ChestInventory(int entity_id, Chest chest) : base(entity_id)
        {
            Tiles = GetTiles();

            Chest = chest;
            ChestTransferForm = new ChestTransferForm(Chest);
        }

        public Chest Chest { get => _chest; set => _chest = value; }
        public ChestTransferForm ChestTransferForm { get => _chestTransferForm; set => _chestTransferForm = value; }

        public List<Tile> GetTiles()
        {
            return Game.DbConnection.GetChestInventoryTiles(OwnerId);
        }
    }
}
