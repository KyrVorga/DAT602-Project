using Battlespire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public partial class Player : Entity
    {
        private int _health;
        private int _current_health;
        private int _attack;
        private int _defense;
        private int _healing;
        private int _accountId;
        private int _killscore;
        private PlayerInventory _inventory;

        public Player (int entityId, int health, int currentHealth, int attack, int defense, int healing, int accountId, string entityType, int tileId, int killscore) : base(entityId, entityType, tileId) {
            Health = health;
            CurrentHealth = currentHealth;
            Attack = attack;
            Defense = defense;
            Healing = healing;
            AccountId = accountId;
            Killscore = killscore;
            Inventory = new PlayerInventory(EntityId, this);
        }

        public int Health { get => _health; set => _health = value; }
        public int CurrentHealth { get => _current_health; set => _current_health = value; }
        public int Attack { get => _attack; set => _attack = value; }
        public int Defense { get => _defense; set => _defense = value; }
        public int Healing { get => _healing; set => _healing = value; }
        public int AccountId { get => _accountId; set => _accountId = value; }
        public int Killscore { get => _killscore; set => _killscore = value; }
        internal PlayerInventory Inventory { get => _inventory; set => _inventory = value; }


        public void PlayerMove(int target_tile_id)
        {

            var target_tile = Game.Tiles
                .First(tile => tile.Id == target_tile_id);

            var current_tile = Game.Tiles
                .First(tile => tile.Id == TileId);

            if (current_tile != null && target_tile != null)
            {
                if (current_tile.X <= target_tile.X + 1 && current_tile.X >= target_tile.X - 1 && current_tile.Y <= target_tile.Y + 1 && current_tile.Y >= target_tile.Y - 1)
                {
                    TileId = target_tile.Id;


                    Game.DbConnection.MovePlayer(target_tile.Id, EntityId);
                    Game.Tiles = Game.GetTiles();
                    Game.UpdateGameBoard(Game.Mainform.board_panel, Game.Tiles, Game.Entities);
                }
            }
        }
        public void CalculateStats()
        {

            var query = from item in Inventory.Items
                        where item.IsEquipped == true
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
        public void UpdateStats()
        {
            Inventory.InventoryForm.UpdateAttackLabel(Attack);
            Inventory.InventoryForm.UpdateDefenseLabel(Defense);
            Inventory.InventoryForm.UpdateHealingLabel(Healing);
            Inventory.InventoryForm.UpdateHealthLabel(Health);
        }
        public override string ToString()
        {
            return string.Format("Type: {0} | ID: {1} | Tile: {2} | Account: {3}", EntityType, EntityId, TileId, AccountId);
        }
    }
}
