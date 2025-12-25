namespace SalesManagementB
{
    partial class PassLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassLogin));
            lblClear = new Panel();
            chxPass = new CheckBox();
            pictureBox2 = new PictureBox();
            pictureBox10 = new PictureBox();
            tbxPass = new TextBox();
            btnLogin = new Button();
            lblPass = new Label();
            lblName = new Label();
            label1 = new Label();
            tbxEmID = new TextBox();
            btnClose = new Button();
            lblClear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            SuspendLayout();
            // 
            // lblClear
            // 
            lblClear.BackColor = Color.White;
            lblClear.Controls.Add(btnClose);
            lblClear.Controls.Add(chxPass);
            lblClear.Controls.Add(pictureBox2);
            lblClear.Controls.Add(pictureBox10);
            lblClear.Controls.Add(tbxPass);
            lblClear.Controls.Add(btnLogin);
            lblClear.Controls.Add(lblPass);
            lblClear.Controls.Add(lblName);
            lblClear.Controls.Add(label1);
            lblClear.Controls.Add(tbxEmID);
            lblClear.Location = new Point(9, 8);
            lblClear.Name = "lblClear";
            lblClear.RightToLeft = RightToLeft.No;
            lblClear.Size = new Size(361, 375);
            lblClear.TabIndex = 15;
            // 
            // chxPass
            // 
            chxPass.AutoSize = true;
            chxPass.Location = new Point(115, 286);
            chxPass.Name = "chxPass";
            chxPass.Size = new Size(124, 24);
            chxPass.TabIndex = 12;
            chxPass.Tag = "**";
            chxPass.Text = "パスワードを表示";
            chxPass.UseVisualStyleBackColor = true;
            chxPass.CheckedChanged += chxPass_CheckedChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(15, 96);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(35, 26);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 11;
            pictureBox2.TabStop = false;
            // 
            // pictureBox10
            // 
            pictureBox10.BackgroundImage = (Image)resources.GetObject("pictureBox10.BackgroundImage");
            pictureBox10.Image = (Image)resources.GetObject("pictureBox10.Image");
            pictureBox10.Location = new Point(15, 180);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(35, 26);
            pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox10.TabIndex = 10;
            pictureBox10.TabStop = false;
            // 
            // tbxPass
            // 
            tbxPass.Location = new Point(71, 180);
            tbxPass.Name = "tbxPass";
            tbxPass.Size = new Size(237, 26);
            tbxPass.TabIndex = 2;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.DimGray;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderColor = Color.White;
            btnLogin.FlatAppearance.MouseDownBackColor = Color.White;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.Chartreuse;
            btnLogin.FlatStyle = FlatStyle.Popup;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(93, 238);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(176, 42);
            btnLogin.TabIndex = 1;
            btnLogin.Text = "ログイン";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.BackColor = Color.White;
            lblPass.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            lblPass.ForeColor = Color.Black;
            lblPass.Location = new Point(71, 160);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(70, 17);
            lblPass.TabIndex = 6;
            lblPass.Text = "パスワード";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.White;
            lblName.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            lblName.ForeColor = Color.Black;
            lblName.Location = new Point(71, 76);
            lblName.Name = "lblName";
            lblName.Size = new Size(63, 17);
            lblName.TabIndex = 5;
            lblName.Text = "社員　ID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Yu Gothic UI", 9F, FontStyle.Underline, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(93, 313);
            label1.Name = "label1";
            label1.Size = new Size(188, 40);
            label1.TabIndex = 8;
            label1.Text = "パスワードを変更します\r\nログイン情報を入力してください";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // tbxEmID
            // 
            tbxEmID.Location = new Point(71, 96);
            tbxEmID.Name = "tbxEmID";
            tbxEmID.Size = new Size(237, 26);
            tbxEmID.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.Font = new Font("游ゴシック", 7F, FontStyle.Bold, GraphicsUnit.Point);
            btnClose.Location = new Point(290, 4);
            btnClose.Name = "btnClose";
            btnClose.RightToLeft = RightToLeft.No;
            btnClose.Size = new Size(68, 33);
            btnClose.TabIndex = 19;
            btnClose.Text = "戻る";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // PassLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DimGray;
            ClientSize = new Size(382, 395);
            Controls.Add(lblClear);
            Name = "PassLogin";
            Text = "パス変更-ログイン";
            lblClear.ResumeLayout(false);
            lblClear.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel lblClear;
        private CheckBox chxPass;
        private PictureBox pictureBox2;
        private PictureBox pictureBox10;
        private TextBox tbxPass;
        private Button btnLogin;
        private Label lblPass;
        private Label lblName;
        private Label label1;
        private TextBox tbxEmID;
        private Button btnClose;
    }
}
