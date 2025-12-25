namespace SalesManagement_SysDev
{
    partial class Employee
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Employee));
            ldlemID = new Label();
            NM21 = new Label();
            lblemname = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            dtpEmDate = new Label();
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            EmHiredate = new DateTimePicker();
            label6 = new Label();
            label5 = new Label();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            label8 = new Label();
            btnList = new Button();
            btnHiddenList = new Button();
            btnSearch = new Button();
            button2 = new Button();
            dgvEm = new DataGridView();
            btnRegist = new Button();
            btnExit = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tbxEmName = new TextBox();
            label11 = new Label();
            cbxEmFlag = new ComboBox();
            tbxEmHidden = new TextBox();
            label10 = new Label();
            label9 = new Label();
            cbxPoName = new ComboBox();
            tbxSoID = new TextBox();
            tbxEmPassword = new TextBox();
            tbxEmPhone = new TextBox();
            tbxEmID = new TextBox();
            cbxSoName = new ComboBox();
            label16 = new Label();
            tbxPoID = new TextBox();
            btnClearInput = new Button();
            label23 = new Label();
            panel1 = new Panel();
            label7 = new Label();
            btnUpdate = new Button();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEm).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ldlemID
            // 
            resources.ApplyResources(ldlemID, "ldlemID");
            ldlemID.Name = "ldlemID";
            ldlemID.Tag = "3";
            // 
            // NM21
            // 
            resources.ApplyResources(NM21, "NM21");
            NM21.BackColor = Color.Green;
            NM21.ForeColor = Color.White;
            NM21.Name = "NM21";
            // 
            // lblemname
            // 
            resources.ApplyResources(lblemname, "lblemname");
            lblemname.Name = "lblemname";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // dtpEmDate
            // 
            resources.ApplyResources(dtpEmDate, "dtpEmDate");
            dtpEmDate.Name = "dtpEmDate";
            // 
            // webView21
            // 
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            resources.ApplyResources(webView21, "webView21");
            webView21.Name = "webView21";
            webView21.ZoomFactor = 1D;
            // 
            // EmHiredate
            // 
            resources.ApplyResources(EmHiredate, "EmHiredate");
            EmHiredate.Name = "EmHiredate";
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.FromArgb(39, 203, 190);
            resources.ApplyResources(pictureBox2, "pictureBox2");
            pictureBox2.Name = "pictureBox2";
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.FromArgb(9, 5, 126);
            resources.ApplyResources(pictureBox3, "pictureBox3");
            pictureBox3.Name = "pictureBox3";
            pictureBox3.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(label8, "label8");
            label8.BackColor = Color.FromArgb(39, 203, 190);
            label8.ForeColor = Color.White;
            label8.Name = "label8";
            // 
            // btnList
            // 
            btnList.BackColor = Color.DarkSlateBlue;
            resources.ApplyResources(btnList, "btnList");
            btnList.ForeColor = SystemColors.Window;
            btnList.Name = "btnList";
            btnList.UseVisualStyleBackColor = false;
            btnList.Click += btnList_Click_1;
            // 
            // btnHiddenList
            // 
            btnHiddenList.BackColor = Color.DarkSlateBlue;
            resources.ApplyResources(btnHiddenList, "btnHiddenList");
            btnHiddenList.ForeColor = SystemColors.Window;
            btnHiddenList.Name = "btnHiddenList";
            btnHiddenList.UseVisualStyleBackColor = false;
            btnHiddenList.Click += btnHiddenList_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(39, 203, 190);
            resources.ApplyResources(btnSearch, "btnSearch");
            btnSearch.ForeColor = SystemColors.Window;
            btnSearch.Name = "btnSearch";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.DeepPink;
            button2.ForeColor = Color.White;
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = false;
            button2.Click += btnExit_Click;
            // 
            // dgvEm
            // 
            dgvEm.BackgroundColor = SystemColors.ButtonHighlight;
            dgvEm.BorderStyle = BorderStyle.Fixed3D;
            dgvEm.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(dgvEm, "dgvEm");
            dgvEm.Name = "dgvEm";
            dgvEm.SelectionChanged += dgvEm_SelectionChanged;
            // 
            // btnRegist
            // 
            btnRegist.BackColor = Color.DarkSlateBlue;
            resources.ApplyResources(btnRegist, "btnRegist");
            btnRegist.ForeColor = SystemColors.Window;
            btnRegist.Name = "btnRegist";
            btnRegist.UseVisualStyleBackColor = false;
            btnRegist.Click += btnRegist_Click;
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.DeepPink;
            btnExit.ForeColor = Color.White;
            resources.ApplyResources(btnExit, "btnExit");
            btnExit.Name = "btnExit";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.FromArgb(238, 253, 255);
            resources.ApplyResources(tableLayoutPanel1, "tableLayoutPanel1");
            tableLayoutPanel1.Controls.Add(tbxEmName, 3, 0);
            tableLayoutPanel1.Controls.Add(label11, 2, 5);
            tableLayoutPanel1.Controls.Add(cbxEmFlag, 3, 4);
            tableLayoutPanel1.Controls.Add(tbxEmHidden, 3, 5);
            tableLayoutPanel1.Controls.Add(label10, 2, 2);
            tableLayoutPanel1.Controls.Add(label9, 0, 2);
            tableLayoutPanel1.Controls.Add(cbxPoName, 3, 2);
            tableLayoutPanel1.Controls.Add(tbxSoID, 1, 3);
            tableLayoutPanel1.Controls.Add(label5, 2, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 3);
            tableLayoutPanel1.Controls.Add(EmHiredate, 1, 4);
            tableLayoutPanel1.Controls.Add(lblemname, 2, 0);
            tableLayoutPanel1.Controls.Add(ldlemID, 0, 0);
            tableLayoutPanel1.Controls.Add(dtpEmDate, 0, 4);
            tableLayoutPanel1.Controls.Add(tbxEmPassword, 3, 1);
            tableLayoutPanel1.Controls.Add(tbxEmPhone, 1, 1);
            tableLayoutPanel1.Controls.Add(tbxEmID, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 2, 3);
            tableLayoutPanel1.Controls.Add(cbxSoName, 3, 3);
            tableLayoutPanel1.Controls.Add(label16, 2, 4);
            tableLayoutPanel1.Controls.Add(tbxPoID, 1, 2);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // tbxEmName
            // 
            resources.ApplyResources(tbxEmName, "tbxEmName");
            tbxEmName.Name = "tbxEmName";
            tbxEmName.TextChanged += tbxEmName_TextChanged;
            tbxEmName.KeyPress += tbxEmName_KeyPress;
            // 
            // label11
            // 
            resources.ApplyResources(label11, "label11");
            label11.Name = "label11";
            // 
            // cbxEmFlag
            // 
            cbxEmFlag.FormattingEnabled = true;
            cbxEmFlag.Items.AddRange(new object[] { resources.GetString("cbxEmFlag.Items"), resources.GetString("cbxEmFlag.Items1") });
            resources.ApplyResources(cbxEmFlag, "cbxEmFlag");
            cbxEmFlag.Name = "cbxEmFlag";
            cbxEmFlag.SelectedIndexChanged += cbxEmFlag_SelectedIndexChanged;
            // 
            // tbxEmHidden
            // 
            resources.ApplyResources(tbxEmHidden, "tbxEmHidden");
            tbxEmHidden.Name = "tbxEmHidden";
            // 
            // label10
            // 
            resources.ApplyResources(label10, "label10");
            label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(label9, "label9");
            label9.Name = "label9";
            // 
            // cbxPoName
            // 
            cbxPoName.FormattingEnabled = true;
            resources.ApplyResources(cbxPoName, "cbxPoName");
            cbxPoName.Name = "cbxPoName";
            cbxPoName.SelectedIndexChanged += cbxPoName_SelectedIndexChanged;
            // 
            // tbxSoID
            // 
            resources.ApplyResources(tbxSoID, "tbxSoID");
            tbxSoID.Name = "tbxSoID";
            tbxSoID.TextChanged += tbxSoID_TextChanged;
            tbxSoID.KeyPress += tbxSoID_KeyPress;
            // 
            // tbxEmPassword
            // 
            resources.ApplyResources(tbxEmPassword, "tbxEmPassword");
            tbxEmPassword.Name = "tbxEmPassword";
            tbxEmPassword.KeyPress += tbxEmPassword_KeyPress;
            // 
            // tbxEmPhone
            // 
            resources.ApplyResources(tbxEmPhone, "tbxEmPhone");
            tbxEmPhone.Name = "tbxEmPhone";
            tbxEmPhone.KeyPress += tbxEmPhone_KeyPress;
            // 
            // tbxEmID
            // 
            resources.ApplyResources(tbxEmID, "tbxEmID");
            tbxEmID.Name = "tbxEmID";
            tbxEmID.TextChanged += tbxEmID_TextChanged;
            tbxEmID.KeyPress += tbxEmID_KeyPress;
            // 
            // cbxSoName
            // 
            cbxSoName.FormattingEnabled = true;
            resources.ApplyResources(cbxSoName, "cbxSoName");
            cbxSoName.Name = "cbxSoName";
            cbxSoName.SelectedIndexChanged += cbxSoName_SelectedIndexChanged;
            // 
            // label16
            // 
            resources.ApplyResources(label16, "label16");
            label16.Name = "label16";
            // 
            // tbxPoID
            // 
            resources.ApplyResources(tbxPoID, "tbxPoID");
            tbxPoID.Name = "tbxPoID";
            tbxPoID.TextChanged += tbxPoID_TextChanged;
            tbxPoID.KeyPress += tbxPoID_KeyPress;
            // 
            // btnClearInput
            // 
            resources.ApplyResources(btnClearInput, "btnClearInput");
            btnClearInput.Name = "btnClearInput";
            btnClearInput.UseVisualStyleBackColor = true;
            btnClearInput.Click += btnClearInput_Click;
            // 
            // label23
            // 
            resources.ApplyResources(label23, "label23");
            label23.BackColor = Color.FromArgb(39, 203, 190);
            label23.ForeColor = Color.White;
            label23.Name = "label23";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(238, 253, 255);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(dgvEm);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // label7
            // 
            resources.ApplyResources(label7, "label7");
            label7.BackColor = Color.FromArgb(39, 203, 190);
            label7.ForeColor = Color.White;
            label7.Name = "label7";
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(39, 203, 190);
            resources.ApplyResources(btnUpdate, "btnUpdate");
            btnUpdate.ForeColor = SystemColors.Window;
            btnUpdate.Name = "btnUpdate";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // Employee
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Snow;
            Controls.Add(button2);
            Controls.Add(label23);
            Controls.Add(btnClearInput);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(btnExit);
            Controls.Add(label8);
            Controls.Add(pictureBox2);
            Controls.Add(btnUpdate);
            Controls.Add(btnRegist);
            Controls.Add(btnList);
            Controls.Add(btnHiddenList);
            Controls.Add(btnSearch);
            Controls.Add(pictureBox3);
            Controls.Add(label6);
            Controls.Add(webView21);
            Controls.Add(NM21);
            Controls.Add(panel1);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "Employee";
            Load += Employee_Load;
            Click += btnExit_Click;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEm).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label ldlemID;
        private Label NM21;
        private Label lblemname;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label dtpEmDate;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private DateTimePicker EmHiredate;
        private Label label6;
        private Label label5;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Label label8;
        private Button btnList;
        private Button btnHiddenList;
        private Button btnSearch;
        private Button button2;
        private DataGridView dgvEm;
        private Button btnRegist;
        private Button btnExit;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox tbxEmID;
        private TextBox tbxPoID;
        private TextBox tbxEmPassword;
        private TextBox tbxEmPhone;
        private Button btnClearInput;
        private TextBox tbxSoID;
        private Label label23;
        private Panel panel1;
        private Label label7;
        private Label label10;
        private Label label9;
        private ComboBox cbxPoName;
        private ComboBox cbxSoName;
        private TextBox tbxEmHidden;
        private Label label16;
        private ComboBox cbxEmFlag;
        private Label label11;
        private TextBox tbxEmName;
        private Button btnUpdate;
    }
}