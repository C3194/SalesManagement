namespace SalesManagementB
{
    partial class PassInput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassInput));
            lblClear = new Panel();
            btnClose = new Button();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            tbxNewPass2 = new TextBox();
            label1 = new Label();
            chxPass = new CheckBox();
            pictureBox10 = new PictureBox();
            btnChange = new Button();
            lblName = new Label();
            tbxNewPass = new TextBox();
            lblClear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            SuspendLayout();
            // 
            // lblClear
            // 
            lblClear.BackColor = Color.White;
            lblClear.Controls.Add(btnClose);
            lblClear.Controls.Add(pictureBox1);
            lblClear.Controls.Add(label2);
            lblClear.Controls.Add(tbxNewPass2);
            lblClear.Controls.Add(label1);
            lblClear.Controls.Add(chxPass);
            lblClear.Controls.Add(pictureBox10);
            lblClear.Controls.Add(btnChange);
            lblClear.Controls.Add(lblName);
            lblClear.Controls.Add(tbxNewPass);
            lblClear.Location = new Point(9, 9);
            lblClear.Name = "lblClear";
            lblClear.Size = new Size(361, 374);
            lblClear.TabIndex = 16;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.Font = new Font("游ゴシック", 7F, FontStyle.Bold, GraphicsUnit.Point);
            btnClose.Location = new Point(290, 3);
            btnClose.Name = "btnClose";
            btnClose.RightToLeft = RightToLeft.No;
            btnClose.Size = new Size(68, 33);
            btnClose.TabIndex = 18;
            btnClose.Text = "戻る";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(17, 180);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(35, 26);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(69, 160);
            label2.Name = "label2";
            label2.Size = new Size(175, 17);
            label2.TabIndex = 15;
            label2.Text = "新しいパスワード（確認）";
            // 
            // tbxNewPass2
            // 
            tbxNewPass2.Location = new Point(69, 180);
            tbxNewPass2.Name = "tbxNewPass2";
            tbxNewPass2.Size = new Size(237, 26);
            tbxNewPass2.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Yu Gothic UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(50, 319);
            label1.Name = "label1";
            label1.Size = new Size(256, 20);
            label1.TabIndex = 13;
            label1.Text = "新しく登録するパスワードを入力してください\r\n";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // chxPass
            // 
            chxPass.AutoSize = true;
            chxPass.Location = new Point(110, 283);
            chxPass.Name = "chxPass";
            chxPass.Size = new Size(124, 24);
            chxPass.TabIndex = 12;
            chxPass.Tag = "**";
            chxPass.Text = "パスワードを表示";
            chxPass.UseVisualStyleBackColor = true;
            chxPass.CheckedChanged += chxPass_CheckedChanged;
            // 
            // pictureBox10
            // 
            pictureBox10.BackgroundImage = (Image)resources.GetObject("pictureBox10.BackgroundImage");
            pictureBox10.Image = (Image)resources.GetObject("pictureBox10.Image");
            pictureBox10.Location = new Point(17, 96);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(35, 26);
            pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox10.TabIndex = 10;
            pictureBox10.TabStop = false;
            // 
            // btnChange
            // 
            btnChange.BackColor = Color.DimGray;
            btnChange.Cursor = Cursors.Hand;
            btnChange.FlatAppearance.BorderColor = Color.White;
            btnChange.FlatAppearance.MouseDownBackColor = Color.White;
            btnChange.FlatAppearance.MouseOverBackColor = Color.Chartreuse;
            btnChange.FlatStyle = FlatStyle.Popup;
            btnChange.ForeColor = Color.White;
            btnChange.Location = new Point(89, 235);
            btnChange.Name = "btnChange";
            btnChange.Size = new Size(176, 42);
            btnChange.TabIndex = 1;
            btnChange.Text = "変更";
            btnChange.UseVisualStyleBackColor = false;
            btnChange.Click += btnChange_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.White;
            lblName.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            lblName.ForeColor = Color.Black;
            lblName.Location = new Point(69, 76);
            lblName.Name = "lblName";
            lblName.Size = new Size(111, 17);
            lblName.TabIndex = 5;
            lblName.Text = "新しいパスワード";
            // 
            // tbxNewPass
            // 
            tbxNewPass.Location = new Point(69, 96);
            tbxNewPass.Name = "tbxNewPass";
            tbxNewPass.Size = new Size(237, 26);
            tbxNewPass.TabIndex = 1;
            // 
            // PassInput
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(382, 395);
            Controls.Add(lblClear);
            Name = "PassInput";
            Text = "パスワード入力";
            lblClear.ResumeLayout(false);
            lblClear.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel lblClear;
        private CheckBox chxPass;
        private PictureBox pictureBox10;
        private Button btnChange;
        private Label lblName;
        private TextBox tbxNewPass;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private TextBox tbxNewPass2;
        private Button btnClose;
    }
}