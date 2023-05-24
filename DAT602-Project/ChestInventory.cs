using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    internal class ChestInventory : Inventory
    {
        public ChestInventory(int entity_id) : base(entity_id)
        {

            InventoryForm = new ChestTransferForm(this, entity_id);
        }
        public override void MoveItem()
        {
            base.MoveItem();
            InventoryForm.UpdateChestBoard();
        }
    }
}
