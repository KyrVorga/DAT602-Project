namespace Battlespire
{
    partial class Game
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
            leaderboard_box = new ListBox();
            label_leaderboard = new Label();
            chat_box = new ListBox();
            label_chat = new Label();
            chat_input = new TextBox();
            update_chat_button = new Button();
            update_leaderboard_button = new Button();
            settings_button = new Button();
            GetTilesTest = new Button();
            Tiles = new ListBox();
            SuspendLayout();
            // 
            // leaderboard_box
            // 
            leaderboard_box.FormattingEnabled = true;
            leaderboard_box.ItemHeight = 30;
            leaderboard_box.Items.AddRange(new object[] { "item", "item1" });
            leaderboard_box.Location = new Point(18, 56);
            leaderboard_box.Margin = new Padding(4);
            leaderboard_box.Name = "leaderboard_box";
            leaderboard_box.Size = new Size(302, 214);
            leaderboard_box.TabIndex = 0;
            // 
            // label_leaderboard
            // 
            label_leaderboard.AutoSize = true;
            label_leaderboard.Location = new Point(18, 23);
            label_leaderboard.Margin = new Padding(4, 0, 4, 0);
            label_leaderboard.Name = "label_leaderboard";
            label_leaderboard.Size = new Size(129, 30);
            label_leaderboard.TabIndex = 1;
            label_leaderboard.Text = "Leaderboard";
            // 
            // chat_box
            // 
            chat_box.FormattingEnabled = true;
            chat_box.ItemHeight = 30;
            chat_box.Location = new Point(18, 356);
            chat_box.Margin = new Padding(4);
            chat_box.Name = "chat_box";
            chat_box.Size = new Size(302, 214);
            chat_box.TabIndex = 2;
            // 
            // label_chat
            // 
            label_chat.AutoSize = true;
            label_chat.Location = new Point(18, 323);
            label_chat.Margin = new Padding(4, 0, 4, 0);
            label_chat.Name = "label_chat";
            label_chat.Size = new Size(56, 30);
            label_chat.TabIndex = 3;
            label_chat.Text = "Chat";
            // 
            // chat_input
            // 
            chat_input.Location = new Point(18, 593);
            chat_input.Margin = new Padding(4);
            chat_input.MaxLength = 500;
            chat_input.Name = "chat_input";
            chat_input.PlaceholderText = "Enter Message";
            chat_input.Size = new Size(302, 35);
            chat_input.TabIndex = 4;
            // 
            // update_chat_button
            // 
            update_chat_button.Location = new Point(167, 312);
            update_chat_button.Margin = new Padding(4);
            update_chat_button.Name = "update_chat_button";
            update_chat_button.Size = new Size(154, 41);
            update_chat_button.TabIndex = 5;
            update_chat_button.Text = "Update";
            update_chat_button.UseVisualStyleBackColor = true;
            update_chat_button.Click += update_chat_button_Click;
            // 
            // update_leaderboard_button
            // 
            update_leaderboard_button.Location = new Point(167, 8);
            update_leaderboard_button.Margin = new Padding(4);
            update_leaderboard_button.Name = "update_leaderboard_button";
            update_leaderboard_button.Size = new Size(154, 41);
            update_leaderboard_button.TabIndex = 6;
            update_leaderboard_button.Text = "Update";
            update_leaderboard_button.UseVisualStyleBackColor = true;
            update_leaderboard_button.Click += update_leaderboard_button_Click;
            // 
            // settings_button
            // 
            settings_button.Location = new Point(1114, 23);
            settings_button.Margin = new Padding(4);
            settings_button.Name = "settings_button";
            settings_button.Size = new Size(108, 49);
            settings_button.TabIndex = 7;
            settings_button.Text = "Settings";
            settings_button.UseVisualStyleBackColor = true;
            settings_button.Click += settings_button_Click;
            // 
            // GetTilesTest
            // 
            GetTilesTest.Location = new Point(425, 591);
            GetTilesTest.Name = "GetTilesTest";
            GetTilesTest.Size = new Size(131, 40);
            GetTilesTest.TabIndex = 8;
            GetTilesTest.Text = "GetTiles";
            GetTilesTest.UseVisualStyleBackColor = true;
            GetTilesTest.Click += GetTilesTest_Click;
            // 
            // Tiles
            // 
            Tiles.FormattingEnabled = true;
            Tiles.ItemHeight = 30;
            Tiles.Location = new Point(472, 366);
            Tiles.Name = "Tiles";
            Tiles.Size = new Size(210, 154);
            Tiles.TabIndex = 9;
            // 
            // Game
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 697);
            Controls.Add(Tiles);
            Controls.Add(GetTilesTest);
            Controls.Add(settings_button);
            Controls.Add(update_leaderboard_button);
            Controls.Add(update_chat_button);
            Controls.Add(chat_input);
            Controls.Add(label_chat);
            Controls.Add(chat_box);
            Controls.Add(label_leaderboard);
            Controls.Add(leaderboard_box);
            Margin = new Padding(4);
            Name = "Game";
            Text = "Game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox leaderboard_box;
        private Label label_leaderboard;
        private ListBox chat_box;
        private Label label_chat;
        private TextBox chat_input;
        private Button update_chat_button;
        private Button update_leaderboard_button;
        private Button settings_button;
        private Button GetTilesTest;
        private ListBox Tiles;
    }
}