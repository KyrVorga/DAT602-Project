using Battlespire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    public class Tile
    {
        private int _id;
        private int _x;
        private int _y;
        private string _tile_type;
        private Board _board;
        public Tile(int id, int x, int y, string tile_type, Board board)
        {
            Id = id;
            X = x;
            Y = y;
            Tile_type = tile_type;
            Board = board;
        }

        public int Id { get => _id; set => _id = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public string Tile_type { get => _tile_type; set => _tile_type = value; }
        public Board Board { get => _board; set => _board = value; }

        public override string ToString()
        {
            return string.Format("ID:{0} | X:{1} | Y:{2} | Type:{3}", Id, X, Y, Tile_type);
        }
        public void tile_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Console.WriteLine(this.ToString());

            int target_tile_id = int.Parse(pictureBox.Name);
            Board.Tile_list = Board.GetTiles();
            Board.PlayerMove(target_tile_id);
        }
    }
}
