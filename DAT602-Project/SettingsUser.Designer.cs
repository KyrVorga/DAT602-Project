namespace Battlespire
{
    partial class SettingsUser
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
            deleteAccountButton = new Button();
            SuspendLayout();
            // 
            // deleteAccountButton
            // 
            deleteAccountButton.Location = new Point(243, 123);
            deleteAccountButton.Name = "deleteAccountButton";
            deleteAccountButton.Size = new Size(101, 23);
            deleteAccountButton.TabIndex = 0;
            deleteAccountButton.Text = "Delete Account";
            deleteAccountButton.UseVisualStyleBackColor = true;
            deleteAccountButton.Click += deleteAccountButton_Click;
            // 
            // SettingsUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 270);
            Controls.Add(deleteAccountButton);
            Margin = new Padding(2, 2, 2, 2);
            Name = "SettingsUser";
            Text = "SettingsUser";
            FormClosed += SettingsUser_FormClosed;
            ResumeLayout(false);
        }

        #endregion

        private Button deleteAccountButton;
    }
}