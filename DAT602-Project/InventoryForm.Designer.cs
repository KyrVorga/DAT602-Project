namespace DAT602_Project
{
    partial class InventoryWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            drop_item_button = new Button();
            player_health_label = new Label();
            player_attack_label = new Label();
            player_defense_label = new Label();
            player_healing_label = new Label();
            inventory_label = new Label();
            equipment_label = new Label();
            player_name_label = new Label();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Location = new Point(12, 224);
            panel1.Name = "panel1";
            panel1.Size = new Size(257, 107);
            panel1.TabIndex = 0;
            // 
            // drop_item_button
            // 
            drop_item_button.Location = new Point(196, 195);
            drop_item_button.Name = "drop_item_button";
            drop_item_button.Size = new Size(75, 23);
            drop_item_button.TabIndex = 1;
            drop_item_button.Text = "Drop";
            drop_item_button.UseVisualStyleBackColor = true;
            // 
            // player_health_label
            // 
            player_health_label.AutoSize = true;
            player_health_label.Location = new Point(196, 42);
            player_health_label.Name = "player_health_label";
            player_health_label.Size = new Size(40, 15);
            player_health_label.TabIndex = 2;
            player_health_label.Text = "health";
            // 
            // player_attack_label
            // 
            player_attack_label.AutoSize = true;
            player_attack_label.Location = new Point(196, 61);
            player_attack_label.Name = "player_attack_label";
            player_attack_label.Size = new Size(39, 15);
            player_attack_label.TabIndex = 3;
            player_attack_label.Text = "attack";
            // 
            // player_defense_label
            // 
            player_defense_label.AutoSize = true;
            player_defense_label.Location = new Point(195, 75);
            player_defense_label.Name = "player_defense_label";
            player_defense_label.Size = new Size(48, 15);
            player_defense_label.TabIndex = 4;
            player_defense_label.Text = "defense";
            // 
            // player_healing_label
            // 
            player_healing_label.AutoSize = true;
            player_healing_label.Location = new Point(193, 93);
            player_healing_label.Name = "player_healing_label";
            player_healing_label.Size = new Size(46, 15);
            player_healing_label.TabIndex = 5;
            player_healing_label.Text = "healing";
            // 
            // inventory_label
            // 
            inventory_label.AutoSize = true;
            inventory_label.Location = new Point(12, 194);
            inventory_label.Name = "inventory_label";
            inventory_label.Size = new Size(57, 15);
            inventory_label.TabIndex = 6;
            inventory_label.Text = "Inventory";
            // 
            // equipment_label
            // 
            equipment_label.AutoSize = true;
            equipment_label.Location = new Point(9, 9);
            equipment_label.Name = "equipment_label";
            equipment_label.Size = new Size(65, 15);
            equipment_label.TabIndex = 7;
            equipment_label.Text = "Equipment";
            // 
            // player_name_label
            // 
            player_name_label.AutoSize = true;
            player_name_label.Location = new Point(71, 164);
            player_name_label.Name = "player_name_label";
            player_name_label.Size = new Size(37, 15);
            player_name_label.TabIndex = 8;
            player_name_label.Text = "name";
            // 
            // InventoryWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(281, 343);
            Controls.Add(player_name_label);
            Controls.Add(equipment_label);
            Controls.Add(inventory_label);
            Controls.Add(player_healing_label);
            Controls.Add(player_defense_label);
            Controls.Add(player_attack_label);
            Controls.Add(player_health_label);
            Controls.Add(drop_item_button);
            Controls.Add(panel1);
            Name = "InventoryWindow";
            Text = "InventoryWindow";
            Load += InventoryWindow_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button drop_item_button;
        private Label player_health_label;
        private Label player_attack_label;
        private Label player_defense_label;
        private Label player_healing_label;
        private Label inventory_label;
        private Label equipment_label;
        private Label player_name_label;
    }
}