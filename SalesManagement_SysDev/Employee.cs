using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SalesManagement_SysDev.Common;
using SalesManagement_SysDev.DataAccess;
using SalesManagement_SysDev.DateAccess;
using SalesManagement_SysDev.新しいフォルダー;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using 画面設計用9._0注文管理;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Windows.Forms.AxHost;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SalesManagement_SysDev
{
    public partial class Employee : Form
    {
        //クラスのインスタンスを生成
        SalesOfficeDataAccess salesOfficeDataAccess = new SalesOfficeDataAccess();
        EmployeeDataAccess employeeDataAccess = new EmployeeDataAccess();
        PositionDataAccess positionDateAccess = new PositionDataAccess();

        //データグリッドビュー用の社員データ
        private static List<MEmployee> employee;
        private static List<MEmployeeDsp> employeeDsp;

        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();

        //自動ループ対策
        private bool SoUpdating = false;
        private bool PoUpdating = false;
        private bool EmUpdating = false;
        private bool isSearching = false;
        private bool clear = false;
        private bool isErrorDisplayed = false;
        private bool hihyouji = true;

        public Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, EventArgs e)

        {
            //コンボボックスの設定
            SetFormComboBox();

            //データグリッドビューの表示
            SetFormDataGridView();

            cbxEmFlag.SelectedIndex = 0; // 表示を選択する
            cbxEmFlag.Enabled = false;

            ClearEm();

        }

        private void SetFormComboBox()
        {
            using (SalesManagementContext context = new SalesManagementContext())
            {
                {   //読み取り専用
                    cbxPoName.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbxSoName.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbxEmFlag.DropDownStyle = ComboBoxStyle.DropDownList;

                    //デフォルト状態に戻す
                    cbxPoName.SelectedIndex = -1;
                    cbxSoName.SelectedIndex = -1;
                    cbxEmFlag.SelectedIndex = -1;
                    tbxEmID.SelectedText = "";
                }

                // データベースからコンボボックスに設定するデータを取得
                List<string> positions = positionDateAccess.PoGetComboboxText();

                // コンボボックスにデータを追加
                foreach (var position in positions)
                {
                    cbxPoName.Items.Add(position);
                }

                // データベースからコンボボックスに設定するデータを取得
                List<string> salesOffices = salesOfficeDataAccess.SoGetComboboxText();

                // コンボボックスにデータを追加
                foreach (var office in salesOffices)
                {
                    cbxSoName.Items.Add(office);
                }
            }
        }
        private void tbxPoID_TextChanged(object sender, EventArgs e)
        {
            //役職ID
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(tbxPoID.Text))
            {
                return; // 処理を終了
            }

            // テキストボックスに入力された役職IDを取得
            if (int.TryParse(tbxPoID.Text, out int prID))
            {
                // 自動入力のループ対策
                if (PoUpdating) return;
                PoUpdating = true;

                // 役職IDに対応する役職名を取得
                string poName = positionDateAccess.GetPoNameById(prID);
                if (!string.IsNullOrEmpty(poName))
                {
                    // コンボボックスの選択肢に役職名を設定
                    cbxPoName.SelectedItem = poName;
                }
                else
                {
                    // 役職名が見つからなかった場合
                    MessageBox.Show("対応する役職名が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 入力されたテキストが数値に変換できなかった場合
                MessageBox.Show("役職IDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            PoUpdating = false;
        }

        private void cbxPoName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //役職名
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(cbxPoName.Text))
            {
                return; // 処理を終了
            }

            // コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            if (cbxPoName.SelectedIndex != -1)
            {
                if (PoUpdating) return;
                PoUpdating = true;
                // コンボボックスで選択された役職名を取得
                string selectedPoName = cbxPoName.SelectedItem.ToString();

                //  役職IDを取得してテキストボックスに表示する
                int? Poid = positionDateAccess.GetPoIdByName(selectedPoName);

                if (Poid.HasValue)
                {
                    tbxPoID.Text = Poid.Value.ToString();
                }
                else
                {
                    tbxPoID.Text = "IDが見つかりません";
                }
            }
            else
            {
                MessageBox.Show("役職名が選択されていません。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //自動入力のループ対策
            PoUpdating = false;
        }

        private void tbxSoID_TextChanged(object sender, EventArgs e)
        {
            //営業所ID
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(tbxSoID.Text))
            {
                return; // 処理を終了
            }

            // テキストボックスに入力された営業所IDを取得
            if (int.TryParse(tbxSoID.Text, out int soID))
            {

                // 自動入力のループ対策
                if (SoUpdating) return;
                SoUpdating = true;

                // 営業所IDに対応する営業所名を取得
                string soName = salesOfficeDataAccess.GetSoNameById(soID);

                if (!string.IsNullOrEmpty(soName))
                {
                    // コンボボックスの選択肢に営業所名を設定
                    cbxSoName.SelectedItem = soName;
                }
                else
                {
                    // 営業所名が見つからなかった場合
                    MessageBox.Show("対応する営業所名が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // 入力されたテキストが数値に変換できなかった場合
                MessageBox.Show("有効な営業所IDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            SoUpdating = false;
        }

        private void cbxSoName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //営業所名
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(cbxSoName.Text))
            {
                return; // 処理を終了
            }

            // コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            if (cbxSoName.SelectedIndex != -1)
            {
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

        private void tbxEmName_TextChanged(object sender, EventArgs e)
        {
            //社員名
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(tbxEmName.Text))
            {
                return;
            }

            if (EmUpdating) return;
            EmUpdating = true;

            // テキストボックスに入力された社員名を取得
            string emName = tbxEmName.Text.Trim();

            // 社員名に対応する社員IDを取得
            int? emId = employeeDataAccess.EmIdByName(emName);

            if (emId.HasValue)
            {
                // 社員IDが見つかった場合、tbxPrIDに表示
                tbxEmID.Text = emId.Value.ToString();
            }
            else
            {
                // 社員名に対応する社員IDが見つからなかった場合
                tbxEmID.Clear(); // 社員IDが見つからない場合はクリア
            }

            EmUpdating = false;
        }

        private void tbxEmID_TextChanged(object sender, EventArgs e)
        {
            //社員ID
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(tbxEmID.Text))
            {
                return; // 処理を終了
            }

            // 自動入力のループ対策
            if (EmUpdating) return;
            EmUpdating = true;

            // テキストボックスに入力された社員IDを取得
            string emId = tbxEmID.Text.Trim();

            // 社員名に対応する社員IDを取得
            string emName = employeeDataAccess.EmNameById(emId);

            if (!string.IsNullOrEmpty(emName))
            {

                tbxEmName.Text = emName.ToString();
            }
            else
            {
                // 社員名に対応する社員IDが見つからなかった場合
                tbxEmName.Clear(); // 社員IDが見つからない場合はクリア
            }

            EmUpdating = false;
        }

        //各テキストボックスの設定
        private void tbxEmID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // バックスペースは許可
            if (char.IsControl(e.KeyChar))
                return;

            // 半角数字かどうかをチェック（全角文字を含まないか）
            if (!char.IsDigit(e.KeyChar) || e.KeyChar >= '０' && e.KeyChar <= '９')
            {
                // 既にエラーメッセージが表示されているか確認
                if (!isErrorDisplayed)
                {
                    MessageBox.Show("社員IDは半角数字です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isErrorDisplayed = true; // エラーメッセージを表示したフラグを立てる
                }
                e.Handled = true; // 無効な入力をキャンセル
            }
            else
            {
                // 半角数字が正しく入力された場合はエラーフラグをリセット
                isErrorDisplayed = false;
            }
        }

        private void tbxEmName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbxEmName != null)
            {
                // テキストの長さをチェック
                if (tbxEmName.Text.Length > 50)
                {
                    // 既にエラーメッセージが表示されていない場合のみ表示
                    if (!isErrorDisplayed)
                    {
                        MessageBox.Show("社員名は50文字以内で入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        isErrorDisplayed = true; // エラーフラグを立てる
                    }

                    // 超過部分を切り捨てる
                    tbxEmName.Text = tbxEmName.Text.Substring(0, 50);

                    // カーソルを末尾に戻す
                    tbxEmName.SelectionStart = tbxEmName.Text.Length;
                    e.Handled = true; // 無効な入力をキャンセル
                }

                else
                {
                    // エラーフラグをリセット
                    isErrorDisplayed = false;
                }
            }
        }

        private void tbxEmPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // バックスペースは許可
            if (char.IsControl(e.KeyChar))
                return;
            // 半角数字かハイフンかどうかをチェック
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '-'))
            {
                // 既にエラーメッセージが表示されているか確認
                if (!isErrorDisplayed)
                {
                    MessageBox.Show("電話番号は半角数字とハイフンのみです。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isErrorDisplayed = true; // エラーメッセージを表示したフラグを立てる
                }

                // 全体の桁数をチェック（ハイフンを含む）
                if (tbxEmPhone.SelectionLength < 9 || tbxEmPhone.SelectionLength > 11)
                {
                    if (!isErrorDisplayed)
                    {
                        MessageBox.Show("社員IDは9桁以上11桁以下（ハイフンを含む）で入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        isErrorDisplayed = true; // エラーメッセージを表示したフラグを立てる
                    }
                    e.Handled = true; // 無効な入力をキャンセル
                }
                else
                {
                    // 正しい場合はエラーフラグをリセット
                    isErrorDisplayed = false;
                }
            }
            e.Handled = true; // 無効な入力をキャンセル
            return;
        }


        private void tbxEmPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 半角数字、および制御文字（バックスペースなど）を許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // 既にエラーメッセージが表示されているか確認
                if (!isErrorDisplayed)
                {
                    MessageBox.Show("パスワードは半角数字です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isErrorDisplayed = true; // エラーメッセージを表示したフラグを立てる
                }
                e.Handled = true; // 無効な入力をキャンセル
            }
        }

        private void tbxPoID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 半角数字、および制御文字（バックスペースなど）を許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // 既にエラーメッセージが表示されているか確認
                if (!isErrorDisplayed)
                {
                    MessageBox.Show("役職IDは半角数字です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isErrorDisplayed = true; // エラーメッセージを表示したフラグを立てる
                }
                e.Handled = true; // 無効な入力をキャンセル
            }
        }

        private void tbxSoID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 半角数字、および制御文字（バックスペースなど）を許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // 既にエラーメッセージが表示されているか確認
                if (!isErrorDisplayed)
                {
                    MessageBox.Show("営業所IDは半角数字です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isErrorDisplayed = true; // エラーメッセージを表示したフラグを立てる
                }
                e.Handled = true; // 無効な入力をキャンセル
            }
        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            //登録
            // 社員情報を得る
            if (!GetValidDataAtRegistration())
            {
                return;
            }

            // 社員IDが既にデータベースに存在するかチェック
            bool isEmployeeIdExists = employeeDataAccess.IsEmployeeIdExists(tbxEmID.Text);
            if (isEmployeeIdExists)
            {
                MessageBox.Show("入力された社員ID、社員名は既に存在しています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EmUpdating = false;
                return;
            }

            // 電話番号が既にデータベースに存在するかチェック
            bool isEmPhoneExists = employeeDataAccess.isEmPhoneExists(tbxEmPhone.Text);
            if (isEmPhoneExists)
            {
                MessageBox.Show("入力された電話番号は既に存在しています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EmUpdating = false;
                return;
            }

            // パスワードが既にデータベースに存在するかチェク
            bool isEmPasswordExists = employeeDataAccess.isEmPasswordExists(tbxEmPassword.Text);
            if (isEmPhoneExists)
            {
                MessageBox.Show("入力されたパスワードは既に存在しています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EmUpdating = false;
                return;
            }

            // 社員情報作成
            var regEmployee = GenerateDataAtRegistration();

            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "入力されている社員情報を登録しますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            if (result == DialogResult.No)
            {
                return;
            }

            bool flg = employeeDataAccess.AddEmployeeData(regEmployee);
            if (flg == true)
            {
                MessageBox.Show("データの登録に成功しました", "登録成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearEm();
            }
            else
            {
                MessageBox.Show("データの登録に失敗しました", "登録失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            //SetFormDataGridView();
            GetDataGridView();
        }

        //try
        //{
        //    // 社員情報の入力値を取得
        //    int? emId = string.IsNullOrWhiteSpace(tbxEmID.Text) ? (int?)null : int.Parse(tbxEmID.Text);
        //    int? soId = string.IsNullOrWhiteSpace(tbxSoID.Text) ? (int?)null : int.Parse(tbxSoID.Text);
        //    int? poId = string.IsNullOrWhiteSpace(tbxPoID.Text) ? (int?)null : int.Parse(tbxPoID.Text);
        //    string? emPassword = string.IsNullOrWhiteSpace(tbxEmPassword.Text) ? null : tbxEmPassword.Text;
        //    string? emPhone = string.IsNullOrWhiteSpace(tbxEmPhone.Text) ? null : tbxEmPhone.Text;
        //    string? emName = string.IsNullOrWhiteSpace(tbxEmName.Text) ? null : tbxEmName.Text;
        //    DateTime? emhireDate = EmHiredate.Checked ? EmHiredate.Value.Date : (DateTime?)null;
        //    int emFlg = cbxEmFlag.SelectedIndex;

        //    if (emFlg == 0)
        //    {
        //        emFlg = 1;
        //    }
        //    else if (emFlg == 1)
        //    {
        //        emFlg = 2;
        //    }

        //    if (string.IsNullOrWhiteSpace(emName) || emPhone == null || emPassword == null || soId == null || poId == null || emhireDate == null || emFlg == 0)
        //    {
        //        MessageBox.Show("必須項目を全て入力してください。（社員名、パスワード、営業所ID、役職ID、入社日）", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    bool isExist = employeeDataAccess.CheckData(emId, emName, emPassword, emPhone);

        //    if (isExist)
        //    {
        //        MessageBox.Show("既に存在している社員データと重複している項目があります", "登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // 新しい社員データを登録
        //    var registeredEmployee = employeeDataAccess.RegisterEmData(
        //          emId,
        //          soId.Value,
        //          poId.Value,
        //          emName,
        //          emhireDate.Value,
        //          emPassword,
        //          emPhone,
        //          emFlg
        //          );

        //    if (registeredEmployee != null)
        //    {
        //        // 登録されたデータを表示
        //        dgvEm.DataSource = new List<MEmployee> { registeredEmployee }; // 登録されたデータのみを表示

        //        // DataGridViewの設定
        //        dgvEm.Columns["EmID"].HeaderText = "社員ID";
        //        dgvEm.Columns["EmName"].HeaderText = "社員名";
        //        dgvEm.Columns["SoID"].HeaderText = "営業所ID";
        //        dgvEm.Columns["PoID"].HeaderText = "役職ID";
        //        dgvEm.Columns["EmHiredate"].HeaderText = "入社年月日";
        //        dgvEm.Columns["EmPassword"].HeaderText = "社員パスワード";
        //        dgvEm.Columns["EmPhone"].HeaderText = "電話番号";

        //        // 不要な列を非表示
        //        dgvEm.Columns["EmFlag"].Visible = false;
        //        dgvEm.Columns["EmHidden"].Visible = false;

        //        // 不要な列を非表示
        //        dgvEm.Columns["Po"].Visible = false;
        //        dgvEm.Columns["So"].Visible = false;
        //        dgvEm.Columns["TArrivals"].Visible = false;
        //        dgvEm.Columns["TChumons"].Visible = false;
        //        dgvEm.Columns["THattyus"].Visible = false;
        //        dgvEm.Columns["TOrders"].Visible = false;
        //        dgvEm.Columns["TSales"].Visible = false;
        //        dgvEm.Columns["TShipments"].Visible = false;
        //        dgvEm.Columns["TSyukkos"].Visible = false;
        //        dgvEm.Columns["TWarehousings"].Visible = false;

        //        // 編集や削除をできないように設定
        //        dgvEm.ReadOnly = true;
        //        dgvEm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //        dgvEm.MultiSelect = false;
        //        dgvEm.AllowUserToAddRows = false;
        //        dgvEm.AllowUserToDeleteRows = false;
        //    }

        //    else
        //    {
        //        MessageBox.Show("社員情報の登録に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //catch (Exception ex)
        //{
        //    MessageBox.Show($"登録中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}

        private bool GetValidDataAtRegistration()
        {
            //社員名
            if (!String.IsNullOrEmpty(tbxEmName.Text.Trim()))
            {
                //文字数チェック
                //50文字以下
                if (tbxEmName.Text.Length > 50)
                {
                    MessageBox.Show("社員名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmName.Focus();
                    return false;
                }
            }
            else
            {
                //商品名テキストボックスが空
                MessageBox.Show("社員名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxEmName.Focus();
                return false;
            }

            //役職ID
            if (!String.IsNullOrEmpty(tbxPoID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPoID.Text.Trim()))
                {
                    MessageBox.Show("役職IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPoID.Focus();
                    return false;
                }
                //役職IDの存在チェック
                if (!positionDateAccess.CheckPositionIDExistence(int.Parse(tbxPoID.Text)))
                {
                    MessageBox.Show("入力された役職IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPoID.Focus();
                    return false;
                }
            }
            else
            {
                //役職名テキストボックスが空
                MessageBox.Show("役職名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPoID.Focus();
                return false;
            }
            //役職名
            if (cbxPoName.SelectedIndex == -1)
            {
                MessageBox.Show("役職名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxPoName.Focus();
                return false;
            }

            //営業所ID
            if (!String.IsNullOrEmpty(tbxSoID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxSoID.Text.Trim()))
                {
                    MessageBox.Show("営業所IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxSoID.Focus();
                    return false;
                }
                //営業所IDの存在チェック
                if (!salesOfficeDataAccess.CheckSalesOfficeIDExistence(int.Parse(tbxSoID.Text)))
                {
                    MessageBox.Show("入力された営業所IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxSoID.Focus();
                    return false;
                }
            }
            else
            {
                //役職名テキストボックスが空
                MessageBox.Show("営業所名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxSoID.Focus();
                return false;
            }
            //営業所名
            if (cbxSoName.SelectedIndex == -1)
            {
                MessageBox.Show("営業所名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxSoName.Focus();
                return false;
            }

            //電話番号
            if (!String.IsNullOrEmpty(tbxEmPhone.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxEmPhone.Text.Trim()))
                {
                    MessageBox.Show("電話番号は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmPhone.Focus();
                    return false;
                }

                // ハイフンチェック
                if (!dataInputFormCheck.CheckPhoneDigitHyphen(tbxEmPhone.Text.Trim()))
                {
                    MessageBox.Show("電話番号はハイフンで入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmPhone.Focus();
                    return false;
                }
            }

            // パスワード
            if (!String.IsNullOrEmpty(tbxEmPassword.Text.Trim()))
            {
                //全角チェック
                if (!dataInputFormCheck.CheckNumeric(tbxEmPassword.Text.Trim()))
                {
                    MessageBox.Show("パスワードは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmPassword.Focus();
                    return false;
                }

                else
                {
                    MessageBox.Show("パスワードを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmPassword.Focus();
                    return false;
                }

            }

            // 生年月日
            if (EmHiredate.CustomFormat == " ")
            {
                MessageBox.Show("生年月日を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EmHiredate.Focus();
                return false;
            }
            return true;
        }

        // 社員情報作成
        private MEmployee GenerateDataAtRegistration()
        {
            return new MEmployee
            {
                EmId = int.Parse(tbxEmID.Text.Trim()),
                EmName = tbxEmName.Text.Trim(),
                EmPhone = tbxEmPhone.Text.Trim(),
                EmPassword = tbxEmPhone.Text.Trim(),
                EmHiredate = EmHiredate.Value,
                SoId = int.Parse(tbxSoID.Text.Trim()),
                PoId = int.Parse(tbxPoID.Text.Trim()),
                EmFlag = 0,
                EmHidden = null
            };
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //更新
            // 社員情報を得る
            if (!GetValidDataAtUpdate())
            {
                return;
            }

            //社員情報作成
            var updEmployee = GenerateDataAtUpdate();

            // 現在の情報と入力された情報を比較して変更がないか確認
            bool isChanged = false;

            // 社員の現在の情報を取得
            var currentEmployee = employeeDataAccess.GetEmployee(int.Parse(tbxEmID.Text));

            if (currentEmployee.EmName != tbxEmName.Text) isChanged = true;
            if (currentEmployee.PoId != int.Parse(tbxPoID.Text)) isChanged = true;
            if (currentEmployee.SoId != int.Parse(tbxSoID.Text)) isChanged = true;
            if (currentEmployee.EmPassword != tbxEmPassword.Text) isChanged = true;
            if (currentEmployee.EmPhone != tbxEmPhone.Text) isChanged = true;
            if (currentEmployee.EmHiredate != EmHiredate.Value) isChanged = true;
            if (currentEmployee.EmFlag != EmFlgNum()) isChanged = true;
            if (currentEmployee.EmHidden != EmHidden()) isChanged = true;

            if (!isChanged)
            {
                MessageBox.Show("変更された項目がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                EmUpdating = false;
                return;
            }

            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "入力されている社員情報を更新しますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );
            if (result == DialogResult.No)
            {
                return;
            }

            bool flg = employeeDataAccess.UpdateEmployeeData(updEmployee);
            if (flg == true)
            {
                MessageBox.Show("データの更新に成功しました", "更新成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearEm();
            }
            else
            {
                MessageBox.Show("データの更新に失敗しました", "更新失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            GetDataGridView();

        }
        //try
        //{
        //    //自動入力のメッセージ対策
        //    if (EmUpdating) return;
        //    EmUpdating = true;

        //    //入力された社員情報を取得
        //    string emids = tbxEmID.Text;
        //    string emnames = tbxEmName.Text;
        //    string soids = tbxSoID.Text;
        //    string poids = tbxPoID.Text;
        //    string emPhone = tbxEmPhone.Text;
        //    string emPass = tbxEmPassword.Text;
        //    int hiflgindex;
        //    DateTime emhidate = EmHiredate.Value;
        //    string poname = cbxPoName.Text;
        //    string soname = cbxSoName.Text;


        //    if (cbxEmFlag.SelectedIndex == 0)
        //    {
        //        hiflgindex = 0;
        //    }
        //    else
        //    {
        //        hiflgindex = 2;
        //    }

        //    //空白チェック
        //    if (string.IsNullOrWhiteSpace(emids) ||
        //        string.IsNullOrWhiteSpace(emnames) ||
        //        string.IsNullOrWhiteSpace(soids) ||
        //        string.IsNullOrWhiteSpace(poids) ||
        //        string.IsNullOrWhiteSpace(emPhone) ||
        //        string.IsNullOrWhiteSpace(emPass) ||

        //        hiflgindex == -1 ||
        //         clear != false)
        //    {
        //        MessageBox.Show("すべての項目を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        cbxEmFlag.SelectedIndex = -1;
        //        EmUpdating = false;
        //        return;
        //    }

        //    //情報をint型に変換
        //    int emid = int.Parse(emids);
        //    int poid = int.Parse(poids);
        //    int soid = int.Parse(soids);

        //    // 社員の現在の情報を取得
        //    var currentEmployee = employeeDataAccess.GetEmployee(emid);

        //    // 更新対象の社員情報をデータベースに反映
        //    bool isExist = false;

        //    if (currentEmployee.EmName != emnames) isExist = true;
        //    if (currentEmployee.EmId != emid) isExist = true;
        //    if (currentEmployee.SoId != soid) isExist = true;
        //    if (currentEmployee.PoId != poid) isExist = true;
        //    if (currentEmployee.EmHiredate != emhidate) isExist = true;
        //    if (currentEmployee.EmPassword != emPass) isExist = true;
        //    if (currentEmployee.EmPhone != emPhone) isExist = true;
        //    if (currentEmployee.EmFlag != hiflgindex) isExist = true;

        //    if (isExist)
        //    {
        //        MessageBox.Show("変更された項目がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // 更新対象の社員情報をデータベースに反映
        //    var employees = employeeDataAccess.UpdateEmployee(emid, emnames, soid, poid, emPhone, emPass, emhidate, hiflgindex);

        //    MessageBox.Show("社員情報が更新されました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    //データグリッドビューに表示

        //    if (employees != null)
        //    {

        //        try
        //        {
        //            // 更新した社員情報を BindingList に追加
        //            BindingList<MEmployee> employeesList = new BindingList<MEmployee> { employees };

        //            // DataGridView のデータソースを BindingList に設定

        //            dgvEm.DataSource = employeesList;
        //            // DataGridViewの設定
        //            dgvEm.Columns["EmID"].HeaderText = "社員ID";
        //            dgvEm.Columns["EmName"].HeaderText = "社員名";
        //            dgvEm.Columns["SoID"].HeaderText = "営業所ID";
        //            dgvEm.Columns["PoID"].HeaderText = "役職ID";
        //            dgvEm.Columns["EmHiredate"].HeaderText = "入社年月日";
        //            dgvEm.Columns["EmPassword"].HeaderText = "社員パスワード";
        //            dgvEm.Columns["EmPhone"].HeaderText = "電話番号";

        //            // 不要な列を非表示
        //            dgvEm.Columns["EmFlag"].Visible = false;
        //            dgvEm.Columns["EmHidden"].Visible = false;
        //            dgvEm.Columns["Po"].Visible = false;
        //            dgvEm.Columns["So"].Visible = false;
        //            dgvEm.Columns["TArrivals"].Visible = false;
        //            dgvEm.Columns["TChumons"].Visible = false;
        //            dgvEm.Columns["THattyus"].Visible = false;
        //            dgvEm.Columns["TOrders"].Visible = false;
        //            dgvEm.Columns["TSales"].Visible = false;
        //            dgvEm.Columns["TShipments"].Visible = false;
        //            dgvEm.Columns["TSyukkos"].Visible = false;
        //            dgvEm.Columns["TWarehousings"].Visible = false;

        //            // 編集や削除をできないように設定
        //            dgvEm.ReadOnly = true;
        //            dgvEm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //            dgvEm.MultiSelect = false;
        //            dgvEm.AllowUserToAddRows = false;
        //            dgvEm.AllowUserToDeleteRows = false;
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"更新情報が表示できません ", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        EmUpdating = false;
        //        return;
        //    }
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show($"更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //}


        private void btnList_Click_1(object sender, EventArgs e)
        {

            //一覧表示
            //データグリッドビューの表示
            SetFormDataGridView();

            //// データを取得
            //var employee = employeeDataAccess.GetEmployeeData();

            ////取得したデータをフィルタリング
            //var filteredEmployee = employeeDataAccess.GetEmployeeData(0);

            //// 取得したデータをDataGridViewに表示
            //if (filteredEmployee != null && employee.Count > 0)
            //{
            //    dgvEm.DataSource = employee;

            //    // DataGridViewに表示されるヘッダー名を変更
            //    dgvEm.Columns["EmID"].HeaderText = "社員ID";
            //    dgvEm.Columns["EmName"].HeaderText = "社員名";
            //    dgvEm.Columns["SoID"].HeaderText = "営業所ID";
            //    dgvEm.Columns["PoID"].HeaderText = "役職ID";
            //    dgvEm.Columns["EmHiredate"].HeaderText = "入社年月日";
            //    dgvEm.Columns["EmPassword"].HeaderText = "社員パスワード";
            //    dgvEm.Columns["EmPhone"].HeaderText = "電話番号";
            //    dgvEm.Columns["EmFlag"].HeaderText = "表示/非表示";
            //    dgvEm.Columns["EmHidden"].HeaderText = "非表示理由";

            //    // 不要な列を非表示
            //    dgvEm.Columns["Po"].Visible = false;
            //    dgvEm.Columns["So"].Visible = false;
            //    dgvEm.Columns["TArrivals"].Visible = false;
            //    dgvEm.Columns["TChumons"].Visible = false;
            //    dgvEm.Columns["THattyus"].Visible = false;
            //    dgvEm.Columns["TOrders"].Visible = false;
            //    dgvEm.Columns["TSales"].Visible = false;
            //    dgvEm.Columns["TShipments"].Visible = false;
            //    dgvEm.Columns["TSyukkos"].Visible = false;
            //    dgvEm.Columns["TWarehousings"].Visible = false;

            //    //Flag(表示/非表示)の列とHidden(非表示理由)を非表示にする
            //    dgvEm.Columns["EmFlag"].Visible = false;
            //    dgvEm.Columns["EmHidden"].Visible = false;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvEm.ReadOnly = true;  // 編集不可
            //    dgvEm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //    dgvEm.MultiSelect = false;  // 複数行選択を無効
            //    dgvEm.AllowUserToAddRows = false;  // 行の追加を無効
            //    dgvEm.AllowUserToDeleteRows = false;  // 行の削除を無効
            //    dgvEm.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //    dgvEm.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvEm.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //}
            //else
            //{
            //    MessageBox.Show("表示するデータがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    // 列をクリアする場合（必要に応じて）
            //    dgvEm.Columns.Clear();

            //    // 行をクリアする場合（必要に応じて）
            //    dgvEm.Rows.Clear();
            //}
        }

        // 社員情報を得る
        private bool GetValidDataAtUpdate()
        {
            //社員名
            if (!String.IsNullOrEmpty(tbxEmName.Text.Trim()))
            {
                //文字数チェック
                //50文字以下
                if (tbxEmName.Text.Length > 50)
                {
                    MessageBox.Show("社員名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmName.Focus();
                    return false;
                }
            }
            else
            {
                //商品名テキストボックスが空
                MessageBox.Show("社員名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxEmName.Focus();
                return false;
            }

            //役職ID
            if (!String.IsNullOrEmpty(tbxPoID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPoID.Text.Trim()))
                {
                    MessageBox.Show("役職IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPoID.Focus();
                    return false;
                }
                //役職IDの存在チェック
                if (!positionDateAccess.CheckPositionIDExistence(int.Parse(tbxPoID.Text)))
                {
                    MessageBox.Show("入力された役職IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPoID.Focus();
                    return false;
                }
            }
            else
            {
                //役職名テキストボックスが空
                MessageBox.Show("役職名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPoID.Focus();
                return false;
            }
            //役職名
            if (cbxPoName.SelectedIndex == -1)
            {
                MessageBox.Show("役職名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxPoName.Focus();
                return false;
            }

            //営業所ID
            if (!String.IsNullOrEmpty(tbxSoID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxSoID.Text.Trim()))
                {
                    MessageBox.Show("営業所IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxSoID.Focus();
                    return false;
                }
                //営業所IDの存在チェック
                if (!salesOfficeDataAccess.CheckSalesOfficeIDExistence(int.Parse(tbxSoID.Text)))
                {
                    MessageBox.Show("入力された営業所IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxSoID.Focus();
                    return false;
                }
            }
            else
            {
                //役職名テキストボックスが空
                MessageBox.Show("営業所名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxSoID.Focus();
                return false;
            }
            //営業所名
            if (cbxSoName.SelectedIndex == -1)
            {
                MessageBox.Show("営業所名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxSoName.Focus();
                return false;
            }

            //電話番号
            if (!String.IsNullOrEmpty(tbxEmPhone.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxEmPhone.Text.Trim()))
                {
                    MessageBox.Show("電話番号は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmPhone.Focus();
                    return false;
                }

                // ハイフンチェック
                if (!dataInputFormCheck.CheckPhoneDigitHyphen(tbxEmPhone.Text.Trim()))
                {
                    MessageBox.Show("電話番号はハイフンで入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmPhone.Focus();
                    return false;
                }
            }

            // パスワード
            if (!String.IsNullOrEmpty(tbxEmPassword.Text.Trim()))
            {
                //全角チェック
                if (!dataInputFormCheck.CheckNumeric(tbxEmPassword.Text.Trim()))
                {
                    MessageBox.Show("パスワードは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmPassword.Focus();
                    return false;
                }

                else
                {
                    MessageBox.Show("パスワードを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmPassword.Focus();
                    return false;
                }

            }
            return true;
        }

        // 社員情報作成
        private MEmployee GenerateDataAtUpdate()
        {
            return new MEmployee
            {
                EmId = int.Parse(tbxEmID.Text.Trim()),
                EmName = tbxEmName.Text.Trim(),
                EmPhone = tbxEmPhone.Text.Trim(),
                EmPassword = tbxEmPhone.Text.Trim(),
                SoId = int.Parse(tbxSoID.Text.Trim()),
                PoId = int.Parse(tbxPoID.Text.Trim()),
                EmFlag = EmFlgNum(),
                EmHidden = EmHidden()
            };
        }

        //デーグリッドビューに表示された項目を選択するとテキストボックス等に表示される機能
        private void dgvEm_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEm.SelectedRows.Count > 0)  // 選択された行があるか確認
            {
                // 選択された行の最初の行を取得
                var selectedRow = dgvEm.SelectedRows[0];

                // 各テキストボックスに選択された行のデータを設定
                tbxSoID.Text = selectedRow.Cells["SoID"].Value.ToString();
                tbxEmID.Text = selectedRow.Cells["EmID"].Value.ToString();
                tbxPoID.Text = selectedRow.Cells["PoID"].Value.ToString();
                tbxEmPhone.Text = selectedRow.Cells["EmPhone"].Value.ToString();
                tbxEmName.Text = selectedRow.Cells["EmName"].Value.ToString();
                EmHiredate.Text = selectedRow.Cells["EmHiredate"].Value.ToString();
                tbxEmPassword.Text = selectedRow.Cells["EmPassword"].Value.ToString();
                tbxEmHidden.Text = selectedRow.Cells["EmHidden"].Value?.ToString() ?? "";
            }


            if (dgvEm.SelectedRows.Count > 0) // 行が選択されている場合
            {
                var selectedRow = dgvEm.SelectedRows[0];

                //非表示の列からフラグ値を取得
                var flagEm = selectedRow.Cells["EmFlag"].Value;
                if (flagEm != null && int.TryParse(flagEm.ToString(), out int flgem))
                {
                    if (flgem == 2)
                    {
                        cbxEmFlag.SelectedIndex = 1;
                    }
                    else if (flgem == 0)
                    {
                        cbxEmFlag.SelectedIndex = 0;
                    }
                    else
                    {
                        cbxEmFlag.SelectedItem = null;
                    }
                }
                else
                {
                    cbxEmFlag.SelectedItem = null;
                }
            }
            EmUpdating = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //検索
            // 社員情報を得る
            if (!GetValidDataAtSlect())
            {
                return;
            }

            // 社員情報抽出
            GenerateDataAtSelect();

            // 社員抽出結果表示
            SetSelectData();
            isSearching = true;

            //// 社員情報の入力値を取得
            //int? emId = string.IsNullOrWhiteSpace(tbxEmID.Text) ? (int?)null : int.Parse(tbxEmID.Text);
            //int? soId = string.IsNullOrWhiteSpace(tbxSoID.Text) ? (int?)null : int.Parse(tbxSoID.Text);
            //int? poId = string.IsNullOrWhiteSpace(tbxPoID.Text) ? (int?)null : int.Parse(tbxPoID.Text);
            //int? emPassword = string.IsNullOrWhiteSpace(tbxEmPassword.Text) ? (int?)null : int.Parse(tbxEmPassword.Text);
            //int? emPhone = string.IsNullOrWhiteSpace(tbxEmPhone.Text) ? (int?)null : int.Parse(tbxEmPhone.Text);
            //string? emName = string.IsNullOrWhiteSpace(tbxEmName.Text) ? null : tbxEmName.Text;
            //DateTime? emhireDate = EmHiredate.Checked ? EmHiredate.Value.Date : (DateTime?)null;

            //// 入力が全て空の場合は検索を実行しない
            //if (emId == null && soId == null && poId == null && emPhone == null && emName == null && emPassword == null && emhireDate == null)
            //{
            //    MessageBox.Show("少なくとも1つの条件を入力してください。(数量、非表示理由を除く)", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //var searchresult = employeeDataAccess.SearchEmData(emId, soId, poId, emPhone, emPassword, emName, emhireDate);
            //// 注文情報の結果から有効な注文IDリストを作成
            //List<int> validEmIds = searchresult.Select(em => em.EmId).ToList();
            //// 検索結果があれば DataGridView に表示
            //if (searchresult.Any())
            //{
            //    dgvEm.DataSource = searchresult;

            //    // DataGridViewに表示されるヘッダー名を変更
            //    dgvEm.Columns["EmID"].HeaderText = "社員ID";
            //    dgvEm.Columns["EmName"].HeaderText = "社員名";
            //    dgvEm.Columns["SoID"].HeaderText = "営業所ID";
            //    dgvEm.Columns["PoID"].HeaderText = "役職ID";
            //    dgvEm.Columns["EmHiredate"].HeaderText = "入社年月日";
            //    dgvEm.Columns["EmPassword"].HeaderText = "社員パスワード";
            //    dgvEm.Columns["EmPhone"].HeaderText = "電話番号";
            //    dgvEm.Columns["EmFlag"].HeaderText = "表示/非表示";
            //    dgvEm.Columns["EmHidden"].HeaderText = "非表示理由";

            //    // 不要な列を非表示
            //    dgvEm.Columns["Po"].Visible = false;
            //    dgvEm.Columns["So"].Visible = false;
            //    dgvEm.Columns["TArrivals"].Visible = false;
            //    dgvEm.Columns["TChumons"].Visible = false;
            //    dgvEm.Columns["THattyus"].Visible = false;
            //    dgvEm.Columns["TOrders"].Visible = false;
            //    dgvEm.Columns["TSales"].Visible = false;
            //    dgvEm.Columns["TShipments"].Visible = false;
            //    dgvEm.Columns["TSyukkos"].Visible = false;
            //    dgvEm.Columns["TWarehousings"].Visible = false;

            //    //Flag(表示/非表示)の列とHidden(非表示理由)を非表示にする
            //    dgvEm.Columns["EmFlag"].Visible = false;
            //    dgvEm.Columns["EmHidden"].Visible = false;
            //    dgvEm.Columns["Po"].Visible = false;
            //    dgvEm.Columns["So"].Visible = false;
            //    dgvEm.Columns["TArrivals"].Visible = false;
            //    dgvEm.Columns["TChumons"].Visible = false;
            //    dgvEm.Columns["THattyus"].Visible = false;
            //    dgvEm.Columns["TOrders"].Visible = false;
            //    dgvEm.Columns["TSales"].Visible = false;
            //    dgvEm.Columns["TShipments"].Visible = false;
            //    dgvEm.Columns["TSyukkos"].Visible = false;
            //    dgvEm.Columns["TWarehousings"].Visible = false;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvEm.ReadOnly = true;  // 編集不可
            //    dgvEm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //    dgvEm.MultiSelect = false;  // 複数行選択を無効
            //    dgvEm.AllowUserToAddRows = false;  // 行の追加を無効
            //    dgvEm.AllowUserToDeleteRows = false;  // 行の削除を無効
            //    dgvEm.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //    dgvEm.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvEm.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //}
            //else
            //{
            //    MessageBox.Show("表示するデータがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
           }
        
            // 社員情報を得る
            private bool GetValidDataAtSlect()
            {
                //社員名
                if (!String.IsNullOrEmpty(tbxEmName.Text.Trim()))
                {
                    //文字数チェック
                    //50文字以下
                    if (tbxEmName.Text.Length > 50)
                    {
                        MessageBox.Show("社員名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxEmName.Focus();
                        return false;
                    }
                }
                //else
                //{
                //    //商品名テキストボックスが空
                //    MessageBox.Show("社員名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    tbxEmName.Focus();
                //    return false;
                //}

                //役職ID
                if (!String.IsNullOrEmpty(tbxPoID.Text.Trim()))
                {
                    //数値チェック
                    if (!dataInputFormCheck.CheckNumeric(tbxPoID.Text.Trim()))
                    {
                        MessageBox.Show("役職IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxPoID.Focus();
                        return false;
                    }
                    //役職IDの存在チェック
                    if (!positionDateAccess.CheckPositionIDExistence(int.Parse(tbxPoID.Text)))
                    {
                        MessageBox.Show("入力された役職IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxPoID.Focus();
                        return false;
                    }
                }
                //else
                //{
                //    //役職名テキストボックスが空
                //    MessageBox.Show("役職名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    tbxPoID.Focus();
                //    return false;
                //}
                //役職名
                //if (cbxPoName.SelectedIndex == -1)
                //{
                //    MessageBox.Show("役職名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    cbxPoName.Focus();
                //    return false;
                //}

                //営業所ID
                if (!String.IsNullOrEmpty(tbxSoID.Text.Trim()))
                {
                    //数値チェック
                    if (!dataInputFormCheck.CheckNumeric(tbxSoID.Text.Trim()))
                    {
                        MessageBox.Show("営業所IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxSoID.Focus();
                        return false;
                    }
                    //営業所IDの存在チェック
                    if (!salesOfficeDataAccess.CheckSalesOfficeIDExistence(int.Parse(tbxSoID.Text)))
                    {
                        MessageBox.Show("入力された営業所IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxSoID.Focus();
                        return false;
                    }
                }
                //else
                //{
                //    //役職名テキストボックスが空
                //    MessageBox.Show("営業所名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    tbxSoID.Focus();
                //    return false;
                //}
                //営業所名
                //if (cbxSoName.SelectedIndex == -1)
                //{
                //    MessageBox.Show("営業所名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    cbxSoName.Focus();
                //    return false;
                //}

                //電話番号
                if (!String.IsNullOrEmpty(tbxEmPhone.Text.Trim()))
                {
                    //数値チェック
                    if (!dataInputFormCheck.CheckNumeric(tbxEmPhone.Text.Trim()))
                    {
                        MessageBox.Show("電話番号は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxEmPhone.Focus();
                        return false;
                    }

                    // ハイフンチェック
                    if (!dataInputFormCheck.CheckPhoneDigitHyphen(tbxEmPhone.Text.Trim()))
                    {
                        MessageBox.Show("電話番号はハイフンで入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxEmPhone.Focus();
                        return false;
                    }
                }

                // パスワード
                if (!String.IsNullOrEmpty(tbxEmPassword.Text.Trim()))
                {
                    //全角チェック
                    if (!dataInputFormCheck.CheckNumeric(tbxEmPassword.Text.Trim()))
                    {
                        MessageBox.Show("パスワードは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tbxEmPassword.Focus();
                        return false;
                    }

                    //else
                    //{
                    //    MessageBox.Show("パスワードを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    tbxEmPassword.Focus();
                    //    return false;
                    //}

                }
                return true;
            }
        

        //社員情報抽出
        private void GenerateDataAtSelect()
        {


             DateTime? time = EmHiredate.Value.Date;

             if (EmHiredate.CustomFormat == " ")
             {
                 time = null;
             }

            //検索条件のセット
            MEmployeeDsp selectCondition = new MEmployeeDsp()
            {
                EmId = tbxEmID.Text,
                EmName = tbxEmName.Text,
                PoId = tbxPoID.Text,
                SoId = tbxSoID.Text,
                EmPassword = tbxEmPhone.Text,
                EmPhone = tbxEmPassword.Text,
            };
             //社員データの抽出
             employeeDsp = employeeDataAccess.GetEmployeeData(selectCondition);
        }

        //社員抽出結果表示
        private void SetSelectData()
        {
             dgvEm.DataSource = employeeDsp;
             dgvEm.Refresh();
        }

        private void cbxEmFlag_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbxEmFlag.SelectedIndex == 0)
            {
                //表示
                tbxEmHidden.Text = "";
                tbxEmHidden.Enabled = false;
            }
            else
            {
                //非表示
                tbxEmHidden.Enabled = true;
            }

            ////非表示表示フラグ
            //if (hihyouji)
            //{
            //    //自動入力のメッセージ対策
            //    if (dgvEm.SelectedRows.Count > 0)
            //    {
            //        if (EmUpdating) return;
            //        EmUpdating = true;

            //        //入力された社員情報を取得
            //        string emids = tbxEmID.Text;
            //        string soids = tbxSoID.Text;
            //        string poids = tbxPoID.Text;
            //        string emnama = tbxEmName.Text;
            //        string empss = tbxEmPassword.Text;
            //        string empns = tbxEmPhone.Text;
            //        int hideflgindex = cbxEmFlag.SelectedIndex;
            //        int hiflg;
            //        DateTime emdate = EmHiredate.Value;
            //        int soname = cbxSoName.SelectedIndex;
            //        int poname = cbxPoName.SelectedIndex;

            //        // フラグが未選択(-1)の場合は何も処理せずに戻る
            //        if (hideflgindex == -1)
            //        {
            //            EmUpdating = false;
            //            return;
            //        }

            //        try
            //        {
            //            // 空白チェ ック
            //            if (string.IsNullOrWhiteSpace(emids) ||
            //                string.IsNullOrWhiteSpace(soids) ||
            //                string.IsNullOrWhiteSpace(poids) ||
            //                string.IsNullOrWhiteSpace(poids) ||
            //                string.IsNullOrWhiteSpace(empss) ||
            //                string.IsNullOrWhiteSpace(empns) ||
            //                hideflgindex == 1 || clear != false)

            //            {
            //                MessageBox.Show("すべての項目を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                cbxEmFlag.SelectedIndex = -1;
            //                EmUpdating = false;
            //                return;
            //            }

            //            // 社員情報をint型に変換
            //            int emid = int.Parse(emids);
            //            int soid = int.Parse(soids);
            //            int poid = int.Parse(poids);
            //            int emps = int.Parse(empss);
            //            int empn = int.Parse(empns);


            //            //データベースで条件をチェック
            //            bool exists = employeeDataAccess.CheckDataE(emid, soid, poid, emps, empn, emdate);

            //            //フラグ変更処理
            //            if (exists)
            //            {
            //                int? emflg = employeeDataAccess.GetEmFlag(emid);
            //                if (hideflgindex == 1 && emflg == 0)
            //                {
            //                    //if (tbxEmHidden.Text != "")
            //                    //{
            //                    var result = MessageBox.Show(
            //                    "社員情報を非表示にしますか？",
            //                    "確認",
            //                    MessageBoxButtons.YesNo,
            //                    MessageBoxIcon.Question
            //                    );

            //                    // 「はい」が選択された場合のみ処理を実行
            //                    if (result == DialogResult.Yes)
            //                    {
            //                        string emhide = tbxEmHidden.Text;
            //                        // 更新する新しい値
            //                        int newflg = 2;

            //                        // データベースの値を更新
            //                        bool changeemflg = employeeDataAccess.ChangeEmhideflg(emid, newflg, emhide);

            //                        // 更新結果を通知
            //                        if (changeemflg)
            //                        {
            //                            MessageBox.Show("社員情報を非表示にしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        }
            //                        else
            //                        {
            //                            MessageBox.Show("社員の更新に失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                        }
            //                        //データグリッドビューをクリア
            //                        dgvEm.DataSource = null; // データソースを解除

            //                        // 列をクリアする場合（必要に応じて）
            //                        dgvEm.Columns.Clear();

            //                        // 行をクリアする場合（必要に応じて）
            //                        dgvEm.Rows.Clear();

            //                        EmUpdating = false;
            //                        return;
            //                    }
            //                    //「いいえ」が選択された時
            //                    else
            //                    {
            //                        cbxEmFlag.SelectedIndex = 0;
            //                        EmUpdating = false;
            //                        return;
            //                    }
            //                    //}
            //                    //else
            //                    //{
            //                    //    MessageBox.Show("非表示理由を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    //    cbxEmFlag.SelectedIndex = 0;
            //                    //    EmUpdating = false;
            //                    //    return;
            //                    //}
            //                }
            //                else if (hideflgindex == 0 && emflg == 2)
            //                {
            //                    var result = MessageBox.Show(
            //                    "社員情報を表示状態にしますか？" +
            //                    "(入力されている非表示理由は削除されます。)",
            //                    "確認",
            //                    MessageBoxButtons.YesNo,
            //                    MessageBoxIcon.Question
            //                    );

            //                    // 「はい」が選択された場合のみ処理を実行
            //                    if (result == DialogResult.Yes)
            //                    {
            //                        // 更新する新しい値
            //                        int newemflg = 0;
            //                        string emhide = "";
            //                        // データベースの値を更新
            //                        bool changechflg = employeeDataAccess.ChangeEmhideflg(emid, newemflg, emhide);

            //                        // 更新結果を通知
            //                        if (changechflg)
            //                        {
            //                            MessageBox.Show("社員情報を表示状態にしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        }
            //                        else
            //                        {
            //                            MessageBox.Show("情報の更新に失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                        }
            //                        EmUpdating = false;
            //                        return;
            //                    }
            //                    //「いいえ」が選択された時
            //                    else
            //                    {
            //                        cbxEmFlag.SelectedIndex = 1;
            //                        EmUpdating = false;
            //                        return;
            //                    }
            //                }
            //                else
            //                {
            //                    MessageBox.Show("エラーが発生しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    EmUpdating = false;
            //                    return;
            //                }
            //            }
            //            else
            //            {
            //                MessageBox.Show("該当するデータがありません。", "確認結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("すべての項目を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            cbxEmFlag.SelectedIndex = -1;
            //            EmUpdating = false;
            //            return;
            //        }
            //    }
            //}
        }


        private void btnHiddenList_Click(object sender, EventArgs e)
        {
            //非表示リスト
            //非表示データグリッドビューの表示
            SetFormHiddenDataGridView();

            //var employee = employeeDataAccess.GetEmployeeData();

            ////取得したデータをフィルタリング
            //var filteredEmployee = employeeDataAccess.GetEmployeeData(2);

            //// 取得したデータをDataGridViewに表示
            //if (filteredEmployee != null && filteredEmployee.Count > 0)
            //{
            //    dgvEm.DataSource = employee;

            //    // DataGridViewに表示されるヘッダー名を変更
            //    dgvEm.Columns["EmID"].HeaderText = "社員ID";
            //    dgvEm.Columns["EmName"].HeaderText = "社員名";
            //    dgvEm.Columns["SoID"].HeaderText = "営業所ID";
            //    dgvEm.Columns["PoID"].HeaderText = "役職ID";
            //    dgvEm.Columns["EmHiredate"].HeaderText = "入社年月日";
            //    dgvEm.Columns["EmPassword"].HeaderText = "社員パスワード";
            //    dgvEm.Columns["EmPhone"].HeaderText = "電話番号";
            //    dgvEm.Columns["EmFlag"].HeaderText = "表示/非表示";
            //    dgvEm.Columns["EmHidden"].HeaderText = "非表示理由";

            //    // 不要な列を非表示
            //    dgvEm.Columns["Po"].Visible = false;
            //    dgvEm.Columns["So"].Visible = false;
            //    dgvEm.Columns["TArrivals"].Visible = false;
            //    dgvEm.Columns["TChumons"].Visible = false;
            //    dgvEm.Columns["THattyus"].Visible = false;
            //    dgvEm.Columns["TOrders"].Visible = false;
            //    dgvEm.Columns["TSales"].Visible = false;
            //    dgvEm.Columns["TShipments"].Visible = false;
            //    dgvEm.Columns["TSyukkos"].Visible = false;
            //    dgvEm.Columns["TWarehousings"].Visible = false;

            //    //Flag(表示/非表示)の列とHidden(非表示理由)を非表示にする
            //    dgvEm.Columns["EmFlag"].Visible = false;
            //    dgvEm.Columns["EmHidden"].Visible = false;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvEm.ReadOnly = true;  // 編集不可
            //    dgvEm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //    dgvEm.MultiSelect = false;  // 複数行選択を無効
            //    dgvEm.AllowUserToAddRows = false;  // 行の追加を無効
            //    dgvEm.AllowUserToDeleteRows = false;  // 行の削除を無効
            //    dgvEm.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //    dgvEm.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvEm.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //}
            //else
            //{
            //    MessageBox.Show("表示するデータがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    // 列をクリアする場合（必要に応じて）
            //    dgvEm.Columns.Clear();

            //    // 行をクリアする場合（必要に応じて）
            //    dgvEm.Rows.Clear();
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //戻る機能
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "メニュー画面に戻りますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            if (result == DialogResult.Yes)
            {
                Menu menu = new Menu(); // 新しいフォームを表示する
                menu.Show();
                //Dispose();
                this.Hide(); // 現在のフォームを隠す
            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
        }


        private void btnClearInput_Click(object sender, EventArgs e)
        {
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

                ClearEm();

            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
            // MessageBox.Show("受注状態が選択されていません。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ClearEm()
        {

            // テキストボックスの内容をクリア
            tbxEmID.Text = "";                  //社員ID
            tbxEmName.Text = "";                 //社員Name
            tbxEmPhone.Text = "";                //社員電話番号
            tbxEmPassword.Text = "";            //社員パスワード
            tbxPoID.Text = "";                  //役職ID
            tbxSoID.Text = "";                  //営業所ID
            tbxEmHidden.Text = "";              // 非表示理由

            // コンボボックスの選択をクリア
            cbxEmFlag.SelectedIndex = -1;     //表示非表示
            cbxPoName.SelectedIndex = -1;     //役職名
            cbxSoName.SelectedIndex = -1;     //営業所名
            EmHiredate.ValueChanged += (s, e) =>
            {
                EmHiredate.Format = DateTimePickerFormat.Custom;
                EmHiredate.CustomFormat = "yyyy年MM月dd日";
            };
        }

        ///////////////////////////////
        //メソッド名：SetFormDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの設定
        ///////////////////////////////
        private void SetFormDataGridView()
        {
            // ユーザーが編集や削除をできないように設定
            dgvEm.ReadOnly = true;  // 編集不可
            dgvEm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            dgvEm.MultiSelect = false;  // 複数行選択を無効
            dgvEm.AllowUserToAddRows = false;  // 行の追加を無効
            dgvEm.AllowUserToDeleteRows = false;  // 行の削除を無効
            dgvEm.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            dgvEm.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            // 行番号の列や選択列のサイズ変更を無効にする
            dgvEm.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //ヘッダー位置の設定
            dgvEm.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //データグリッドビューのデータ取得
            GetDataGridView();
        }


        ///////////////////////////////
        //メソッド名：SetFormHiddenDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの設定(非表示一覧版)
        ///////////////////////////////
        private void SetFormHiddenDataGridView()
        {
            // ユーザーが編集や削除をできないように設定
            dgvEm.ReadOnly = true;  // 編集不可
            dgvEm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            dgvEm.MultiSelect = false;  // 複数行選択を無効
            dgvEm.AllowUserToAddRows = false;  // 行の追加を無効
            dgvEm.AllowUserToDeleteRows = false;  // 行の削除を無効
            dgvEm.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            dgvEm.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            // 行番号の列や選択列のサイズ変更を無効にする
            dgvEm.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //ヘッダー位置の設定
            dgvEm.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //データグリッドビューのデータ取得(非表示一覧版)
            GetHiddenDataGridView();
        }

        ///////////////////////////////
        //メソッド名：GetDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューに表示するデータの取得
        ///////////////////////////////
        private void GetDataGridView()
        {
            //商品データの取得
            employeeDsp = employeeDataAccess.GetEmployeeData();

            //dgv(DataGridView)に表示するデータを指定
            SetDataGridView();
        }

        ///////////////////////////////
        //メソッド名：GetHiddenDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューに表示するデータの取得
        //           (非表示一覧版)
        ///////////////////////////////
        private void GetHiddenDataGridView()
        {
            //顧客データの取得
            employeeDsp = employeeDataAccess.GetEmployeeHiddenData();

            //dgv(DataGridView)に表示するデータを指定
            SetDataGridView();
        }

        ///////////////////////////////
        //メソッド名：SetDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューへの表示
        ///////////////////////////////
        private void SetDataGridView()
        {

            dgvEm.DataSource = employeeDsp;

            dgvEm.Refresh();
        }

        private int EmFlgNum()
        {
            int flg;
            if (cbxEmFlag.SelectedIndex == 0)
            {
                //表示
                flg = 0;
            }
            else
            {
                //非表示
                flg = 2;
            }

            return flg;
        }

        private string EmHidden()
        {
            string hidden;
            if (cbxEmFlag.SelectedIndex == 0)
            {
                hidden = null;
            }
            else
            {
                hidden = tbxEmHidden.Text.Trim();
            }

            return hidden;
        }
      
    }
}












