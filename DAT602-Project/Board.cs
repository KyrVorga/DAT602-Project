using Battlespire;
using Microsoft.VisualBasic;
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
                    Current_player.Tile_id = target_tile.Id;

                    GameDAO db_connection = new();
                    db_connection.MovePlayer(target_tile.Id, Current_player.Entity_id);
                    Tile_list = GetTiles();
                    GenerateBoard();
                }
            }
        }

        public List<Tile> GetTiles()
        {
            GameDAO db_connection = new();
            return db_connection.GetTilesByPlayer(this, Current_player.Entity_id);
        }

        public static void GenerateBoard()
        {
            int tiles_accross = 11;
            int tile_border = 1 * tiles_accross;
            int board_width = Game.board_panel.Width - tile_border;
            int board_height = Game.board_panel.Height - tile_border;
            int tile_width = board_width / tiles_accross;
            int tile_height = board_height / tiles_accross;
            int ending_position = ((tiles_accross - 1) / 2);
            int starting_position = ((tiles_accross - 1) / 2) * -1;

            Game.board_panel.Controls.Clear();

            for (int i = starting_position;  i <= ending_position; i++)
            {
                for (int j = starting_position; j <= ending_position; j++)
                {
                    PictureBox pictureBox = new();
                    pictureBox.BackColor = Color.Gray;
                    pictureBox.Width = tile_width;
                    pictureBox.Height = tile_height;
                    pictureBox.Location = new Point(board_width / 2 + i * (pictureBox.Height + 1), board_height / 2 + j * (pictureBox.Width + 1));
                    Game.board_panel.Controls.Add(pictureBox);
                }
            }


            UpdateBoard();
        }


        public static void UpdateBoard()
        {

            Console.Write( Tile_list.Count);
            for (int i = 0; i < Game.board_panel.Controls.Count; i++)
            {
                Tile tile = Tile_list[i];
                Control box = Game.board_panel.Controls[i];

                box.Name = tile.Id.ToString();
                box.Click += new EventHandler(tile.Tile_Click);
            }




            var query = from entity in entitiy_list
                        join tile in tile_list on entity.Tile_id equals tile.Id
                        select new { entity.Entity_id, entity.Tile_id, entity.Entity_type };
            foreach (var entity in query)
            {
                if (entity.Entity_id == current_player.Entity_id)
                {
                    Game.board_panel.Controls[entity.Tile_id.ToString()].BackColor = Color.Purple;
                } else if (entity.Entity_type == "player")
                {
                    Game.board_panel.Controls[entity.Tile_id.ToString()].BackColor = Color.Green;
                } else if (entity.Entity_type == "monster")
                {
                    Game.board_panel.Controls[entity.Tile_id.ToString()].BackColor = Color.Red;
                } else if (entity.Entity_type == "chest")
                {
                    Game.board_panel.Controls[entity.Tile_id.ToString()].BackColor = Color.Yellow;
                }
            }

            Game.board_panel.Controls[current_player.Tile_id.ToString()].BackColor = Color.BlueViolet;

        }
    }
}
