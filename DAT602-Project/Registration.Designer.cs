namespace Battlespire
{
    partial class Registration
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
            this.Title = new System.Windows.Forms.Label();
            this.label_username = new System.Windows.Forms.Label();
            this.username_input = new System.Windows.Forms.TextBox();
            this.password_input = new System.Windows.Forms.TextBox();
            this.label_password = new System.Windows.Forms.Label();
            this.register_button = new System.Windows.Forms.Button();
            this.email_input = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.redirect_label = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Title.Location = new System.Drawing.Point(270, 63);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(274, 44);
            this.Title.TabIndex = 0;
            this.Title.Text = "RE:Battlespire";
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Location = new System.Drawing.Point(157, 157);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(91, 25);
            this.label_username.TabIndex = 1;
            this.label_username.Text = "Username";
            this.label_username.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // username_input
            // 
            this.username_input.Location = new System.Drawing.Point(254, 154);
            this.username_input.Name = "username_input";
            this.username_input.Size = new System.Drawing.Size(337, 31);
            this.username_input.TabIndex = 2;
            // 
            // password_input
            // 
            this.password_input.Location = new System.Drawing.Point(254, 255);
            this.password_input.Name = "password_input";
            this.password_input.PasswordChar = '*';
            this.password_input.Size = new System.Drawing.Size(337, 31);
            this.password_input.TabIndex = 3;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(161, 258);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(87, 25);
            this.label_password.TabIndex = 4;
            this.label_password.Text = "Password";
            this.label_password.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // register_button
            // 
            this.register_button.Location = new System.Drawing.Point(337, 366);
            this.register_button.Name = "register_button";
            this.register_button.Size = new System.Drawing.Size(112, 34);
            this.register_button.TabIndex = 5;
            this.register_button.Text = "Submit";
            this.register_button.UseVisualStyleBackColor = true;
            this.register_button.Click += new System.EventHandler(this.register_button_Click);
            // 
            // email_input
            // 
            this.email_input.Location = new System.Drawing.Point(254, 205);
            this.email_input.Name = "email_input";
            this.email_input.Size = new System.Drawing.Size(337, 31);
            this.email_input.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Location = new System.Drawing.Point(194, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Email";
            // 
            // redirect_label
            // 
            this.redirect_label.AutoSize = true;
            this.redirect_label.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.redirect_label.Location = new System.Drawing.Point(405, 289);
            this.redirect_label.Name = "redirect_label";
            this.redirect_label.Size = new System.Drawing.Size(186, 21);
            this.redirect_label.TabIndex = 8;
            this.redirect_label.TabStop = true;
            this.redirect_label.Text = "Already have an account?";
            this.redirect_label.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.redirect_label_LinkClicked);
            // 
            // Registration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.redirect_label);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.email_input);
            this.Controls.Add(this.register_button);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.password_input);
            this.Controls.Add(this.username_input);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.Title);
            this.Name = "Registration";
            this.Text = "LoginAndRegistration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label Title;
        private Label label_username;
        private TextBox username_input;
        private TextBox password_input;
        private Label label_password;
        private Button register_button;
        private TextBox email_input;
        private Label label1;
        private LinkLabel redirect_label;
    }
}