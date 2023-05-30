using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    internal class Monster : Entity
    {
        private string _name;
        private int _health;
        private int _damageTaken;
        private int _attack;
        private int _defense;
        private int _healing;
        public Monster(int entityId, string name, int health, int damageTaken, int attack, int defense, int healing, string entityType, int tileId) : base(entityId, entityType, tileId)
        {
            Name = name;
            Health = health;
            DamageTaken = damageTaken;
            Attack = attack;
            Defense = defense;
            Healing = healing;
        }

        public string Name { get => _name; set => _name = value; }
        public int Health { get => _health; set => _health = value; }
        public int DamageTaken { get => _damageTaken; set => _damageTaken = value; }
        public int Attack { get => _attack; set => _attack = value; }
        public int Defense { get => _defense; set => _defense = value; }
        public int Healing { get => _healing; set => _healing = value; }

        public override string ToString()
        {
            return string.Format("Type: {0} | ID: {1} | Tile: {2} | Name: {3}", EntityType, EntityId, TileId, Name);
        }
    }
}
