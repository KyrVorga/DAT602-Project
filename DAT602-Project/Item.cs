using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAT602_Project
{
    internal class Item : Entity
    {
        private string _name;
        private int _owner_id;
        private int _health;
        private int _attack;
        private int _defense;
        private int _healing;

        public Item(int entity_id, string name, string entity_type, int tile_id, int owner_id, int health, int attack, int defense, int healing) : base(entity_id, entity_type, tile_id)
        {
            Owner_id = owner_id;
            Name = name;
            Health = health;
            Attack = attack;
            Defense = defense;
            Healing = healing;
        }

        public int Health { get => _health; set => _health = value; }
        public int Attack { get => _attack; set => _attack = value; }
        public int Defense { get => _defense; set => _defense = value; }
        public int Healing { get => _healing; set => _healing = value; }
        public string Name { get => _name; set => _name = value; }
        public int Owner_id { get => _owner_id; set => _owner_id = value; }
    }
}
