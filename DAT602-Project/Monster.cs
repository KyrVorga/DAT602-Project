using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    internal class Monster : Entity
    {
        private int _entity_id;
        private string _name;
        private int _health;
        private int _current_health;
        private int _attack;
        private int _defense;
        private int _healing;
        public Monster(int entity_id, string name, int health, int current_health, int attack, int defense, int healing, string entity_type, int tile_id) : base(entity_id, entity_type, tile_id)
        {
            Name = name;
            Health = health;
            Current_health = current_health;
            Attack = attack;
            Defense = defense;
            Healing = healing;
        }

        public string Name { get => _name; set => _name = value; }
        public int Health { get => _health; set => _health = value; }
        public int Current_health { get => _current_health; set => _current_health = value; }
        public int Attack { get => _attack; set => _attack = value; }
        public int Defense { get => _defense; set => _defense = value; }
        public int Healing { get => _healing; set => _healing = value; }

        public override string ToString()
        {
            return string.Format("Type: {0} | ID: {1} | Tile: {2} | Name: {3}", Entity_type, Entity_id, Tile_id, Name);
        }
    }
}
