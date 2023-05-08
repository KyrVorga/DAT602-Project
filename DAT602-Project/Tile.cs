using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    internal class Tile
    {
        private int _id;
        private int _x;
        private int _y;
        private string _tile_type;
        public Tile(int id, int x, int y, string tile_type)
        {
            _id = id;
            _x = x;
            _y = y;
            _tile_type = tile_type;
        }

        public override string ToString()
        {
            return string.Format("ID:{0} | X:{1} | Y: {2} | Type:{3}", _id, _x, _y, _tile_type);
        }
    }
}
