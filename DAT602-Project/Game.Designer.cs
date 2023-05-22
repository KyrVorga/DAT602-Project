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
            components = new System.ComponentModel.Container();
            leaderboard_box = new ListBox();
            label_leaderboard = new Label();
            chat_box = new ListBox();
            label_chat = new Label();
            chat_input = new TextBox();
            update_chat_button = new Button();
            update_leaderboard_button = new Button();
            settings_button = new Button();
            board_panel = new Panel();
            inventory_icon = new PictureBox();
            board_refresh = new System.Windows.Forms.Timer(components);
            chat_refresh = new System.Windows.Forms.Timer(components);
            leaderboard_refresh = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)inventory_icon).BeginInit();
            SuspendLayout();
            // 
            // leaderboard_box
            // 
            leaderboard_box.FormattingEnabled = true;
            leaderboard_box.ItemHeight = 15;
            leaderboard_box.Items.AddRange(new object[] { "item", "item1" });
            leaderboard_box.Location = new Point(11, 58);
            leaderboard_box.Margin = new Padding(2);
            leaderboard_box.Name = "leaderboard_box";
            leaderboard_box.Size = new Size(178, 184);
            leaderboard_box.TabIndex = 0;
            // 
            // label_leaderboard
            // 
            label_leaderboard.AutoSize = true;
            label_leaderboard.Location = new Point(11, 41);
            label_leaderboard.Margin = new Padding(2, 0, 2, 0);
            label_leaderboard.Name = "label_leaderboard";
            label_leaderboard.Size = new Size(73, 15);
            label_leaderboard.TabIndex = 1;
            label_leaderboard.Text = "Leaderboard";
            // 
            // chat_box
            // 
            chat_box.FormattingEnabled = true;
            chat_box.ItemHeight = 15;
            chat_box.Location = new Point(11, 292);
            chat_box.Margin = new Padding(2);
            chat_box.Name = "chat_box";
            chat_box.Size = new Size(178, 214);
            chat_box.TabIndex = 2;
            // 
            // label_chat
            // 
            label_chat.AutoSize = true;
            label_chat.Location = new Point(11, 274);
            label_chat.Margin = new Padding(2, 0, 2, 0);
            label_chat.Name = "label_chat";
            label_chat.Size = new Size(32, 15);
            label_chat.TabIndex = 3;
            label_chat.Text = "Chat";
            // 
            // chat_input
            // 
            chat_input.Location = new Point(11, 520);
            chat_input.Margin = new Padding(2);
            chat_input.MaxLength = 500;
            chat_input.Name = "chat_input";
            chat_input.PlaceholderText = "Enter Message";
            chat_input.Size = new Size(178, 23);
            chat_input.TabIndex = 4;
            // 
            // update_chat_button
            // 
            update_chat_button.Location = new Point(99, 263);
            update_chat_button.Margin = new Padding(2);
            update_chat_button.Name = "update_chat_button";
            update_chat_button.Size = new Size(90, 25);
            update_chat_button.TabIndex = 5;
            update_chat_button.Text = "Update";
            update_chat_button.UseVisualStyleBackColor = true;
            update_chat_button.Click += update_chat_button_Click;
            // 
            // update_leaderboard_button
            // 
            update_leaderboard_button.Location = new Point(99, 30);
            update_leaderboard_button.Margin = new Padding(2);
            update_leaderboard_button.Name = "update_leaderboard_button";
            update_leaderboard_button.Size = new Size(90, 26);
            update_leaderboard_button.TabIndex = 6;
            update_leaderboard_button.Text = "Update";
            update_leaderboard_button.UseVisualStyleBackColor = true;
            update_leaderboard_button.Click += update_leaderboard_button_Click;
            // 
            // settings_button
            // 
            settings_button.Location = new Point(779, 12);
            settings_button.Margin = new Padding(2);
            settings_button.Name = "settings_button";
            settings_button.Size = new Size(63, 24);
            settings_button.TabIndex = 7;
            settings_button.Text = "Settings";
            settings_button.UseVisualStyleBackColor = true;
            settings_button.Click += settings_button_Click;
            // 
            // board_panel
            // 
            board_panel.Location = new Point(223, 41);
            board_panel.Name = "board_panel";
            board_panel.Size = new Size(500, 500);
            board_panel.TabIndex = 8;
            // 
            // inventory_icon
            // 
            inventory_icon.BackColor = SystemColors.MenuHighlight;
            inventory_icon.Location = new Point(769, 496);
            inventory_icon.Name = "inventory_icon";
            inventory_icon.Size = new Size(50, 45);
            inventory_icon.TabIndex = 9;
            inventory_icon.TabStop = false;
            inventory_icon.Click += inventory_icon_Click;
            // 
            // board_refresh
            // 
            board_refresh.Enabled = true;
            board_refresh.Tick += update_timer_Tick;
            // 
            // chat_refresh
            // 
            chat_refresh.Enabled = true;
            chat_refresh.Interval = 1000;
            chat_refresh.Tick += chat_refresh_Tick;
            // 
            // leaderboard_refresh
            // 
            leaderboard_refresh.Enabled = true;
            leaderboard_refresh.Interval = 10000;
            leaderboard_refresh.Tick += leaderboard_refresh_Tick;
            // 
            // Game
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(853, 568);
            Controls.Add(inventory_icon);
            Controls.Add(board_panel);
            Controls.Add(settings_button);
            Controls.Add(update_leaderboard_button);
            Controls.Add(update_chat_button);
            Controls.Add(chat_input);
            Controls.Add(label_chat);
            Controls.Add(chat_box);
            Controls.Add(label_leaderboard);
            Controls.Add(leaderboard_box);
            Margin = new Padding(2);
            Name = "Game";
            Text = "Game";
            Load += Game_Load;
            ((System.ComponentModel.ISupportInitialize)inventory_icon).EndInit();
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
        public Panel board_panel;
        private PictureBox inventory_icon;
        private System.Windows.Forms.Timer board_refresh;
        private System.Windows.Forms.Timer chat_refresh;
        private System.Windows.Forms.Timer leaderboard_refresh;
    }
}