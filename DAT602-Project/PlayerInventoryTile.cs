using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public class PlayerInventoryTile : Tile
    {
        private int _ownerId;
        public PlayerInventoryTile(int id, int x, int y, string tile_type, int ownerId) : base(id, x, y, tile_type)
        {
            OwnerId = ownerId;
        }
        public int OwnerId { get => _ownerId; set => _ownerId = value; }



        public override void Tile_Click(object sender, EventArgs e)
        {
            try
            {
                if (Game.InitialTile != null)
                {
                    Game.TargetTile = this;
                    Game.MoveItem(Game.CurrentPlayer);
                }
                else
                {
                    Game.InitialTile = this;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

    }
}
