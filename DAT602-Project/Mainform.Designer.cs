namespace Battlespire
{
    partial class Mainform
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
            settings_button = new Button();
            board_panel = new Panel();
            inventory_icon = new PictureBox();
            board_refresh = new System.Windows.Forms.Timer(components);
            chat_refresh = new System.Windows.Forms.Timer(components);
            leaderboard_refresh = new System.Windows.Forms.Timer(components);
            MonsterMove = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)inventory_icon).BeginInit();
            SuspendLayout();
            // 
            // leaderboard_box
            // 
            leaderboard_box.FormattingEnabled = true;
            leaderboard_box.ItemHeight = 30;
            leaderboard_box.Items.AddRange(new object[] { "item", "item1" });
            leaderboard_box.Location = new Point(19, 116);
            leaderboard_box.Margin = new Padding(3, 4, 3, 4);
            leaderboard_box.Name = "leaderboard_box";
            leaderboard_box.Size = new Size(302, 364);
            leaderboard_box.TabIndex = 0;
            // 
            // label_leaderboard
            // 
            label_leaderboard.AutoSize = true;
            label_leaderboard.Location = new Point(19, 82);
            label_leaderboard.Name = "label_leaderboard";
            label_leaderboard.Size = new Size(129, 30);
            label_leaderboard.TabIndex = 1;
            label_leaderboard.Text = "Leaderboard";
            // 
            // chat_box
            // 
            chat_box.FormattingEnabled = true;
            chat_box.ItemHeight = 30;
            chat_box.Location = new Point(19, 584);
            chat_box.Margin = new Padding(3, 4, 3, 4);
            chat_box.Name = "chat_box";
            chat_box.Size = new Size(302, 424);
            chat_box.TabIndex = 2;
            // 
            // label_chat
            // 
            label_chat.AutoSize = true;
            label_chat.Location = new Point(19, 548);
            label_chat.Name = "label_chat";
            label_chat.Size = new Size(56, 30);
            label_chat.TabIndex = 3;
            label_chat.Text = "Chat";
            // 
            // chat_input
            // 
            chat_input.Location = new Point(19, 1040);
            chat_input.Margin = new Padding(3, 4, 3, 4);
            chat_input.MaxLength = 500;
            chat_input.Name = "chat_input";
            chat_input.PlaceholderText = "Enter Message";
            chat_input.Size = new Size(302, 35);
            chat_input.TabIndex = 4;
            // 
            // settings_button
            // 
            settings_button.Location = new Point(1335, 24);
            settings_button.Margin = new Padding(3, 4, 3, 4);
            settings_button.Name = "settings_button";
            settings_button.Size = new Size(108, 48);
            settings_button.TabIndex = 7;
            settings_button.Text = "Settings";
            settings_button.UseVisualStyleBackColor = true;
            settings_button.Click += settings_button_Click;
            // 
            // board_panel
            // 
            board_panel.Location = new Point(382, 82);
            board_panel.Margin = new Padding(5, 6, 5, 6);
            board_panel.Name = "board_panel";
            board_panel.Size = new Size(857, 1000);
            board_panel.TabIndex = 8;
            // 
            // inventory_icon
            // 
            inventory_icon.BackColor = SystemColors.MenuHighlight;
            inventory_icon.Location = new Point(1318, 992);
            inventory_icon.Margin = new Padding(5, 6, 5, 6);
            inventory_icon.Name = "inventory_icon";
            inventory_icon.Size = new Size(86, 90);
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
            // MonsterMove
            // 
            MonsterMove.Enabled = true;
            MonsterMove.Interval = 1500;
            MonsterMove.Tick += MonsterMove_Tick;
            // 
            // Mainform
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1462, 1136);
            Controls.Add(inventory_icon);
            Controls.Add(board_panel);
            Controls.Add(settings_button);
            Controls.Add(chat_input);
            Controls.Add(label_chat);
            Controls.Add(chat_box);
            Controls.Add(label_leaderboard);
            Controls.Add(leaderboard_box);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Mainform";
            Text = "Game";
            FormClosing += Mainform_FormClosing;
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
        private Button settings_button;
        public Panel board_panel;
        private PictureBox inventory_icon;
        private System.Windows.Forms.Timer board_refresh;
        private System.Windows.Forms.Timer chat_refresh;
        private System.Windows.Forms.Timer leaderboard_refresh;
        private System.Windows.Forms.Timer MonsterMove;
    }
}