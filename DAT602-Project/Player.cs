using Battlespire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    public partial class Player : Entity
    {
        private int _health;
        private int _current_health;
        private int _attack;
        private int _defense;
        private int _healing;
        private int _account_id;
        private int _killscore;
        private Inventory _inventory;

        public Player (int entity_id, int health, int current_health, int attack, int defense, int healing, int account_id, string entity_type, int tile_id, int killscore) : base(entity_id, entity_type, tile_id) {
            Health = health;
            Current_health = current_health;
            Attack = attack;
            Defense = defense;
            Healing = healing;
            Account_id = account_id;
            Killscore = killscore;
            Inventory = new(Entity_id);
        }

        public void CalculateStats()
        {

            var query = from item in Inventory.Item_list
                        where item.Is_equipped == true
                        select new { item.Attack, item.Healing, item.Health, item.Defense };

            int total_attack = 0;
            int total_defense = 0;
            int total_health = 0;
            int total_healing = 0;

            foreach (var item in query)
            {
                if (item != null)
                {
                    total_attack += item.Attack;
                    total_defense += item.Defense;
                    total_health += item.Health;
                    total_healing += item.Healing;
                }

            }
            Attack = total_attack;
            Defense = total_defense;
            Health = total_health;
            Healing = total_healing;
        }

        public int Health { get => _health; set => _health = value; }
        public int Current_health { get => _current_health; set => _current_health = value; }
        public int Attack { get => _attack; set => _attack = value; }
        public int Defense { get => _defense; set => _defense = value; }
        public int Healing { get => _healing; set => _healing = value; }
        public int Account_id { get => _account_id; set => _account_id = value; }
        public int Killscore { get => _killscore; set => _killscore = value; }
        internal Inventory Inventory { get => _inventory; set => _inventory = value; }

        public override string ToString()
        {
            return string.Format("Type: {0} | ID: {1} | Tile: {2} | Account: {3}", Entity_type, Entity_id, Tile_id, Account_id);
        }
    }
}
