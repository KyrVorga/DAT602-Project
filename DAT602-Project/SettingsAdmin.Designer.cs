namespace Battlespire
{
    partial class SettingsAdmin
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
            this.label1 = new System.Windows.Forms.Label();
            this.player_box = new System.Windows.Forms.ListBox();
            this.update_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "All Players";
            // 
            // player_box
            // 
            this.player_box.FormattingEnabled = true;
            this.player_box.ItemHeight = 25;
            this.player_box.Location = new System.Drawing.Point(272, 113);
            this.player_box.Name = "player_box";
            this.player_box.Size = new System.Drawing.Size(180, 129);
            this.player_box.TabIndex = 1;
            // 
            // update_button
            // 
            this.update_button.Location = new System.Drawing.Point(272, 248);
            this.update_button.Name = "update_button";
            this.update_button.Size = new System.Drawing.Size(180, 34);
            this.update_button.TabIndex = 2;
            this.update_button.Text = "Update";
            this.update_button.UseVisualStyleBackColor = true;
            this.update_button.Click += new System.EventHandler(this.update_button_Click);
            // 
            // SettingsAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.update_button);
            this.Controls.Add(this.player_box);
            this.Controls.Add(this.label1);
            this.Name = "SettingsAdmin";
            this.Text = "SettingsAdmin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsAdmin_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private ListBox player_box;
        private Button update_button;
    }
}