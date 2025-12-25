namespace SalesManagement_SysDev
{
    partial class F_login
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
            btn_CleateDabase = new Button();
            btn_InsertSampleData = new Button();
            SuspendLayout();
            // 
            // btn_CleateDabase
            // 
            btn_CleateDabase.Location = new Point(881, 505);
            btn_CleateDabase.Margin = new Padding(4, 5, 4, 5);
            btn_CleateDabase.Name = "btn_CleateDabase";
            btn_CleateDabase.Size = new Size(141, 78);
            btn_CleateDabase.TabIndex = 0;
            btn_CleateDabase.Text = "データベース生成";
            btn_CleateDabase.UseVisualStyleBackColor = true;
            btn_CleateDabase.Click += btn_CleateDabase_Click;
            // 
            // btn_InsertSampleData
            // 
            btn_InsertSampleData.Location = new Point(881, 616);
            btn_InsertSampleData.Margin = new Padding(4, 5, 4, 5);
            btn_InsertSampleData.Name = "btn_InsertSampleData";
            btn_InsertSampleData.Size = new Size(141, 78);
            btn_InsertSampleData.TabIndex = 0;
            btn_InsertSampleData.Text = "サンプルデータ登録";
            btn_InsertSampleData.UseVisualStyleBackColor = true;
            btn_InsertSampleData.Click += btn_InsertSampleData_Click;
            // 
            // F_login
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1067, 704);
            Controls.Add(btn_InsertSampleData);
            Controls.Add(btn_CleateDabase);
            Margin = new Padding(4, 5, 4, 5);
            Name = "F_login";
            Text = "販売管理システムログイン画面";
            Load += F_login_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btn_CleateDabase;
        private System.Windows.Forms.Button btn_InsertSampleData;
    }
}
