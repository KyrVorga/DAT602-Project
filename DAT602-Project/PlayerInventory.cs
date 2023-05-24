using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battlespire
{
    public class PlayerInventory : Inventory
    {
        public PlayerInventory(int entity_id) : base(entity_id)
        {

            InventoryForm = new InventoryForm(this);
        }
        public override void MoveItem()
        {
            base.MoveItem();
            InventoryForm.UpdateBoard();
        }


    }
}
