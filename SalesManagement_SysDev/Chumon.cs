using a;
using Microsoft.Data.SqlClient;
using SalesManagement_SysDev;
using SalesManagement_SysDev.DataAccess;
using SalesManagement_SysDev.DateAccess;
using SalesManagement_SysDev.Entity;
using SalesManagement_SysDev.新しいフォルダー;
using System.DirectoryServices;
using System.Windows.Forms;
using System.Xml.Linq;
using 画面設計用9._0注文管理;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 注文管理画面
{
    public partial class Chumon : Form
    {
        //クラスのインスタンスを生成
        SalesOfficeDataAccess salesOfficeDataAccess = new SalesOfficeDataAccess();
        ClientDataAccess clientDataAccess = new ClientDataAccess();
        EmployeeDataAccess employeeDataAccess = new EmployeeDataAccess();
        ProductDataAccess productDataAccess = new ProductDataAccess();
        ChumonDataAccess chumonDataAccess = new ChumonDataAccess();
        ChumonDetailDataAccess chumonDetailDataAccess = new ChumonDetailDataAccess();
        //自動入力のループ回避変数
        private bool SoUpdating = false;
        private bool ClUpdating = false;
        private bool EmUpdating = false;
        private bool PrUpdating = false;
        private bool HdUpdating = false;
        private bool clear = false;
        private bool isMessageBoxShown = false;

        public Chumon()
        {
            InitializeComponent();
        }






        private void Chumon_Load(object sender, EventArgs e)
        {
            //コンボボックスの設定
            SetFormComboBox();
            //cbxChstateFlag.SelectedIndex = 0;

            //dgvCh.AutoGenerateColumns = false;
            //dgvChDetail.AutoGenerateColumns = false;

        }

        private void SetFormComboBox()
        {
            using (SalesManagementContext context = new SalesManagementContext())
            {
                // コンボボックスを読み取り専用
                cbxSoName.DropDownStyle = ComboBoxStyle.DropDownList;
                cbxClName.DropDownStyle = ComboBoxStyle.DropDownList;
                cbxEmName.DropDownStyle = ComboBoxStyle.DropDownList;
                cbxChFlag.DropDownStyle = ComboBoxStyle.DropDownList;
                cbxChstateFlag.DropDownStyle = ComboBoxStyle.DropDownList;
                cbxPrName.DropDownStyle = ComboBoxStyle.DropDownList;
                //コンボボックスをデフォルト状態に設定
                cbxSoName.SelectedIndex = -1;
                cbxClName.SelectedIndex = -1;
                cbxEmName.SelectedIndex = -1;
                cbxChFlag.SelectedIndex = -1;
                cbxChstateFlag.SelectedIndex = -1;
                cbxPrName.SelectedIndex = -1;


                // データベースからコンボボックスに設定するデータを取得
                List<string> salesOffices = salesOfficeDataAccess.SoGetComboboxText();

                // コンボボックスにデータを追加
                foreach (var office in salesOffices)
                {
                    cbxSoName.Items.Add(office);
                }

                // ClientDateAccess クラスのインスタンスを作成
                ClientDataAccess clientDateAccess = new ClientDataAccess();
                // データベースからコンボボックスに設定するデータを取得    
                List<string> cliantnames = clientDateAccess.ClGetComboboxText();

                // コンボボックスにデータを追加
                foreach (var clname in cliantnames)
                {
                    cbxClName.Items.Add(clname);
                }


                // データベースからコンボボックスに設定するデータを取得 
                List<string> employeenames = employeeDataAccess.EmGetComboboxText();

                // コンボボックスにデータを追加
                foreach (var emname in employeenames)
                {
                    cbxEmName.Items.Add(emname);
                }


                // データベースからコンボボックスに設定するデータを取得 
                List<string> productnames = productDataAccess.PrGetComboboxText();

                // コンボボックスにデータを追加
                foreach (var prname in productnames)
                {
                    cbxPrName.Items.Add(prname);
                }


            }


        }


        //注文情報クリア
        private void ClearCh()
        {
            tbxChID.Text = "";
            tbxOrID.Text = "";
            tbxSoID.Text = "";
            cbxSoName.SelectedIndex = -1;
            tbxClID.Text = "";
            cbxClName.SelectedIndex = -1;
            tbxEmID.Text = "";
            cbxEmName.SelectedIndex = -1;
            cbxChstateFlag.SelectedIndex = -1;
            cbxChFlag.SelectedIndex = -1;
            dtpChDate.Format = DateTimePickerFormat.Custom;
            dtpChDate.Value = DateTime.Now;
            dtpChDate.Format = DateTimePickerFormat.Custom;
            dtpChDate.CustomFormat = "yyyy年MM月dd日";
            dtpChDate.ValueChanged += (s, e) =>
            {
                dtpChDate.Format = DateTimePickerFormat.Custom;
                dtpChDate.CustomFormat = "yyyy年MM月dd日";
            };
            tbxPrHidden.Text = "";
        }



        private void ClearChDetail()
        {
            tbxChDetailID.Text = "";
            tbxPrID.Text = "";
            cbxPrName.SelectedIndex = -1;
            tbxQuantity.Text = "";
        }



        private void tbxPrID_TextChanged(object sender, EventArgs e)
        {
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(tbxPrID.Text))
            {
                return; // 処理を終了
            }

            // 自動入力のループ対策
            if (PrUpdating) return;
            PrUpdating = true;

            // テキストボックスに入力された商品IDを取得
            if (int.TryParse(tbxPrID.Text, out int prID))
            {
                // 商品IDに対応する商品名を取得
                string prName = productDataAccess.GetPrNameById(prID);

                if (!string.IsNullOrEmpty(prName))
                {
                    // コンボボックスの選択肢に社員名を設定
                    cbxPrName.SelectedItem = prName;
                }
                //else
                //{
                //    // 商品名が見つからなかった場合
                //    MessageBox.Show("対応する商品名が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
            {
                // 入力されたテキストが数値に変換できなかった場合
                MessageBox.Show("有効な商品IDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            PrUpdating = false;
        }

        private void btnList_Click(object sender, EventArgs e)
        {

            //取得したデータをフィルタリング
            var filteredChumon = chumonDataAccess.GetChumonData(0);

            // 取得したデータをDataGridViewに表示
            if (filteredChumon != null && filteredChumon.Count > 0)
            {
                dgvCh.DataSource = filteredChumon;
                // データグリッドビューの列ヘッダー名を変更
                dgvCh.Columns["ChID"].HeaderText = "注文ID";
                dgvCh.Columns["SoID"].HeaderText = "営業所ID";
                dgvCh.Columns["EmID"].HeaderText = "社員ID";
                dgvCh.Columns["ClID"].HeaderText = "顧客ID";
                dgvCh.Columns["OrID"].HeaderText = "受注ID";
                dgvCh.Columns["ChDate"].HeaderText = "注文年月日";
                dgvCh.Columns["ChStateFlag"].HeaderText = "注文状態";
                //dgvCh.Columns["ChFlag"].HeaderText = "表示/非表示";
                dgvCh.Columns["ChHidden"].HeaderText = "非表示理由";

                //ChFlag(表示/非表示)の列を非表示にする
                dgvCh.Columns["ChFlag"].Visible = false;

                dgvCh.Columns["Cl"].Visible = false;
                dgvCh.Columns["Em"].Visible = false;
                dgvCh.Columns["Or"].Visible = false;
                dgvCh.Columns["So"].Visible = false;
                dgvCh.Columns["TChumonDetails"].Visible = false;

                // 列幅を設定
                dgvCh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvCh.Columns["ChHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvCh.Columns["ChHidden"].Width = 500;



                // ユーザーが編集や削除をできないように設定
                dgvCh.ReadOnly = true;  // 編集不可
                dgvCh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
                dgvCh.MultiSelect = false;  // 複数行選択を無効
                dgvCh.AllowUserToAddRows = false;  // 行の追加を無効
                dgvCh.AllowUserToDeleteRows = false;  // 行の削除を無効
                dgvCh.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
                dgvCh.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

                // 行番号の列や選択列のサイズ変更を無効にする
                dgvCh.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            }
            else
            {
                MessageBox.Show("表示する注文情報がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCh.DataSource = null; // データソースを解除

                // 列をクリアする場合（必要に応じて）
                dgvCh.Columns.Clear();

                // 行をクリアする場合（必要に応じて）
                dgvCh.Rows.Clear();
            }

            var Chdetails = chumonDetailDataAccess.GetChumonDetails(0);

            if (Chdetails != null && Chdetails.Count > 0)
            {
                // 2つ目のデータグリッドビューにデータを設定
                dgvChDetail.DataSource = Chdetails;

                //列ヘッダーの設定
                dgvChDetail.Columns["ChDetailID"].HeaderText = "注文詳細ID";
                dgvChDetail.Columns["ChID"].HeaderText = "注文ID";
                dgvChDetail.Columns["PrID"].HeaderText = "商品ID";
                dgvChDetail.Columns["ChQuantity"].HeaderText = "数量";

                dgvChDetail.Columns["Ch"].Visible = false;
                dgvChDetail.Columns["Pr"].Visible = false;

                // 列幅を設定
                dgvChDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvChDetail.Columns["ChQuantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // ユーザーが編集や削除をできないように設定
                dgvChDetail.ReadOnly = true;  // 編集不可
                dgvChDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
                dgvChDetail.MultiSelect = false;  // 複数行選択を無効
                dgvChDetail.AllowUserToAddRows = false;  // 行の追加を無効
                dgvChDetail.AllowUserToDeleteRows = false;  // 行の削除を無効
                dgvChDetail.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
                dgvChDetail.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

                // 行番号の列や選択列のサイズ変更を無効にする
                dgvChDetail.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            }
            else
            {
                MessageBox.Show("表示する注文詳細情報がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvChDetail.DataSource = null; // データソースを解除

                // 列をクリアする場合（必要に応じて）
                dgvChDetail.Columns.Clear();

                // 行をクリアする場合（必要に応じて）
                dgvChDetail.Rows.Clear();
            }
        }

        private void dgvCh_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 'ChStateFlag' 列に対して処理を行う
            //自動入力のメッセージ対策
            HdUpdating = true;
            if (dgvCh.Columns[e.ColumnIndex].Name == "ChStateFlag")
            {
                if (e.Value != null)
                {
                    int stateFlag = Convert.ToInt32(e.Value);
                    // 1の場合は「確定」、2の場合は「未確定」、それ以外は「その他」
                    e.Value = stateFlag switch
                    {
                        1 => "確定",
                        0 => "未確定",
                        _ => "その他"
                    };
                    e.FormattingApplied = true; // 書式設定を適用したことを示す
                }
            }
            HdUpdating = false;
        }

        private void dgvCh_SelectionChanged(object sender, EventArgs e)
        {
            // 自動入力のメッセージ対策
            if (HdUpdating) return;
            HdUpdating = true;

            if (dgvCh.SelectedRows.Count > 0)  // 選択された行があるか確認
            {
                // 選択された行の最初の行を取得
                var selectedRow = dgvCh.SelectedRows[0];

                // 各テキストボックスに選択された行のデータを設定
                tbxChID.Text = selectedRow.Cells["ChID"].Value?.ToString() ?? "";
                tbxSoID.Text = selectedRow.Cells["SoID"].Value?.ToString() ?? "";
                tbxEmID.Text = selectedRow.Cells["EmID"].Value?.ToString() ?? ""; // nullの場合は空文字列を設定
                tbxClID.Text = selectedRow.Cells["ClID"].Value?.ToString() ?? "";
                tbxOrID.Text = selectedRow.Cells["OrID"].Value?.ToString() ?? "";
                dtpChDate.Text = selectedRow.Cells["ChDate"].Value?.ToString() ?? "";
                cbxChstateFlag.Text = selectedRow.Cells["ChStateFlag"].Value?.ToString() ?? "";
                tbxPrHidden.Text = selectedRow.Cells["ChHidden"].Value?.ToString() ?? "";
            }

            if (dgvCh.SelectedRows.Count > 0) // 選択された行があるか確認
            {
                var selectedRow = dgvCh.SelectedRows[0];
                var flagSCh = selectedRow.Cells["ChStateFlag"].Value;

                if (flagSCh != null && int.TryParse(flagSCh.ToString(), out int flag))
                {
                    if (flag == 1)
                    {
                        cbxChstateFlag.SelectedIndex = 0;
                    }
                    else if (flag == 0)
                    {
                        cbxChstateFlag.SelectedIndex = 1;
                    }
                    else
                    {
                        cbxChstateFlag.SelectedItem = null;
                    }
                }
                else
                {
                    cbxChstateFlag.SelectedItem = null;
                }
            }

            if (dgvCh.SelectedRows.Count > 0)
            {
                var selectedRow = dgvCh.SelectedRows[0];
                var flagCh = selectedRow.Cells["ChFlag"].Value;

                if (flagCh != null && int.TryParse(flagCh.ToString(), out int flgch))
                {
                    if (flgch == 2)
                    {
                        cbxChFlag.SelectedIndex = 1;
                    }
                    else if (flgch == 0)
                    {
                        cbxChFlag.SelectedIndex = 0;
                    }
                    else
                    {
                        cbxChFlag.SelectedItem = null;
                    }
                }
                else
                {
                    cbxChFlag.SelectedItem = null;
                }
            }
            HdUpdating = false;
        }

        private void dgvChDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvChDetail.SelectedRows.Count > 0)  // 選択された行があるか確認
            {
                // 選択された行の最初の行を取得
                var selectedRow = dgvChDetail.SelectedRows[0];

                // 各テキストボックスに選択された行のデータを設定
                tbxChDetailID.Text = selectedRow.Cells["ChDetailID"].Value.ToString();
                tbxPrID.Text = selectedRow.Cells["PrID"].Value.ToString();
                tbxQuantity.Text = selectedRow.Cells["ChQuantity"].Value.ToString();
                tbxChID.Text = selectedRow.Cells["ChID"].Value.ToString();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 注文情報の入力値を取得
            int? chId = string.IsNullOrWhiteSpace(tbxChID.Text) ? (int?)null : int.Parse(tbxChID.Text);
            int? soId = string.IsNullOrWhiteSpace(tbxSoID.Text) ? (int?)null : int.Parse(tbxSoID.Text);
            int? emId = string.IsNullOrWhiteSpace(tbxEmID.Text) ? (int?)null : int.Parse(tbxEmID.Text);
            int? clId = string.IsNullOrWhiteSpace(tbxClID.Text) ? (int?)null : int.Parse(tbxClID.Text);
            int? orId = string.IsNullOrWhiteSpace(tbxOrID.Text) ? (int?)null : int.Parse(tbxOrID.Text);
            DateTime? chDate = dtpChDate.Checked ? dtpChDate.Value.Date : (DateTime?)null;
            int stflg = cbxChstateFlag.SelectedIndex;

            if (stflg == 0)
            {
                stflg = 1;
            }
            else if (stflg == 1)
            {
                stflg = 0;
            }

            // 注文詳細情報の入力値を取得
            int? chdeId = string.IsNullOrWhiteSpace(tbxChDetailID.Text) ? (int?)null : int.Parse(tbxChDetailID.Text);
            int? prId = string.IsNullOrWhiteSpace(tbxPrID.Text) ? (int?)null : int.Parse(tbxPrID.Text);

            // 入力が全て空の場合は検索を実行しない
            if (chId == null && soId == null && emId == null && clId == null && orId == null && chDate == null && stflg == -1 && chdeId == null && prId == null)
            {
                MessageBox.Show("少なくとも1つの条件を入力してください。(数量、非表示理由を除く)", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var searchresult = chumonDataAccess.SearchChData(chId, soId, emId, clId, orId, chDate, stflg);
            // 注文情報の結果から有効な注文IDリストを作成
            List<int> validChIds = searchresult.Select(ch => ch.ChId).ToList();

            var detailsearchresult = chumonDetailDataAccess.SearchChDetailData(chdeId, chId, prId, validChIds);

            // 詳細グリッドビューに表示されるデータのchid のみを取得
            var chfidList = detailsearchresult.Select(detail => detail.ChId).Distinct().ToList();
            //非表示の注文IDを取得
            var hiddenChIds = chumonDataAccess.GetHiddenChIds(validChIds);
            //非表示のものを除外する
            detailsearchresult = detailsearchresult
            .Where(detail => !hiddenChIds.Contains(detail.ChId))
            .ToList();


            // 検索結果があれば DataGridView に表示
            if (searchresult.Any() && detailsearchresult.Any())
            {
                dgvCh.DataSource = searchresult;
                // データグリッドビューの列ヘッダー名を変更
                dgvCh.Columns["ChID"].HeaderText = "注文ID";
                dgvCh.Columns["SoID"].HeaderText = "営業所ID";
                dgvCh.Columns["EmID"].HeaderText = "社員ID";
                dgvCh.Columns["ClID"].HeaderText = "顧客ID";
                dgvCh.Columns["OrID"].HeaderText = "受注ID";
                dgvCh.Columns["ChDate"].HeaderText = "注文年月日";
                dgvCh.Columns["ChStateFlag"].HeaderText = "注文状態";
                //dgvCh.Columns["ChFlag"].HeaderText = "表示/非表示";
                dgvCh.Columns["ChHidden"].HeaderText = "非表示理由";

                //ChFlag(表示/非表示)の列を非表示にする
                dgvCh.Columns["ChFlag"].Visible = false;
                dgvCh.Columns["Cl"].Visible = false;
                dgvCh.Columns["Em"].Visible = false;
                dgvCh.Columns["Or"].Visible = false;
                dgvCh.Columns["So"].Visible = false;
                dgvCh.Columns["TChumonDetails"].Visible = false;

                // 列幅を設定
                dgvCh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvCh.Columns["ChHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvCh.Columns["ChHidden"].Width = 500;

                // ユーザーが編集や削除をできないように設定
                dgvCh.ReadOnly = true;  // 編集不可
                dgvCh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
                dgvCh.MultiSelect = false;  // 複数行選択を無効
                dgvCh.AllowUserToAddRows = false;  // 行の追加を無効
                dgvCh.AllowUserToDeleteRows = false;  // 行の削除を無効
                dgvCh.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
                dgvCh.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

                // 行番号の列や選択列のサイズ変更を無効にする
                dgvCh.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;



                dgvChDetail.DataSource = detailsearchresult;
                //列ヘッダーの設定
                dgvChDetail.Columns["ChDetailID"].HeaderText = "注文詳細ID";
                dgvChDetail.Columns["ChID"].HeaderText = "注文ID";
                dgvChDetail.Columns["PrID"].HeaderText = "商品ID";
                dgvChDetail.Columns["ChQuantity"].HeaderText = "数量";

                dgvChDetail.Columns["Ch"].Visible = false;
                dgvChDetail.Columns["Pr"].Visible = false;

                // 列幅を設定
                dgvChDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvChDetail.Columns["ChQuantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // ユーザーが編集や削除をできないように設定
                dgvChDetail.ReadOnly = true;  // 編集不可
                dgvChDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
                dgvChDetail.MultiSelect = false;  // 複数行選択を無効
                dgvChDetail.AllowUserToAddRows = false;  // 行の追加を無効
                dgvChDetail.AllowUserToDeleteRows = false;  // 行の削除を無効
                dgvChDetail.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
                dgvChDetail.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

                // 行番号の列や選択列のサイズ変更を無効にする
                dgvChDetail.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            }
            else
            {
                MessageBox.Show("条件に合うデータがありませんでした。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCh.DataSource = null; // データソースを解除

                // 列をクリアする場合（必要に応じて）
                dgvCh.Columns.Clear();

                // 行をクリアする場合（必要に応じて）
                dgvCh.Rows.Clear();

                dgvChDetail.DataSource = null; // データソースを解除

                // 列をクリアする場合（必要に応じて）
                dgvChDetail.Columns.Clear();

                // 行をクリアする場合（必要に応じて）
                dgvChDetail.Rows.Clear();
            }
        }

        private void cbxChFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            //自動入力のメッセージ対策
            if (HdUpdating) return;
            HdUpdating = true;
            if (clear) return;

            //入力された注文情報を取得
            string chids = tbxChID.Text;
            string eiids = tbxSoID.Text;
            string syids = tbxEmID.Text;
            string koids = tbxClID.Text;
            int hiflgindex = cbxChFlag.SelectedIndex;
            int stflgindex = cbxChstateFlag.SelectedIndex;
            int stflg;
            int hiflg;
            DateTime chdate = dtpChDate.Value;
            int einame = cbxSoName.SelectedIndex;
            int syname = cbxEmName.SelectedIndex;
            int koname = cbxClName.SelectedIndex;
            // 空白チェック
            if ((string.IsNullOrWhiteSpace(chids) ||
                string.IsNullOrWhiteSpace(eiids) ||
                string.IsNullOrWhiteSpace(syids) ||
                string.IsNullOrWhiteSpace(koids) ||
                stflgindex == -1 || hiflgindex == -1 ||
                einame == -1 || syname == -1 || koname == -1))
            {
                MessageBox.Show("すべての項目を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxChFlag.SelectedIndex = -1;
                HdUpdating = false;
                return;
            }
            //コンボボックスのIndex値をフラグ値に変更
            if (stflgindex == 0)
            {
                stflg = 1;
            }
            else if (stflgindex == 1)
            {
                stflg = 0;
            }
            else
            {
                MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (hiflgindex == 0)
            //{
            //    hiflg = 0;
            //}
            //else if (hiflgindex == 1)
            //{
            //    hiflg = 2;
            //}
            //else
            //{
            //    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //注文情報をint型に変換          
            int chid = int.Parse(chids);
            int eiid = int.Parse(eiids);
            int syid = int.Parse(syids);
            int koid = int.Parse(koids);

            //データベースで条件をチェック
            bool exists = chumonDataAccess.CheckDataExistence(chid, eiid, syid, koid, stflg, chdate);

            //フラグ変更処理
            if (exists)
            {
                int? chflg = chumonDataAccess.GetChFlag(chid);
                if (hiflgindex == 1 && chflg == 0)
                {
                    if (tbxPrHidden.Text != "")
                    {
                        var result = MessageBox.Show(
                        "注文情報を非表示にしますか？",
                        "確認",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                        );

                        // 「はい」が選択された場合のみ処理を実行
                        if (result == DialogResult.Yes)
                        {
                            string chhide = tbxPrHidden.Text;
                            // 更新する新しい値
                            int newchflg = 2;

                            // データベースの値を更新
                            bool changechflg = chumonDataAccess.ChangeChhideflg(chid, newchflg, chhide);

                            // 更新結果を通知
                            if (changechflg)
                            {
                                MessageBox.Show("注文情報を非表示にしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("情報の更新に失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            //データグリッドビューをクリア
                            dgvCh.DataSource = null; // データソースを解除

                            // 列をクリアする場合（必要に応じて）
                            dgvCh.Columns.Clear();

                            // 行をクリアする場合（必要に応じて）
                            dgvCh.Rows.Clear();

                            dgvChDetail.DataSource = null; // データソースを解除

                            // 列をクリアする場合（必要に応じて）
                            dgvChDetail.Columns.Clear();

                            // 行をクリアする場合（必要に応じて）
                            dgvChDetail.Rows.Clear();

                            HdUpdating = false;
                            return;
                        }
                        //「いいえ」が選択された時
                        else
                        {
                            cbxChFlag.SelectedIndex = 0;
                            HdUpdating = false;
                            return;
                        }
                    }
                    else
                    {
                        var result = MessageBox.Show(
                        "非表示理由が入力されていませんが非表示にしますか？",
                        "確認",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                        );

                        // 「はい」が選択された場合のみ処理を実行
                        if (result == DialogResult.Yes)
                        {
                            string chhide = tbxPrHidden.Text;
                            // 更新する新しい値
                            int newchflg = 2;

                            // データベースの値を更新
                            bool changechflg = chumonDataAccess.ChangeChhideflg(chid, newchflg, chhide);

                            // 更新結果を通知
                            if (changechflg)
                            {
                                MessageBox.Show("注文情報を非表示にしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("情報の更新に失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            //データグリッドビューをクリア
                            dgvCh.DataSource = null; // データソースを解除

                            // 列をクリアする場合（必要に応じて）
                            dgvCh.Columns.Clear();

                            // 行をクリアする場合（必要に応じて）
                            dgvCh.Rows.Clear();

                            dgvChDetail.DataSource = null; // データソースを解除

                            // 列をクリアする場合（必要に応じて）
                            dgvChDetail.Columns.Clear();

                            // 行をクリアする場合（必要に応じて）
                            dgvChDetail.Rows.Clear();

                            HdUpdating = false;
                            return;
                        }
                        //「いいえ」が選択された時
                        else
                        {
                            cbxChFlag.SelectedIndex = 0;
                            HdUpdating = false;
                            return;
                        }
                    }

                }
                else if (hiflgindex == 0 && chflg == 2)
                {
                    var result = MessageBox.Show(
                    "注文情報を表示状態にしますか？" +
                    "(入力されている非表示理由は削除されます。)",
                    "確認",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                    );

                    // 「はい」が選択された場合のみ処理を実行
                    if (result == DialogResult.Yes)
                    {
                        // 更新する新しい値
                        int newchflg = 0;
                        string chhide = "";
                        // データベースの値を更新
                        bool changechflg = chumonDataAccess.ChangeChhideflg(chid, newchflg, chhide);

                        // 更新結果を通知
                        if (changechflg)
                        {
                            MessageBox.Show("注文情報を表示状態にしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("情報の更新に失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        //データグリッドビューをクリア
                        dgvCh.DataSource = null; // データソースを解除

                        // 列をクリアする場合（必要に応じて）
                        dgvCh.Columns.Clear();

                        // 行をクリアする場合（必要に応じて）
                        dgvCh.Rows.Clear();

                        dgvChDetail.DataSource = null; // データソースを解除

                        // 列をクリアする場合（必要に応じて）
                        dgvChDetail.Columns.Clear();

                        // 行をクリアする場合（必要に応じて）
                        dgvChDetail.Rows.Clear();

                        HdUpdating = false;
                        return;
                    }
                    //「いいえ」が選択された時
                    else
                    {
                        cbxChFlag.SelectedIndex = 1;
                        HdUpdating = false;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("エラーが発生しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    HdUpdating = false;
                    return;
                }
            }
            else
            {
                MessageBox.Show("該当するデータがありません。", "確認結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHidden_Click(object sender, EventArgs e)
        {

            //取得したデータをフィルタリング
            var filteredChumon = chumonDataAccess.GetChumonData(2);

            // 取得したデータをDataGridViewに表示
            if (filteredChumon != null && filteredChumon.Count > 0)
            {
                dgvCh.DataSource = filteredChumon;
                // データグリッドビューの列ヘッダー名を変更
                dgvCh.Columns["ChID"].HeaderText = "注文ID";
                dgvCh.Columns["SoID"].HeaderText = "営業所ID";
                dgvCh.Columns["EmID"].HeaderText = "社員ID";
                dgvCh.Columns["ClID"].HeaderText = "顧客ID";
                dgvCh.Columns["OrID"].HeaderText = "受注ID";
                dgvCh.Columns["ChDate"].HeaderText = "注文年月日";
                dgvCh.Columns["ChStateFlag"].HeaderText = "注文状態";
                //dgvCh.Columns["ChFlag"].HeaderText = "表示/非表示";
                dgvCh.Columns["ChHidden"].HeaderText = "非表示理由";

                //ChFlag(表示/非表示)の列を非表示にする
                dgvCh.Columns["ChFlag"].Visible = false;

                dgvCh.Columns["Cl"].Visible = false;
                dgvCh.Columns["Em"].Visible = false;
                dgvCh.Columns["Or"].Visible = false;
                dgvCh.Columns["So"].Visible = false;
                dgvCh.Columns["TChumonDetails"].Visible = false;

                // 列幅を設定
                dgvCh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvCh.Columns["ChHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvCh.Columns["ChHidden"].Width = 500;

                // ユーザーが編集や削除をできないように設定
                dgvCh.ReadOnly = true;  // 編集不可
                dgvCh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
                dgvCh.MultiSelect = false;  // 複数行選択を無効
                dgvCh.AllowUserToAddRows = false;  // 行の追加を無効
                dgvCh.AllowUserToDeleteRows = false;  // 行の削除を無効
                dgvCh.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
                dgvCh.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

                // 行番号の列や選択列のサイズ変更を無効にする
                dgvCh.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            }
            else
            {
                MessageBox.Show("表示する注文情報がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvCh.DataSource = null; // データソースを解除

                // 列をクリアする場合（必要に応じて）
                dgvCh.Columns.Clear();

                // 行をクリアする場合（必要に応じて）
                dgvCh.Rows.Clear();
            }

            var Chdetails = chumonDetailDataAccess.GetChumonDetails(2);

            if (Chdetails != null && Chdetails.Count > 0)
            {
                // 2つ目のデータグリッドビューにデータを設定
                dgvChDetail.DataSource = Chdetails;

                //列ヘッダーの設定
                dgvChDetail.Columns["ChDetailID"].HeaderText = "注文詳細ID";
                dgvChDetail.Columns["ChID"].HeaderText = "注文ID";
                dgvChDetail.Columns["PrID"].HeaderText = "商品ID";
                dgvChDetail.Columns["ChQuantity"].HeaderText = "数量";

                dgvChDetail.Columns["Ch"].Visible = false;
                dgvChDetail.Columns["Pr"].Visible = false;

                // 列幅を設定
                dgvChDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvChDetail.Columns["ChQuantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // ユーザーが編集や削除をできないように設定
                dgvChDetail.ReadOnly = true;  // 編集不可
                dgvChDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
                dgvChDetail.MultiSelect = false;  // 複数行選択を無効
                dgvChDetail.AllowUserToAddRows = false;  // 行の追加を無効
                dgvChDetail.AllowUserToDeleteRows = false;  // 行の削除を無効
                dgvChDetail.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
                dgvChDetail.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

                // 行番号の列や選択列のサイズ変更を無効にする
                dgvChDetail.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            }
            else
            {
                MessageBox.Show("表示する注文詳細情報がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvChDetail.DataSource = null; // データソースを解除

                // 列をクリアする場合（必要に応じて）
                dgvChDetail.Columns.Clear();

                // 行をクリアする場合（必要に応じて）
                dgvChDetail.Rows.Clear();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // 注文情報の入力値を取得
            int? chId = string.IsNullOrWhiteSpace(tbxChID.Text) ? (int?)null : int.Parse(tbxChID.Text);
            int? soId = string.IsNullOrWhiteSpace(tbxSoID.Text) ? (int?)null : int.Parse(tbxSoID.Text);
            int? emId = string.IsNullOrWhiteSpace(tbxEmID.Text) ? (int?)null : int.Parse(tbxEmID.Text);
            int? clId = string.IsNullOrWhiteSpace(tbxClID.Text) ? (int?)null : int.Parse(tbxClID.Text);
            int? orId = string.IsNullOrWhiteSpace(tbxOrID.Text) ? (int?)null : int.Parse(tbxOrID.Text);
            DateTime? chDate = dtpChDate.Checked ? dtpChDate.Value.Date : (DateTime?)null;
            int stflg = cbxChstateFlag.SelectedIndex;
            int chflg = cbxChFlag.SelectedIndex;
            int soname = cbxSoName.SelectedIndex;
            int clname = cbxClName.SelectedIndex;
            int emname = cbxEmName.SelectedIndex;

            if (stflg == 0)
            {
                stflg = 1;
            }
            else if (stflg == 1)
            {
                stflg = 0;
            }

            if (chflg == 0)
            {
                chflg = 0;
            }
            else if (chflg == 1)
            {
                chflg = 2;
            }

            if (chId == null || soId == null || clId == null ||
                orId == null || chDate == null || stflg == -1 || chflg == -1 ||
                soname == -1 || clname == -1)
            {
                MessageBox.Show("全ての注文情報を入力してください", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (stflg == 1)
            {
                MessageBox.Show("正しい注文情報を入力してください", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //入力されたデータの存在チェック
            var searchresult = chumonDataAccess.SearchChData(chId, soId, emId, clId, orId, chDate, stflg);

            if (!searchresult.Any())
            {
                MessageBox.Show("一致するデータが見つかりませんでした。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // メソッドを終了
            }

            //在庫数更新処理・フラグ値変更処理
            var subStockQuantity = new StockDataAccess();
            subStockQuantity.SubStockQuantity(chId.Value);

            var syukkoDataAccess = new SyukkoDataAccess();
            var syukkoDetailDataAccess = new SyukkoDetailDataAccess();
            int loggedInEmId = SessionManager.LoggedInEmployeeID; // ログイン中の社員IDを取得するメソッド

            try
            {
                // 出庫テーブルに登録し、出庫IDを取得
                int syukkoId = syukkoDataAccess.ChumonConfirm(chId.Value, loggedInEmId);

                // 出庫詳細テーブルに登録
                syukkoDetailDataAccess.ChumonDetailConfirm(syukkoId, chId.Value);

                MessageBox.Show("注文確定処理が完了しました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //データグリッドビューをクリア
                dgvCh.DataSource = null; // データソースを解除

                // 列をクリアする場合（必要に応じて）
                dgvCh.Columns.Clear();

                // 行をクリアする場合（必要に応じて）
                dgvCh.Rows.Clear();

                dgvChDetail.DataSource = null; // データソースを解除

                // 列をクリアする場合（必要に応じて）
                dgvChDetail.Columns.Clear();

                // 行をクリアする場合（必要に応じて）
                dgvChDetail.Rows.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "メニュー画面に戻りますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            // 「はい」が選ばれた場合のみ処理を続行
            if (result == DialogResult.Yes)
            {

                画面設計用9._0注文管理.Menu menu = new 画面設計用9._0注文管理.Menu(); // 新しいフォームを表示する
                menu.Show();
                this.Hide(); // 現在のフォームを隠す

            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
        }

        private void cbxSoName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(cbxSoName.Text))
            {
                return; // 処理を終了
            }
            // コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            if (cbxSoName.SelectedIndex != -1)
            {
                //自動入力のループ対策
                if (SoUpdating) return;
                SoUpdating = true;
                // コンボボックスで選択された営業所名を取得
                string selectedSoName = cbxSoName.SelectedItem.ToString();

                //営業所IDを取得してテキストボックスに表示する
                int? Soid = salesOfficeDataAccess.GetSoIdByName(selectedSoName);
                if (Soid.HasValue)
                {
                    tbxSoID.Text = Soid.Value.ToString();
                }
                else
                {
                    tbxSoID.Text = "IDが見つかりません";
                }
            }
            else
            {
                MessageBox.Show("営業所名が選択されていません。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //自動入力のループ対策
            SoUpdating = false;
        }

        private void cbxClName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(cbxClName.Text))
            {
                return; // 処理を終了
            }
            // コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            if (cbxClName.SelectedIndex != -1)
            {
                //自動入力のループ対策
                if (ClUpdating) return;
                ClUpdating = true;
                // コンボボックスで選択された社員名を取得
                string selectedClName = cbxClName.SelectedItem.ToString();

                //社員IDを取得してテキストボックスに表示する
                int? Clid = clientDataAccess.GetClIdByName(selectedClName);
                if (Clid.HasValue)
                {
                    tbxClID.Text = Clid.Value.ToString();
                }
                else
                {
                    tbxClID.Text = "IDが見つかりません";
                }
            }
            else
            {
                MessageBox.Show("社員名が選択されていません。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //自動入力のループ対策
            ClUpdating = false;
        }

        private void cbxEmName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(cbxEmName.Text))
            {
                return; // 処理を終了
            }
            // コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            if (cbxEmName.SelectedIndex != -1)
            {
                //自動入力のループ対策
                if (EmUpdating) return;
                EmUpdating = true;
                // コンボボックスで選択された社員名を取得
                string selectedEmName = cbxEmName.SelectedItem.ToString();

                //社員IDを取得してテキストボックスに表示する
                int? Emid = employeeDataAccess.GetEmIdByName(selectedEmName);
                if (Emid.HasValue)
                {
                    tbxEmID.Text = Emid.Value.ToString();
                }
                else
                {
                    tbxEmID.Text = "IDが見つかりません";
                }
            }
            else
            {
                MessageBox.Show("顧客名が選択されていません。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //自動入力のループ対策
            EmUpdating = false;
        }

        private void cbxPrName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(cbxPrName.Text))
            {
                return; // 処理を終了
            }
            // コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            if (cbxPrName.SelectedIndex != -1)
            {
                //自動入力のループ対策
                if (PrUpdating) return;
                PrUpdating = true;
                // コンボボックスで選択された商品名を取得
                string selectedPrName = cbxPrName.SelectedItem.ToString();

                //商品IDを取得してテキストボックスに表示する
                int? Prid = productDataAccess.GetPrIdByName(selectedPrName);
                if (Prid.HasValue)
                {
                    tbxPrID.Text = Prid.Value.ToString();
                }
                else
                {
                    tbxPrID.Text = "IDが見つかりません";
                }
            }
            else
            {
                MessageBox.Show("商品名が選択されていません。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //自動入力のループ対策
            PrUpdating = false;
        }

        private void btnChClearInput_Click(object sender, EventArgs e)
        {
            clear = true;
            //クリアボタン
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "入力されている注文情報をクリアしますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            // 「はい」が選ばれた場合のみ処理を続行
            if (result == DialogResult.Yes)
            {

                ClearCh();
                dtpChDate.Checked = false;
                dgvCh.ClearSelection();
            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
            clear = false;
        }

        private void btnChDetailClearINput_Click(object sender, EventArgs e)
        {
            //クリアボタン
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "入力されている注文詳細情報をクリアしますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            // 「はい」が選ばれた場合のみ処理を続行
            if (result == DialogResult.Yes)
            {

                ClearChDetail();
                dgvChDetail.ClearSelection();
            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
        }

        private void tbxChID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }

            // 入力された文字が全角の場合
            if (IsFullWidth(e.KeyChar))
            {
                e.Handled = true; // 入力を無効化
                                  // 入力された文字が全角かどうかを判定
                if (IsFullWidth(e.KeyChar))
                {
                    e.Handled = true; // 入力を無効化

                    // メッセージボックスを一度だけ表示
                    if (!isMessageBoxShown)
                    {
                        isMessageBoxShown = true;
                        MessageBox.Show("全角文字は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // タイマーを使ってフラグをリセット
                        var resetTimer = new System.Windows.Forms.Timer();
                        resetTimer.Interval = 500; // 0.5秒後にリセット
                        resetTimer.Tick += (s, args) =>
                        {
                            isMessageBoxShown = false;
                            resetTimer.Stop();
                            resetTimer.Dispose();
                        };
                        resetTimer.Start();
                    }
                }
            }
        }

        // Unicodeカテゴリで全角かどうかを判定
        private bool IsFullWidth(char c)
        {
            // Unicodeカテゴリで全角かどうかを判定
            return System.Text.Encoding.UTF8.GetByteCount(c.ToString()) > 1;
        }

        private void tbxOrID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }

            // 入力された文字が全角の場合
            if (IsFullWidth(e.KeyChar))
            {
                e.Handled = true; // 入力を無効化
                                  // 入力された文字が全角かどうかを判定
                if (IsFullWidth(e.KeyChar))
                {
                    e.Handled = true; // 入力を無効化

                    // メッセージボックスを一度だけ表示
                    if (!isMessageBoxShown)
                    {
                        isMessageBoxShown = true;
                        MessageBox.Show("全角文字は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // タイマーを使ってフラグをリセット
                        var resetTimer = new System.Windows.Forms.Timer();
                        resetTimer.Interval = 500; // 0.5秒後にリセット
                        resetTimer.Tick += (s, args) =>
                        {
                            isMessageBoxShown = false;
                            resetTimer.Stop();
                            resetTimer.Dispose();
                        };
                        resetTimer.Start();
                    }
                }
            }
        }

        private void tbxSoID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }

            // 入力された文字が全角の場合
            if (IsFullWidth(e.KeyChar))
            {
                e.Handled = true; // 入力を無効化
                                  // 入力された文字が全角かどうかを判定
                if (IsFullWidth(e.KeyChar))
                {
                    e.Handled = true; // 入力を無効化

                    // メッセージボックスを一度だけ表示
                    if (!isMessageBoxShown)
                    {
                        isMessageBoxShown = true;
                        MessageBox.Show("全角文字は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // タイマーを使ってフラグをリセット
                        var resetTimer = new System.Windows.Forms.Timer();
                        resetTimer.Interval = 500; // 0.5秒後にリセット
                        resetTimer.Tick += (s, args) =>
                        {
                            isMessageBoxShown = false;
                            resetTimer.Stop();
                            resetTimer.Dispose();
                        };
                        resetTimer.Start();
                    }
                }
            }
        }

        private void tbxClID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }

            // 入力された文字が全角の場合
            if (IsFullWidth(e.KeyChar))
            {
                e.Handled = true; // 入力を無効化
                                  // 入力された文字が全角かどうかを判定
                if (IsFullWidth(e.KeyChar))
                {
                    e.Handled = true; // 入力を無効化

                    // メッセージボックスを一度だけ表示
                    if (!isMessageBoxShown)
                    {
                        isMessageBoxShown = true;
                        MessageBox.Show("全角文字は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // タイマーを使ってフラグをリセット
                        var resetTimer = new System.Windows.Forms.Timer();
                        resetTimer.Interval = 500; // 0.5秒後にリセット
                        resetTimer.Tick += (s, args) =>
                        {
                            isMessageBoxShown = false;
                            resetTimer.Stop();
                            resetTimer.Dispose();
                        };
                        resetTimer.Start();
                    }
                }
            }
        }

        private void tbxEmID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }

            // 入力された文字が全角の場合
            if (IsFullWidth(e.KeyChar))
            {
                e.Handled = true; // 入力を無効化
                                  // 入力された文字が全角かどうかを判定
                if (IsFullWidth(e.KeyChar))
                {
                    e.Handled = true; // 入力を無効化

                    // メッセージボックスを一度だけ表示
                    if (!isMessageBoxShown)
                    {
                        isMessageBoxShown = true;
                        MessageBox.Show("全角文字は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // タイマーを使ってフラグをリセット
                        var resetTimer = new System.Windows.Forms.Timer();
                        resetTimer.Interval = 500; // 0.5秒後にリセット
                        resetTimer.Tick += (s, args) =>
                        {
                            isMessageBoxShown = false;
                            resetTimer.Stop();
                            resetTimer.Dispose();
                        };
                        resetTimer.Start();
                    }
                }
            }
        }

        private void tbxChDetailID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }

            // 入力された文字が全角の場合
            if (IsFullWidth(e.KeyChar))
            {
                e.Handled = true; // 入力を無効化
                                  // 入力された文字が全角かどうかを判定
                if (IsFullWidth(e.KeyChar))
                {
                    e.Handled = true; // 入力を無効化

                    // メッセージボックスを一度だけ表示
                    if (!isMessageBoxShown)
                    {
                        isMessageBoxShown = true;
                        MessageBox.Show("全角文字は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // タイマーを使ってフラグをリセット
                        var resetTimer = new System.Windows.Forms.Timer();
                        resetTimer.Interval = 500; // 0.5秒後にリセット
                        resetTimer.Tick += (s, args) =>
                        {
                            isMessageBoxShown = false;
                            resetTimer.Stop();
                            resetTimer.Dispose();
                        };
                        resetTimer.Start();
                    }
                }
            }
        }

        private void tbxPrID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }

            // 入力された文字が全角の場合
            if (IsFullWidth(e.KeyChar))
            {
                e.Handled = true; // 入力を無効化
                                  // 入力された文字が全角かどうかを判定
                if (IsFullWidth(e.KeyChar))
                {
                    e.Handled = true; // 入力を無効化

                    // メッセージボックスを一度だけ表示
                    if (!isMessageBoxShown)
                    {
                        isMessageBoxShown = true;
                        MessageBox.Show("全角文字は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // タイマーを使ってフラグをリセット
                        var resetTimer = new System.Windows.Forms.Timer();
                        resetTimer.Interval = 500; // 0.5秒後にリセット
                        resetTimer.Tick += (s, args) =>
                        {
                            isMessageBoxShown = false;
                            resetTimer.Stop();
                            resetTimer.Dispose();
                        };
                        resetTimer.Start();
                    }
                }
            }
        }

        private void tbxSoID_TextChanged(object sender, EventArgs e)
        {
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(tbxSoID.Text))
            {
                return; // 処理を終了
            }

            // 自動入力のループ対策
            if (SoUpdating) return;
            SoUpdating = true;

            // テキストボックスに入力された営業所IDを取得
            if (int.TryParse(tbxSoID.Text, out int soID))
            {
                // 営業所IDに対応する営業所名を取得
                string soName = salesOfficeDataAccess.GetSoNameById(soID);

                if (!string.IsNullOrEmpty(soName))
                {
                    // コンボボックスの選択肢に営業所名を設定
                    cbxSoName.SelectedItem = soName;
                }
                //else
                //{
                //    // 営業所名が見つからなかった場合
                //    MessageBox.Show("対応する営業所名が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
            {
                // 入力されたテキストが数値に変換できなかった場合
                MessageBox.Show("有効な営業所IDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            SoUpdating = false;
        }

        private void tbxClID_TextChanged(object sender, EventArgs e)
        {
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(tbxClID.Text))
            {
                return; // 処理を終了
            }

            // 自動入力のループ対策
            if (ClUpdating) return;
            ClUpdating = true;

            // テキストボックスに入力された顧客IDを取得
            if (int.TryParse(tbxClID.Text, out int clID))
            {
                // 顧客IDに対応する顧客名を取得
                string clName = clientDataAccess.GetClNameById(clID);

                if (!string.IsNullOrEmpty(clName))
                {
                    // コンボボックスの選択肢に顧客名を設定
                    cbxClName.SelectedItem = clName;
                }
                //else
                //{
                //    // 顧客名が見つからなかった場合
                //    MessageBox.Show("対応する顧客名が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
            {
                // 入力されたテキストが数値に変換できなかった場合
                MessageBox.Show("有効な顧客IDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            ClUpdating = false;
        }

        private void tbxEmID_TextChanged(object sender, EventArgs e)
        {
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(tbxEmID.Text))
            {
                return; // 処理を終了
            }

            // 自動入力のループ対策
            if (EmUpdating) return;
            EmUpdating = true;

            // テキストボックスに入力された社員IDを取得
            if (int.TryParse(tbxEmID.Text, out int emID))
            {
                // 社員IDに対応する社員名を取得
                string emName = employeeDataAccess.GetEmNameById(emID);

                if (!string.IsNullOrEmpty(emName))
                {
                    // コンボボックスの選択肢に社員名を設定
                    cbxEmName.SelectedItem = emName;
                }
                //else
                //{
                //    // 社員名が見つからなかった場合
                //    MessageBox.Show("対応する社員名が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
            }
            else
            {
                // 入力されたテキストが数値に変換できなかった場合
                MessageBox.Show("有効な社員IDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            EmUpdating = false;
        }



    }
}