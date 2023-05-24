using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    internal class BoardTile : Tile
    {
        private Game _board;
        public BoardTile(int id, int x, int y, string tile_type, Game board) : base(id, x, y, tile_type)
        {
            Board = board;
        }

        public Game Board { get => _board; set => _board = value; }

        public override void Tile_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;

            //Console.WriteLine(this.ToString());
            int tile_id = Int32.Parse(pictureBox.Name);

            var query = from entity in Game.Entitiy_list
                        join tile in Game.Tile_list on entity.Tile_id equals tile.Id
                        where entity.Tile_id == tile_id
                        select new { entity.Entity_id, entity.Tile_id, entity.Entity_type };

            if (query.Count() > 0 )
            {
                foreach (var entity in query)
                {
                    if (entity.Entity_type == "player")
                    {
                        // player click function
                    }
                    else if (entity.Entity_type == "monster")
                    {
                        // monster click function
                    }
                    else if (entity.Entity_type == "chest")
                    {
                        // chest click function
                        //Inventory inventory = Board.Current_player.Inventory;
                        //InventoryForm inventoryForm = inventory.InventoryForm;
                        //inventoryForm.Show();
                        
                        ChestTransferForm transferWindow = new ChestTransferForm(Game.Current_player.Inventory, entity.Entity_id);
                        transferWindow.Show();
                    }
                }
            } else
            {
                Board.PlayerMove(tile_id);
            }
        }
    }
}
