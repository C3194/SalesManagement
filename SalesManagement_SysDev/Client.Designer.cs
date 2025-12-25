namespace a
{
    partial class Client
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
            lblClID = new Label();
            lblName = new Label();
            label = new Label();
            lblClAddress = new Label();
            lblClPhone = new Label();
            lblClPostal = new Label();
            lblClFAX = new Label();
            btnUpdate = new Button();
            btnList = new Button();
            btnHidden = new Button();
            btnSearch = new Button();
            btnRegist = new Button();
            lblClName = new Label();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            btnExit = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblClHidden = new Label();
            lblClFlag = new Label();
            tbxClHidden = new TextBox();
            cbxClFlag = new ComboBox();
            cbxSoName = new ComboBox();
            lblSoID = new Label();
            cbxClName = new ComboBox();
            tbxClFAX = new TextBox();
            tbxClPhone = new TextBox();
            tbxClPostal = new TextBox();
            tbxClAddress = new TextBox();
            tbxClID = new TextBox();
            tbxSoID = new TextBox();
            btnClearlnput = new Button();
            panel1 = new Panel();
            label1 = new Label();
            dgvCl = new DataGridView();
            label11 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCl).BeginInit();
            SuspendLayout();
            // 
            // lblClID
            // 
            lblClID.AutoSize = true;
            lblClID.BackColor = Color.FromArgb(238, 253, 255);
            lblClID.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblClID.ForeColor = Color.Black;
            lblClID.Location = new Point(6, 3);
            lblClID.Name = "lblClID";
            lblClID.Size = new Size(61, 23);
            lblClID.TabIndex = 11;
            lblClID.Text = "顧客ID";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Yu Gothic UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            lblName.Location = new Point(401, 3);
            lblName.Name = "lblName";
            lblName.Size = new Size(61, 23);
            lblName.TabIndex = 13;
            lblName.Text = "顧客名";
            // 
            // label
            // 
            label.AutoSize = true;
            label.Font = new Font("Yu Gothic UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            label.Location = new Point(401, 132);
            label.Name = "label";
            label.Size = new Size(78, 23);
            label.TabIndex = 15;
            label.Text = "営業所名";
            // 
            // lblClAddress
            // 
            lblClAddress.AutoSize = true;
            lblClAddress.Font = new Font("Yu Gothic UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            lblClAddress.Location = new Point(401, 46);
            lblClAddress.Name = "lblClAddress";
            lblClAddress.Size = new Size(44, 23);
            lblClAddress.TabIndex = 17;
            lblClAddress.Text = "住所";
            // 
            // lblClPhone
            // 
            lblClPhone.AutoSize = true;
            lblClPhone.Font = new Font("Yu Gothic UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            lblClPhone.Location = new Point(6, 89);
            lblClPhone.Name = "lblClPhone";
            lblClPhone.Size = new Size(78, 23);
            lblClPhone.TabIndex = 19;
            lblClPhone.Text = "電話番号";
            // 
            // lblClPostal
            // 
            lblClPostal.AutoSize = true;
            lblClPostal.Font = new Font("Yu Gothic UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            lblClPostal.Location = new Point(6, 46);
            lblClPostal.Name = "lblClPostal";
            lblClPostal.Size = new Size(78, 23);
            lblClPostal.TabIndex = 21;
            lblClPostal.Text = "郵便番号";
            // 
            // lblClFAX
            // 
            lblClFAX.AutoSize = true;
            lblClFAX.Font = new Font("Yu Gothic UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            lblClFAX.Location = new Point(401, 89);
            lblClFAX.Name = "lblClFAX";
            lblClFAX.Size = new Size(38, 23);
            lblClFAX.TabIndex = 23;
            lblClFAX.Text = "FAX";
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(39, 203, 190);
            btnUpdate.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnUpdate.ForeColor = SystemColors.Window;
            btnUpdate.ImeMode = ImeMode.NoControl;
            btnUpdate.Location = new Point(1225, 75);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(140, 57);
            btnUpdate.TabIndex = 70;
            btnUpdate.Text = "更新";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnList
            // 
            btnList.BackColor = Color.DarkSlateBlue;
            btnList.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnList.ForeColor = SystemColors.Window;
            btnList.Location = new Point(1370, 75);
            btnList.Name = "btnList";
            btnList.Size = new Size(140, 57);
            btnList.TabIndex = 68;
            btnList.Text = "一覧表示";
            btnList.UseVisualStyleBackColor = false;
            btnList.Click += btnList_Click;
            // 
            // btnHidden
            // 
            btnHidden.BackColor = Color.DarkSlateBlue;
            btnHidden.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnHidden.ForeColor = SystemColors.Window;
            btnHidden.Location = new Point(1660, 75);
            btnHidden.Name = "btnHidden";
            btnHidden.Size = new Size(140, 57);
            btnHidden.TabIndex = 66;
            btnHidden.Text = "非表示リスト";
            btnHidden.UseVisualStyleBackColor = false;
            btnHidden.Click += btnHidden_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(39, 203, 190);
            btnSearch.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnSearch.ForeColor = SystemColors.Window;
            btnSearch.Location = new Point(1515, 75);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(140, 57);
            btnSearch.TabIndex = 65;
            btnSearch.Text = "検索";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnRegist
            // 
            btnRegist.BackColor = Color.DarkSlateBlue;
            btnRegist.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnRegist.ForeColor = SystemColors.Window;
            btnRegist.Location = new Point(1080, 75);
            btnRegist.Name = "btnRegist";
            btnRegist.Size = new Size(140, 57);
            btnRegist.TabIndex = 61;
            btnRegist.Text = "登録";
            btnRegist.UseVisualStyleBackColor = false;
            btnRegist.Click += btnRegist_Click;
            // 
            // lblClName
            // 
            lblClName.AutoSize = true;
            lblClName.BackColor = Color.FromArgb(39, 203, 190);
            lblClName.Font = new Font("Yu Gothic UI", 25F, FontStyle.Bold, GraphicsUnit.Point);
            lblClName.ForeColor = Color.White;
            lblClName.Location = new Point(50, 0);
            lblClName.Name = "lblClName";
            lblClName.Size = new Size(193, 57);
            lblClName.TabIndex = 60;
            lblClName.Text = "顧客管理";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(39, 203, 190);
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1920, 69);
            pictureBox1.TabIndex = 63;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(9, 5, 126);
            pictureBox3.Location = new Point(0, 69);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(1920, 69);
            pictureBox3.TabIndex = 64;
            pictureBox3.TabStop = false;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.DeepPink;
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(1727, 6);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(120, 57);
            btnExit.TabIndex = 71;
            btnExit.Text = "戻る";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.FromArgb(238, 253, 255);
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.98165F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72.01835F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 166F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 550F));
            tableLayoutPanel2.Controls.Add(lblClHidden, 2, 4);
            tableLayoutPanel2.Controls.Add(lblClFlag, 0, 4);
            tableLayoutPanel2.Controls.Add(tbxClHidden, 3, 4);
            tableLayoutPanel2.Controls.Add(cbxClFlag, 1, 4);
            tableLayoutPanel2.Controls.Add(cbxSoName, 3, 3);
            tableLayoutPanel2.Controls.Add(lblSoID, 0, 3);
            tableLayoutPanel2.Controls.Add(cbxClName, 3, 0);
            tableLayoutPanel2.Controls.Add(tbxClFAX, 3, 2);
            tableLayoutPanel2.Controls.Add(tbxClPhone, 1, 2);
            tableLayoutPanel2.Controls.Add(tbxClPostal, 1, 1);
            tableLayoutPanel2.Controls.Add(tbxClAddress, 3, 1);
            tableLayoutPanel2.Controls.Add(lblName, 2, 0);
            tableLayoutPanel2.Controls.Add(lblClID, 0, 0);
            tableLayoutPanel2.Controls.Add(lblClAddress, 2, 1);
            tableLayoutPanel2.Controls.Add(lblClPostal, 0, 1);
            tableLayoutPanel2.Controls.Add(tbxClID, 1, 0);
            tableLayoutPanel2.Controls.Add(lblClPhone, 0, 2);
            tableLayoutPanel2.Controls.Add(label, 2, 3);
            tableLayoutPanel2.Controls.Add(lblClFAX, 2, 2);
            tableLayoutPanel2.Controls.Add(tbxSoID, 1, 3);
            tableLayoutPanel2.Location = new Point(58, 170);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 135F));
            tableLayoutPanel2.Size = new Size(1121, 282);
            tableLayoutPanel2.TabIndex = 73;
            // 
            // lblClHidden
            // 
            lblClHidden.AutoSize = true;
            lblClHidden.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            lblClHidden.ImeMode = ImeMode.NoControl;
            lblClHidden.Location = new Point(400, 175);
            lblClHidden.Margin = new Padding(2, 0, 2, 0);
            lblClHidden.Name = "lblClHidden";
            lblClHidden.Size = new Size(95, 23);
            lblClHidden.TabIndex = 97;
            lblClHidden.Text = "非表示理由";
            // 
            // lblClFlag
            // 
            lblClFlag.AutoSize = true;
            lblClFlag.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblClFlag.Location = new Point(6, 175);
            lblClFlag.Name = "lblClFlag";
            lblClFlag.Size = new Size(100, 40);
            lblClFlag.TabIndex = 96;
            lblClFlag.Text = "顧客管理\r\n(表示/非表示)";
            // 
            // tbxClHidden
            // 
            tbxClHidden.Enabled = false;
            tbxClHidden.Location = new Point(570, 178);
            tbxClHidden.Multiline = true;
            tbxClHidden.Name = "tbxClHidden";
            tbxClHidden.Size = new Size(537, 96);
            tbxClHidden.TabIndex = 10;
            // 
            // cbxClFlag
            // 
            cbxClFlag.DisplayMember = "0";
            cbxClFlag.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxClFlag.FormattingEnabled = true;
            cbxClFlag.Items.AddRange(new object[] { "表示", "非表示" });
            cbxClFlag.Location = new Point(118, 178);
            cbxClFlag.Name = "cbxClFlag";
            cbxClFlag.Size = new Size(164, 28);
            cbxClFlag.TabIndex = 9;
            cbxClFlag.SelectedIndexChanged += cbxClFlag_SelectedIndexChanged;
            // 
            // cbxSoName
            // 
            cbxSoName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxSoName.FormattingEnabled = true;
            cbxSoName.ItemHeight = 20;
            cbxSoName.Location = new Point(570, 135);
            cbxSoName.Name = "cbxSoName";
            cbxSoName.Size = new Size(164, 28);
            cbxSoName.TabIndex = 8;
            cbxSoName.SelectedIndexChanged += cbxSoName_SelectedIndexChanged;
            // 
            // lblSoID
            // 
            lblSoID.AutoSize = true;
            lblSoID.Font = new Font("Yu Gothic UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            lblSoID.Location = new Point(6, 132);
            lblSoID.Name = "lblSoID";
            lblSoID.Size = new Size(78, 23);
            lblSoID.TabIndex = 91;
            lblSoID.Text = "営業所ID";
            // 
            // cbxClName
            // 
            cbxClName.FormattingEnabled = true;
            cbxClName.Location = new Point(570, 6);
            cbxClName.Name = "cbxClName";
            cbxClName.Size = new Size(164, 28);
            cbxClName.TabIndex = 2;
            cbxClName.SelectedIndexChanged += cbxClName_SelectedIndexChanged;
            // 
            // tbxClFAX
            // 
            tbxClFAX.Location = new Point(570, 92);
            tbxClFAX.Name = "tbxClFAX";
            tbxClFAX.Size = new Size(190, 27);
            tbxClFAX.TabIndex = 6;
            // 
            // tbxClPhone
            // 
            tbxClPhone.Location = new Point(118, 92);
            tbxClPhone.Name = "tbxClPhone";
            tbxClPhone.Size = new Size(190, 27);
            tbxClPhone.TabIndex = 5;
            // 
            // tbxClPostal
            // 
            tbxClPostal.Location = new Point(118, 49);
            tbxClPostal.Name = "tbxClPostal";
            tbxClPostal.Size = new Size(190, 27);
            tbxClPostal.TabIndex = 3;
            // 
            // tbxClAddress
            // 
            tbxClAddress.Location = new Point(570, 49);
            tbxClAddress.Name = "tbxClAddress";
            tbxClAddress.Size = new Size(190, 27);
            tbxClAddress.TabIndex = 4;
            // 
            // tbxClID
            // 
            tbxClID.Location = new Point(118, 6);
            tbxClID.Name = "tbxClID";
            tbxClID.Size = new Size(190, 27);
            tbxClID.TabIndex = 1;
            tbxClID.TextChanged += tbxClID_TextChanged;
            // 
            // tbxSoID
            // 
            tbxSoID.Location = new Point(118, 135);
            tbxSoID.Name = "tbxSoID";
            tbxSoID.Size = new Size(190, 27);
            tbxSoID.TabIndex = 7;
            tbxSoID.TextChanged += tbxSoID_TextChanged;
            // 
            // btnClearlnput
            // 
            btnClearlnput.Location = new Point(1225, 382);
            btnClearlnput.Margin = new Padding(4);
            btnClearlnput.Name = "btnClearlnput";
            btnClearlnput.Size = new Size(106, 70);
            btnClearlnput.TabIndex = 76;
            btnClearlnput.Text = "入力クリア";
            btnClearlnput.UseVisualStyleBackColor = true;
            btnClearlnput.Click += btnClearlnput_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(238, 253, 255);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(dgvCl);
            panel1.Location = new Point(0, 500);
            panel1.Name = "panel1";
            panel1.Size = new Size(1550, 540);
            panel1.TabIndex = 88;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(39, 203, 190);
            label1.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(58, 2);
            label1.Name = "label1";
            label1.Size = new Size(69, 20);
            label1.TabIndex = 88;
            label1.Text = "顧客情報";
            // 
            // dgvCl
            // 
            dgvCl.BackgroundColor = SystemColors.ButtonHighlight;
            dgvCl.BorderStyle = BorderStyle.Fixed3D;
            dgvCl.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCl.Location = new Point(58, 22);
            dgvCl.Name = "dgvCl";
            dgvCl.RowHeadersWidth = 51;
            dgvCl.Size = new Size(1408, 450);
            dgvCl.TabIndex = 62;
            dgvCl.CellClick += dgvCl_CellClick;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.FromArgb(39, 203, 190);
            label11.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label11.ForeColor = Color.White;
            label11.ImeMode = ImeMode.NoControl;
            label11.Location = new Point(58, 150);
            label11.Name = "label11";
            label11.Size = new Size(69, 20);
            label11.TabIndex = 89;
            label11.Text = "顧客情報";
            // 
            // Client
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Snow;
            ClientSize = new Size(1902, 1033);
            Controls.Add(label11);
            Controls.Add(panel1);
            Controls.Add(btnClearlnput);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(btnExit);
            Controls.Add(lblClName);
            Controls.Add(pictureBox1);
            Controls.Add(btnUpdate);
            Controls.Add(btnList);
            Controls.Add(btnHidden);
            Controls.Add(btnSearch);
            Controls.Add(btnRegist);
            Controls.Add(pictureBox3);
            Name = "Client";
            Text = "販売管理システム・顧客管理";
            Load += Client_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCl).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Button button9;
        private Button button13;
        private Button button12;
        private Button button10;
        private Button button14;
        private Button button16;
        private Label label10;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private Button button3;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox textBox8;
        private TextBox textBox12;
        private TextBox textBox13;
        private TextBox textBox11;

        private TextBox textBox5;
        private Panel panel1;
        private Label label1;
        private DataGridView dataGridView1;
        private TextBox textBox1;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label9;
        private ComboBox cbxPrName;
        private TextBox tbxClHidden;
        private Label label16;
        private Label label11;
        private Label label12;
        private DataGridView dgvOr;
        private Label lblClName;
        private Label lblClID;
        private Label lblName;
        private Label lblSoID;
        private ComboBox cbxClName;
        private TextBox tbxSoID;
        private TextBox tbxClID;
        private Label lblClAddress;
        private Label lblClPhone;
        private Label lblClPostal;
        private TextBox tbxClPhone;
        private TextBox tbxClPostal;
        private TextBox tbxClAddress;
        private Label lblClFAX;
        private Button Update;
        private Button List;
        private Button Search;
        private Button Regist;
        private Label lblClHidden;
        private TextBox tbxClFAX;
        private Button Delete;
        private Button Hidden;
        private Button Exit;
        private Button Clearlnput;
        private Label lblClFlag;
        private ComboBox cbxClFlag;
        private DataGridView dgvCl;
        private ComboBox cbx;
        private Label label;
        private ComboBox cbxSoName;
        private Button btnClearlnput;
        private Button btnUpdate;
        private Button btnList;
        private Button btnHidden;
        private Button btnSearch;
        private Button btnRegist;
        private Button btnExit;
    }
}