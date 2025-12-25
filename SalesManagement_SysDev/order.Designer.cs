
namespace SalesManagement_SysDev
{
    partial class Order
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
            components = new System.ComponentModel.Container();
            label2 = new Label();
            label3 = new Label();
            tbxOrQuantity = new TextBox();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            tbxOrID = new TextBox();
            label8 = new Label();
            tbxOrDetailID = new TextBox();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            tbxEmID = new TextBox();
            dtpOrDate = new DateTimePicker();
            btnConfirm = new Button();
            btnList = new Button();
            btnHiddenList = new Button();
            btnSearch = new Button();
            btnRegist = new Button();
            label18 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tbxTotalPrice = new TextBox();
            label14 = new Label();
            label17 = new Label();
            tbxPrID = new TextBox();
            label1 = new Label();
            tbxOrPrice = new TextBox();
            cbxPrName = new ComboBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            tbxClCharge = new TextBox();
            label19 = new Label();
            cbxOrFlag = new ComboBox();
            cbxOrStateFlag = new ComboBox();
            cbxSoName = new ComboBox();
            cbxEmName = new ComboBox();
            cbxClName = new ComboBox();
            tbxOrHidden = new TextBox();
            label16 = new Label();
            label15 = new Label();
            tbxClID = new TextBox();
            tbxSoID = new TextBox();
            dgvOr = new DataGridView();
            dgvOrDetail = new DataGridView();
            btnOrClearInput = new Button();
            btnOrDetailClearInput = new Button();
            label20 = new Label();
            label21 = new Label();
            label22 = new Label();
            label23 = new Label();
            panel1 = new Panel();
            bindingSource1 = new BindingSource(components);
            btnExit = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOr).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrDetail).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(7, 3);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(61, 23);
            label2.TabIndex = 2;
            label2.Text = "受注ID";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(774, 46);
            label3.Name = "label3";
            label3.Size = new Size(61, 23);
            label3.TabIndex = 9;
            label3.Text = "顧客名";
            // 
            // tbxOrQuantity
            // 
            tbxOrQuantity.Location = new Point(220, 92);
            tbxOrQuantity.Name = "tbxOrQuantity";
            tbxOrQuantity.Size = new Size(190, 27);
            tbxOrQuantity.TabIndex = 17;
            tbxOrQuantity.TextChanged += tbxOrQuantity_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(6, 46);
            label4.Name = "label4";
            label4.Size = new Size(61, 23);
            label4.TabIndex = 12;
            label4.Text = "顧客ID";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(774, 89);
            label5.Name = "label5";
            label5.Size = new Size(61, 23);
            label5.TabIndex = 16;
            label5.Text = "社員名";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(6, 89);
            label6.Name = "label6";
            label6.Size = new Size(61, 23);
            label6.TabIndex = 18;
            label6.Text = "社員ID";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(774, 3);
            label7.Name = "label7";
            label7.Size = new Size(112, 23);
            label7.TabIndex = 20;
            label7.Text = "顧客担当者名";
            // 
            // tbxOrID
            // 
            tbxOrID.Location = new Point(213, 6);
            tbxOrID.Name = "tbxOrID";
            tbxOrID.Size = new Size(190, 30);
            tbxOrID.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(774, 132);
            label8.Name = "label8";
            label8.Size = new Size(78, 23);
            label8.TabIndex = 22;
            label8.Text = "営業所名";
            // 
            // tbxOrDetailID
            // 
            tbxOrDetailID.Location = new Point(220, 6);
            tbxOrDetailID.Name = "tbxOrDetailID";
            tbxOrDetailID.Size = new Size(190, 27);
            tbxOrDetailID.TabIndex = 13;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label9.Location = new Point(6, 132);
            label9.Name = "label9";
            label9.Size = new Size(78, 23);
            label9.TabIndex = 24;
            label9.Text = "営業所ID";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label10.Location = new Point(6, 46);
            label10.Name = "label10";
            label10.Size = new Size(61, 23);
            label10.TabIndex = 26;
            label10.Text = "商品名";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(741, 3);
            label11.Name = "label11";
            label11.Size = new Size(61, 23);
            label11.TabIndex = 28;
            label11.Text = "商品ID";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label12.Location = new Point(6, 175);
            label12.Name = "label12";
            label12.Size = new Size(95, 23);
            label12.TabIndex = 30;
            label12.Text = "受注年月日";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label13.Location = new Point(6, 89);
            label13.Name = "label13";
            label13.Size = new Size(44, 23);
            label13.TabIndex = 32;
            label13.Text = "数量";
            // 
            // tbxEmID
            // 
            tbxEmID.Location = new Point(213, 92);
            tbxEmID.Name = "tbxEmID";
            tbxEmID.Size = new Size(190, 30);
            tbxEmID.TabIndex = 5;
            tbxEmID.TextChanged += tbxEmID_TextChanged;
            // 
            // dtpOrDate
            // 
            dtpOrDate.Checked = false;
            dtpOrDate.Location = new Point(213, 178);
            dtpOrDate.Name = "dtpOrDate";
            dtpOrDate.ShowCheckBox = true;
            dtpOrDate.Size = new Size(250, 30);
            dtpOrDate.TabIndex = 9;
            // 
            // btnConfirm
            // 
            btnConfirm.BackColor = Color.FromArgb(39, 203, 190);
            btnConfirm.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnConfirm.ForeColor = SystemColors.Window;
            btnConfirm.ImeMode = ImeMode.NoControl;
            btnConfirm.Location = new Point(1179, 69);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(140, 57);
            btnConfirm.TabIndex = 70;
            btnConfirm.TabStop = false;
            btnConfirm.Text = "更新";
            btnConfirm.UseVisualStyleBackColor = false;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // btnList
            // 
            btnList.BackColor = Color.DarkSlateBlue;
            btnList.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnList.ForeColor = SystemColors.Window;
            btnList.Location = new Point(1325, 69);
            btnList.Name = "btnList";
            btnList.Size = new Size(140, 57);
            btnList.TabIndex = 68;
            btnList.TabStop = false;
            btnList.Text = "一覧表示";
            btnList.UseVisualStyleBackColor = false;
            btnList.Click += btnList_Click;
            // 
            // btnHiddenList
            // 
            btnHiddenList.BackColor = Color.DarkSlateBlue;
            btnHiddenList.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnHiddenList.ForeColor = SystemColors.Window;
            btnHiddenList.Location = new Point(1617, 69);
            btnHiddenList.Name = "btnHiddenList";
            btnHiddenList.Size = new Size(140, 57);
            btnHiddenList.TabIndex = 66;
            btnHiddenList.TabStop = false;
            btnHiddenList.Text = "非表示リスト";
            btnHiddenList.UseVisualStyleBackColor = false;
            btnHiddenList.Click += btnHiddenList_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(39, 203, 190);
            btnSearch.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnSearch.ForeColor = SystemColors.Window;
            btnSearch.Location = new Point(1471, 69);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(140, 57);
            btnSearch.TabIndex = 65;
            btnSearch.TabStop = false;
            btnSearch.Text = "検索";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnRegist
            // 
            btnRegist.BackColor = Color.DarkSlateBlue;
            btnRegist.Font = new Font("Yu Gothic UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
            btnRegist.ForeColor = SystemColors.Window;
            btnRegist.Location = new Point(1033, 69);
            btnRegist.Name = "btnRegist";
            btnRegist.Size = new Size(140, 57);
            btnRegist.TabIndex = 61;
            btnRegist.TabStop = false;
            btnRegist.Text = "登録";
            btnRegist.UseVisualStyleBackColor = false;
            btnRegist.Click += btnRegist_Click;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.FromArgb(39, 203, 190);
            label18.Font = new Font("Yu Gothic UI", 25F, FontStyle.Bold, GraphicsUnit.Point);
            label18.ForeColor = Color.White;
            label18.Location = new Point(46, 4);
            label18.Margin = new Padding(4, 0, 4, 0);
            label18.Name = "label18";
            label18.Size = new Size(193, 57);
            label18.TabIndex = 60;
            label18.Text = "受注管理";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(39, 203, 190);
            pictureBox1.Location = new Point(-20, -5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1937, 68);
            pictureBox1.TabIndex = 63;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(9, 5, 126);
            pictureBox3.Location = new Point(-3, 63);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(1920, 69);
            pictureBox3.TabIndex = 64;
            pictureBox3.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(238, 253, 255);
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28.92562F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 71.07438F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 171F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 340F));
            tableLayoutPanel1.Controls.Add(tbxTotalPrice, 3, 2);
            tableLayoutPanel1.Controls.Add(tbxOrDetailID, 1, 0);
            tableLayoutPanel1.Controls.Add(tbxOrQuantity, 1, 2);
            tableLayoutPanel1.Controls.Add(label14, 2, 2);
            tableLayoutPanel1.Controls.Add(label17, 0, 0);
            tableLayoutPanel1.Controls.Add(label13, 0, 2);
            tableLayoutPanel1.Controls.Add(tbxPrID, 3, 0);
            tableLayoutPanel1.Controls.Add(label1, 2, 1);
            tableLayoutPanel1.Controls.Add(label10, 0, 1);
            tableLayoutPanel1.Controls.Add(label11, 2, 0);
            tableLayoutPanel1.Controls.Add(tbxOrPrice, 3, 1);
            tableLayoutPanel1.Controls.Add(cbxPrName, 1, 1);
            tableLayoutPanel1.Location = new Point(36, 545);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1256, 158);
            tableLayoutPanel1.TabIndex = 71;
            // 
            // tbxTotalPrice
            // 
            tbxTotalPrice.Location = new Point(915, 92);
            tbxTotalPrice.Name = "tbxTotalPrice";
            tbxTotalPrice.ReadOnly = true;
            tbxTotalPrice.Size = new Size(190, 27);
            tbxTotalPrice.TabIndex = 18;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label14.Location = new Point(741, 89);
            label14.Name = "label14";
            label14.Size = new Size(78, 23);
            label14.TabIndex = 82;
            label14.Text = "合計金額";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label17.Location = new Point(7, 3);
            label17.Margin = new Padding(4, 0, 4, 0);
            label17.Name = "label17";
            label17.Size = new Size(95, 23);
            label17.TabIndex = 87;
            label17.Text = "受注詳細ID";
            // 
            // tbxPrID
            // 
            tbxPrID.Location = new Point(915, 6);
            tbxPrID.Name = "tbxPrID";
            tbxPrID.Size = new Size(190, 27);
            tbxPrID.TabIndex = 14;
            tbxPrID.TextChanged += tbxPrID_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(741, 46);
            label1.Name = "label1";
            label1.Size = new Size(44, 23);
            label1.TabIndex = 81;
            label1.Text = "金額";
            // 
            // tbxOrPrice
            // 
            tbxOrPrice.Location = new Point(915, 49);
            tbxOrPrice.Name = "tbxOrPrice";
            tbxOrPrice.ReadOnly = true;
            tbxOrPrice.Size = new Size(190, 27);
            tbxOrPrice.TabIndex = 16;
            tbxOrPrice.TextChanged += tbxOrPrice_TextChanged;
            // 
            // cbxPrName
            // 
            cbxPrName.FormattingEnabled = true;
            cbxPrName.Location = new Point(220, 49);
            cbxPrName.Name = "cbxPrName";
            cbxPrName.Size = new Size(164, 28);
            cbxPrName.TabIndex = 15;
            cbxPrName.SelectedIndexChanged += cbxPrName_SelectedIndexChanged;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.BackColor = Color.FromArgb(238, 253, 255);
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 26.73913F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.26087F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 567F));
            tableLayoutPanel2.Controls.Add(tbxClCharge, 3, 0);
            tableLayoutPanel2.Controls.Add(label19, 2, 5);
            tableLayoutPanel2.Controls.Add(cbxOrFlag, 1, 5);
            tableLayoutPanel2.Controls.Add(cbxOrStateFlag, 3, 4);
            tableLayoutPanel2.Controls.Add(cbxSoName, 3, 3);
            tableLayoutPanel2.Controls.Add(cbxEmName, 3, 2);
            tableLayoutPanel2.Controls.Add(cbxClName, 3, 1);
            tableLayoutPanel2.Controls.Add(tbxOrHidden, 3, 5);
            tableLayoutPanel2.Controls.Add(label16, 2, 4);
            tableLayoutPanel2.Controls.Add(label15, 0, 5);
            tableLayoutPanel2.Controls.Add(tbxClID, 1, 1);
            tableLayoutPanel2.Controls.Add(tbxSoID, 1, 3);
            tableLayoutPanel2.Controls.Add(tbxEmID, 1, 2);
            tableLayoutPanel2.Controls.Add(label12, 0, 4);
            tableLayoutPanel2.Controls.Add(label7, 2, 0);
            tableLayoutPanel2.Controls.Add(label5, 2, 2);
            tableLayoutPanel2.Controls.Add(label6, 0, 2);
            tableLayoutPanel2.Controls.Add(label4, 0, 1);
            tableLayoutPanel2.Controls.Add(dtpOrDate, 1, 4);
            tableLayoutPanel2.Controls.Add(label9, 0, 3);
            tableLayoutPanel2.Controls.Add(label8, 2, 3);
            tableLayoutPanel2.Controls.Add(tbxOrID, 1, 0);
            tableLayoutPanel2.Controls.Add(label2, 0, 0);
            tableLayoutPanel2.Controls.Add(label3, 2, 1);
            tableLayoutPanel2.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            tableLayoutPanel2.Location = new Point(37, 173);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 6;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 135F));
            tableLayoutPanel2.Size = new Size(1515, 335);
            tableLayoutPanel2.TabIndex = 72;
            // 
            // tbxClCharge
            // 
            tbxClCharge.Location = new Point(947, 6);
            tbxClCharge.Name = "tbxClCharge";
            tbxClCharge.Size = new Size(190, 30);
            tbxClCharge.TabIndex = 2;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Yu Gothic UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            label19.ImeMode = ImeMode.NoControl;
            label19.Location = new Point(773, 222);
            label19.Margin = new Padding(2, 0, 2, 0);
            label19.Name = "label19";
            label19.Size = new Size(95, 23);
            label19.TabIndex = 89;
            label19.Text = "非表示理由";
            // 
            // cbxOrFlag
            // 
            cbxOrFlag.FormattingEnabled = true;
            cbxOrFlag.Location = new Point(213, 225);
            cbxOrFlag.Name = "cbxOrFlag";
            cbxOrFlag.Size = new Size(164, 31);
            cbxOrFlag.TabIndex = 11;
            cbxOrFlag.SelectedIndexChanged += cbxOrFlag_SelectedIndexChanged;
            // 
            // cbxOrStateFlag
            // 
            cbxOrStateFlag.FormattingEnabled = true;
            cbxOrStateFlag.Items.AddRange(new object[] { "未確定", "確定" });
            cbxOrStateFlag.Location = new Point(947, 178);
            cbxOrStateFlag.Name = "cbxOrStateFlag";
            cbxOrStateFlag.Size = new Size(164, 31);
            cbxOrStateFlag.TabIndex = 10;
            // 
            // cbxSoName
            // 
            cbxSoName.FormattingEnabled = true;
            cbxSoName.Location = new Point(947, 135);
            cbxSoName.Name = "cbxSoName";
            cbxSoName.Size = new Size(164, 31);
            cbxSoName.TabIndex = 8;
            cbxSoName.SelectedIndexChanged += cbxSoName_SelectedIndexChanged;
            // 
            // cbxEmName
            // 
            cbxEmName.FormattingEnabled = true;
            cbxEmName.Location = new Point(947, 92);
            cbxEmName.Name = "cbxEmName";
            cbxEmName.Size = new Size(164, 31);
            cbxEmName.TabIndex = 6;
            cbxEmName.SelectedIndexChanged += cbxEmName_SelectedIndexChanged;
            // 
            // cbxClName
            // 
            cbxClName.FormattingEnabled = true;
            cbxClName.Location = new Point(947, 49);
            cbxClName.Name = "cbxClName";
            cbxClName.Size = new Size(164, 31);
            cbxClName.TabIndex = 4;
            cbxClName.SelectedIndexChanged += cbxClName_SelectedIndexChanged;
            // 
            // tbxOrHidden
            // 
            tbxOrHidden.Location = new Point(947, 225);
            tbxOrHidden.Multiline = true;
            tbxOrHidden.Name = "tbxOrHidden";
            tbxOrHidden.Size = new Size(533, 96);
            tbxOrHidden.TabIndex = 12;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label16.ImeMode = ImeMode.NoControl;
            label16.Location = new Point(774, 175);
            label16.Name = "label16";
            label16.Size = new Size(100, 40);
            label16.TabIndex = 98;
            label16.Text = "受注状態\r\n(未確定/確定)";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Yu Gothic UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label15.ImeMode = ImeMode.NoControl;
            label15.Location = new Point(6, 222);
            label15.Name = "label15";
            label15.Size = new Size(100, 40);
            label15.TabIndex = 98;
            label15.Text = "受注管理\r\n(表示/非表示)";
            // 
            // tbxClID
            // 
            tbxClID.Location = new Point(213, 49);
            tbxClID.Name = "tbxClID";
            tbxClID.Size = new Size(190, 30);
            tbxClID.TabIndex = 3;
            tbxClID.TextChanged += tbxClID_TextChanged;
            // 
            // tbxSoID
            // 
            tbxSoID.Location = new Point(213, 135);
            tbxSoID.Name = "tbxSoID";
            tbxSoID.Size = new Size(190, 30);
            tbxSoID.TabIndex = 7;
            tbxSoID.TextChanged += tbxSoID_TextChanged;
            // 
            // dgvOr
            // 
            dgvOr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvOr.BackgroundColor = SystemColors.ButtonHighlight;
            dgvOr.BorderStyle = BorderStyle.Fixed3D;
            dgvOr.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOr.Location = new Point(40, 42);
            dgvOr.Name = "dgvOr";
            dgvOr.RowHeadersWidth = 51;
            dgvOr.Size = new Size(1098, 240);
            dgvOr.TabIndex = 73;
            dgvOr.TabStop = false;
            dgvOr.SelectionChanged += dgvOr_SelectionChanged;
            // 
            // dgvOrDetail
            // 
            dgvOrDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvOrDetail.BackgroundColor = SystemColors.ButtonHighlight;
            dgvOrDetail.BorderStyle = BorderStyle.Fixed3D;
            dgvOrDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrDetail.Location = new Point(1182, 42);
            dgvOrDetail.Name = "dgvOrDetail";
            dgvOrDetail.RowHeadersWidth = 51;
            dgvOrDetail.Size = new Size(564, 240);
            dgvOrDetail.TabIndex = 74;
            dgvOrDetail.TabStop = false;
            dgvOrDetail.SelectionChanged += dgvOrDetail_SelectionChanged;
            // 
            // btnOrClearInput
            // 
            btnOrClearInput.Location = new Point(1579, 438);
            btnOrClearInput.Name = "btnOrClearInput";
            btnOrClearInput.Size = new Size(106, 70);
            btnOrClearInput.TabIndex = 78;
            btnOrClearInput.TabStop = false;
            btnOrClearInput.Text = "入力クリア";
            btnOrClearInput.UseVisualStyleBackColor = true;
            btnOrClearInput.Click += btnOrClearInput_Click;
            // 
            // btnOrDetailClearInput
            // 
            btnOrDetailClearInput.Location = new Point(1325, 633);
            btnOrDetailClearInput.Name = "btnOrDetailClearInput";
            btnOrDetailClearInput.Size = new Size(106, 70);
            btnOrDetailClearInput.TabIndex = 79;
            btnOrDetailClearInput.TabStop = false;
            btnOrDetailClearInput.Text = "入力クリア";
            btnOrDetailClearInput.UseVisualStyleBackColor = true;
            btnOrDetailClearInput.Click += btnOrDetailClearInput_Click;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.BackColor = Color.FromArgb(39, 203, 190);
            label20.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label20.ForeColor = Color.White;
            label20.Location = new Point(1182, 19);
            label20.Name = "label20";
            label20.Size = new Size(69, 20);
            label20.TabIndex = 82;
            label20.Text = "受注詳細";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.BackColor = Color.FromArgb(39, 203, 190);
            label21.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label21.ForeColor = Color.White;
            label21.Location = new Point(40, 19);
            label21.Name = "label21";
            label21.Size = new Size(69, 20);
            label21.TabIndex = 83;
            label21.Text = "受注情報";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.BackColor = Color.FromArgb(39, 203, 190);
            label22.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label22.ForeColor = Color.White;
            label22.Location = new Point(37, 522);
            label22.Name = "label22";
            label22.Size = new Size(69, 20);
            label22.TabIndex = 84;
            label22.Text = "受注詳細";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.BackColor = Color.FromArgb(39, 203, 190);
            label23.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label23.ForeColor = Color.White;
            label23.Location = new Point(36, 150);
            label23.Name = "label23";
            label23.Size = new Size(69, 20);
            label23.TabIndex = 85;
            label23.Text = "受注情報";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(238, 253, 255);
            panel1.Controls.Add(label21);
            panel1.Controls.Add(dgvOr);
            panel1.Controls.Add(dgvOrDetail);
            panel1.Controls.Add(label20);
            panel1.Location = new Point(-3, 722);
            panel1.Name = "panel1";
            panel1.Size = new Size(1909, 316);
            panel1.TabIndex = 86;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.DeepPink;
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(1729, 6);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(120, 57);
            btnExit.TabIndex = 87;
            btnExit.Text = "戻る";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // Order
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Snow;
            CausesValidation = false;
            ClientSize = new Size(1902, 1033);
            Controls.Add(btnExit);
            Controls.Add(label23);
            Controls.Add(label22);
            Controls.Add(btnOrDetailClearInput);
            Controls.Add(btnOrClearInput);
            Controls.Add(label18);
            Controls.Add(pictureBox1);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btnConfirm);
            Controls.Add(btnList);
            Controls.Add(btnHiddenList);
            Controls.Add(btnSearch);
            Controls.Add(btnRegist);
            Controls.Add(panel1);
            Controls.Add(pictureBox3);
            Name = "Order";
            Text = "販売管理システム・受注管理";
            Load += Order_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvOr).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrDetail).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion
        private Label label2;
        private Label label3;
        private TextBox tbxOrQuantity;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox tbxOrID;
        private Label label8;
        private TextBox tbxOrDetailID;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private TextBox tbxEmID;
        private DateTimePicker dtpOrDate;
        private Button btnConfirm;
        private Button btnList;
        private Button btnHiddenList;
        private Button btnSearch;
        private Button btnExit;
        private Button btnRegist;
        private Label label18;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private DataGridView dgvOr;
        private DataGridView dgvOrDetail;
        private Button btnOrClearInput;
        private Button btnOrDetailClearInput;
        private TextBox tbxOrPrice;
        private TextBox tbxPrID;
        private Label label14;
        private Label label1;
        private TextBox tbxClID;
        private TextBox tbxSoID;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Panel panel1;
        private Label label16;
        private Label label15;
        private TextBox tbxTotalPrice;
        private Label label17;
        private TextBox tbxOrHidden;
        private ComboBox cbxPrName;
        private ComboBox cbxOrFlag;
        private ComboBox cbxSoName;
        private ComboBox cbxEmName;
        private ComboBox cbxClName;
        private Label label19;
        private TextBox tbxClCharge;
        private BindingSource bindingSource1;
        private ComboBox cbxOrStateFlag;
    }
}