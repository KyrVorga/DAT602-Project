using Battlespire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public abstract class Tile
    {
        private int _id;
        private int _x;
        private int _y;
        private string _tile_type;
        public Tile(int id, int x, int y, string tile_type)
        {
            Id = id;
            X = x;
            Y = y;
            Tile_type = tile_type;
        }

        public int Id { get => _id; set => _id = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public string Tile_type { get => _tile_type; set => _tile_type = value; }

        public override string ToString()
        {
            return string.Format("ID:{0} | X:{1} | Y:{2} | Type:{3}", Id, X, Y, Tile_type);
        }
        public virtual void Tile_Click(object sender, EventArgs e)
        {
        }
        
    }
}
