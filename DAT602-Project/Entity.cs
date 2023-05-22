using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public abstract class Entity
    {
        private int _entity_id;
        private int _tile_id;
        private string _entity_type;

        public Entity(int entity_id, string entity_type, int tile_id) {
            Entity_id = entity_id;
            Tile_id = tile_id;
            Entity_type = entity_type;
        }

        public int Entity_id { get => _entity_id; set => _entity_id = value; }
        public int Tile_id { get => _tile_id; set => _tile_id = value; }
        public string Entity_type { get => _entity_type; set => _entity_type = value; }
    }
}
