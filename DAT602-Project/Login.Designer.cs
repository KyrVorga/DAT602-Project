namespace Battlespire
{
    partial class Login
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
            title = new Label();
            label_username = new Label();
            label_password = new Label();
            username_input = new TextBox();
            password_input = new TextBox();
            login_button = new Button();
            redirect_label = new LinkLabel();
            SuspendLayout();
            // 
            // title
            // 
            title.AutoSize = true;
            title.Font = new Font("Verdana", 18F, FontStyle.Regular, GraphicsUnit.Point);
            title.Location = new Point(182, 35);
            title.Margin = new Padding(2, 0, 2, 0);
            title.Name = "title";
            title.Size = new Size(182, 29);
            title.TabIndex = 0;
            title.Text = "RE:Battlespire";
            // 
            // label_username
            // 
            label_username.AutoSize = true;
            label_username.Location = new Point(124, 106);
            label_username.Margin = new Padding(2, 0, 2, 0);
            label_username.Name = "label_username";
            label_username.Size = new Size(60, 15);
            label_username.TabIndex = 1;
            label_username.Text = "Username";
            label_username.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label_password
            // 
            label_password.AutoSize = true;
            label_password.Location = new Point(127, 146);
            label_password.Margin = new Padding(2, 0, 2, 0);
            label_password.Name = "label_password";
            label_password.Size = new Size(57, 15);
            label_password.TabIndex = 2;
            label_password.Text = "Password";
            label_password.TextAlign = ContentAlignment.MiddleRight;
            // 
            // username_input
            // 
            username_input.Location = new Point(192, 104);
            username_input.Margin = new Padding(2, 2, 2, 2);
            username_input.Name = "username_input";
            username_input.Size = new Size(207, 23);
            username_input.TabIndex = 3;
            // 
            // password_input
            // 
            password_input.Location = new Point(192, 144);
            password_input.Margin = new Padding(2, 2, 2, 2);
            password_input.Name = "password_input";
            password_input.PasswordChar = '*';
            password_input.Size = new Size(207, 23);
            password_input.TabIndex = 4;
            // 
            // login_button
            // 
            login_button.Location = new Point(244, 196);
            login_button.Margin = new Padding(2, 2, 2, 2);
            login_button.Name = "login_button";
            login_button.Size = new Size(78, 20);
            login_button.TabIndex = 5;
            login_button.Text = "Login";
            login_button.UseVisualStyleBackColor = true;
            login_button.Click += login_button_Click;
            // 
            // redirect_label
            // 
            redirect_label.AutoSize = true;
            redirect_label.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            redirect_label.Location = new Point(271, 169);
            redirect_label.Margin = new Padding(2, 0, 2, 0);
            redirect_label.Name = "redirect_label";
            redirect_label.Size = new Size(128, 13);
            redirect_label.TabIndex = 6;
            redirect_label.TabStop = true;
            redirect_label.Text = "Don't have an account?";
            redirect_label.LinkClicked += redirect_label_LinkClicked;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 278);
            Controls.Add(redirect_label);
            Controls.Add(login_button);
            Controls.Add(password_input);
            Controls.Add(username_input);
            Controls.Add(label_password);
            Controls.Add(label_username);
            Controls.Add(title);
            Margin = new Padding(2, 2, 2, 2);
            Name = "Login";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label title;
        private Label label_username;
        private Label label_password;
        private TextBox username_input;
        private TextBox password_input;
        private Button login_button;
        private LinkLabel redirect_label;
    }
}