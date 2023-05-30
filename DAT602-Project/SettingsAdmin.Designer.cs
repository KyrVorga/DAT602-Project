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
            label1 = new Label();
            player_box = new ListBox();
            resetGameButton = new Button();
            moveHomeButton = new Button();
            regenerateMapButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(190, 46);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 0;
            label1.Text = "All Players";
            // 
            // player_box
            // 
            player_box.FormattingEnabled = true;
            player_box.ItemHeight = 15;
            player_box.Location = new Point(190, 68);
            player_box.Margin = new Padding(2);
            player_box.Name = "player_box";
            player_box.Size = new Size(127, 79);
            player_box.TabIndex = 1;
            // 
            // resetGameButton
            // 
            resetGameButton.Location = new Point(340, 68);
            resetGameButton.Name = "resetGameButton";
            resetGameButton.Size = new Size(81, 23);
            resetGameButton.TabIndex = 3;
            resetGameButton.Text = "Reset Game";
            resetGameButton.UseVisualStyleBackColor = true;
            resetGameButton.Click += resetGameButton_Click;
            // 
            // moveHomeButton
            // 
            moveHomeButton.Location = new Point(340, 104);
            moveHomeButton.Name = "moveHomeButton";
            moveHomeButton.Size = new Size(81, 43);
            moveHomeButton.TabIndex = 4;
            moveHomeButton.Text = "Move to Home";
            moveHomeButton.UseVisualStyleBackColor = true;
            moveHomeButton.Click += moveHomeButton_Click;
            // 
            // regenerateMapButton
            // 
            regenerateMapButton.Location = new Point(340, 165);
            regenerateMapButton.Name = "regenerateMapButton";
            regenerateMapButton.Size = new Size(81, 42);
            regenerateMapButton.TabIndex = 5;
            regenerateMapButton.Text = "Regenerate Map";
            regenerateMapButton.UseVisualStyleBackColor = true;
            regenerateMapButton.Click += regenerateMapButton_Click;
            // 
            // SettingsAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 270);
            Controls.Add(regenerateMapButton);
            Controls.Add(moveHomeButton);
            Controls.Add(resetGameButton);
            Controls.Add(player_box);
            Controls.Add(label1);
            Margin = new Padding(2);
            Name = "SettingsAdmin";
            Text = "SettingsAdmin";
            FormClosed += SettingsAdmin_FormClosed;
            Load += SettingsAdmin_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox player_box;
        private Button resetGameButton;
        private Button moveHomeButton;
        private Button regenerateMapButton;
    }
}