using Battlespire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DAT602_Project
{
    public class Board
    {

        private static List<Entity> entitiy_list = new();
        private static List<Tile> tile_list = new();
        private static Player current_player;
        private static Game _game;
        public Board(Game game)
        {
            Game = game;
            GameDAO db_connection = new();

            Current_player = db_connection.LoadPlayer(Game.Username);
            Entitiy_list = db_connection.LoadEntities();
            Tile_list = GetTiles();
        }
        public static List<Entity> Entitiy_list { get => entitiy_list; set => entitiy_list = value; }
        public static List<Tile> Tile_list { get => tile_list; set => tile_list = value; }
        public static Game Game { get => _game; set => _game = value; }
        public static Player Current_player { get => current_player; set => current_player = value; }

        public void PlayerMove(int target_tile_id)
        {

            var target_tile = Tile_list
                .First(tile => tile.Id == target_tile_id);

            var current_tile = Tile_list
                .First(tile => tile.Id == Current_player.Tile_id);

            if (current_tile != null && target_tile != null)
            {
                if (current_tile.X <= target_tile.X + 1 && current_tile.X >= target_tile.X - 1 && current_tile.Y <= target_tile.Y + 1 && current_tile.Y >= target_tile.Y - 1)
                {
                    Current_player.Tile_id = current_tile.Id;

                    GameDAO db_connection = new();
                    db_connection.MovePlayer(target_tile.Id, Current_player.Entity_id);
                    GenerateBoard(Game);
                }
            }
        }

        public List<Tile> GetTiles()
        {
            GameDAO db_connection = new();
            return db_connection.GetTilesByPlayer(this, Current_player.Entity_id);
        }
        /* TODO: Pictureboxes should not associate with a tile by name
         * UpdateBoard should look through the exisitng tile list and query for any missing tiles.
         */
        public static void GenerateBoard(Game game)
        {
            Console.WriteLine("Generate board");
            int tiles_accross = 11;
            int tile_border = 1 * tiles_accross;
            int board_width = game.board_panel.Width - tile_border;
            int board_height = game.board_panel.Height - tile_border;
            int tile_width = 30;// board_width / tiles_accross;
            int tile_height = 30; // board_height / tiles_accross;
            game.board_panel.Controls.Clear();
            Tile_list.ForEach(tile =>
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Name = tile.Id.ToString();
                pictureBox.BackColor = Color.Gray;
                pictureBox.Width = tile_width;
                pictureBox.Height = tile_height;
                //pictureBox.Location = new Point(board_width / 2 + tile.X * (pictureBox.Height + 1), board_height / 2 + tile.Y * (pictureBox.Width + 1));
                pictureBox.Location = new Point(250 + (tile.X * (pictureBox.Height + 1)), 250 + (tile.Y * (pictureBox.Width + 1)));
                pictureBox.Click += new EventHandler(tile.tile_Click);

                game.board_panel.Controls.Add(pictureBox);
            });

            UpdateBoard(Game);
        }


        public static void UpdateBoard(Game game)
        {
            var query = from entity in Entitiy_list
                        join tile in Tile_list on entity.Tile_id equals tile.Id
                        select new { entity.Entity_id, entity.Tile_id };
            foreach (var entity in query)
            {
                game.board_panel.Controls[entity.Tile_id.ToString()].BackColor = Color.Blue;
            }
        }
    }
}
