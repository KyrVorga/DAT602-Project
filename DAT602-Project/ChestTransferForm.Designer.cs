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
            chest_inventory_board = new Panel();

            chest_inventory_board.Location = new Point(12, 364);
            chest_inventory_board.Name = "inventory_board";
            chest_inventory_board.Size = new Size(260, 130);
            chest_inventory_board.TabIndex = 0;

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 506);
            this.Text = "ChestTransferForm";

            Controls.Add(chest_inventory_board);
            Load += ChestTransferForm_Load;
        }

        #endregion

        private Panel chest_inventory_board;
    }
}