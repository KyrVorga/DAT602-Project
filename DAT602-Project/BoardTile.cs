using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    internal class BoardTile : Tile
    {
        private Board _board;
        public BoardTile(int id, int x, int y, string tile_type, Board board) : base(id, x, y, tile_type)
        {
            Board = board;
        }

        public Board Board { get => _board; set => _board = value; }

        public override void Tile_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            Console.WriteLine(this.ToString());

            int target_tile_id = int.Parse(pictureBox.Name);
            Board.PlayerMove(target_tile_id);
        }
    }
}
