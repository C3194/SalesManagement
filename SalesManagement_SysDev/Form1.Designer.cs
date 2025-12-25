namespace OIC_LoginFrom
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            label2 = new Label();
            btnCalc = new Button();
            pictureBox1 = new PictureBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            pictureBox3 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox5 = new PictureBox();
            pictureBox6 = new PictureBox();
            pictureBox7 = new PictureBox();
            pictureBox8 = new PictureBox();
            pictureBox9 = new PictureBox();
            lblClear = new Panel();
            label5 = new Label();
            checkBoxPass = new CheckBox();
            pictureBox2 = new PictureBox();
            pictureBox10 = new PictureBox();
            txtPass = new TextBox();
            btnSum = new Button();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            txtName = new TextBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            lblClear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Controls.Add(label2);
            panel1.Controls.Add(btnCalc);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Fill;
            panel1.ForeColor = SystemColors.ControlText;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(880, 600);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(12, 7);
            label2.Name = "label2";
            label2.Size = new Size(123, 20);
            label2.TabIndex = 1;
            label2.Text = "販売管理システム";
            label2.Click += label2_Click;
            // 
            // btnCalc
            // 
            btnCalc.BackColor = Color.Snow;
            btnCalc.BackgroundImage = (Image)resources.GetObject("btnCalc.BackgroundImage");
            btnCalc.BackgroundImageLayout = ImageLayout.Stretch;
            btnCalc.FlatAppearance.BorderSize = 0;
            btnCalc.FlatAppearance.MouseOverBackColor = Color.Red;
            btnCalc.FlatStyle = FlatStyle.Flat;
            btnCalc.Location = new Point(849, 0);
            btnCalc.Name = "btnCalc";
            btnCalc.Size = new Size(31, 27);
            btnCalc.TabIndex = 0;
            btnCalc.UseVisualStyleBackColor = false;
            btnCalc.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(880, 466);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 13;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImageLayout = ImageLayout.Center;
            pictureBox3.Dock = DockStyle.Fill;
            pictureBox3.Location = new Point(0, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(880, 600);
            pictureBox3.TabIndex = 6;
            pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.Dock = DockStyle.Fill;
            pictureBox4.Location = new Point(0, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(880, 600);
            pictureBox4.TabIndex = 7;
            pictureBox4.TabStop = false;
            pictureBox4.Click += pictureBox4_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.Dock = DockStyle.Fill;
            pictureBox5.Location = new Point(0, 0);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(880, 600);
            pictureBox5.TabIndex = 8;
            pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            pictureBox6.Dock = DockStyle.Fill;
            pictureBox6.Location = new Point(0, 0);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(880, 600);
            pictureBox6.TabIndex = 9;
            pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            pictureBox7.Dock = DockStyle.Fill;
            pictureBox7.Location = new Point(0, 0);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(880, 600);
            pictureBox7.TabIndex = 10;
            pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            pictureBox8.Dock = DockStyle.Fill;
            pictureBox8.Location = new Point(0, 0);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(880, 600);
            pictureBox8.TabIndex = 11;
            pictureBox8.TabStop = false;
            // 
            // pictureBox9
            // 
            pictureBox9.Dock = DockStyle.Fill;
            pictureBox9.Location = new Point(0, 0);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(880, 600);
            pictureBox9.TabIndex = 12;
            pictureBox9.TabStop = false;
            // 
            // lblClear
            // 
            lblClear.BackColor = SystemColors.ScrollBar;
            lblClear.BackgroundImage = (Image)resources.GetObject("lblClear.BackgroundImage");
            lblClear.Controls.Add(label5);
            lblClear.Controls.Add(checkBoxPass);
            lblClear.Controls.Add(pictureBox2);
            lblClear.Controls.Add(pictureBox10);
            lblClear.Controls.Add(txtPass);
            lblClear.Controls.Add(btnSum);
            lblClear.Controls.Add(label4);
            lblClear.Controls.Add(label3);
            lblClear.Controls.Add(label1);
            lblClear.Controls.Add(txtName);
            lblClear.Location = new Point(30, 126);
            lblClear.Name = "lblClear";
            lblClear.Size = new Size(333, 301);
            lblClear.TabIndex = 14;
            lblClear.Paint += panel2_Paint;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Silver;
            label5.ForeColor = Color.Red;
            label5.Location = new Point(52, 252);
            label5.Name = "label5";
            label5.Size = new Size(54, 20);
            label5.TabIndex = 14;
            label5.Text = "クリーン";
            label5.Click += label5_Click;
            // 
            // checkBoxPass
            // 
            checkBoxPass.AutoSize = true;
            checkBoxPass.Location = new Point(172, 252);
            checkBoxPass.Name = "checkBoxPass";
            checkBoxPass.Size = new Size(134, 24);
            checkBoxPass.TabIndex = 12;
            checkBoxPass.Text = "パスワードを表示";
            checkBoxPass.UseVisualStyleBackColor = true;
            checkBoxPass.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(15, 85);
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
            pictureBox10.Location = new Point(15, 138);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(35, 27);
            pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox10.TabIndex = 10;
            pictureBox10.TabStop = false;
            pictureBox10.Click += pictureBox10_Click;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(56, 138);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.Size = new Size(237, 27);
            txtPass.TabIndex = 7;
            // 
            // btnSum
            // 
            btnSum.BackColor = Color.MidnightBlue;
            btnSum.Cursor = Cursors.Hand;
            btnSum.FlatAppearance.BorderColor = Color.White;
            btnSum.FlatAppearance.MouseDownBackColor = Color.White;
            btnSum.FlatAppearance.MouseOverBackColor = Color.Chartreuse;
            btnSum.FlatStyle = FlatStyle.Popup;
            btnSum.ForeColor = Color.White;
            btnSum.Location = new Point(77, 185);
            btnSum.Name = "btnSum";
            btnSum.Size = new Size(155, 44);
            btnSum.TabIndex = 1;
            btnSum.Text = "ログイン";
            btnSum.UseVisualStyleBackColor = false;
            btnSum.Click += btnSum_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(224, 224, 224);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(52, 115);
            label4.Name = "label4";
            label4.Size = new Size(71, 20);
            label4.TabIndex = 6;
            label4.Text = "パスワード";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(224, 224, 224);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(52, 62);
            label3.Name = "label3";
            label3.Size = new Size(67, 20);
            label3.TabIndex = 5;
            label3.Text = "社員　ID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(224, 224, 224);
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(119, 23);
            label1.Name = "label1";
            label1.Size = new Size(82, 21);
            label1.TabIndex = 8;
            label1.Text = "Welcome";
            label1.Click += label1_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(56, 85);
            txtName.Name = "txtName";
            txtName.Size = new Size(237, 27);
            txtName.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(880, 600);
            Controls.Add(lblClear);
            Controls.Add(panel1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox6);
            Controls.Add(pictureBox7);
            Controls.Add(pictureBox8);
            Controls.Add(pictureBox9);
            Controls.Add(pictureBox4);
            DoubleBuffered = true;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            lblClear.ResumeLayout(false);
            lblClear.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel lblClear;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btnSum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox checkBoxPass;
        private System.Windows.Forms.Label label5;
    }
}

