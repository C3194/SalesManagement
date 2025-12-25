namespace SalesManagement_SysDev
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            Form = new Label();
            panel1 = new Panel();
            btnClose = new Button();
            btnPassChange = new Button();
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
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            lblClear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Form
            // 
            Form.AutoSize = true;
            Form.BackColor = Color.FromArgb(9, 5, 126);
            Form.FlatStyle = FlatStyle.System;
            Form.Font = new Font("游ゴシック", 10.2F, FontStyle.Bold, GraphicsUnit.Point);
            Form.ForeColor = SystemColors.ControlLightLight;
            Form.Location = new Point(3, 24);
            Form.Name = "Form";
            Form.Size = new Size(145, 22);
            Form.TabIndex = 1;
            Form.Text = "販売管理システム";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(9, 5, 126);
            panel1.Controls.Add(btnClose);
            panel1.Controls.Add(btnPassChange);
            panel1.Controls.Add(Form);
            panel1.Controls.Add(lblClear);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.ForeColor = SystemColors.ControlText;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(880, 569);
            panel1.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.White;
            btnClose.Font = new Font("游ゴシック", 7F, FontStyle.Bold, GraphicsUnit.Point);
            btnClose.Location = new Point(800, 8);
            btnClose.Name = "btnClose";
            btnClose.RightToLeft = RightToLeft.No;
            btnClose.Size = new Size(68, 35);
            btnClose.TabIndex = 17;
            btnClose.Text = "終了\r\n";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnPassChange
            // 
            btnPassChange.BackColor = Color.White;
            btnPassChange.Font = new Font("游ゴシック", 7F, FontStyle.Bold, GraphicsUnit.Point);
            btnPassChange.Location = new Point(747, 448);
            btnPassChange.Name = "btnPassChange";
            btnPassChange.RightToLeft = RightToLeft.No;
            btnPassChange.Size = new Size(109, 48);
            btnPassChange.TabIndex = 16;
            btnPassChange.Text = "パスワード変更";
            btnPassChange.UseVisualStyleBackColor = false;
            btnPassChange.Click += btnPassChange_Click;
            // 
            // lblClear
            // 
            lblClear.BackColor = Color.FromArgb(238, 253, 255);
            lblClear.Controls.Add(chxPass);
            lblClear.Controls.Add(pictureBox2);
            lblClear.Controls.Add(pictureBox10);
            lblClear.Controls.Add(tbxPass);
            lblClear.Controls.Add(btnLogin);
            lblClear.Controls.Add(lblPass);
            lblClear.Controls.Add(lblName);
            lblClear.Controls.Add(label1);
            lblClear.Controls.Add(tbxEmID);
            lblClear.Location = new Point(267, 108);
            lblClear.Name = "lblClear";
            lblClear.Size = new Size(310, 347);
            lblClear.TabIndex = 14;
            // 
            // chxPass
            // 
            chxPass.AutoSize = true;
            chxPass.Location = new Point(92, 301);
            chxPass.Name = "chxPass";
            chxPass.Size = new Size(127, 24);
            chxPass.TabIndex = 12;
            chxPass.Tag = "**";
            chxPass.Text = "パスワードを表示";
            chxPass.UseVisualStyleBackColor = true;
            chxPass.CheckedChanged += chxPass_CheckedChanged_1;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(15, 101);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(35, 27);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 11;
            pictureBox2.TabStop = false;
            // 
            // pictureBox10
            // 
            pictureBox10.BackgroundImage = (Image)resources.GetObject("pictureBox10.BackgroundImage");
            pictureBox10.Image = (Image)resources.GetObject("pictureBox10.Image");
            pictureBox10.Location = new Point(15, 191);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(35, 27);
            pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox10.TabIndex = 10;
            pictureBox10.TabStop = false;
            // 
            // tbxPass
            // 
            tbxPass.Location = new Point(56, 191);
            tbxPass.Name = "tbxPass";
            tbxPass.Size = new Size(237, 27);
            tbxPass.TabIndex = 1;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(9, 5, 126);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderColor = Color.White;
            btnLogin.FlatAppearance.MouseDownBackColor = Color.White;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.Chartreuse;
            btnLogin.FlatStyle = FlatStyle.Popup;
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(69, 247);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(176, 44);
            btnLogin.TabIndex = 2;
            btnLogin.Text = "ログイン";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.BackColor = Color.FromArgb(238, 253, 255);
            lblPass.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            lblPass.ForeColor = Color.Black;
            lblPass.Location = new Point(56, 168);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(64, 19);
            lblPass.TabIndex = 6;
            lblPass.Text = "パスワード";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.BackColor = Color.FromArgb(238, 253, 255);
            lblName.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            lblName.ForeColor = Color.Black;
            lblName.Location = new Point(56, 79);
            lblName.Name = "lblName";
            lblName.Size = new Size(60, 19);
            lblName.TabIndex = 5;
            lblName.Text = "社員　ID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(238, 253, 255);
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(119, 23);
            label1.Name = "label1";
            label1.Size = new Size(84, 23);
            label1.TabIndex = 8;
            label1.Text = "Welcome";
            // 
            // tbxEmID
            // 
            tbxEmID.Location = new Point(56, 101);
            tbxEmID.Name = "tbxEmID";
            tbxEmID.Size = new Size(237, 27);
            tbxEmID.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-3, 48);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(883, 465);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(880, 569);
            Controls.Add(panel1);
            Name = "Login";
            Text = "販売管理システム-ログイン";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            lblClear.ResumeLayout(false);
            lblClear.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Label Form;
        private Panel panel1;
        private Button btnPassChange;
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
        private PictureBox pictureBox1;
        private Button btnClose;
    }
}