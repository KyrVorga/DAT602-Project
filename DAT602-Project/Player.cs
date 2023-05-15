using Battlespire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    public class Player : Entity
    {
        private int _health;
        private int _current_health;
        private int _attack;
        private int _defense;
        private int _healing;
        private int _account_id;
        private int _killscore;
        public Player (int entity_id, int health, int current_health, int attack, int defense, int healing, int account_id, string entity_type, int tile_id, int killscore) : base(entity_id, entity_type, tile_id) {
            
            Health = health;
            Current_health = current_health;
            Attack = attack;
            Defense = defense;
            Healing = healing;
            Account_id = account_id;
            Killscore = killscore;
        }

        public int Health { get => _health; set => _health = value; }
        public int Current_health { get => _current_health; set => _current_health = value; }
        public int Attack { get => _attack; set => _attack = value; }
        public int Defense { get => _defense; set => _defense = value; }
        public int Healing { get => _healing; set => _healing = value; }
        public int Account_id { get => _account_id; set => _account_id = value; }
        public int Killscore { get => _killscore; set => _killscore = value; }


        public override string ToString()
        {
            return string.Format("Type: {0} | ID: {1} | Tile: {2} | Account: {3}", Entity_type, Entity_id, Tile_id, Account_id);
        }

        public void PlayerMove(int target_tile_id)
        {

            var target_tile = Game.Tile_list
                .First(tile => tile.Id == target_tile_id);

            var current_tile = Game.Tile_list
                .First(tile => tile.Id == Game.Current_player.Tile_id);

            if (current_tile != null && target_tile != null)
            {
                if (current_tile.X <= target_tile.X + 1 && current_tile.X >= target_tile.X - 1 && current_tile.Y <= target_tile.Y + 1 && current_tile.Y >= target_tile.Y - 1)
                {
                    Game.Current_player.Tile_id = current_tile.Id;

                    GameDAO db_connection = new();
                    db_connection.MovePlayer(target_tile.Id, Game.Current_player.Entity_id);
                }
            }
        }
    }
}
