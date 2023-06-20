using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Battlespire
{
    internal class BoardTile : Tile
    {
        public BoardTile(int id, int x, int y, string tileType) : base(id, x, y, tileType)
        {
            
        }


        public override void Tile_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox pictureBox = (PictureBox)sender;

                int tileId = Int32.Parse(pictureBox.Name);

                var query = from entity in Game.Entities
                            join tile in Game.Tiles on entity.TileId equals tile.Id
                            where entity.TileId == tileId
                            select new { entity.EntityId, entity.TileId, entity.EntityType };

                if (query.Count() > 0)
                {
                    foreach (var entity in query)
                    {
                        if (entity.EntityType == "player" || entity.EntityType == "monster")
                        {
                            // player click function
                            Game.DamageEntity(Game.CurrentPlayer.EntityId, entity.EntityId);
                        }
                        else if (entity.EntityType == "chest")
                        {
                            int chestId = entity.EntityId;

                            foreach (var gameEntity in query)
                            {
                                Chest chest = (Chest)Game.Entities.Single(gameEntity => gameEntity.EntityId == chestId);
                                if (chest != null)
                                {
                                    chest.Inventory.ChestTransferForm = new ChestTransferForm(chest);
                                    chest.Inventory.ChestTransferForm.Show();
                                }
                            }
                        }
                    }
                }
                else
                {
                    //var currentTile = from tile in Game.Tiles where tile.Id == tileId select tile;
                    Tile currentTile = Game.Tiles.FirstOrDefault(t => t.Id == tileId);
                    if (currentTile != null)
                    {
                        if (currentTile.TileType == "exit")
                        {
                            Game.CurrentPlayer.ExitGame();
                        }
                        else if (currentTile.TileType == "ground")
                        {
                            Game.CurrentPlayer.PlayerMove(tileId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }
    }
}
