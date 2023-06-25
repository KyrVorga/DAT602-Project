using Battlespire;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Battlespire
{
    public class Game
    {

        private static Mainform _mainform;
        private static Player _currentPlayer;
        private static List<Entity> _entities = new();
        private static List<Tile> _tiles = new();
        private static GameDAO _dbConnection;
        private static Tile? _initialTile;
        private static Tile? _targetTile;
        private static string _playerName;
        public Game(Mainform mainform, string username)
        {
            Mainform = mainform;

            DbConnection = new();
            PlayerName = username;
            CurrentPlayer = DbConnection.LoadPlayer(username);
            Tiles = GetTiles();
            Entities = GetEntities();
            GenerateBoard(Mainform.board_panel, Tiles, CurrentPlayer.EntityId, -5, -5, 5, 5);
            UpdateGameBoard(Mainform.board_panel, Tiles, Entities);
        }

        public static Mainform Mainform { get => _mainform; set => _mainform = value; }
        public static Player CurrentPlayer { get => _currentPlayer; set => _currentPlayer = value; }
        public static List<Entity> Entities { get => _entities; set => _entities = value; }
        public static List<Tile> Tiles { get => _tiles; set => _tiles = value; }
        internal static GameDAO DbConnection { get => _dbConnection; set => _dbConnection = value; }
        public static Tile? TargetTile { get => _targetTile; set => _targetTile = value; }
        public static Tile? InitialTile { get => _initialTile; set => _initialTile = value; }
        public static string PlayerName { get => _playerName; set => _playerName = value; }

        public static void GenerateBoard(Control panel, List<Tile> tiles, int ownerId, int xStart, int yStart, int xEnd, int yEnd)
        {
            try
            {
                int tilesAccross = xEnd - xStart + 1;
                int tilesVertical = yEnd - yStart + 1;
                int tileBorder = 1 * tilesAccross;
                int boardWidth = panel.Width;
                int boardHeight = panel.Height;
                int tileDimension = boardWidth / tilesAccross;
                int tileWidth = boardWidth / tilesAccross;
                int tileHeight = boardHeight / tilesVertical;
                panel.Controls.Clear();


                int index = 0;
                for (int i = xStart; i <= xEnd; i++)
                {
                    for (int j = yStart; j <= yEnd; j++)
                    {
                        PictureBox pictureBox = new();
                        pictureBox.BackColor = Color.Gray;
                        pictureBox.Tag = ownerId;
                        if (xStart < 0 && yStart < 0)
                        {
                            pictureBox.Width = tileWidth;
                            pictureBox.Height = tileHeight;
                            pictureBox.Location = new Point(boardWidth / 2 + i * (pictureBox.Width + 1), boardHeight / 2 + j * (pictureBox.Height + 1));
                        }
                        else
                        {
                            pictureBox.Width = tileDimension;
                            pictureBox.Height = tileDimension;
                            pictureBox.Location = new Point(i * (pictureBox.Height + 1), j * (pictureBox.Width + 1));
                        }
                        pictureBox.Click += tiles[index].Tile_Click;
                        panel.Controls.Add(pictureBox);
                        index++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        public static List<Tile> GetTiles()
        {
            return DbConnection.GetTilesByPlayer(CurrentPlayer.EntityId);
        }
        public static List<Entity> GetEntities()
        {
            return DbConnection.LoadEntities(CurrentPlayer.EntityId);
        }


        public static void MoveNPCMonsters()
        {
            try
            {
                var query = from entity in Entities
                            select new { entity.EntityId, entity.EntityType };

                foreach (var entity in query)
                {
                    if (entity.EntityType == "monster")
                    {
                        DbConnection.MoveMonster(entity.EntityId);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        public static void MoveItem(Player player)
        {
            try
            {
                List<Tile> tiles = player.Inventory.Tiles;
                List<Item> items = player.Inventory.Items;

                // check if the tile has an item on it.
                var query = from item in items
                            join tile in tiles on item.TileId equals tile.Id
                            where tile.Id == InitialTile.Id
                            select new { item.EntityId };


                var query1 = from item in items
                             join tile in tiles on item.TileId equals tile.Id
                             where tile.Id == TargetTile.Id
                             select new { item.EntityId };


                var zip = query.Zip(query1);
                foreach (var pair in zip)
                {
                    if (pair.First != null)
                    {
                        DbConnection.MoveInventoryItem(pair.First.EntityId, InitialTile.Id, TargetTile.Id);
                    }
                    if (pair.Second != null)
                    {

                        DbConnection.MoveInventoryItem(pair.Second.EntityId, TargetTile.Id, InitialTile.Id);
                    }
                }
                if (!query1.Any())
                {

                    foreach (var item in query)
                    {
                        if (item != null)
                        {
                            DbConnection.MoveInventoryItem(item.EntityId, InitialTile.Id, TargetTile.Id);
                        }
                    }
                }


                player.Inventory.GetItems();
                TargetTile = null;
                InitialTile = null;
                player.Inventory.InventoryForm.UpdateBoard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        public static void MoveItem(Chest chest)
        {
            try
            {
                List<Tile> tiles = chest.Inventory.Tiles;
                List<Item> items = chest.Inventory.Items;

                // check if the tile has an item on it.
                var query = from item in items
                            join tile in tiles on item.TileId equals tile.Id
                            where tile.Id == InitialTile.Id
                            select new { item.EntityId };


                var query1 = from item in items
                             join tile in tiles on item.TileId equals tile.Id
                             where tile.Id == TargetTile.Id
                             select new { item.EntityId };


                var zip = query.Zip(query1);
                foreach (var pair in zip)
                {
                    if (pair.First != null)
                    {
                        DbConnection.MoveInventoryItem(pair.First.EntityId, InitialTile.Id, TargetTile.Id);
                    }
                    if (pair.Second != null)
                    {

                        DbConnection.MoveInventoryItem(pair.Second.EntityId, TargetTile.Id, InitialTile.Id);
                    }
                }
                if (!query1.Any())
                {

                    foreach (var item in query)
                    {
                        if (item != null)
                        {
                            DbConnection.MoveInventoryItem(item.EntityId, InitialTile.Id, TargetTile.Id);
                        }
                    }
                }


                chest.Inventory.GetItems();
                TargetTile = null;
                InitialTile = null;
                chest.Inventory.ChestTransferForm.UpdateBoard();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        public static void TransferItem(Item item)
        {
            try
            {
                DbConnection.TransferItem(item.EntityId, CurrentPlayer.EntityId);
                InitialTile = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        public static void ClearBoard(Control panel, List<Tile> tiles)
        {
            try
            {
                int i = 0;
                foreach (var tile in tiles)
                {
                    Control box = panel.Controls[i];
                    box.Name = tile.Id.ToString();
                    if (tile.TileType == "wall")
                    {
                        box.BackColor = Color.Black;
                    }
                    else if (tile.TileType == "exit")
                    {
                        box.BackColor = Color.Beige;
                    }
                    else
                    {
                        box.BackColor = Color.Gray;
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }


        public static void UpdateGameBoard(Control panel, List<Tile> tiles, List<Entity> entities)
        {
            try
            {
                ClearBoard(panel, tiles);
                var query = from entity in entities
                            join tile in tiles on entity.TileId equals tile.Id
                            select new { entity.EntityId, entity.TileId, entity.EntityType };

                foreach (var entity in query)
                {
                    if (entity.EntityId == CurrentPlayer.EntityId)
                    {
                        panel.Controls[entity.TileId.ToString()].BackColor = Color.Purple;
                    }
                    else if (entity.EntityType == "player")
                    {
                        panel.Controls[entity.TileId.ToString()].BackColor = Color.Green;
                    }
                    else if (entity.EntityType == "monster")
                    {
                        panel.Controls[entity.TileId.ToString()].BackColor = Color.Red;
                    }
                    else if (entity.EntityType == "chest")
                    {
                        panel.Controls[entity.TileId.ToString()].BackColor = Color.Yellow;
                    }
                }

                panel.Controls[CurrentPlayer.TileId.ToString()].BackColor = Color.BlueViolet;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        public static void UpdateInventoryBoard(Control panel, List<Tile> tiles, List<Item> items)
        {
            try
            {
                ClearBoard(panel, tiles);
                var query = from item in items
                            join tile in tiles on item.TileId equals tile.Id
                            select new { item.EntityId, item.TileId, item.Name, item.IsEquipped };

                foreach (var item in query)
                {
                    if (item.Name.StartsWith("Amulet"))
                    {
                        panel.Controls[item.TileId.ToString()].BackColor = Color.Green;
                    }
                    else if (item.Name.StartsWith("Sword"))
                    {
                        panel.Controls[item.TileId.ToString()].BackColor = Color.Orange;
                    }
                    else if (item.Name.StartsWith("Armour"))
                    {
                        panel.Controls[item.TileId.ToString()].BackColor = Color.Red;
                    }
                    else if (item.Name.StartsWith("Shield"))
                    {
                        panel.Controls[item.TileId.ToString()].BackColor = Color.Blue;
                    }
                    if (item.IsEquipped == true)
                    {
                        panel.Controls[item.TileId.ToString()].BackColor = Color.Pink;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }

        internal static void DamageEntity(int attackerId, int defenderId)
        {
            try
            {
                DbConnection.DamageEntity(attackerId, defenderId);
                Mainform.ReloadGame();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Something went wrong.\n{0}", ex.Message));
            }
        }
    }
}
