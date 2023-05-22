using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    public class Chest : Entity
    {
        public Chest(int entity_id, string entity_type, int tile_id) : base(entity_id, entity_type, tile_id) {
        }



        public override string ToString()
        {
            return string.Format("Type: {0} | ID: {1} | Tile: {2}", Entity_type, Entity_id, Tile_id);
        }
    }
}
