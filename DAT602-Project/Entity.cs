using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public abstract class Entity
    {
        private int _entityId;
        private int _tileId;
        private string _entityType;

        public Entity(int entityId, string entityType, int tileId) {
            EntityId = entityId;
            TileId = tileId;
            EntityType = entityType;
        }

        public int EntityId { get => _entityId; set => _entityId = value; }
        public int TileId { get => _tileId; set => _tileId = value; }
        public string EntityType { get => _entityType; set => _entityType = value; }
    }
}
