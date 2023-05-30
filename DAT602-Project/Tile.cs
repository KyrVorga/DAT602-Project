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
        private string _tileType;
        public Tile(int id, int x, int y, string tileType)
        {
            Id = id;
            X = x;
            Y = y;
            TileType = tileType;
        }

        public int Id { get => _id; set => _id = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public string TileType { get => _tileType; set => _tileType = value; }

        public override string ToString()
        {
            return string.Format("ID:{0} | X:{1} | Y:{2} | Type:{3}", Id, X, Y, TileType);
        }
        public virtual void Tile_Click(object sender, EventArgs e)
        {
        }
        
    }
}
