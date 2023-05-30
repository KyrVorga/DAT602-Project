using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public class Chest : Entity
    {
        private ChestInventory _inventory;
        public Chest(int entityId, string entityType, int tileId) : base(entityId, entityType, tileId) {
            Inventory = new(entityId, this);
        }

        internal ChestInventory Inventory { get => _inventory; set => _inventory = value; }

        public override string ToString()
        {
            return string.Format("Type: {0} | ID: {1} | Tile: {2}", EntityType, EntityId, TileId);
        }
    }
}
