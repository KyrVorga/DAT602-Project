using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public class PlayerInventory : Inventory
    {
        private InventoryForm _inventoryForm;
        private Player _player;
        public PlayerInventory(int entityId, Player player) : base(entityId)
        {
            Tiles = GetTiles();
            Player = player;
            InventoryForm = new InventoryForm(Player);
        }
        public InventoryForm InventoryForm { get => _inventoryForm; set => _inventoryForm = value; }
        public Player Player { get => _player; set => _player = value; }

        public List<Tile> GetTiles()
        {
            return Game.DbConnection.GetPlayerInventoryTiles(OwnerId);
        }


    }
}
