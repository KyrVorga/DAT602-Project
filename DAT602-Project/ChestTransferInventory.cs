﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battlespire
{
    public partial class ChestTransferInventory : InventoryForm
    {
        public ChestTransferInventory(Inventory inventory, int chest_id) : base(inventory) //
        {
            InitializeComponent();
        }
    }
}