using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public class Item : Entity
    {
        private string _name;
        private int _ownerId;
        private int _health;
        private int _attack;
        private int _defense;
        private int _healing;
        private bool _isEquipped;

        public Item(int entityId, string name, string entityType, int tileId, int ownerId, int health, int attack, int defense, int healing, bool isEquipped) : base(entityId, entityType, tileId)
        {
            OwnerId = ownerId;
            Name = name;
            Health = health;
            Attack = attack;
            Defense = defense;
            Healing = healing;
            IsEquipped = isEquipped;
        }

        public int Health { get => _health; set => _health = value; }
        public int Attack { get => _attack; set => _attack = value; }
        public int Defense { get => _defense; set => _defense = value; }
        public int Healing { get => _healing; set => _healing = value; }
        public string Name { get => _name; set => _name = value; }
        public int OwnerId { get => _ownerId; set => _ownerId = value; }
        public bool IsEquipped { get => _isEquipped; set => _isEquipped = value; }
    }
}
