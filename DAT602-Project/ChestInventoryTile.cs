using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public class ChestInventoryTile : Tile
    {
        private int _ownerId;
        public ChestInventoryTile(int id, int x, int y, string tile_type, int ownerId) : base(id, x, y, tile_type)
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
                    PictureBox pictureBox = (PictureBox)sender;
                    if (pictureBox != null)
                    {
                        int chestId = Int32.Parse(pictureBox.Tag.ToString());
                        // need to find the chest using the list of game tiles and entities, and the id of a tile belonging to the chest.
                        var query = from entity in Game.Entities
                                    join tile in Game.Tiles on entity.TileId equals tile.Id
                                    where entity.EntityId == chestId
                                    select new { entity.EntityId, entity.TileId };

                        foreach (var entity in query)
                        {
                            Chest chest = (Chest)Game.Entities.Single(entity => entity.EntityId == chestId);
                            if (chest != null)
                            {
                                Game.MoveItem(chest);
                            }
                        }

                    }
                }
                else
                {
                    Game.InitialTile = this;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
