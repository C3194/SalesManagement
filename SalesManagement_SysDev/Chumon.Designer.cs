namespace 注文管理画面
{
    partial class Chumon
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
            btnConfirm = new Button();
            btnList = new Button();
            btnHidden = new Button();
            btnSearch = new Button();
            btnExit = new Button();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            btnChClearInput = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            tbxChDetailID = new TextBox();
            label17 = new Label();
            tbxPrID = new TextBox();
            label11 = new Label();
            label15 = new Label();
            label13 = new Label();
            tbxQuantity = new TextBox();
            cbxPrName = new ComboBox();
            tbxEmID = new TextBox();
            label9 = new Label();
            label8 = new Label();
            label10 = new Label();
            label7 = new Label();
            label6 = new Label();
            label3 = new Label();
            label4 = new Label();
            tbxOrID = new TextBox();
            tbxSoID = new TextBox();
            tbxChID = new TextBox();
            label5 = new Label();
            label2 = new Label();
            dtpChDate = new DateTimePicker();
            tbxClID = new TextBox();
            label16 = new Label();
            label12 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label19 = new Label();
            tbxPrHidden = new TextBox();
            cbxChFlag = new ComboBox();
            cbxChstateFlag = new ComboBox();
            cbxEmName = new ComboBox();
            cbxSoName = new ComboBox();
            cbxClName = new ComboBox();
            btnChDetailClearINput = new Button();
            label21 = new Label();
            label20 = new Label();
            dgvChDetail = new DataGridView();
            dgvCh = new DataGridView();
            panel1 = new Panel();
            label14 = new Label();
            label18 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChDetail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCh).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.FromArgb(39, 203, 190);
            btnConfirm.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnConfirm.ForeColor = SystemColors.Window;
            btnConfirm.ImeMode = ImeMode.NoControl;
            btnConfirm.Location = new Point(1222, 75);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(140, 57);
            btnConfirm.TabIndex = 88;
            btnConfirm.Text = "確定";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnList
            // 
            btnList.BackColor = Color.DarkSlateBlue;
            btnList.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnList.ForeColor = SystemColors.Window;
            btnList.Location = new Point(1368, 75);
            btnList.Name = "btnList";
            btnList.Size = new Size(140, 57);
            btnList.TabIndex = 86;
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
            btnHidden.TabIndex = 84;
            btnHidden.Text = "非表示リスト";
            btnHidden.UseVisualStyleBackColor = false;
            btnHidden.Click += btnHidden_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(39, 203, 190);
            btnSearch.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnSearch.ForeColor = SystemColors.Window;
            btnSearch.Location = new Point(1514, 75);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(140, 57);
            btnSearch.TabIndex = 83;
            btnSearch.Text = "検索";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.DeepPink;
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(1727, 6);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(120, 57);
            btnExit.TabIndex = 80;
            btnExit.Text = "戻る";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(39, 203, 190);
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1920, 69);
            pictureBox1.TabIndex = 81;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(9, 5, 126);
            pictureBox3.Location = new Point(0, 69);
            pictureBox3.Margin = new Padding(4);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(1920, 69);
            pictureBox3.TabIndex = 82;
            pictureBox3.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(39, 203, 190);
            label1.Font = new Font("Yu Gothic UI", 25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(50, 0);
            label1.Name = "label1";
            label1.Size = new Size(229, 67);
            label1.TabIndex = 89;
            label1.Text = "注文管理";
            // 
            // btnChClearInput
            // 
            btnChClearInput.Location = new Point(1594, 479);
            btnChClearInput.Margin = new Padding(4);
            btnChClearInput.Name = "btnChClearInput";
            btnChClearInput.Size = new Size(106, 70);
            btnChClearInput.TabIndex = 91;
            btnChClearInput.Text = "入力クリア";
            btnChClearInput.UseVisualStyleBackColor = true;
            btnChClearInput.Click += btnChClearInput_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.FromArgb(238, 253, 255);
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.92562F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 71.07438F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 171F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 346F));
            tableLayoutPanel2.Controls.Add(tbxChDetailID, 1, 0);
            tableLayoutPanel2.Controls.Add(label17, 0, 0);
            tableLayoutPanel2.Controls.Add(tbxPrID, 3, 0);
            tableLayoutPanel2.Controls.Add(label11, 2, 0);
            tableLayoutPanel2.Controls.Add(label15, 2, 1);
            tableLayoutPanel2.Controls.Add(label13, 0, 1);
            tableLayoutPanel2.Controls.Add(tbxQuantity, 3, 1);
            tableLayoutPanel2.Controls.Add(cbxPrName, 1, 1);
            tableLayoutPanel2.Location = new Point(47, 594);
            tableLayoutPanel2.Margin = new Padding(4);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 19F));
            tableLayoutPanel2.Size = new Size(1171, 91);
            tableLayoutPanel2.TabIndex = 92;
            // 
            // tbxChDetailID
            // 
            tbxChDetailID.Location = new Point(194, 7);
            tbxChDetailID.Margin = new Padding(4);
            tbxChDetailID.Name = "tbxChDetailID";
            tbxChDetailID.Size = new Size(190, 31);
            tbxChDetailID.TabIndex = 13;
            tbxChDetailID.KeyPress += tbxChDetailID_KeyPress;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label17.Location = new Point(6, 3);
            label17.Name = "label17";
            label17.Size = new Size(111, 28);
            label17.TabIndex = 87;
            label17.Text = "注文詳細ID";
            // 
            // tbxPrID
            // 
            tbxPrID.Location = new Point(825, 7);
            tbxPrID.Margin = new Padding(4);
            tbxPrID.Name = "tbxPrID";
            tbxPrID.Size = new Size(190, 31);
            tbxPrID.TabIndex = 83;
            tbxPrID.TextChanged += tbxPrID_TextChanged;
            tbxPrID.KeyPress += tbxPrID_KeyPress;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(650, 3);
            label11.Name = "label11";
            label11.Size = new Size(71, 28);
            label11.TabIndex = 28;
            label11.Text = "商品ID";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(650, 44);
            label15.Name = "label15";
            label15.Size = new Size(52, 28);
            label15.TabIndex = 32;
            label15.Text = "数量";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(6, 44);
            label13.Name = "label13";
            label13.Size = new Size(72, 28);
            label13.TabIndex = 26;
            label13.Text = "商品名";
            // 
            // tbxQuantity
            // 
            tbxQuantity.Location = new Point(825, 48);
            tbxQuantity.Margin = new Padding(4);
            tbxQuantity.Name = "tbxQuantity";
            tbxQuantity.Size = new Size(190, 31);
            tbxQuantity.TabIndex = 16;
            // 
            // cbxPrName
            // 
            cbxPrName.FormattingEnabled = true;
            cbxPrName.Location = new Point(194, 48);
            cbxPrName.Margin = new Padding(4);
            cbxPrName.Name = "cbxPrName";
            cbxPrName.Size = new Size(190, 33);
            cbxPrName.TabIndex = 15;
            cbxPrName.SelectedIndexChanged += cbxPrName_SelectedIndexChanged;
            // 
            // tbxEmID
            // 
            tbxEmID.Location = new Point(190, 130);
            tbxEmID.Margin = new Padding(4);
            tbxEmID.Name = "tbxEmID";
            tbxEmID.Size = new Size(190, 31);
            tbxEmID.TabIndex = 7;
            tbxEmID.TextChanged += tbxEmID_TextChanged;
            tbxEmID.KeyPress += tbxEmID_KeyPress;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(5, 126);
            label9.Margin = new Padding(2, 0, 2, 0);
            label9.Name = "label9";
            label9.Size = new Size(71, 28);
            label9.TabIndex = 22;
            label9.Text = "社員ID";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(703, 85);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(72, 28);
            label8.TabIndex = 20;
            label8.Text = "顧客名";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(703, 126);
            label10.Margin = new Padding(2, 0, 2, 0);
            label10.Name = "label10";
            label10.Size = new Size(72, 28);
            label10.TabIndex = 24;
            label10.Text = "社員名";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(5, 85);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(71, 28);
            label7.TabIndex = 18;
            label7.Text = "顧客ID";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(5, 246);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(112, 28);
            label6.TabIndex = 16;
            label6.Text = "注文年月日";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(5, 44);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(91, 28);
            label3.TabIndex = 10;
            label3.Text = "営業所ID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(703, 44);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(92, 28);
            label4.TabIndex = 12;
            label4.Text = "営業所名";
            // 
            // tbxOrID
            // 
            tbxOrID.Location = new Point(856, 7);
            tbxOrID.Margin = new Padding(4);
            tbxOrID.Name = "tbxOrID";
            tbxOrID.Size = new Size(190, 31);
            tbxOrID.TabIndex = 2;
            tbxOrID.KeyPress += tbxOrID_KeyPress;
            // 
            // tbxSoID
            // 
            tbxSoID.Location = new Point(190, 48);
            tbxSoID.Margin = new Padding(4);
            tbxSoID.Name = "tbxSoID";
            tbxSoID.Size = new Size(190, 31);
            tbxSoID.TabIndex = 3;
            tbxSoID.TextChanged += tbxSoID_TextChanged;
            tbxSoID.KeyPress += tbxSoID_KeyPress;
            // 
            // tbxChID
            // 
            tbxChID.Location = new Point(190, 7);
            tbxChID.Margin = new Padding(4);
            tbxChID.Name = "tbxChID";
            tbxChID.Size = new Size(190, 31);
            tbxChID.TabIndex = 1;
            tbxChID.KeyPress += tbxChID_KeyPress;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(703, 3);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(71, 28);
            label5.TabIndex = 15;
            label5.Text = "受注ID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(5, 3);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(71, 28);
            label2.TabIndex = 8;
            label2.Text = "注文ID";
            // 
            // dtpChDate
            // 
            dtpChDate.Checked = false;
            dtpChDate.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dtpChDate.Location = new Point(190, 250);
            dtpChDate.Margin = new Padding(4);
            dtpChDate.Name = "dtpChDate";
            dtpChDate.ShowCheckBox = true;
            dtpChDate.Size = new Size(250, 34);
            dtpChDate.TabIndex = 11;
            // 
            // tbxClID
            // 
            tbxClID.Location = new Point(190, 89);
            tbxClID.Margin = new Padding(4);
            tbxClID.Name = "tbxClID";
            tbxClID.Size = new Size(190, 31);
            tbxClID.TabIndex = 5;
            tbxClID.TextChanged += tbxClID_TextChanged;
            tbxClID.KeyPress += tbxClID_KeyPress;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label16.ImeMode = ImeMode.NoControl;
            label16.Location = new Point(6, 167);
            label16.Name = "label16";
            label16.Size = new Size(119, 50);
            label16.TabIndex = 99;
            label16.Text = "注文状態\r\n(確定/未確定)";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label12.ImeMode = ImeMode.NoControl;
            label12.Location = new Point(704, 167);
            label12.Name = "label12";
            label12.Size = new Size(119, 50);
            label12.TabIndex = 99;
            label12.Text = "注文管理\r\n(表示/非表示)";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(238, 253, 255);
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.03687F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.9631348F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 148F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 574F));
            tableLayoutPanel1.Controls.Add(label19, 2, 5);
            tableLayoutPanel1.Controls.Add(tbxPrHidden, 3, 5);
            tableLayoutPanel1.Controls.Add(cbxChFlag, 3, 4);
            tableLayoutPanel1.Controls.Add(cbxChstateFlag, 1, 4);
            tableLayoutPanel1.Controls.Add(cbxEmName, 3, 3);
            tableLayoutPanel1.Controls.Add(cbxSoName, 3, 1);
            tableLayoutPanel1.Controls.Add(cbxClName, 3, 2);
            tableLayoutPanel1.Controls.Add(label12, 2, 4);
            tableLayoutPanel1.Controls.Add(label16, 0, 4);
            tableLayoutPanel1.Controls.Add(tbxClID, 1, 2);
            tableLayoutPanel1.Controls.Add(dtpChDate, 1, 5);
            tableLayoutPanel1.Controls.Add(label2, 0, 0);
            tableLayoutPanel1.Controls.Add(label5, 2, 0);
            tableLayoutPanel1.Controls.Add(tbxChID, 1, 0);
            tableLayoutPanel1.Controls.Add(tbxSoID, 1, 1);
            tableLayoutPanel1.Controls.Add(tbxOrID, 3, 0);
            tableLayoutPanel1.Controls.Add(label4, 2, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(label6, 0, 5);
            tableLayoutPanel1.Controls.Add(label7, 0, 2);
            tableLayoutPanel1.Controls.Add(label10, 2, 3);
            tableLayoutPanel1.Controls.Add(label8, 2, 2);
            tableLayoutPanel1.Controls.Add(label9, 0, 3);
            tableLayoutPanel1.Controls.Add(tbxEmID, 1, 3);
            tableLayoutPanel1.Location = new Point(47, 177);
            tableLayoutPanel1.Margin = new Padding(4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 103F));
            tableLayoutPanel1.Size = new Size(1430, 372);
            tableLayoutPanel1.TabIndex = 90;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label19.ImeMode = ImeMode.NoControl;
            label19.Location = new Point(703, 246);
            label19.Margin = new Padding(2, 0, 2, 0);
            label19.Name = "label19";
            label19.Size = new Size(112, 28);
            label19.TabIndex = 101;
            label19.Text = "非表示理由";
            // 
            // tbxPrHidden
            // 
            tbxPrHidden.Location = new Point(856, 250);
            tbxPrHidden.Margin = new Padding(4);
            tbxPrHidden.Multiline = true;
            tbxPrHidden.Name = "tbxPrHidden";
            tbxPrHidden.Size = new Size(567, 115);
            tbxPrHidden.TabIndex = 12;
            // 
            // cbxChFlag
            // 
            cbxChFlag.FormattingEnabled = true;
            cbxChFlag.Items.AddRange(new object[] { "表示", "非表示" });
            cbxChFlag.Location = new Point(856, 171);
            cbxChFlag.Margin = new Padding(4);
            cbxChFlag.Name = "cbxChFlag";
            cbxChFlag.Size = new Size(190, 33);
            cbxChFlag.TabIndex = 10;
            cbxChFlag.SelectedIndexChanged += cbxChFlag_SelectedIndexChanged;
            // 
            // cbxChstateFlag
            // 
            cbxChstateFlag.FormattingEnabled = true;
            cbxChstateFlag.Items.AddRange(new object[] { "確定", "未確定" });
            cbxChstateFlag.Location = new Point(190, 171);
            cbxChstateFlag.Margin = new Padding(4);
            cbxChstateFlag.Name = "cbxChstateFlag";
            cbxChstateFlag.Size = new Size(190, 33);
            cbxChstateFlag.TabIndex = 9;
            // 
            // cbxEmName
            // 
            cbxEmName.FormattingEnabled = true;
            cbxEmName.Location = new Point(856, 130);
            cbxEmName.Margin = new Padding(4);
            cbxEmName.Name = "cbxEmName";
            cbxEmName.Size = new Size(190, 33);
            cbxEmName.TabIndex = 8;
            cbxEmName.SelectedIndexChanged += cbxEmName_SelectedIndexChanged;
            // 
            // cbxSoName
            // 
            cbxSoName.FormattingEnabled = true;
            cbxSoName.Location = new Point(856, 48);
            cbxSoName.Margin = new Padding(4);
            cbxSoName.Name = "cbxSoName";
            cbxSoName.Size = new Size(190, 33);
            cbxSoName.TabIndex = 4;
            cbxSoName.SelectedIndexChanged += cbxSoName_SelectedIndexChanged;
            // 
            // cbxClName
            // 
            cbxClName.FormattingEnabled = true;
            cbxClName.Location = new Point(856, 89);
            cbxClName.Margin = new Padding(4);
            cbxClName.Name = "cbxClName";
            cbxClName.Size = new Size(190, 33);
            cbxClName.TabIndex = 6;
            cbxClName.SelectedIndexChanged += cbxClName_SelectedIndexChanged;
            // 
            // btnChDetailClearINput
            // 
            btnChDetailClearINput.Location = new Point(1328, 618);
            btnChDetailClearINput.Margin = new Padding(4);
            btnChDetailClearINput.Name = "btnChDetailClearINput";
            btnChDetailClearINput.Size = new Size(106, 70);
            btnChDetailClearINput.TabIndex = 93;
            btnChDetailClearINput.Text = "入力クリア";
            btnChDetailClearINput.UseVisualStyleBackColor = true;
            btnChDetailClearINput.Click += btnChDetailClearINput_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.BackColor = Color.FromArgb(39, 203, 190);
            label21.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label21.ForeColor = Color.White;
            label21.Location = new Point(28, 0);
            label21.Name = "label21";
            label21.Size = new Size(84, 25);
            label21.TabIndex = 97;
            label21.Text = "注文情報";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.BackColor = Color.FromArgb(39, 203, 190);
            label20.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label20.ForeColor = Color.White;
            label20.Location = new Point(1350, 0);
            label20.Name = "label20";
            label20.Size = new Size(84, 25);
            label20.TabIndex = 96;
            label20.Text = "注文詳細";
            // 
            // dgvChDetail
            // 
            dgvChDetail.BackgroundColor = SystemColors.ButtonHighlight;
            dgvChDetail.BorderStyle = BorderStyle.Fixed3D;
            dgvChDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChDetail.Location = new Point(1350, 25);
            dgvChDetail.Margin = new Padding(4);
            dgvChDetail.Name = "dgvChDetail";
            dgvChDetail.RowHeadersWidth = 51;
            dgvChDetail.Size = new Size(535, 251);
            dgvChDetail.TabIndex = 95;
            dgvChDetail.SelectionChanged += dgvChDetail_SelectionChanged;
            // 
            // dgvCh
            // 
            dgvCh.BackgroundColor = SystemColors.ButtonHighlight;
            dgvCh.BorderStyle = BorderStyle.Fixed3D;
            dgvCh.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCh.Location = new Point(28, 25);
            dgvCh.Margin = new Padding(4);
            dgvCh.Name = "dgvCh";
            dgvCh.RowHeadersWidth = 51;
            dgvCh.Size = new Size(1307, 251);
            dgvCh.TabIndex = 94;
            dgvCh.CellFormatting += dgvCh_CellFormatting;
            dgvCh.SelectionChanged += dgvCh_SelectionChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(238, 253, 255);
            panel1.Controls.Add(label21);
            panel1.Controls.Add(dgvCh);
            panel1.Controls.Add(label20);
            panel1.Controls.Add(dgvChDetail);
            panel1.Location = new Point(0, 735);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1920, 368);
            panel1.TabIndex = 98;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = Color.FromArgb(39, 203, 190);
            label14.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label14.ForeColor = Color.White;
            label14.Location = new Point(47, 152);
            label14.Name = "label14";
            label14.Size = new Size(84, 25);
            label14.TabIndex = 99;
            label14.Text = "注文情報";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.FromArgb(39, 203, 190);
            label18.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label18.ForeColor = Color.White;
            label18.Location = new Point(47, 569);
            label18.Name = "label18";
            label18.Size = new Size(84, 25);
            label18.TabIndex = 100;
            label18.Text = "注文詳細";
            // 
            // Chumon
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            ClientSize = new Size(1898, 1024);
            Controls.Add(label18);
            Controls.Add(label14);
            Controls.Add(panel1);
            Controls.Add(btnChDetailClearINput);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(btnChClearInput);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btnExit);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(btnConfirm);
            Controls.Add(btnList);
            Controls.Add(btnHidden);
            Controls.Add(btnSearch);
            Controls.Add(pictureBox3);
            Margin = new Padding(2);
            Name = "Chumon";
            Text = "販売管理システム・注文管理";
            WindowState = FormWindowState.Maximized;
            Load += Chumon_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChDetail).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCh).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnConfirm;
        private Button btnList;
        private Button btnHidden;
        private Button btnSearch;
        private Button btnExit;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private Label label1;
        private Button btnChClearInput;
        private TableLayoutPanel tableLayoutPanel2;
        private ComboBox cbxPrName;
        private TextBox tbxChDetailID;
        private TextBox tbxQuantity;
        private Label label17;
        private Label label13;
        private TextBox tbxPrID;
        private Label label15;
        private Label label11;
        private TextBox tbxEmID;
        private Label label9;
        private Label label8;
        private Label label10;
        private Label label7;
        private Label label6;
        private Label label3;
        private Label label4;
        private TextBox tbxOrID;
        private TextBox tbxSoID;
        private TextBox tbxChID;
        private Label label5;
        private Label label2;
        private DateTimePicker dtpChDate;
        private TextBox tbxClID;
        private Label label16;
        private Label label12;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox cbxChFlag;
        private ComboBox cbxChstateFlag;
        private ComboBox cbxEmName;
        private ComboBox cbxSoName;
        private ComboBox cbxClName;
        private TextBox tbxPrHidden;
        private Button btnChDetailClearINput;
        private Label label21;
        private Label label20;
        private DataGridView dgvChDetail;
        private DataGridView dgvCh;
        private Panel panel1;
        private Label label14;
        private Label label18;
        private Label label19;
    }
}