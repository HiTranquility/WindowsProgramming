namespace LoginForm
{
    partial class LoginForm
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
            this.usernameLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.loginButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.passwordTxt = new System.Windows.Forms.TextBox();
            this.usernameTxt = new System.Windows.Forms.TextBox();
            this.profilePic = new System.Windows.Forms.PictureBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.btShowImage = new System.Windows.Forms.Button();
            this.btChangeImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).BeginInit();
            this.SuspendLayout();
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.Location = new System.Drawing.Point(152, 178);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 13);
            this.usernameLabel.TabIndex = 0;
            this.usernameLabel.Text = "Username";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(152, 249);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "Password";
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(340, 309);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(117, 48);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(481, 334);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // passwordTxt
            // 
            this.passwordTxt.Location = new System.Drawing.Point(297, 246);
            this.passwordTxt.Name = "passwordTxt";
            this.passwordTxt.Size = new System.Drawing.Size(195, 20);
            this.passwordTxt.TabIndex = 4;
            // 
            // usernameTxt
            // 
            this.usernameTxt.Location = new System.Drawing.Point(297, 178);
            this.usernameTxt.Name = "usernameTxt";
            this.usernameTxt.Size = new System.Drawing.Size(195, 20);
            this.usernameTxt.TabIndex = 5;
            // 
            // profilePic
            // 
            this.profilePic.Location = new System.Drawing.Point(43, 43);
            this.profilePic.Name = "profilePic";
            this.profilePic.Size = new System.Drawing.Size(100, 50);
            this.profilePic.TabIndex = 6;
            this.profilePic.TabStop = false;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(276, 98);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(76, 13);
            this.titleLabel.TabIndex = 7;
            this.titleLabel.Text = "Account Login";
            // 
            // btShowImage
            // 
            this.btShowImage.Location = new System.Drawing.Point(51, 123);
            this.btShowImage.Name = "btShowImage";
            this.btShowImage.Size = new System.Drawing.Size(75, 23);
            this.btShowImage.TabIndex = 8;
            this.btShowImage.Text = "ShowImage";
            this.btShowImage.UseVisualStyleBackColor = true;
            this.btShowImage.Click += new System.EventHandler(this.btShowImage_Click);
            // 
            // btChangeImage
            // 
            this.btChangeImage.Location = new System.Drawing.Point(51, 168);
            this.btChangeImage.Name = "btChangeImage";
            this.btChangeImage.Size = new System.Drawing.Size(92, 23);
            this.btChangeImage.TabIndex = 9;
            this.btChangeImage.Text = "ChangeImage";
            this.btChangeImage.UseVisualStyleBackColor = true;
            this.btChangeImage.Click += new System.EventHandler(this.btChangeImage_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btChangeImage);
            this.Controls.Add(this.btShowImage);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.profilePic);
            this.Controls.Add(this.usernameTxt);
            this.Controls.Add(this.passwordTxt);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.usernameLabel);
            this.Name = "LoginForm";
            this.Text = "22110060_LoginForm";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox passwordTxt;
        private System.Windows.Forms.TextBox usernameTxt;
        private System.Windows.Forms.PictureBox profilePic;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button btShowImage;
        private System.Windows.Forms.Button btChangeImage;
    }
}

