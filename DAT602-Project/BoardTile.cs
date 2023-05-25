using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    internal class BoardTile : Tile
    {
        public BoardTile(int id, int x, int y, string tileType) : base(id, x, y, tileType)
        {
            
        }


        public override void Tile_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;

            //Console.WriteLine(this.ToString());
            int tileId = Int32.Parse(pictureBox.Name);

            var query = from entity in Game.Entities
                        join tile in Game.Tiles on entity.TileId equals tile.Id
                        where entity.TileId == tileId
                        select new { entity.EntityId, entity.TileId, entity.EntityType };

            if (query.Count() > 0)
            {
                foreach (var entity in query)
                {
                    if (entity.EntityType == "player")
                    {
                        // player click function
                    }
                    else if (entity.EntityType == "monster")
                    {
                        // monster click function
                    }
                    else if (entity.EntityType == "chest")
                    {
                        // chest click function
                        //Inventory inventory = Board.Current_player.Inventory;
                        //InventoryForm inventoryForm = inventory.InventoryForm;
                        //inventoryForm.Show();

                        int chestId = entity.EntityId;

                        foreach (var gameEntity in query)
                        {
                            Chest chest = (Chest)Game.Entities.Single(gameEntity => gameEntity.EntityId == chestId);
                            Console.WriteLine(chest.Inventory.ChestTransferForm.Board.Controls.Count);
                            if (chest != null)
                            {
                                chest.Inventory.ChestTransferForm = new ChestTransferForm(chest);
                                Console.WriteLine(chest.Inventory.ChestTransferForm.Board.Controls.Count);
                                chest.Inventory.ChestTransferForm.Show();
                            }
                        }
                    }
                }
            }
            else
            {
                Game.CurrentPlayer.PlayerMove(tileId);
            }
        }
    }
}
