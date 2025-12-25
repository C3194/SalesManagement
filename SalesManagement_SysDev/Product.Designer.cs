namespace Product_Management
{
    partial class Product
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
            label1 = new Label();
            btnRegist = new Button();
            btnExit = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label10 = new Label();
            tbxPrSafetyStock = new TextBox();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            btnSearch = new Button();
            btnHidden = new Button();
            btnList = new Button();
            btnUpdate = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            textBox1 = new TextBox();
            tbxColor = new TextBox();
            tbxPrName = new TextBox();
            label17 = new Label();
            cbxMaName = new ComboBox();
            label14 = new Label();
            label9 = new Label();
            tbxPrHidden = new TextBox();
            dtpReleaseDate = new DateTimePicker();
            label13 = new Label();
            label12 = new Label();
            cbxPrFlag = new ComboBox();
            label16 = new Label();
            tbxPrModelNumber = new TextBox();
            tbxPrJCode = new TextBox();
            tbxPrID = new TextBox();
            tbxMaID = new TextBox();
            tbxPrice = new TextBox();
            btnClearInput = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            label2 = new Label();
            label8 = new Label();
            cbxMcName = new ComboBox();
            cbxScName = new ComboBox();
            label15 = new Label();
            label11 = new Label();
            panel1 = new Panel();
            label18 = new Label();
            dgvPr = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPr).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(39, 203, 190);
            label1.Font = new Font("Yu Gothic UI", 25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(50, 0);
            label1.Name = "label1";
            label1.Size = new Size(193, 57);
            label1.TabIndex = 0;
            label1.Text = "商品管理";
            // 
            // btnRegist
            // 
            btnRegist.BackColor = Color.DarkSlateBlue;
            btnRegist.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnRegist.ForeColor = SystemColors.Window;
            btnRegist.Location = new Point(1076, 75);
            btnRegist.Name = "btnRegist";
            btnRegist.Size = new Size(140, 57);
            btnRegist.TabIndex = 6;
            btnRegist.Text = "登録";
            btnRegist.UseVisualStyleBackColor = false;
            btnRegist.Click += btnRegist_Click;
            // 
            // btnExit
            // 
            btnExit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExit.BackColor = Color.DeepPink;
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(1727, 6);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(120, 57);
            btnExit.TabIndex = 9;
            btnExit.Text = "戻る";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(6, 46);
            label3.Name = "label3";
            label3.Size = new Size(63, 23);
            label3.TabIndex = 11;
            label3.Text = "メーカID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(568, 132);
            label4.Name = "label4";
            label4.Size = new Size(44, 23);
            label4.TabIndex = 12;
            label4.Text = "型番";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(6, 175);
            label5.Name = "label5";
            label5.Size = new Size(95, 23);
            label5.TabIndex = 13;
            label5.Text = "安全在庫数";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(568, 89);
            label6.Name = "label6";
            label6.Size = new Size(75, 23);
            label6.TabIndex = 14;
            label6.Text = "JANコード";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(6, 89);
            label7.Name = "label7";
            label7.Size = new Size(44, 23);
            label7.TabIndex = 15;
            label7.Text = "価格";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(6, 132);
            label10.Name = "label10";
            label10.Size = new Size(27, 23);
            label10.TabIndex = 18;
            label10.Text = "色";
            // 
            // tbxPrSafetyStock
            // 
            tbxPrSafetyStock.Font = new Font("MS UI Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbxPrSafetyStock.Location = new Point(180, 178);
            tbxPrSafetyStock.Multiline = true;
            tbxPrSafetyStock.Name = "tbxPrSafetyStock";
            tbxPrSafetyStock.Size = new Size(190, 27);
            tbxPrSafetyStock.TabIndex = 9;
            tbxPrSafetyStock.KeyPress += tbxPrSafetyStock_KeyPress;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackColor = Color.FromArgb(39, 203, 190);
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(2295, 69);
            pictureBox1.TabIndex = 30;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox3.BackColor = Color.FromArgb(9, 5, 126);
            pictureBox3.Location = new Point(0, 69);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(2295, 69);
            pictureBox3.TabIndex = 44;
            pictureBox3.TabStop = false;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(39, 203, 190);
            btnSearch.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnSearch.ForeColor = SystemColors.Window;
            btnSearch.Location = new Point(1514, 75);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(140, 57);
            btnSearch.TabIndex = 45;
            btnSearch.Text = "検索";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnHidden
            // 
            btnHidden.BackColor = Color.DarkSlateBlue;
            btnHidden.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnHidden.ForeColor = SystemColors.Window;
            btnHidden.Location = new Point(1660, 75);
            btnHidden.Name = "btnHidden";
            btnHidden.Size = new Size(140, 57);
            btnHidden.TabIndex = 46;
            btnHidden.Text = "非表示リスト";
            btnHidden.UseVisualStyleBackColor = false;
            btnHidden.Click += btnHidden_Click;
            // 
            // btnList
            // 
            btnList.BackColor = Color.DarkSlateBlue;
            btnList.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnList.ForeColor = SystemColors.Window;
            btnList.Location = new Point(1368, 75);
            btnList.Name = "btnList";
            btnList.Size = new Size(140, 57);
            btnList.TabIndex = 48;
            btnList.Text = "一覧表示";
            btnList.UseVisualStyleBackColor = false;
            btnList.Click += btnList_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(39, 203, 190);
            btnUpdate.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnUpdate.ForeColor = SystemColors.Window;
            btnUpdate.ImeMode = ImeMode.NoControl;
            btnUpdate.Location = new Point(1222, 75);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(140, 57);
            btnUpdate.TabIndex = 59;
            btnUpdate.Text = "更新";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(238, 253, 255);
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.74935F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 69.25065F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 234F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 502F));
            tableLayoutPanel1.Controls.Add(textBox1, 0, 6);
            tableLayoutPanel1.Controls.Add(tbxColor, 1, 3);
            tableLayoutPanel1.Controls.Add(tbxPrName, 3, 0);
            tableLayoutPanel1.Controls.Add(label17, 2, 5);
            tableLayoutPanel1.Controls.Add(cbxMaName, 3, 1);
            tableLayoutPanel1.Controls.Add(label14, 2, 1);
            tableLayoutPanel1.Controls.Add(label9, 0, 5);
            tableLayoutPanel1.Controls.Add(tbxPrHidden, 3, 5);
            tableLayoutPanel1.Controls.Add(dtpReleaseDate, 1, 5);
            tableLayoutPanel1.Controls.Add(label13, 0, 0);
            tableLayoutPanel1.Controls.Add(label12, 2, 0);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(label7, 0, 2);
            tableLayoutPanel1.Controls.Add(label10, 0, 3);
            tableLayoutPanel1.Controls.Add(label4, 2, 3);
            tableLayoutPanel1.Controls.Add(label6, 2, 2);
            tableLayoutPanel1.Controls.Add(cbxPrFlag, 3, 4);
            tableLayoutPanel1.Controls.Add(tbxPrSafetyStock, 1, 4);
            tableLayoutPanel1.Controls.Add(label16, 2, 4);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(tbxPrModelNumber, 3, 3);
            tableLayoutPanel1.Controls.Add(tbxPrJCode, 3, 2);
            tableLayoutPanel1.Controls.Add(tbxPrID, 1, 0);
            tableLayoutPanel1.Controls.Add(tbxMaID, 1, 1);
            tableLayoutPanel1.Controls.Add(tbxPrice, 1, 2);
            tableLayoutPanel1.Location = new Point(37, 193);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 135F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1308, 329);
            tableLayoutPanel1.TabIndex = 73;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Left;
            textBox1.Font = new Font("MS UI Gothic", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(6, 359);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(165, 25);
            textBox1.TabIndex = 90;
            // 
            // tbxColor
            // 
            tbxColor.Font = new Font("MS UI Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbxColor.Location = new Point(180, 135);
            tbxColor.Multiline = true;
            tbxColor.Name = "tbxColor";
            tbxColor.Size = new Size(190, 27);
            tbxColor.TabIndex = 7;
            tbxColor.KeyPress += tbxColor_KeyPress;
            // 
            // tbxPrName
            // 
            tbxPrName.Font = new Font("MS UI Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbxPrName.Location = new Point(805, 6);
            tbxPrName.Multiline = true;
            tbxPrName.Name = "tbxPrName";
            tbxPrName.Size = new Size(190, 27);
            tbxPrName.TabIndex = 2;
            tbxPrName.TextChanged += tbxPrName_TextChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label17.ImeMode = ImeMode.NoControl;
            label17.Location = new Point(567, 218);
            label17.Margin = new Padding(2, 0, 2, 0);
            label17.Name = "label17";
            label17.Size = new Size(95, 23);
            label17.TabIndex = 89;
            label17.Text = "非表示理由";
            // 
            // cbxMaName
            // 
            cbxMaName.FormattingEnabled = true;
            cbxMaName.Location = new Point(805, 49);
            cbxMaName.Name = "cbxMaName";
            cbxMaName.Size = new Size(164, 28);
            cbxMaName.TabIndex = 4;
            cbxMaName.SelectedIndexChanged += cbxMaName_SelectedIndexChanged;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(568, 46);
            label14.Name = "label14";
            label14.Size = new Size(63, 23);
            label14.TabIndex = 84;
            label14.Text = "メーカ名";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(6, 218);
            label9.Name = "label9";
            label9.Size = new Size(61, 23);
            label9.TabIndex = 17;
            label9.Text = "発売日";
            // 
            // tbxPrHidden
            // 
            tbxPrHidden.Enabled = false;
            tbxPrHidden.Location = new Point(805, 221);
            tbxPrHidden.Multiline = true;
            tbxPrHidden.Name = "tbxPrHidden";
            tbxPrHidden.Size = new Size(497, 96);
            tbxPrHidden.TabIndex = 12;
            // 
            // dtpReleaseDate
            // 
            dtpReleaseDate.Checked = false;
            dtpReleaseDate.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            dtpReleaseDate.Location = new Point(180, 221);
            dtpReleaseDate.Name = "dtpReleaseDate";
            dtpReleaseDate.ShowCheckBox = true;
            dtpReleaseDate.Size = new Size(235, 30);
            dtpReleaseDate.TabIndex = 11;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(6, 3);
            label13.Name = "label13";
            label13.Size = new Size(61, 23);
            label13.TabIndex = 28;
            label13.Text = "商品ID";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(568, 3);
            label12.Name = "label12";
            label12.Size = new Size(61, 23);
            label12.TabIndex = 26;
            label12.Text = "商品名";
            // 
            // cbxPrFlag
            // 
            cbxPrFlag.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxPrFlag.FormattingEnabled = true;
            cbxPrFlag.Items.AddRange(new object[] { "表示", "非表示" });
            cbxPrFlag.Location = new Point(805, 178);
            cbxPrFlag.Name = "cbxPrFlag";
            cbxPrFlag.Size = new Size(164, 28);
            cbxPrFlag.TabIndex = 10;
            cbxPrFlag.SelectedIndexChanged += cbxPrFlag_SelectedIndexChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(568, 175);
            label16.Name = "label16";
            label16.Size = new Size(100, 40);
            label16.TabIndex = 85;
            label16.Text = "商品管理\r\n(表示/非表示)";
            // 
            // tbxPrModelNumber
            // 
            tbxPrModelNumber.Font = new Font("MS UI Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbxPrModelNumber.Location = new Point(805, 135);
            tbxPrModelNumber.Multiline = true;
            tbxPrModelNumber.Name = "tbxPrModelNumber";
            tbxPrModelNumber.Size = new Size(190, 27);
            tbxPrModelNumber.TabIndex = 8;
            tbxPrModelNumber.KeyPress += tbxPrModelNumber_KeyPress;
            // 
            // tbxPrJCode
            // 
            tbxPrJCode.Font = new Font("MS UI Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbxPrJCode.Location = new Point(805, 92);
            tbxPrJCode.Multiline = true;
            tbxPrJCode.Name = "tbxPrJCode";
            tbxPrJCode.Size = new Size(190, 27);
            tbxPrJCode.TabIndex = 6;
            // 
            // tbxPrID
            // 
            tbxPrID.Font = new Font("MS UI Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbxPrID.Location = new Point(180, 6);
            tbxPrID.Multiline = true;
            tbxPrID.Name = "tbxPrID";
            tbxPrID.Size = new Size(190, 27);
            tbxPrID.TabIndex = 1;
            tbxPrID.TextChanged += tbxPrID_TextChanged;
            tbxPrID.KeyPress += tbxPrID_KeyPress;
            // 
            // tbxMaID
            // 
            tbxMaID.Font = new Font("MS UI Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbxMaID.Location = new Point(180, 49);
            tbxMaID.Multiline = true;
            tbxMaID.Name = "tbxMaID";
            tbxMaID.Size = new Size(190, 27);
            tbxMaID.TabIndex = 3;
            tbxMaID.TextChanged += tbxMaID_TextChanged;
            tbxMaID.KeyPress += tbxMaID_KeyPress;
            // 
            // tbxPrice
            // 
            tbxPrice.Font = new Font("MS UI Gothic", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbxPrice.Location = new Point(180, 92);
            tbxPrice.Multiline = true;
            tbxPrice.Name = "tbxPrice";
            tbxPrice.Size = new Size(190, 27);
            tbxPrice.TabIndex = 5;
            tbxPrice.KeyPress += tbxPrice_KeyPress;
            // 
            // btnClearInput
            // 
            btnClearInput.Location = new Point(1384, 452);
            btnClearInput.Name = "btnClearInput";
            btnClearInput.Size = new Size(106, 70);
            btnClearInput.TabIndex = 77;
            btnClearInput.Text = "入力クリア";
            btnClearInput.UseVisualStyleBackColor = true;
            btnClearInput.Click += btnClearInput_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.FromArgb(238, 253, 255);
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.95036F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.0496445F));
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(label8, 0, 1);
            tableLayoutPanel2.Controls.Add(cbxMcName, 1, 0);
            tableLayoutPanel2.Controls.Add(cbxScName, 1, 1);
            tableLayoutPanel2.Location = new Point(1416, 193);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.Size = new Size(403, 88);
            tableLayoutPanel2.TabIndex = 13;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 3);
            label2.Name = "label2";
            label2.Size = new Size(54, 20);
            label2.TabIndex = 0;
            label2.Text = "大分類";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(6, 46);
            label8.Name = "label8";
            label8.Size = new Size(54, 20);
            label8.TabIndex = 1;
            label8.Text = "小分類";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cbxMcName
            // 
            cbxMcName.FormattingEnabled = true;
            cbxMcName.Location = new Point(115, 6);
            cbxMcName.Name = "cbxMcName";
            cbxMcName.Size = new Size(225, 28);
            cbxMcName.TabIndex = 2;
            cbxMcName.SelectedIndexChanged += cbxMcName_SelectedIndexChanged;
            // 
            // cbxScName
            // 
            cbxScName.FormattingEnabled = true;
            cbxScName.Location = new Point(115, 49);
            cbxScName.Name = "cbxScName";
            cbxScName.Size = new Size(225, 28);
            cbxScName.TabIndex = 14;
            cbxScName.SelectedIndexChanged += cbxScName_SelectedIndexChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.FromArgb(39, 203, 190);
            label15.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label15.ForeColor = Color.White;
            label15.Location = new Point(37, 173);
            label15.Name = "label15";
            label15.Size = new Size(69, 20);
            label15.TabIndex = 81;
            label15.Text = "商品情報";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.FromArgb(39, 203, 190);
            label11.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label11.ForeColor = Color.White;
            label11.Location = new Point(1416, 173);
            label11.Name = "label11";
            label11.Size = new Size(39, 20);
            label11.TabIndex = 82;
            label11.Text = "分類";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(238, 253, 255);
            panel1.Controls.Add(label18);
            panel1.Controls.Add(dgvPr);
            panel1.Location = new Point(0, 613);
            panel1.Name = "panel1";
            panel1.Size = new Size(2069, 426);
            panel1.TabIndex = 89;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.FromArgb(39, 203, 190);
            label18.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label18.ForeColor = Color.White;
            label18.ImeMode = ImeMode.NoControl;
            label18.Location = new Point(58, 2);
            label18.Name = "label18";
            label18.Size = new Size(69, 20);
            label18.TabIndex = 88;
            label18.Text = "商品情報";
            // 
            // dgvPr
            // 
            dgvPr.BackgroundColor = SystemColors.ButtonHighlight;
            dgvPr.BorderStyle = BorderStyle.Fixed3D;
            dgvPr.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPr.Location = new Point(58, 22);
            dgvPr.Name = "dgvPr";
            dgvPr.RowHeadersWidth = 51;
            dgvPr.Size = new Size(1789, 298);
            dgvPr.TabIndex = 62;
            dgvPr.SelectionChanged += dgvPr_SelectionChanged;
            // 
            // Product
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Snow;
            ClientSize = new Size(1902, 1033);
            Controls.Add(panel1);
            Controls.Add(label11);
            Controls.Add(label15);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(btnClearInput);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btnUpdate);
            Controls.Add(btnList);
            Controls.Add(btnHidden);
            Controls.Add(btnSearch);
            Controls.Add(btnExit);
            Controls.Add(btnRegist);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Product";
            Text = "販売管理システム・商品管理";
            WindowState = FormWindowState.Maximized;
            Load += Product_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPr).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnRegist;
        private Button btnExit;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label10;
        private TextBox tbxPrSafetyStock;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private Button btnSearch;
        private Button btnHidden;
        private Button btnList;
        private Button btnUpdate;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label12;
        private Label label13;
        private TextBox tbxMaID;
        private TextBox tbxPrJCode;
        private ComboBox cbxPrName;
        private ComboBox cbxPrFlag;
        private TextBox tbxPrModelNumber;
        private Button btnClearInput;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label2;
        private Label label8;
        private ComboBox cbxMcName;
        private ComboBox cbxScName;
        private Label label15;
        private Label label11;
        private Label label14;
        private Label label16;
        private ComboBox cbxMaName;
        private ComboBox comboBox1;
        private Label label9;
        private TextBox tbxPrHidden;
        private DateTimePicker dtpReleaseDate;
        private Panel panel1;
        private Label label18;
        private DataGridView dgvPr;
        private Label label17;
        private TextBox tbxColor;
        private TextBox tbxPrName;
        private TextBox textBox1;
        private TextBox tbxPrID;
        private TextBox tbxPrice;
    }
}
