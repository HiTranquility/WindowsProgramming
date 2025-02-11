namespace Day1
{
    partial class ComplexNumberForm
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
            this.firstButton = new System.Windows.Forms.Button();
            this.secondButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.multiplyButton = new System.Windows.Forms.Button();
            this.substractButton = new System.Windows.Forms.Button();
            this.realLabel = new System.Windows.Forms.Label();
            this.imaginaryLabel = new System.Windows.Forms.Label();
            this.realTextBox = new System.Windows.Forms.TextBox();
            this.imaginaryTextBox = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // firstButton
            // 
            this.firstButton.Location = new System.Drawing.Point(445, 23);
            this.firstButton.Name = "firstButton";
            this.firstButton.Size = new System.Drawing.Size(116, 30);
            this.firstButton.TabIndex = 0;
            this.firstButton.Text = "firstBt";
            this.firstButton.UseVisualStyleBackColor = true;
            this.firstButton.Click += new System.EventHandler(this.firstButton_Click);
            // 
            // secondButton
            // 
            this.secondButton.Location = new System.Drawing.Point(463, 104);
            this.secondButton.Name = "secondButton";
            this.secondButton.Size = new System.Drawing.Size(98, 26);
            this.secondButton.TabIndex = 1;
            this.secondButton.Text = "secondBt";
            this.secondButton.UseVisualStyleBackColor = true;
            this.secondButton.Click += new System.EventHandler(this.secondButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(80, 180);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 26);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // multiplyButton
            // 
            this.multiplyButton.Location = new System.Drawing.Point(327, 180);
            this.multiplyButton.Name = "multiplyButton";
            this.multiplyButton.Size = new System.Drawing.Size(75, 26);
            this.multiplyButton.TabIndex = 3;
            this.multiplyButton.Text = "multiply";
            this.multiplyButton.UseVisualStyleBackColor = true;
            this.multiplyButton.Click += new System.EventHandler(this.multiplyButton_Click);
            // 
            // substractButton
            // 
            this.substractButton.Location = new System.Drawing.Point(202, 180);
            this.substractButton.Name = "substractButton";
            this.substractButton.Size = new System.Drawing.Size(75, 26);
            this.substractButton.TabIndex = 4;
            this.substractButton.Text = "subtract";
            this.substractButton.UseVisualStyleBackColor = true;
            this.substractButton.Click += new System.EventHandler(this.substractButton_Click);
            // 
            // realLabel
            // 
            this.realLabel.AutoSize = true;
            this.realLabel.Location = new System.Drawing.Point(97, 40);
            this.realLabel.Name = "realLabel";
            this.realLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.realLabel.Size = new System.Drawing.Size(24, 13);
            this.realLabel.TabIndex = 5;
            this.realLabel.Text = "real";
            this.realLabel.Click += new System.EventHandler(this.realLabel_Click);
            // 
            // imaginaryLabel
            // 
            this.imaginaryLabel.AutoSize = true;
            this.imaginaryLabel.Location = new System.Drawing.Point(107, 111);
            this.imaginaryLabel.Name = "imaginaryLabel";
            this.imaginaryLabel.Size = new System.Drawing.Size(51, 13);
            this.imaginaryLabel.TabIndex = 7;
            this.imaginaryLabel.Text = "imaginary";
            // 
            // realTextBox
            // 
            this.realTextBox.Location = new System.Drawing.Point(212, 37);
            this.realTextBox.Name = "realTextBox";
            this.realTextBox.Size = new System.Drawing.Size(139, 20);
            this.realTextBox.TabIndex = 8;
            this.realTextBox.TextChanged += new System.EventHandler(this.realTextBox_TextChanged);
            // 
            // imaginaryTextBox
            // 
            this.imaginaryTextBox.Location = new System.Drawing.Point(263, 104);
            this.imaginaryTextBox.Name = "imaginaryTextBox";
            this.imaginaryTextBox.Size = new System.Drawing.Size(139, 20);
            this.imaginaryTextBox.TabIndex = 9;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(94, 239);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(38, 13);
            this.statusLabel.TabIndex = 10;
            this.statusLabel.Text = "Notice";
            this.statusLabel.Click += new System.EventHandler(this.statusLabel_Click);
            // 
            // ComplexNumberForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.imaginaryTextBox);
            this.Controls.Add(this.realTextBox);
            this.Controls.Add(this.imaginaryLabel);
            this.Controls.Add(this.realLabel);
            this.Controls.Add(this.substractButton);
            this.Controls.Add(this.multiplyButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.secondButton);
            this.Controls.Add(this.firstButton);
            this.Name = "ComplexNumberForm";
            this.Text = "ComplexNumber_22110089_DangHoangViet";
            this.Load += new System.EventHandler(this.ComplexNumberForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button firstButton;
        private System.Windows.Forms.Button secondButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button multiplyButton;
        private System.Windows.Forms.Button substractButton;
        private System.Windows.Forms.Label realLabel;
        private System.Windows.Forms.Label imaginaryLabel;
        private System.Windows.Forms.TextBox realTextBox;
        private System.Windows.Forms.TextBox imaginaryTextBox;
        private System.Windows.Forms.Label statusLabel;
    }
}

