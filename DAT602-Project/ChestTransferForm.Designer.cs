namespace Battlespire
{
    partial class ChestTransferForm
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
            inventory_board = new Panel();
            inventory_label = new Label();
            take_button = new Button();
            SuspendLayout();
            // 
            // inventory_board
            // 
            inventory_board.Location = new Point(12, 44);
            inventory_board.Name = "inventory_board";
            inventory_board.Size = new Size(260, 130);
            inventory_board.TabIndex = 0;
            // 
            // inventory_label
            // 
            inventory_label.AutoSize = true;
            inventory_label.Location = new Point(12, 26);
            inventory_label.Name = "inventory_label";
            inventory_label.Size = new Size(57, 15);
            inventory_label.TabIndex = 7;
            inventory_label.Text = "Inventory";
            // 
            // take_button
            // 
            take_button.Location = new Point(197, 18);
            take_button.Name = "take_button";
            take_button.Size = new Size(75, 23);
            take_button.TabIndex = 8;
            take_button.Text = "Take";
            take_button.UseVisualStyleBackColor = true;
            take_button.Click += take_button_Click;
            // 
            // ChestTransferForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(286, 186);
            Controls.Add(take_button);
            Controls.Add(inventory_label);
            Controls.Add(inventory_board);
            Name = "ChestTransferForm";
            Text = "ChestTransferForm";
            Load += ChestTransferForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel inventory_board;
        private Label inventory_label;
        private Button take_button;
    }
}