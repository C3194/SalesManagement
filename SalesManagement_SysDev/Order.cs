using SalesManagement_SysDev.DateAccess;
using SalesManagement_SysDev.Common;
using SalesManagement_SysDev.新しいフォルダー;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 画面設計用9._0注文管理;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using SalesManagement_SysDev.DataAccess;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;



namespace SalesManagement_SysDev
{
    public partial class Order : Form
    {

        // フォーム初期化時にデータテーブルを準備
        DataTable dtOrders = new DataTable();      // 受注情報データテーブル
        DataTable dtOrderDetails = new DataTable(); // 受注詳細情報データテーブル

        //クラスのインスタンスを生成
        SalesOfficeDataAccess salesOfficeDataAccess = new SalesOfficeDataAccess();
        ClientDataAccess clientDataAccess = new ClientDataAccess();
        EmployeeDataAccess employeeDataAccess = new EmployeeDataAccess();
        ProductDataAccess productDataAccess = new ProductDataAccess();
        OrderDataAccess orderDataAccess = new OrderDataAccess();
        OrderDetailDataAccess orderDetailDataAccess = new OrderDetailDataAccess();
        ChumonDataAccess chumonDataAccess = new ChumonDataAccess();
        ChumonDetailDataAccess chumonDetailDataAccess = new ChumonDetailDataAccess();

        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の受注データ
        private static List<TOrderDsp> order;
        //データグリッドビュー用の受注詳細データ
        private static List<TOrderDetailDsp> orderDetail;
        //コンボボックス用の顧客データ
        private static List<MClient> client;
        //コンボボックス用の社員データ
        private static List<MEmployee> employee;
        //コンボボックス用の営業所データ
        private static List<MSalesOffice> salesOffice;
        //コンボボックス用の商品データ
        private static List<MProduct> product;

        //自動入力のループ回避変数
        private bool SoUpdating = false;
        private bool ClUpdating = false;
        private bool EmUpdating = false;
        private bool PrUpdating = false;
        private bool HdUpdating = false;
        private bool clear = false;
        //private bool isMessageBoxShown = false;


        public Order()
        {
            InitializeComponent();

            // データテーブルの列設定
            //InitializeOrderDataTable();
            //InitializeOrderDetailDataTable();

            // イベントを登録
            tbxOrPrice.TextChanged += tbxOrPrice_TextChanged;
            tbxOrQuantity.TextChanged += tbxOrQuantity_TextChanged;
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

                Menu menu = new Menu(); // 新しいフォームを表示する
                menu.Show();
                this.Hide(); // 現在のフォームを隠す

            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
        }

        private void Order_Load(object sender, EventArgs e)
        {
            //コンボボックスの設定
            SetFormComboBox();

            InitializeOrFlagComboBox();

            //データグリッドビューの表示
            SetFormDataGridView();

            //ClearOr();
            //ClearOrDetail();
        }

        // 受注情報のデータテーブルを初期化
        private void InitializeOrderDataTable()
        {
            dtOrders.Columns.Add("受注ID");
            dtOrders.Columns.Add("顧客ID");
            dtOrders.Columns.Add("社員ID");
            dtOrders.Columns.Add("営業所ID");
            dtOrders.Columns.Add("受注管理");
            dtOrders.Columns.Add("顧客担当者名");
            dtOrders.Columns.Add("顧客名");
            dtOrders.Columns.Add("社員名");
            dtOrders.Columns.Add("営業所名");
            dtOrders.Columns.Add("受注状態");
            dtOrders.Columns.Add("非表示理由");

            dgvOr.DataSource = dtOrders; // データグリッドビューにバインド
        }

        // 受注詳細情報のデータテーブルを初期化
        private void InitializeOrderDetailDataTable()
        {
            dtOrderDetails.Columns.Add("受注詳細ID");
            dtOrderDetails.Columns.Add("商品名");
            dtOrderDetails.Columns.Add("数量");
            dtOrderDetails.Columns.Add("商品ID");
            dtOrderDetails.Columns.Add("金額");
            dtOrderDetails.Columns.Add("合計金額");

            dgvOrDetail.DataSource = dtOrderDetails; // データグリッドビューにバインド
        }


        private void btnRegist_Click(object sender, EventArgs e)
        {
            //受注詳細入力欄に何も無い場合
            if (String.IsNullOrEmpty(tbxOrDetailID.Text.Trim())
                && String.IsNullOrEmpty(tbxOrID.Text.Trim()))
            {
                MessageBox.Show("[受注ID]又は[受注詳細ID]を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxOrID.Focus();
                return;
            }
            //受注登録//
            else if (String.IsNullOrEmpty(tbxOrDetailID.Text.Trim()))
            {
                //受注情報を得る//
                DialogResult result = MessageBox.Show("[受注データ]を登録しますか？",
                                                        "登録確認", MessageBoxButtons.OKCancel,
                                                                MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    //受注情報を得る//
                    if (!GetValidDataAtRegistration())
                    {
                        return;
                    }

                    //受注情報作成//
                    var regOrder = GenerateDataAtRegistration();
                    //受注情報重複チェック//
                    if (orderDataAccess.IsOrderIdExists(regOrder))
                    {
                        MessageBox.Show("同じ受注データがあります", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //受注詳細情報作成//
                    var regOrderDetail = GenerateDetailDataAtRegistration();

                    //受注情報登録//
                    RegistrationOrder(regOrder, regOrderDetail);
                }
            }
            //受注詳細だけ登録//
            else if (String.IsNullOrEmpty(tbxOrID.Text.Trim()))
            {
                DialogResult result = MessageBox.Show("[受注詳細データ]を登録しますか？",
                                                    "登録確認", MessageBoxButtons.OKCancel,
                                                            MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    //受注詳細情報のみを得る//
                    if (!GetValidDetailDataAtRegistration())
                    {
                        return;
                    }

                    //受注詳細情報のみ作成//
                    var regOrderDetail = GenerateDetailDataAtRegistrationOnly();

                    //受注詳細情報のみ登録//
                    RegistrationOrderDetail(regOrderDetail);
                }
            }

            //    // データを取得
            //    var Order = OrderDataAccess.GetOrderData();

            //    //取得したデータをフィルタリング
            //    var filteredOrder = OrderDataAccess.GetOrderData(0);

            //    // --- 変更点: 受注IDと顧客担当者名の必須チェックを削除 ---

            //    // データ取得処理や準備

            //    try
            //    {
            //        // 受注情報の入力値を取得
            //        int? orId = string.IsNullOrWhiteSpace(tbxOrID.Text) ? (int?)null : int.Parse(tbxOrID.Text);
            //        int? soId = string.IsNullOrWhiteSpace(tbxSoID.Text) ? (int?)null : int.Parse(tbxSoID.Text);
            //        int? emId = string.IsNullOrWhiteSpace(tbxEmID.Text) ? (int?)null : int.Parse(tbxEmID.Text);
            //        int? clId = string.IsNullOrWhiteSpace(tbxClID.Text) ? (int?)null : int.Parse(tbxClID.Text);
            //        DateTime? orDate = dtpOrDate.Checked ? dtpOrDate.Value.Date : (DateTime?)null;

            //        int orStateFlag = cbxOrStateFlag.SelectedIndex;

            //        if (orStateFlag == 0)
            //        {
            //            orStateFlag = 1; // 通常
            //        }
            //        else if (orStateFlag == 1)
            //        {
            //            orStateFlag = 2; // 非表示
            //        }

            //        if (soId == null || emId == null || clId == null || orDate == null || orStateFlag == 0)
            //        {
            //            MessageBox.Show("必須項目を全て入力してください。（営業所ID、社員ID、顧客ID、受注日）", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }

            //        bool isExist = orderDataAccess.CheckData(orId, soId, emId, clId, orDate);

            //        if (isExist)
            //        {
            //            MessageBox.Show("既に存在している受注データと重複している項目があります", "登録エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            return;
            //        }

            //        // 新しい受注データを登録
            //        var registeredOrder = orderDataAccess.RegisterOrData(
            //            orId,
            //            soId.Value,
            //            emId.Value,
            //            clId.Value,
            //            orDate.Value,
            //            orStateFlag,

            //        );

            //        if (registeredOrder != null)
            //        {
            //            // 登録されたデータを表示
            //            dgvOr.DataSource = new List<TOrder> { registeredOrder }; // 登録されたデータのみを表示

            //            // DataGridViewの設定
            //            dgvOr.Columns["OrID"].HeaderText = "受注ID";
            //            dgvOr.Columns["SoID"].HeaderText = "営業所ID";
            //            dgvOr.Columns["EmID"].HeaderText = "社員ID";
            //            dgvOr.Columns["ClID"].HeaderText = "顧客ID";
            //            dgvOr.Columns["OrDate"].HeaderText = "受注日";
            //            dgvOr.Columns["OrStateFlag"].HeaderText = "受注状態";
            //            dgvOr.Columns["OrHidden"].HeaderText = "非表示理由";

            //            // 不要な列を非表示
            //            dgvOr.Columns["Em"].Visible = false;
            //            dgvOr.Columns["So"].Visible = false;
            //            dgvOr.Columns["Cl"].Visible = false;
            //            dgvOr.Columns["TOrderDetails"].Visible = false;

            //            // 編集や削除をできないように設定
            //            dgvOr.ReadOnly = true;
            //            dgvOr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //            dgvOr.MultiSelect = false;
            //            dgvOr.AllowUserToAddRows = false;
            //            dgvOr.AllowUserToDeleteRows = false;
            //        }
            //        else
            //        {
            //            MessageBox.Show("受注情報の登録に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"登録中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }

            //}

            //// データグリッドビューに受注情報を表示
            //private void UpdateOrderGridView()
            //{
            //    try
            //    {
            //        using (var context = new SalesManagementContext())
            //        {
            //            // TOrdersテーブルのデータを取得
            //            var orders = context.TOrders
            //                .Select(o => new
            //                {
            //                    o.OrId,          // 受注ID
            //                    o.SoId,          // 営業所ID
            //                    o.EmId,          // 社員ID
            //                    o.ClId,          // 顧客ID
            //                    o.ClCharge,      // 顧客担当者名
            //                    OrDate = o.OrDate.ToString("yyyy/MM/dd"), // 受注日
            //                    OrState = o.OrStateFlag == 0 ? "確定" : "未確定", // 状態フラグを文字列に変換
            //                    o.OrFlag,        // 受注フラグ
            //                    o.OrHidden       // 非表示理由
            //                })
            //                .ToList();

            //            dgvOr.DataSource = orders;

            //            // 列ヘッダーの設定
            //            dgvOr.Columns["OrId"].HeaderText = "受注ID";
            //            dgvOr.Columns["SoId"].HeaderText = "営業所ID";
            //            dgvOr.Columns["EmId"].HeaderText = "社員ID";
            //            dgvOr.Columns["ClId"].HeaderText = "顧客ID";
            //            dgvOr.Columns["ClCharge"].HeaderText = "顧客担当者名";
            //            dgvOr.Columns["OrDate"].HeaderText = "受注日";
            //            dgvOr.Columns["OrState"].HeaderText = "受注状態"; // 状態フラグを表示
            //            dgvOr.Columns["OrFlag"].HeaderText = "受注フラグ";
            //            dgvOr.Columns["OrHidden"].HeaderText = "非表示理由";

            //            // 列幅を自動調整
            //            dgvOr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"受注情報の取得中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
        }

        //受注情報を得る//
        private bool GetValidDataAtRegistration()
        {
            ///受注情報///
            //顧客担当者名
            //50文字以内
            if (!String.IsNullOrEmpty(tbxClCharge.Text.Trim()))
            {
                if (tbxClCharge.Text.Length > 50)
                {
                    MessageBox.Show("顧客担当者名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClCharge.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("顧客担当者名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClCharge.Focus();
                return false;
            }

            //顧客ID
            if (!String.IsNullOrEmpty(tbxClID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxClID.Text.Trim()))
                {
                    MessageBox.Show("顧客IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClID.Focus();
                    return false;
                }
                //顧客IDの存在チェック
                if (!clientDataAccess.CheckClientIDExistence(int.Parse(tbxClID.Text.Trim())))
                {
                    MessageBox.Show("入力された顧客IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("顧客IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClID.Focus();
                return false;
            }

            //社員ID
            if (!String.IsNullOrEmpty(tbxEmID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxEmID.Text.Trim()))
                {
                    MessageBox.Show("社員IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmID.Focus();
                    return false;
                }
                //社員IDの存在チェック
                if (!employeeDataAccess.CheckEmployeeIDExistence(int.Parse(tbxEmID.Text.Trim())))
                {
                    MessageBox.Show("入力された社員IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("社員IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxEmID.Focus();
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
                if (!salesOfficeDataAccess.CheckSalesOfficeIDExistence(int.Parse(tbxSoID.Text.Trim())))
                {
                    MessageBox.Show("入力された営業所IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxSoID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("営業所IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxSoID.Focus();
                return false;
            }

            //受注年月日

            ///受注詳細情報///
            //商品ID
            if (!String.IsNullOrEmpty(tbxPrID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrID.Text.Trim()))
                {
                    MessageBox.Show("商品IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrID.Focus();
                    return false;
                }
                //営業所IDの存在チェック
                if (!productDataAccess.CheckProductIDExistence(int.Parse(tbxPrID.Text.Trim())))
                {
                    MessageBox.Show("入力された商品IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("商品IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrID.Focus();
                return false;
            }

            //数量
            //４桁以内
            if (!String.IsNullOrEmpty(tbxOrQuantity.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxOrQuantity.Text.Trim()))
                {
                    MessageBox.Show("数量は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrQuantity.Focus();
                    return false;
                }
                //桁数チェック
                //4桁
                if (tbxOrQuantity.Text.Length > 4)
                {
                    MessageBox.Show("数量は4桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrQuantity.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("数量を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxOrQuantity.Focus();
                return false;
            }

            return true;
        }
        //受注詳細情報のみを得る//
        private bool GetValidDetailDataAtRegistration()
        {
            ///受注詳細情報///
            //受注ID
            if (!String.IsNullOrEmpty(tbxOrID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxOrID.Text.Trim()))
                {
                    MessageBox.Show("受注IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrID.Focus();
                    return false;
                }
                //6桁以内
                if (tbxOrID.Text.Length > 6)
                {
                    MessageBox.Show("受注IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrID.Focus();
                    return false;
                }
                //存在チェック
                if (!orderDataAccess.CheckOrderIDExistence(int.Parse(tbxOrID.Text.Trim())))
                {
                    MessageBox.Show("入力された受注IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("受注IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxOrID.Focus();
                return false;
            }

            //商品ID
            if (!String.IsNullOrEmpty(tbxPrID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrID.Text.Trim()))
                {
                    MessageBox.Show("商品IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrID.Focus();
                    return false;
                }
                //商品IDの存在チェック
                if (!productDataAccess.CheckProductIDExistence(int.Parse(tbxPrID.Text.Trim())))
                {
                    MessageBox.Show("入力された商品IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("商品IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrID.Focus();
                return false;
            }

            //数量
            //４桁以内
            if (!String.IsNullOrEmpty(tbxOrQuantity.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxOrQuantity.Text.Trim()))
                {
                    MessageBox.Show("数量は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrQuantity.Focus();
                    return false;
                }
                //桁数チェック
                //4桁
                if (tbxOrQuantity.Text.Length > 4)
                {
                    MessageBox.Show("数量は4桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrQuantity.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("数量を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxOrQuantity.Focus();
                return false;
            }

            return true;
        }

        //受注情報作成//
        private TOrder GenerateDataAtRegistration()
        {
            return new TOrder
            {
                ClCharge = tbxClCharge.Text.Trim(),
                ClId = int.Parse(tbxClID.Text.Trim()),
                EmId = int.Parse(tbxEmID.Text.Trim()),
                SoId = int.Parse(tbxSoID.Text.Trim()),
                OrDate = dtpOrDate.Value,
                OrStateFlag = 0,
                OrFlag = 0,
                OrHidden = null
            };
        }
        //受注詳細情報作成//
        private TOrderDetail GenerateDetailDataAtRegistration()
        {
            return new TOrderDetail
            {
                OrId = orderDataAccess.GetOrderId() + 1,
                PrId = int.Parse(tbxPrID.Text.Trim()),
                OrQuantity = int.Parse(tbxOrQuantity.Text.Trim()),
                OrTotalPrice = int.Parse(tbxTotalPrice.Text.Trim()),
            };
        }
        //受注詳細情報のみ作成//
        private TOrderDetail GenerateDetailDataAtRegistrationOnly()
        {
            return new TOrderDetail
            {
                OrId = int.Parse(tbxOrID.Text.Trim()),
                PrId = int.Parse(tbxPrID.Text.Trim()),
                OrQuantity = int.Parse(tbxOrQuantity.Text.Trim()),
                OrTotalPrice = int.Parse(tbxTotalPrice.Text.Trim()),
            };
        }

        //受注情報登録
        private void RegistrationOrder(TOrder regOrder, TOrderDetail regOrderDetail)
        {
            //登録確認メッセージ
            //DialogResult result = MessageBox.Show("受注データを登録します\rよろしいですか？",
            //                                        "登録確認", MessageBoxButtons.OKCancel,
            //                                              MessageBoxIcon.Question);
            //if (result == DialogResult.Cancel)
            //{
            //    return;
            //}

            //受注情報の登録
            bool flgOrder = orderDataAccess.AddOrderData(regOrder);
            bool flgDetail = orderDetailDataAccess.AddOrderDetailData(regOrderDetail);
            if (flgOrder == true && flgDetail == true)
            {
                MessageBox.Show("データの登録に成功しました", "登録成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearOr();
                ClearOrDetail();
            }
            else
            {
                MessageBox.Show("データの登録に失敗しました", "登録失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            GetDataGridView();
        }
        //受注詳細情報のみ登録//
        private void RegistrationOrderDetail(TOrderDetail regOrderDetail)
        {
            //登録確認メッセージ
            //DialogResult result = MessageBox.Show("受注詳細データを登録します\rよろしいですか？",
            //                                        "登録確認", MessageBoxButtons.OKCancel,
            //                                              MessageBoxIcon.Question);
            //if (result == DialogResult.Cancel)
            //{
            //    return;
            //}

            //受注詳細情報の登録
            bool flgDetail = orderDetailDataAccess.AddOrderDetailData(regOrderDetail);

            if (flgDetail == true)
            {
                MessageBox.Show("データの登録に成功しました", "登録成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearOr();
                ClearOrDetail();
            }
            else
            {
                MessageBox.Show("データの登録に失敗しました", "登録失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            GetDataGridView();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //受注詳細入力欄に何も無い場合
            if (String.IsNullOrEmpty(tbxOrDetailID.Text.Trim())
                && String.IsNullOrEmpty(tbxOrID.Text.Trim()))
            {
                MessageBox.Show("[受注ID]又は[受注詳細ID]を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxOrID.Focus();
                return;
            }
            //両方更新//
            else if (!String.IsNullOrEmpty(tbxOrDetailID.Text.Trim())
                && !String.IsNullOrEmpty(tbxOrID.Text.Trim()))
            {
                DialogResult result = MessageBox.Show("[受注データ],[受注詳細データ]を更新しますか？",
                                                   "更新確認", MessageBoxButtons.OKCancel,
                                                           MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    //受注情報を得る//
                    if (!GetValidDataAtUpdate())
                    {
                        return;
                    }
                    //受注詳細を得る//
                    if (!GetValidDetailDataAtUpdate())
                    {
                        return;
                    }
                    //更新するデータの受注状態が「確定の場合」
                    var OrderStateflg = orderDataAccess.GetOrderConfirmData(tbxOrID.Text.Trim());
                    if (OrderStateflg == 1)
                    {
                        MessageBox.Show("確定された受注データは変更出来ません", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //更新するデータの受注状態が「未確定の場合」
                    else
                    {
                        //確定
                        if(cbxOrStateFlag.SelectedIndex == 1)
                        {
                            MessageBox.Show("受注,受注詳細同時に更新する場合" +
                                            "\n確定することは出来ません", "選択エラー", 
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        //普通の更新
                        else
                        {
                            //受注情報作成//
                            var updOrder = GenerateDataAtUpdate();
                            //受注詳細情報作成//
                            var updOrderDetail = GenerateDetailDataAtUpdate();
                            //両方変更されていないデータの場合//
                            if (orderDataAccess.IsOrderIdExists(updOrder))
                            {
                                MessageBox.Show("同じ受注データがあります", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else if(orderDetailDataAccess.IsOrderDetailIdExists(updOrderDetail))
                            {
                                MessageBox.Show("同じ受注詳細データがあります", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            //受注情報更新//
                            UpdateOrder(updOrder);
                            //受注詳細情報更新//
                            UpdateOrderDetail(updOrderDetail);
                        }
                    }
                }
            }
            //受注だけ更新//
            else if (String.IsNullOrEmpty(tbxOrDetailID.Text.Trim()))
            {
                DialogResult result = MessageBox.Show("[受注データ]を更新しますか？",
                                                    "更新確認", MessageBoxButtons.OKCancel,
                                                            MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    //受注情報を得る//
                    if (!GetValidDataAtUpdate())
                    {
                        return;
                    }

                    //更新するデータの受注状態が「確定の場合」
                    var OrderStateflg = orderDataAccess.GetOrderConfirmData(tbxOrID.Text.Trim());
                    if (OrderStateflg == 1)
                    {
                        MessageBox.Show("確定された受注データは変更出来ません", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //更新するデータの受注状態が「未確定の場合」
                    else
                    {
                        //確定
                        if (cbxOrStateFlag.SelectedIndex == 1)
                        {
                            DialogResult resultConfirm = MessageBox.Show("[受注データ]を確定しますか？" +
                                                    "\n※確定したデータは更新出来なくなります",
                                                   "確定確認", MessageBoxButtons.OKCancel,
                                                           MessageBoxIcon.Question);
                            if (resultConfirm == DialogResult.Cancel)
                            {
                                return;
                            }

                            //受注情報作成//
                            var updOrder = GenerateDataAtUpdate();
                            //受注詳細情報作成//
                            ////更新されていない変更されているデータの場合//
                            //if (!orderDataAccess.IsOrderIdExists(updOrder))
                            //{
                            //    MessageBox.Show("変更された受注データです" +
                            //                    "\n更新してからやり直してください", "入力エラー", 
                            //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    return;
                            //}

                            //注文情報作成//
                            var regChumon = GenerateChumonDataAtRegistration();
                            //注文詳細情報作成//
                            var regChumonDetail = GenerateChumonDetailDataAtRegistration();
                            //受注確定//
                            ConfirmOrder(updOrder, regChumon, regChumonDetail);
                        }
                        //普通の更新
                        else
                        {
                            //受注情報作成//
                            var updOrder = GenerateDataAtUpdate();
                            //受注詳細情報作成//
                            //変更されていないデータの場合//
                            if (orderDataAccess.IsOrderIdExists(updOrder))
                            {
                                MessageBox.Show("変更されていない受注データです", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            //受注情報更新//
                            UpdateOrder(updOrder);
                        }
                    }
                }
            }
            //受注詳細だけ更新//
            else if (String.IsNullOrEmpty(tbxOrID.Text.Trim()))
            {
                DialogResult result = MessageBox.Show("[受注詳細データ]を更新しますか？",
                                                    "更新確認", MessageBoxButtons.OKCancel,
                                                            MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    //受注詳細を得る//
                    if (!GetValidDetailDataAtUpdate())
                    {
                        return;
                    }

                    //受注詳細情報作成//
                    var updOrderDetail = GenerateDetailDataAtUpdate();
                    //変更されていないデータの場合//
                    if (orderDetailDataAccess.IsOrderDetailIdExists(updOrderDetail))
                    {
                        MessageBox.Show("変更されていない受注詳細データです", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    //受注詳細情報更新//
                    UpdateOrderDetail(updOrderDetail);
                }
            }
            //受注情報を得る//
            //if (!GetValidDataAtUpdate())
            //{
            //    return;
            //}

            //受注情報作成//
            //var updOrder = GenerateDataAtUpdate();

            //受注情報更新//



            //// DataGridViewで選択された受注を確認
            //if (dgvOr.SelectedRows.Count > 0)
            //{
            //    //選択されたデータ取得//
            //    var selectedRow = dgvOr.SelectedRows[0];
            //    var selectedRowDetail = dgvOrDetail.SelectedRows[0];

            //    if (selectedRow.Cells["OrStateFlag"].Value.ToString() == "1")
            //    {
            //        MessageBox.Show("既に確定されたデータです", "確定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //    //受注情報作成//
            //    TOrder conOrder = new TOrder
            //    {
            //        OrId = int.Parse(selectedRow.Cells["OrId"].Value.ToString()),
            //        ClCharge = selectedRow.Cells["ClCharge"].Value.ToString(),
            //        ClId = int.Parse(selectedRow.Cells["ClId"].Value.ToString()),
            //        EmId = int.Parse(selectedRow.Cells["EmId"].Value.ToString()),
            //        SoId = int.Parse(selectedRow.Cells["SoId"].Value.ToString()),
            //        OrDate = DateTime.Parse(selectedRow.Cells["OrDate"].Value.ToString()),
            //        OrStateFlag = 1,
            //        OrFlag = int.Parse(selectedRow.Cells["OrFlag"].Value.ToString()),
            //        OrHidden = OrHiddenConfirm(),
            //    };
            //    //受注詳細情報作成//
            //    TOrderDetail conOrderDetail = new TOrderDetail
            //    {
            //        OrDetailId = int.Parse(selectedRowDetail.Cells["OrDetailId"].Value.ToString()),
            //        OrId = int.Parse(selectedRowDetail.Cells["OrId"].Value.ToString()),
            //        PrId = int.Parse(selectedRowDetail.Cells["PrId"].Value.ToString()),
            //        OrQuantity = int.Parse(selectedRowDetail.Cells["OrQuantity"].Value.ToString()),
            //        OrTotalPrice = int.Parse(selectedRowDetail.Cells["OrTotalPrice"].Value.ToString())
            //    };
            //    //注文情報作成//
            //    var regChumon = GenerateChumonDataAtRegistration(conOrder);
            //    //注文詳細情報作成//
            //    var regChumonDetail = GenerateChumonDetailDataAtRegistration(conOrderDetail);

            //    //受注情報確定
            //    ConfirmOrder(conOrder, regChumon, regChumonDetail);

            //    //// 選択された受注のIDを取得
            //    //int selectedOrderId = Convert.ToInt32(dgvOr.SelectedRows[0].Cells["OrID"].Value);

            //    //// 受注状態フラグを更新
            //    //bool isUpdated = UpdateOrderStateFlag(selectedOrderId, 1); // フラグを1に変更

            //    //// 更新結果をユーザーに通知
            //    //if (isUpdated)
            //    //{
            //    //    MessageBox.Show("受注が確定されました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    //    // DataGridViewをリフレッシュする（データの再読み込みなど）
            //    //    RefreshOrderList();
            //    //}
            //    //else
            //    //{
            //    //    MessageBox.Show("受注の確定に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //}
            //}
            //else
            //{
            //    MessageBox.Show("受注を選択してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
        }

        //受注情報を得る//
        private bool GetValidDataAtUpdate()
        {
            ///受注情報///
            //受注ID
            if (!String.IsNullOrEmpty(tbxOrID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxOrID.Text.Trim()))
                {
                    MessageBox.Show("受注IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrID.Focus();
                    return false;
                }
                //6桁以内
                if (tbxOrID.Text.Length > 6)
                {
                    MessageBox.Show("受注IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("受注IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxOrID.Focus();
                return false;
            }

            //顧客担当者名
            //50文字以内
            if (!String.IsNullOrEmpty(tbxClCharge.Text.Trim()))
            {
                if (tbxClCharge.Text.Length > 50)
                {
                    MessageBox.Show("顧客担当者名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClCharge.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("顧客担当者名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClCharge.Focus();
                return false;
            }

            //顧客ID
            if (!String.IsNullOrEmpty(tbxClID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxClID.Text.Trim()))
                {
                    MessageBox.Show("顧客IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClID.Focus();
                    return false;
                }
                //顧客IDの存在チェック
                if (!clientDataAccess.CheckClientIDExistence(int.Parse(tbxClID.Text.Trim())))
                {
                    MessageBox.Show("入力された顧客IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("顧客IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClID.Focus();
                return false;
            }

            //社員ID
            if (!String.IsNullOrEmpty(tbxEmID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxEmID.Text.Trim()))
                {
                    MessageBox.Show("社員IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmID.Focus();
                    return false;
                }
                //営業所IDの存在チェック
                if (!employeeDataAccess.CheckEmployeeIDExistence(int.Parse(tbxEmID.Text.Trim())))
                {
                    MessageBox.Show("入力された社員IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxEmID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("社員IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxEmID.Focus();
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
                if (!salesOfficeDataAccess.CheckSalesOfficeIDExistence(int.Parse(tbxSoID.Text.Trim())))
                {
                    MessageBox.Show("入力された営業所IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxSoID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("営業所IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxSoID.Focus();
                return false;
            }

            //受注年月日

            return true;
        }
        //受注詳細のみを得る//
        private bool GetValidDetailDataAtUpdate()
        {
            ///受注詳細情報///
            //受注詳細ID
            if (!String.IsNullOrEmpty(tbxOrDetailID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxOrDetailID.Text.Trim()))
                {
                    MessageBox.Show("受注詳細IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrDetailID.Focus();
                    return false;
                }
                //6桁以内
                if (tbxOrDetailID.TextLength > 6)
                {
                    MessageBox.Show("受注詳細IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrDetailID.Focus();
                    return false;
                }
                //受注詳細IDの存在チェック
                if (!orderDetailDataAccess.CheckOrderDetailIDExistence(int.Parse(tbxOrDetailID.Text.Trim())))
                {
                    MessageBox.Show("入力された受注詳細IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrDetailID.Focus();
                    return false;
                }
            }

            //受注ID
            if (!String.IsNullOrEmpty(tbxOrID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxOrID.Text.Trim()))
                {
                    MessageBox.Show("受注IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrID.Focus();
                    return false;
                }
                //6桁以内
                if (tbxOrID.Text.Length > 6)
                {
                    MessageBox.Show("受注IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrID.Focus();
                    return false;
                }
                //受注詳細IDの存在チェック
                if (!orderDataAccess.CheckOrderIDExistence(int.Parse(tbxOrID.Text.Trim())))
                {
                    MessageBox.Show("入力された受注詳細IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("受注IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxOrID.Focus();
                return false;
            }

            //商品ID
            if (!String.IsNullOrEmpty(tbxPrID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrID.Text.Trim()))
                {
                    MessageBox.Show("商品IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrID.Focus();
                    return false;
                }
                //商品IDの存在チェック
                if (!productDataAccess.CheckProductIDExistence(int.Parse(tbxPrID.Text.Trim())))
                {
                    MessageBox.Show("入力された商品IDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrID.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("商品IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrID.Focus();
                return false;
            }

            //数量
            //４桁以内
            if (!String.IsNullOrEmpty(tbxOrQuantity.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxOrQuantity.Text.Trim()))
                {
                    MessageBox.Show("数量は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrQuantity.Focus();
                    return false;
                }
                //桁数チェック
                //4桁
                if (tbxOrQuantity.Text.Length > 4)
                {
                    MessageBox.Show("数量は4桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrQuantity.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("数量を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxOrQuantity.Focus();
                return false;
            }

            return true;
        }
        //受注情報作成//
        private TOrder GenerateDataAtUpdate()
        {
            //更新データのセット
            return new TOrder
            {
                OrId = int.Parse(tbxOrID.Text.Trim()),
                ClCharge = tbxClCharge.Text.Trim(),
                ClId = int.Parse(tbxClID.Text.Trim()),
                EmId = int.Parse(tbxEmID.Text.Trim()),
                SoId = int.Parse(tbxSoID.Text.Trim()),
                OrDate = dtpOrDate.Value,
                OrStateFlag = cbxOrStateFlag.SelectedIndex,
                OrFlag = OrFlgNum(),
                OrHidden = OrHidden(),
            };
        }
        //受注済み受注情報作成//
        //private TOrder GenerateConfirmedDataAtUpdate()
        //{
        //    //更新データのセット
        //    return new TOrder
        //    {
        //        OrId = int.Parse(tbxOrID.Text.Trim()),
        //        ClCharge = tbxClCharge.Text.Trim(),
        //        ClId = int.Parse(tbxClID.Text.Trim()),
        //        EmId = int.Parse(tbxEmID.Text.Trim()),
        //        SoId = int.Parse(tbxSoID.Text.Trim()),
        //        OrDate = dtpOrDate.Value,
        //        OrStateFlag = 1,
        //        OrFlag = OrFlgNum(),
        //        OrHidden = OrHidden(),
        //    };
        //}
        //受注詳細情報作成//
        private TOrderDetail GenerateDetailDataAtUpdate()
        {
            //更新データのセット
            return new TOrderDetail
            {
                OrDetailId = int.Parse(tbxOrDetailID.Text.Trim()),
                OrId = int.Parse(tbxOrID.Text.Trim()),
                PrId = int.Parse(tbxPrID.Text.Trim()),
                OrQuantity = int.Parse(tbxOrQuantity.Text.Trim()),
                OrTotalPrice = int.Parse(tbxTotalPrice.Text.Trim()),
            };
        }
        //受注情報更新//
        private void UpdateOrder(TOrder updOrder)
        {
            //受注情報の更新
            bool flgOrder = orderDataAccess.UpdateOrderData(updOrder);
            if (flgOrder == true)
            {
                MessageBox.Show("データの更新に成功しました", "更新成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearOr();
                ClearOrDetail();
            }
            else
            {
                MessageBox.Show("データの更新に失敗しました", "更新失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            GetDataGridView();
        }
        //受注詳細情報更新//
        private void UpdateOrderDetail(TOrderDetail updOrderDetail)
        {
            //受注詳細情報の更新
            bool flgOrder = orderDetailDataAccess.UpdateOrderDetailData(updOrderDetail);
            if (flgOrder == true)
            {
                MessageBox.Show("データの更新に成功しました", "更新成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearOr();
                ClearOrDetail();
            }
            else
            {
                MessageBox.Show("データの更新に失敗しました", "更新失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            GetDataGridView();
        }

        //注文情報作成//
        private TChumon GenerateChumonDataAtRegistration()
        {
            return new TChumon
            {
                SoId = int.Parse(tbxSoID.Text.Trim()),
                EmId = int.Parse(tbxEmID.Text.Trim()),
                ClId = int.Parse(tbxClID.Text.Trim()),
                OrId = int.Parse(tbxOrID.Text.Trim()),
                ChDate = DateTime.Now,
                ChStateFlag = 0,
                ChFlag = 0,
                ChHidden = null
            };
        }
        //注文詳細情報作成//
        private List<TChumonDetail> GenerateChumonDetailDataAtRegistration()
        {
            //OrIDが一致する受注詳細データを全選択
            var orderDetailData = orderDetailDataAccess.GetOrderDetailData(tbxOrID.Text.Trim());
            //注文詳細情報作成//
            List<TChumonDetail> regChumonDetail = new List<TChumonDetail>();
            foreach (var p in orderDetailData)
            {
                regChumonDetail.Add(new TChumonDetail()
                {
                    ChId = chumonDataAccess.GetChumonId() + 1,
                    PrId = p.PrId,
                    ChQuantity = p.OrQuantity
                });
            }

            return regChumonDetail;

            //return new TChumonDetail
            //{
            //    ChId = chumonDataAccess.GetChumonId(),
            //    PrId = conOrderDetail.PrId,
            //    ChQuantity = conOrderDetail.OrQuantity
            //};
        }
        //受注確定//
        private void ConfirmOrder(TOrder updOrder, TChumon regChumon, List<TChumonDetail> regChumonDetail)
        {
            bool Orflg = orderDataAccess.UpdateOrderData(updOrder);
            bool Chflg = chumonDataAccess.AddChumonData(regChumon);
            bool ChDeflg = chumonDetailDataAccess.AddChumonDetailData(regChumonDetail);
            if (Orflg == true && Chflg == true && ChDeflg == true)
            {
                MessageBox.Show("データの確定に成功しました", "確定成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearOr();
                ClearOrDetail();
            }
            else
            {
                MessageBox.Show("データの確定に失敗しました", "確定失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            GetDataGridView();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            //データグリッドビューの表示
            SetFormDataGridView();


            //// データを取得
            //var orderhyoji = OrderDataAccess.GetOrderData();

            ////取得したデータをフィルタリング
            //var filteredOrder = OrderDataAccess.GetOrderData(0);

            //// 取得したデータをDataGridViewに表示
            //if (filteredOrder != null && filteredOrder.Count > 0)
            //{
            //    dgvOr.DataSource = orderhyoji;

            //    // データグリッドビューの列ヘッダー名を変更
            //    dgvOr.Columns["OrID"].HeaderText = "受注ID";
            //    dgvOr.Columns["SoID"].HeaderText = "営業所ID";
            //    dgvOr.Columns["EmID"].HeaderText = "社員ID";
            //    dgvOr.Columns["ClID"].HeaderText = "顧客ID";
            //    dgvOr.Columns["ClCharge"].HeaderText = "顧客担当者名"; // 顧客名の列を追加
            //    dgvOr.Columns["OrDate"].HeaderText = "受注年月日";
            //    dgvOr.Columns["OrStateFlag"].HeaderText = "受注状態";
            //    // dgvCh.Columns["ChFlag"].HeaderText = "表示/非表示";
            //    dgvOr.Columns["OrHidden"].HeaderText = "非表示理由";


            //    //ChFlag(表示/非表示)の列を非表示にする
            //    dgvOr.Columns["OrFlag"].Visible = false;

            //    // 列幅を設定
            //    dgvOr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //    dgvOr.Columns["OrHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //    dgvOr.Columns["OrHidden"].Width = 500;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvOr.ReadOnly = true;  // 編集不可
            //    dgvOr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //    dgvOr.MultiSelect = false;  // 複数行選択を無効
            //    dgvOr.AllowUserToAddRows = false;  // 行の追加を無効
            //    dgvOr.AllowUserToDeleteRows = false;  // 行の削除を無効
            //    dgvOr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //    dgvOr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvOr.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //}
            //else
            //{
            //    MessageBox.Show("表示する受注情報がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dgvOr.DataSource = null; // データソースを解除

            //    // 列をクリアする場合（必要に応じて）
            //    dgvOr.Columns.Clear();

            //    // 行をクリアする場合（必要に応じて）
            //    dgvOr.Rows.Clear();
            //}

            //var Ordetails = orderDetailDataAccess.GetOrderDetails(0);

            //if (Ordetails != null && Ordetails.Count > 0)
            //{
            //    // 2つ目のデータグリッドビューにデータを設定
            //    dgvOrDetail.DataSource = Ordetails;

            //    // 列ヘッダーの設定
            //    dgvOrDetail.Columns["OrDetailID"].HeaderText = "受注詳細ID";
            //    dgvOrDetail.Columns["OrID"].HeaderText = "受注ID";
            //    dgvOrDetail.Columns["PrID"].HeaderText = "商品ID";
            //    dgvOrDetail.Columns["OrQuantity"].HeaderText = "数量";
            //    dgvOrDetail.Columns["OrTotalPrice"].HeaderText = "合計金額"; // 合計金額を追加



            //    // 列幅を設定
            //    dgvOrDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //    dgvOrDetail.Columns["OrQuantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvOrDetail.ReadOnly = true;  // 編集不可
            //    dgvOrDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //    dgvOrDetail.MultiSelect = false;  // 複数行選択を無効
            //    dgvOrDetail.AllowUserToAddRows = false;  // 行の追加を無効
            //    dgvOrDetail.AllowUserToDeleteRows = false;  // 行の削除を無効
            //    dgvOrDetail.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //    dgvOrDetail.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvOrDetail.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //}
            //else
            //{
            //    MessageBox.Show("表示する受注詳細情報がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dgvOrDetail.DataSource = null; // データソースを解除

            //    // 列をクリアする場合（必要に応じて）
            //    dgvOrDetail.Columns.Clear();

            //    // 行をクリアする場合（必要に応じて）
            //    dgvOrDetail.Rows.Clear();
            //}
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //受注情報を得る
            if (!GetValidDataAtSlect())
            {
                return;
            }

            //受注情報抽出
            GenerateDataAtSelect();

            //受注抽出結果表示
            SetSelectData();

            //// 受注情報の入力値を取得
            //int? orId = string.IsNullOrWhiteSpace(tbxOrID.Text) ? (int?)null : int.Parse(tbxOrID.Text);
            //int? soId = string.IsNullOrWhiteSpace(tbxSoID.Text) ? (int?)null : int.Parse(tbxSoID.Text);
            //int? emId = string.IsNullOrWhiteSpace(tbxEmID.Text) ? (int?)null : int.Parse(tbxEmID.Text);
            //int? clId = string.IsNullOrWhiteSpace(tbxClID.Text) ? (int?)null : int.Parse(tbxClID.Text);
            //DateTime? orDate = dtpOrDate.Checked ? dtpOrDate.Value.Date : (DateTime?)null;
            //int stflg = cbxOrStateFlag.SelectedIndex;

            //if (stflg == 0)
            //{
            //    stflg = 1;
            //}
            //else if (stflg == 1)
            //{
            //    stflg = 0;
            //}

            //// 受注詳細情報の入力値を取得
            //int? ordetailId = string.IsNullOrWhiteSpace(tbxOrDetailID.Text) ? (int?)null : int.Parse(tbxOrDetailID.Text);
            //int? prId = string.IsNullOrWhiteSpace(tbxPrID.Text) ? (int?)null : int.Parse(tbxPrID.Text);

            //// 入力が全て空の場合は検索を実行しない
            //if (orId == null && soId == null && emId == null && clId == null && orDate == null && stflg == -1 && ordetailId == null && prId == null)
            //{
            //    MessageBox.Show("少なくとも1つの条件を入力してください。(数量、非表示理由を除く)", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //var searchresult = orderDataAccess.SearchOrData(orId, soId, emId, clId, orDate, stflg);

            //// 受注情報の結果から有効な受注IDリストを作成
            //List<int> validOrIds = searchresult.Select(or => or.OrId).ToList();

            //var detailsearchresult = orderDetailDataAccess.SearchOrDetailData(ordetailId, orId, prId, validOrIds);

            //// 詳細グリッドビューに表示されるデータの受注IDのみを取得
            //var orfidList = detailsearchresult.Select(detail => detail.OrId).Distinct().ToList();
            //// 非表示の受注IDを取得
            //var hiddenOrIds = orderDataAccess.GetHiddenOrIds(validOrIds);
            //// 非表示のものを除外する
            //detailsearchresult = detailsearchresult
            //    .Where(detail => !hiddenOrIds.Contains(detail.OrId))
            //    .ToList();

            //// 検索結果があれば DataGridView に表示
            //if (searchresult.Any() && detailsearchresult.Any())
            //{
            //    dgvOr.DataSource = searchresult;

            //    // データグリッドビューの列ヘッダー名を変更
            //    dgvOr.Columns["OrID"].HeaderText = "受注ID";
            //    dgvOr.Columns["SoID"].HeaderText = "営業所ID";
            //    dgvOr.Columns["EmID"].HeaderText = "社員ID";
            //    dgvOr.Columns["ClID"].HeaderText = "顧客ID";
            //    dgvOr.Columns["ClCharge"].HeaderText = "顧客担当者名";
            //    dgvOr.Columns["OrDate"].HeaderText = "受注年月日";
            //    dgvOr.Columns["OrStateFlag"].HeaderText = "受注状態";
            //    dgvOr.Columns["OrHidden"].HeaderText = "非表示理由";

            //    // 非表示列の設定
            //    dgvOr.Columns["OrFlag"].Visible = false;
            //    dgvOr.Columns["Cl"].Visible = false;
            //    dgvOr.Columns["Em"].Visible = false;
            //    dgvOr.Columns["So"].Visible = false;
            //    dgvOr.Columns["TOrderDetails"].Visible = false;

            //    // 列幅を設定
            //    dgvOr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //    dgvOr.Columns["OrHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //    dgvOr.Columns["OrHidden"].Width = 500;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvOr.ReadOnly = true;
            //    dgvOr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //    dgvOr.MultiSelect = false;
            //    dgvOr.AllowUserToAddRows = false;
            //    dgvOr.AllowUserToDeleteRows = false;
            //    dgvOr.AllowUserToResizeColumns = false;
            //    dgvOr.AllowUserToResizeRows = false;

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvOr.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //    dgvOrDetail.DataSource = detailsearchresult;

            //    // 列ヘッダーの設定
            //    dgvOrDetail.Columns["OrDetailID"].HeaderText = "受注詳細ID";
            //    dgvOrDetail.Columns["OrID"].HeaderText = "受注ID";
            //    dgvOrDetail.Columns["PrID"].HeaderText = "商品ID";
            //    dgvOrDetail.Columns["OrQuantity"].HeaderText = "数量";
            //    dgvOrDetail.Columns["OrTotalPrice"].HeaderText = "合計金額";

            //    // 非表示列の設定
            //    dgvOrDetail.Columns["OrDetailId"].Visible = false;
            //    dgvOrDetail.Columns["OrId"].Visible = false;
            //    dgvOrDetail.Columns["PrId"].Visible = false;
            //    dgvOrDetail.Columns["Or"].Visible = false;
            //    dgvOrDetail.Columns["Pr"].Visible = false;

            //    // 列幅を設定
            //    dgvOrDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //    dgvOrDetail.Columns["OrQuantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvOrDetail.ReadOnly = true;
            //    dgvOrDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //    dgvOrDetail.MultiSelect = false;
            //    dgvOrDetail.AllowUserToAddRows = false;
            //    dgvOrDetail.AllowUserToDeleteRows = false;
            //    dgvOrDetail.AllowUserToResizeColumns = false;
            //    dgvOrDetail.AllowUserToResizeRows = false;

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvOrDetail.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //}
            //else
            //{
            //    MessageBox.Show("条件に合うデータがありませんでした。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dgvOr.DataSource = null;
            //    dgvOr.Columns.Clear();
            //    dgvOr.Rows.Clear();

            //    dgvOrDetail.DataSource = null;
            //    dgvOrDetail.Columns.Clear();
            //    dgvOrDetail.Rows.Clear();
            //}

        }
        //受注情報を得る//
        private bool GetValidDataAtSlect()
        {
            //受注//
            //受注ID
            if (!String.IsNullOrEmpty(tbxOrID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxOrID.Text.Trim()))
                {
                    MessageBox.Show("受注IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrID.Focus();
                    return false;
                }
                //6桁以内
                if (tbxOrID.Text.Length > 6)
                {
                    MessageBox.Show("受注IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrID.Focus();
                    return false;
                }
            }

            //顧客担当者名
            if (!String.IsNullOrEmpty(tbxClCharge.Text.Trim()))
            {
                if (tbxClCharge.Text.Length > 50)
                {
                    MessageBox.Show("顧客担当者名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClCharge.Focus();
                    return false;
                }
            }

            //顧客ID
            if (!String.IsNullOrEmpty(tbxClID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxClID.Text.Trim()))
                {
                    MessageBox.Show("顧客IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClID.Focus();
                    return false;
                }
                //文字数チェック
                //6桁以内
                if (tbxClID.TextLength > 6)
                {
                    MessageBox.Show("顧客IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClID.Focus();
                    return false;
                }
            }

            //社員ID
            if (!String.IsNullOrEmpty(tbxClID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxClID.Text.Trim()))
                {
                    MessageBox.Show("顧客IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClID.Focus();
                    return false;
                }
                //文字数チェック
                //6桁以内
                if (tbxClID.TextLength > 6)
                {
                    MessageBox.Show("顧客IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClID.Focus();
                    return false;
                }
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
                //文字数チェック
                //2桁以内
                if (tbxSoID.TextLength > 2)
                {
                    MessageBox.Show("営業所IDは2桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxSoID.Focus();
                    return false;
                }
            }

            //受注詳細//
            //受注詳細ID
            if (!String.IsNullOrEmpty(tbxOrDetailID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxOrDetailID.Text.Trim()))
                {
                    MessageBox.Show("受注詳細IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrDetailID.Focus();
                    return false;
                }
                //6桁以内
                if (tbxOrDetailID.TextLength > 6)
                {
                    MessageBox.Show("受注詳細IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrDetailID.Focus();
                    return false;
                }
            }

            //商品ID
            if (!String.IsNullOrEmpty(tbxPrID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrID.Text.Trim()))
                {
                    MessageBox.Show("商品IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrID.Focus();
                    return false;
                }
                //6桁以内
                if (tbxPrID.TextLength > 6)
                {
                    MessageBox.Show("商品IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrID.Focus();
                    return false;
                }
            }

            //数量
            if (!String.IsNullOrEmpty(tbxOrQuantity.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxOrQuantity.Text.Trim()))
                {
                    MessageBox.Show("数量は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrQuantity.Focus();
                    return false;
                }
                //桁数チェック
                //4桁
                if (tbxOrQuantity.Text.Length > 4)
                {
                    MessageBox.Show("数量は4桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxOrQuantity.Focus();
                    return false;
                }
            }

            return true;
        }
        //受注情報抽出//
        private void GenerateDataAtSelect()
        {
            //検索条件のセット
            TOrderDsp selectCondition = new TOrderDsp()
            {
                OrId = tbxOrID.Text,
                ClCharge = tbxClCharge.Text,
                ClId = tbxClID.Text,
                EmId = tbxEmID.Text,
                SoId = tbxSoID.Text,
            };
            //受注データの抽出
            order = orderDataAccess.GetOrderData(selectCondition);

            //検索条件のセット(詳細)
            TOrderDetailDsp selectConditionDetail = new TOrderDetailDsp()
            {
                OrDetailId = tbxOrDetailID.Text,
                OrId = tbxOrID.Text,
                PrId = tbxPrID.Text,
                OrQuantity = tbxOrQuantity.Text,
                OrTotalPrice = tbxTotalPrice.Text,
            };
            //受注詳細データの抽出
            orderDetail = orderDetailDataAccess.GetOrderDetailData(selectConditionDetail);

        }
        //受注抽出結果表示//
        private void SetSelectData()
        {
            //受注//
            dgvOr.DataSource = order;
            dgvOr.Refresh();
            //受注詳細//
            dgvOrDetail.DataSource = orderDetail;
            dgvOrDetail.Refresh();
        }

        private void btnHiddenList_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvOr.SelectedRows)
            {
                row.Selected = false;
            }

            //非表示データグリッドビューの表示
            SetFormHiddenDataGridView();

            //// データを取得
            //var orderhyoji = OrderDataAccess.GetOrderData();

            ////取得したデータをフィルタリング
            //var filteredorder = OrderDataAccess.GetOrderData(2);

            //// 取得したデータをDataGridViewに表示
            //if (filteredorder != null && filteredorder.Count > 0)



            //{
            //    dgvOr.DataSource = filteredorder;
            //    // データグリッドビューの列ヘッダー名を変更
            //    dgvOr.Columns["OrID"].HeaderText = "受注ID";
            //    dgvOr.Columns["SoID"].HeaderText = "営業所ID";
            //    dgvOr.Columns["EmID"].HeaderText = "社員ID";
            //    dgvOr.Columns["ClID"].HeaderText = "顧客ID";
            //    dgvOr.Columns["OrDate"].HeaderText = "受注年月日";
            //    dgvOr.Columns["OrStateFlag"].HeaderText = "受注状態";
            //    //dgvOr.Columns["OrFlag"].HeaderText = "表示/非表示";
            //    dgvOr.Columns["OrHidden"].HeaderText = "非表示理由";

            //    //OrFlag(表示/非表示)の列を非表示にする
            //    dgvOr.Columns["OrFlag"].Visible = false;

            //    // 列幅を設定
            //    dgvOr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //    dgvOr.Columns["OrHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //    dgvOr.Columns["OrHidden"].Width = 500;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvOr.ReadOnly = true;  // 編集不可
            //    dgvOr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //    dgvOr.MultiSelect = false;  // 複数行選択を無効
            //    dgvOr.AllowUserToAddRows = false;  // 行の追加を無効
            //    dgvOr.AllowUserToDeleteRows = false;  // 行の削除を無効
            //    dgvOr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //    dgvOr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvOr.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //}
            //else
            //{
            //    MessageBox.Show("表示する受注情報がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dgvOr.DataSource = null; // データソースを解除

            //    // 列をクリアする場合（必要に応じて）
            //    dgvOr.Columns.Clear();

            //    // 行をクリアする場合（必要に応じて）
            //    dgvOr.Rows.Clear();
            //}

            //var Ordetails = orderDetailDataAccess.GetOrderDetails(2);

            //if (Ordetails != null && Ordetails.Count > 0)
            //{
            //    // 2つ目のデータグリッドビューにデータを設定
            //    dgvOrDetail.DataSource = Ordetails;

            //    //列ヘッダーの設定
            //    dgvOrDetail.Columns["OrDetailID"].HeaderText = "受注詳細ID";
            //    dgvOrDetail.Columns["OrID"].HeaderText = "受注ID";
            //    dgvOrDetail.Columns["PrID"].HeaderText = "商品ID";
            //    dgvOrDetail.Columns["OrQuantity"].HeaderText = "数量";

            //    // 列幅を設定
            //    dgvOrDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //    dgvOrDetail.Columns["OrQuantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvOrDetail.ReadOnly = true;  // 編集不可
            //    dgvOrDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //    dgvOrDetail.MultiSelect = false;  // 複数行選択を無効
            //    dgvOrDetail.AllowUserToAddRows = false;  // 行の追加を無効
            //    dgvOrDetail.AllowUserToDeleteRows = false;  // 行の削除を無効
            //    dgvOrDetail.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //    dgvOrDetail.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvOrDetail.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //}
            //else
            //{
            //    MessageBox.Show("表示する受注詳細情報がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dgvOrDetail.DataSource = null; // データソースを解除

            //    // 列をクリアする場合（必要に応じて）
            //    dgvOrDetail.Columns.Clear();

            //    // 行をクリアする場合（必要に応じて）
            //    dgvOrDetail.Rows.Clear();
            //}
        }

        private void UpdateOrderDetailGridView(int orderId)
        {
            using (var context = new SalesManagementContext())
            {
                // 指定された受注IDに関連する詳細データを取得
                var orderDetails = context.TOrderDetails
                    .Where(od => od.OrId == orderId)
                    .Select(od => new
                    {
                        od.OrDetailId,
                        od.PrId,
                        od.OrQuantity,
                        od.OrTotalPrice
                    }).ToList();

                dgvOrDetail.DataSource = orderDetails;

                // 列ヘッダーの設定
                dgvOrDetail.Columns["OrDetailId"].HeaderText = "受注詳細ID";
                dgvOrDetail.Columns["PrName"].HeaderText = "商品名";
                dgvOrDetail.Columns["PrId"].HeaderText = "商品ID";
                dgvOrDetail.Columns["OrQuantity"].HeaderText = "数量";
                dgvOrDetail.Columns["OrPrice"].HeaderText = "金額";
                dgvOrDetail.Columns["OrTotalPrice"].HeaderText = "合計金額";

                // 列幅を自動調整
                dgvOrDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }


        ///////////クリアボタン/////////////
        private void btnOrClearInput_Click(object sender, EventArgs e)
        {
            //clear = true;
            //クリアボタン
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "入力されている受注情報をクリアしますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            // 「はい」が選ばれた場合のみ処理を続行
            if (result == DialogResult.Yes)
            {
                ClearOr();
                //選択を解除
                foreach (DataGridViewRow row in dgvOr.SelectedRows)
                {
                    row.Selected = false;
                }
            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
            //clear = false;
        }

        //受注情報クリア
        private void ClearOr()
        {

            // 各入力欄を初期化
            tbxOrID.Text = "";             // 受注ID
            tbxSoID.Text = "";             // 営業所ID
            cbxSoName.SelectedIndex = -1;  // 営業所名
            tbxClID.Text = "";             // 顧客ID
            cbxClName.SelectedIndex = -1;  // 顧客名
            tbxEmID.Text = "";             // 社員ID
            cbxEmName.SelectedIndex = -1;  // 社員名
            tbxClCharge.Text = "";         // 顧客担当者名 <- ここが追加
            cbxOrStateFlag.SelectedIndex = -1; // 状態フラグ
            cbxOrFlag.SelectedIndex = -1;  // 受注フラグ
            dtpOrDate.Value = DateTime.Now; // 日付を現在時刻に
            tbxOrHidden.Text = "";         // 非表示理由
        }

        private void btnOrDetailClearInput_Click(object sender, EventArgs e)
        {
            //クリアボタン
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "入力されている受注詳細情報をクリアしますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            // 「はい」が選ばれた場合のみ処理を続行
            if (result == DialogResult.Yes)
            {

                ClearOrDetail();
                //選択を解除
                foreach (DataGridViewRow row in dgvOrDetail.SelectedRows)
                {
                    row.Selected = false;
                }
            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
        }

        //受注詳細情報クリア
        private void ClearOrDetail()
        {
            // テキストボックスの内容をクリア
            tbxOrDetailID.Text = "";       // 受注詳細ID
            tbxOrQuantity.Text = "";       // 数量
            tbxPrID.Text = "";             // 商品ID
            tbxOrPrice.Text = "";          // 金額
            tbxTotalPrice.Text = "";       // 合計金額

            // コンボボックスの選択をクリア
            cbxPrName.SelectedIndex = -1;  // 商品名
        }

        ///////////////////////////////
        //メソッド名：SetFormComboBox()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：コンボボックスのデータ設定
        ///////////////////////////////
        private void SetFormComboBox()
        {
            // コンボボックスを読み取り専用
            cbxSoName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxClName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxEmName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxOrFlag.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxOrStateFlag.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxPrName.DropDownStyle = ComboBoxStyle.DropDownList;
            //コンボボックスをデフォルト状態に設定
            //cbxSoName.SelectedIndex = -1;
            //cbxClName.SelectedIndex = -1;
            //cbxEmName.SelectedIndex = -1;
            //cbxOrFlag.SelectedIndex = -1;
            //cbxOrStateFlag.SelectedIndex = -1;
            //cbxPrName.SelectedIndex = -1;

            //顧客名のデータの取得
            client = clientDataAccess.GetClientDspData();
            //顧客データとコンボボックスをリンクさせる
            cbxClName.DataSource = client;
            //[DisplayMember]表示する名前 = ClName(顧客名)
            cbxClName.DisplayMember = "ClName";
            //[ValueMember]裏でリンクしているデータ = ClID(顧客ID)
            cbxClName.ValueMember = "ClID";
            //何も表示しない
            cbxClName.SelectedIndex = -1;

            //社員名のデータの取得
            employee = employeeDataAccess.GetEmployeeDspData();
            //社員データとコンボボックスをリンクさせる
            cbxEmName.DataSource = employee;
            //[DisplayMember]表示する名前 = EmName(社員名)
            cbxEmName.DisplayMember = "EmName";
            //[ValueMember]裏でリンクしているデータ = EmID(社員ID)
            cbxEmName.ValueMember = "EmID";
            //何も表示しない
            cbxEmName.SelectedIndex = -1;

            //営業所名データの取得
            salesOffice = salesOfficeDataAccess.GetSalesOfficeDspData();
            //営業所データとコンボボックスをリンクさせる
            cbxSoName.DataSource = salesOffice;
            //[DisplayMember]表示する名前 = SoName(営業所名)
            cbxSoName.DisplayMember = "SoName";
            //[ValueMember]裏でリンクしているデータ = SoID(営業所ID)
            cbxSoName.ValueMember = "SoID";
            //何も表示しない
            cbxSoName.SelectedIndex = -1;

            //商品名のデータの取得
            product = productDataAccess.GetProductDspData();
            //商品データとコンボボックスをリンクさせる
            cbxPrName.DataSource = product;
            //[DisplayMember]表示する名前 = PrName(商品名)
            cbxPrName.DisplayMember = "PrName";
            //[ValueMember]裏でリンクしているデータ = PrID(商品ID)
            cbxPrName.ValueMember = "PrID";
            //何も表示しない
            cbxPrName.SelectedIndex = -1;

            //using (SalesManagementContext context = new SalesManagementContext())
            //{

            //    // データベースからコンボボックスに設定するデータを取得
            //    List<string> salesOffices = salesOfficeDataAccess.SoGetComboboxText();

            //    // コンボボックスにデータを追加
            //    foreach (var office in salesOffices)
            //    {
            //        cbxSoName.Items.Add(office);
            //    }

            //    // ClientDateAccess クラスのインスタンスを作成

            //    // データベースからコンボボックスに設定するデータを取得    
            //    List<string> cliantnames = clientDataAccess.ClGetComboboxText();

            //    // コンボボックスにデータを追加
            //    foreach (var clname in cliantnames)
            //    {
            //        cbxClName.Items.Add(clname);
            //    }


            //    // データベースからコンボボックスに設定するデータを取得 
            //    List<string> employeenames = employeeDataAccess.EmGetComboboxText();

            //    // コンボボックスにデータを追加
            //    foreach (var emname in employeenames)
            //    {
            //        cbxEmName.Items.Add(emname);
            //    }


            //    // データベースからコンボボックスに設定するデータを取得 
            //    List<string> productnames = productDataAccess.PrGetComboboxText();

            //    // コンボボックスにデータを追加
            //    foreach (var prname in productnames)
            //    {
            //        cbxPrName.Items.Add(prname);
            //    }

            //}
        }

        ///////////////////////////////
        //メソッド名：SetFormDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの設定
        ///////////////////////////////
        private void SetFormDataGridView()
        {
            //読み取り専用
            dgvOr.ReadOnly = true;  // 編集不可
            dgvOr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            dgvOr.MultiSelect = false;  // 複数行選択を無効
            dgvOr.AllowUserToAddRows = false;  // 行の追加を無効
            dgvOr.AllowUserToDeleteRows = false;  // 行の削除を無効
            dgvOr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            dgvOr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            dgvOrDetail.ReadOnly = true;
            dgvOrDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            dgvOrDetail.MultiSelect = false;  // 複数行選択を無効
            dgvOrDetail.AllowUserToAddRows = false;  // 行の追加を無効
            dgvOrDetail.AllowUserToDeleteRows = false;  // 行の削除を無効
            dgvOrDetail.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            dgvOrDetail.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //ヘッダー位置の設定
            dgvOr.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrDetail.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            //読み取り専用
            dgvOr.ReadOnly = true;  // 編集不可
            dgvOr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            dgvOr.MultiSelect = false;  // 複数行選択を無効
            dgvOr.AllowUserToAddRows = false;  // 行の追加を無効
            dgvOr.AllowUserToDeleteRows = false;  // 行の削除を無効
            dgvOr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            dgvOr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            dgvOrDetail.ReadOnly = true;
            dgvOrDetail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            dgvOrDetail.MultiSelect = false;  // 複数行選択を無効
            dgvOrDetail.AllowUserToAddRows = false;  // 行の追加を無効
            dgvOrDetail.AllowUserToDeleteRows = false;  // 行の削除を無効
            dgvOrDetail.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            dgvOrDetail.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //ヘッダー位置の設定
            dgvOr.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrDetail.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            //受注データの取得
            order = orderDataAccess.GetOrderData();
            //受注詳細データの取得
            orderDetail = orderDetailDataAccess.GetOrderDetailData();

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
            //受注データの取得
            order = orderDataAccess.GetOrderHiddenData();
            //受注詳細データの取得
            orderDetail = orderDetailDataAccess.GetOrderDetailHiddenData();

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
            ////受注テーブル////
            dgvOr.DataSource = order;
            //列幅を設定
            dgvOr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //[8]受注年月日
            //[9]受注状態フラグ　OrStateFlag
            dgvOr.Columns["OrStateFlag"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOr.Columns["OrStateFlag"].Width = 100;
            //[11]非表示理由
            dgvOr.Columns["OrHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvOr.Columns["OrHidden"].Width = 250;

            //不要な要素を非表示
            //[2]顧客ID　ClId
            dgvOr.Columns["ClId"].Visible = false;
            //[4]社員ID　EmId
            dgvOr.Columns["EmId"].Visible = false;
            //[6]営業所ID　SoId
            dgvOr.Columns["SoId"].Visible = false;
            //[10]非表示フラグ　OrFlag
            dgvOr.Columns["OrFlag"].Visible = false;

            //各列の文字位置の指定
            dgvOr.Columns["OrId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOr.Columns["OrStateFlag"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvOr.Refresh();

            ////受注詳細テーブル////
            dgvOrDetail.DataSource = orderDetail;
            //列幅を設定
            dgvOrDetail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //不要な要素を非表示
            //[2]商品ID　PrID
            dgvOrDetail.Columns["PrId"].Visible = false;

            //各列の文字位置の指定
            dgvOrDetail.Columns["OrDetailId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvOrDetail.Columns["OrQuantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvOrDetail.Columns["OrTotalPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvOrDetail.Refresh();
        }

        //顧客IDテキストボックス、文字を打った時のプログラム
        private void tbxClID_TextChanged(object sender, EventArgs e)
        {
            string text = tbxClID.Text;
            bool flg = false;

            //コンボボックスに入っているデータ全部を調べることが出来る
            foreach (var item in cbxClName.Items)
            {
                if (item is SalesManagement_SysDev.MClient mClient)
                {
                    if (mClient.ClId.ToString() == text)
                    {
                        cbxClName.SelectedItem = mClient;
                        flg = true;
                        break;
                    }
                }
            }

            if (!flg)
            {
                cbxClName.SelectedIndex = -1;
            }

            ////入力クリア時に、エラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(tbxClID.Text))
            //{
            //    return; // 処理を終了
            //}

            //// 自動入力のループ対策
            //if (ClUpdating) return;
            //ClUpdating = true;

            //// テキストボックスに入力された顧客IDを取得
            //if (int.TryParse(tbxClID.Text, out int clID))
            //{
            //    // 顧客IDに対応する顧客名を取得
            //    string clName = clientDataAccess.GetClNameById(clID);

            //    if (!string.IsNullOrEmpty(clName))
            //    {
            //        // コンボボックスの選択肢に顧客名を設定
            //        cbxClName.SelectedItem = clName;
            //    }
            //    //else
            //    //{
            //    //    // 顧客名が見つからなかった場合
            //    //    MessageBox.Show("対応する顧客名が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //}
            //}
            //else
            //{
            //    // 入力されたテキストが数値に変換できなかった場合
            //    MessageBox.Show("有効な顧客IDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            //ClUpdating = false;
        }

        //顧客名コンボボックス選択時のプログラム
        private void cbxClName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SelectedIndex ≠ SelectedValue
            //SelectedIndexは0から始まるコンボボックスにあるデータの番号
            //SelectedValueは選択したデータの番号
            if (cbxClName.SelectedIndex >= 0)
            {
                tbxClID.Text = cbxClName.SelectedValue.ToString();
            }
            else
            {
                tbxClID.Text = "";
            }

            ////入力クリア時に、エラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(cbxClName.Text))
            //{
            //    return; // 処理を終了
            //}
            ////顧客cbx

            //// コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            //if (cbxClName.SelectedIndex != -1)
            //{
            //    // コンボボックスで選択された社員名を取得
            //    string selectedClName = cbxClName.SelectedItem.ToString();

            //    //社員IDを取得してテキストボックスに表示する
            //    int? Clid = clientDataAccess.GetClIdByName(selectedClName);
            //    if (Clid.HasValue)
            //    {
            //        tbxClID.Text = Clid.Value.ToString();
            //    }
            //    else
            //    {
            //        tbxClID.Text = "IDが見つかりません";
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("社員名が選択されていません。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            ////自動入力のループ対策
            //ClUpdating = false;
        }

        //社員IDテキストボックス、文字を打った時のプログラム
        private void tbxEmID_TextChanged(object sender, EventArgs e)
        {
            string text = tbxEmID.Text;
            bool flg = false;

            //コンボボックスに入っているデータ全部を調べることが出来る
            foreach (var item in cbxEmName.Items)
            {
                if (item is SalesManagement_SysDev.MEmployee mEmployee)
                {
                    if (mEmployee.EmId.ToString() == text)
                    {
                        cbxEmName.SelectedItem = mEmployee;
                        flg = true;
                        break;
                    }
                }
            }

            //一致しなかった場合
            if (!flg)
            {
                cbxEmName.SelectedIndex = -1;
            }

            ////入力クリア時に、エラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(tbxEmID.Text))
            //{
            //    return; // 処理を終了
            //}

            //// 自動入力のループ対策
            //if (EmUpdating) return;
            //EmUpdating = true;

            //// テキストボックスに入力された社員IDを取得
            //if (int.TryParse(tbxEmID.Text, out int emID))
            //{
            //    // 社員IDに対応する社員名を取得
            //    string emName = employeeDataAccess.GetEmNameById(emID);

            //    if (!string.IsNullOrEmpty(emName))
            //    {
            //        // コンボボックスの選択肢に社員名を設定
            //        cbxEmName.SelectedItem = emName;
            //    }
            //    //else
            //    //{
            //    //    // 社員名が見つからなかった場合
            //    //    MessageBox.Show("対応する社員名が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //}
            //}
            //else
            //{
            //    // 入力されたテキストが数値に変換できなかった場合
            //    MessageBox.Show("有効な社員IDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            //EmUpdating = false;
        }

        //社員名コンボボックス選択時のプログラム
        private void cbxEmName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxEmName.SelectedIndex >= 0)
            {
                tbxEmID.Text = cbxEmName.SelectedValue.ToString();
            }
            else
            {
                tbxEmID.Text = "";
            }

            ////入力クリア時に、エラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(cbxEmName.Text))
            //{
            //    return; // 処理を終了
            //}
            //// コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            //if (cbxEmName.SelectedIndex != -1)
            //{
            //    // コンボボックスで選択された社員名を取得
            //    string selectedEmName = cbxEmName.SelectedItem.ToString();

            //    //社員IDを取得してテキストボックスに表示する
            //    int? Emid = employeeDataAccess.GetEmIdByName(selectedEmName);
            //    if (Emid.HasValue)
            //    {
            //        tbxEmID.Text = Emid.Value.ToString();
            //    }
            //    else
            //    {
            //        tbxEmID.Text = "IDが見つかりません";
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("顧客名が選択されていません。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            ////自動入力のループ対策
            //EmUpdating = false;
        }

        //営業所IDテキストボックス、文字を打った時のプログラム
        private void tbxSoID_TextChanged(object sender, EventArgs e)
        {
            string text = tbxSoID.Text;
            bool flg = false;

            //コンボボックスに入っているデータ全部を調べることが出来る
            foreach (var item in cbxSoName.Items)
            {
                if (item is SalesManagement_SysDev.MSalesOffice mSalesOffice)
                {
                    if (mSalesOffice.SoId.ToString() == text)
                    {
                        cbxSoName.SelectedItem = mSalesOffice;
                        flg = true;
                        break;
                    }
                }
            }

            //一致しなかった場合
            if (!flg)
            {
                cbxSoName.SelectedIndex = -1;
            }

            ////入力クリア時に、エラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(tbxSoID.Text))
            //{
            //    return; // 処理を終了
            //}

            //// 自動入力のループ対策
            //if (SoUpdating) return;
            //SoUpdating = true;

            //// テキストボックスに入力された営業所IDを取得
            //if (int.TryParse(tbxSoID.Text, out int soID))
            //{
            //    // 営業所IDに対応する営業所名を取得
            //    string soName = salesOfficeDataAccess.GetSoNameById(soID);

            //    if (!string.IsNullOrEmpty(soName))
            //    {
            //        // コンボボックスの選択肢に営業所名を設定
            //        cbxSoName.SelectedItem = soName;
            //    }
            //    //else
            //    //{
            //    //    // 営業所名が見つからなかった場合
            //    //    MessageBox.Show("対応する営業所名が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //}
            //}
            //else
            //{
            //    // 入力されたテキストが数値に変換できなかった場合
            //    MessageBox.Show("有効な営業所IDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            //SoUpdating = false;
        }

        //営業所名コンボボックス選択時のプログラム
        private void cbxSoName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxSoName.SelectedIndex >= 0)
            {
                tbxSoID.Text = cbxSoName.SelectedValue.ToString();
            }
            else
            {
                tbxSoID.Text = "";
            }

            ////入力クリア時に、エラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(cbxSoName.Text))
            //{
            //    return; // 処理を終了
            //}
            //// コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            //if (cbxSoName.SelectedIndex != -1)
            //{
            //    // コンボボックスで選択された営業所名を取得
            //    string selectedSoName = cbxSoName.SelectedItem.ToString();

            //    //営業所IDを取得してテキストボックスに表示する
            //    int? Soid = salesOfficeDataAccess.GetSoIdByName(selectedSoName);
            //    if (Soid.HasValue)
            //    {
            //        tbxSoID.Text = Soid.Value.ToString();
            //    }
            //    else
            //    {
            //        tbxSoID.Text = "IDが見つかりません";
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("営業所名が選択されていません。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            ////自動入力のループ対策
            //SoUpdating = false;
        }

        //商品IDテキストボックス、文字を打った時のプログラム
        private void tbxPrID_TextChanged(object sender, EventArgs e)
        {
            string text = tbxPrID.Text;
            bool flg = false;

            //コンボボックスに入っているデータ全部を調べることが出来る
            foreach (var item in cbxPrName.Items)
            {
                if (item is SalesManagement_SysDev.MProduct mProduct)
                {
                    if (mProduct.PrId.ToString() == text)
                    {
                        cbxPrName.SelectedItem = mProduct;
                        tbxOrPrice.Text = mProduct.Price.ToString();
                        flg = true;
                        break;
                    }
                }
            }

            //一致しなかった場合
            if (!flg)
            {
                cbxPrName.SelectedIndex = -1;
            }


            ////入力クリア時に、エラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(tbxPrID.Text))
            //{
            //    return; // 処理を終了
            //}

            //// 自動入力のループ対策
            //if (PrUpdating) return;
            //PrUpdating = true;

            //// テキストボックスに入力された商品IDを取得
            //if (int.TryParse(tbxPrID.Text, out int prID))
            //{
            //    // 商品IDに対応する商品名を取得
            //    string prName = productDataAccess.GetPrNameById(prID);

            //    if (!string.IsNullOrEmpty(prName))
            //    {
            //        // コンボボックスの選択肢に社員名を設定
            //        cbxPrName.SelectedItem = prName;
            //    }
            //    //else
            //    //{
            //    //    // 商品名が見つからなかった場合
            //    //    MessageBox.Show("対応する商品名が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //}
            //}
            //else
            //{
            //    // 入力されたテキストが数値に変換できなかった場合
            //    MessageBox.Show("有効な商品IDを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            //PrUpdating = false;
        }

        //商品名コンボボックス選択時のプログラム
        private void cbxPrName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPrName.SelectedIndex >= 0)
            {
                tbxPrID.Text = cbxPrName.SelectedValue.ToString();
            }
            else
            {
                tbxPrID.Text = "";
            }

            ////入力クリア時に、エラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(cbxPrName.Text))
            //{
            //    return; // 処理を終了
            //}
            //// コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            //if (cbxPrName.SelectedIndex != -1)
            //{
            //    // コンボボックスで選択された社員名を取得
            //    string selectedPrName = cbxPrName.SelectedItem.ToString();

            //    //社員IDを取得してテキストボックスに表示する
            //    int? Prid = productDataAccess.GetPrIdByName(selectedPrName);
            //    if (Prid.HasValue)
            //    {
            //        tbxPrID.Text = Prid.Value.ToString();
            //    }
            //    else
            //    {
            //        tbxPrID.Text = "IDが見つかりません";
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("商品名が選択されていません。", "選択エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            ////自動入力のループ対策
            //PrUpdating = false;
        }

        private void tbxChID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }
        }
        private void tbxOrID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }
        }

        private void tbxSoID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }
        }

        private void tbxClID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }
        }

        private void tbxEmID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }
        }

        private void tbxChDetailID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }
        }

        private void tbxPrID_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 数字とバックスペースを許可
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // 無効な入力をキャンセル
            }
        }

        private void cbxOrFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            //自動入力のメッセージ対策
            //if (HdUpdating) return;
            //HdUpdating = true;
            //if (clear) return;

            //// 選択された項目を取得
            //string selectedFlag = cbxOrFlag.SelectedItem?.ToString();

            //if (!string.IsNullOrEmpty(selectedFlag))
            //{
            //    if (selectedFlag == "表示")
            //    {
            //        // 表示が選択された場合、非表示理由を入力不可にし、内容をクリアする
            //        tbxOrHidden.Enabled = false;
            //        tbxOrHidden.Text = string.Empty; // 入力内容をクリア
            //    }
            //    else if (selectedFlag == "非表示")
            //    {
            //        // 非表示が選択された場合、非表示理由を入力可能にする
            //        tbxOrHidden.Enabled = true;
            //    }
            //}
            //// 自動入力のメッセージ対策
            //if (HdUpdating) return;
            //HdUpdating = true;
            //if (clear) return;

            // 入力された受注情報を取得
            //string eiids = tbxSoID.Text;
            //string syids = tbxEmID.Text;
            //string koids = tbxClID.Text;
            //int hiflgindex = cbxOrFlag.SelectedIndex;
            //int stflgindex = cbxOrStateFlag.SelectedIndex;
            //int stflg;
            //int hiflg;
            //DateTime ordate = dtpOrDate.Value;
            //int einame = cbxSoName.SelectedIndex;
            //int syname = cbxEmName.SelectedIndex;

            //// 空白チェック
            //if (string.IsNullOrWhiteSpace(eiids) || // 営業所ID
            //    string.IsNullOrWhiteSpace(syids) || // 社員ID
            //    string.IsNullOrWhiteSpace(koids) || // 顧客ID
            //    stflgindex == -1 ||                 // 状態フラグ
            //    hiflgindex == -1 ||                 // フラグ
            //    einame == -1 ||                     // 営業所名
            //    syname == -1)                       // 社員名
            //{
            //    MessageBox.Show("!必須項目を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cbxOrFlag.SelectedIndex = -1;
            //    HdUpdating = false;
            //    return;
            //}


            //// コンボボックスのIndex値をフラグ値に変更
            //if (stflgindex == 0)
            //{
            //    stflg = 1;
            //}
            //else if (stflgindex == 1)
            //{
            //    stflg = 0;
            //}
            //else
            //{
            //    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //try
            //{
            //    // 入力データをint型に変換
            //    int eiid = int.Parse(eiids);
            //    int syid = int.Parse(syids);
            //    int koid = int.Parse(koids);

            //    // データベースで条件をチェック
            //    bool exists = orderDataAccess.CheckDataExistence(eiid, syid, koid, stflg, ordate);


            //    // フラグ変更処理
            //    if (exists)
            //    {
            //        int? orflg = orderDataAccess.GetOrFlag(eiid);
            //        if (hiflgindex == 1 && orflg == 0)
            //        {
            //            if (tbxOrHidden.Text != "")
            //            {
            //                var result = MessageBox.Show(
            //                "受注情報を非表示にしますか？",
            //                "確認",
            //                MessageBoxButtons.YesNo,
            //                MessageBoxIcon.Question
            //                );

            //                // 「はい」が選択された場合のみ処理を実行
            //                if (result == DialogResult.Yes)
            //                {
            //                    string orhide = tbxOrHidden.Text;
            //                    // 更新する新しい値
            //                    int newOrflg = 2;

            //                    // データベースの値を更新
            //                    bool changeOrflg = orderDataAccess.ChangeOrhideflg(eiid, newOrflg, orhide);

            //                    // 更新結果を通知
            //                    if (changeOrflg)
            //                    {
            //                        MessageBox.Show("受注情報を非表示にしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    }
            //                    else
            //                    {
            //                        MessageBox.Show("情報の更新に失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    }
            //                    HdUpdating = false;
            //                    return;
            //                }
            //                //「いいえ」が選択された時
            //                else
            //                {
            //                    cbxOrFlag.SelectedIndex = 0;
            //                    HdUpdating = false;
            //                    return;
            //                }
            //            }
            //            else
            //            {
            //                var result = MessageBox.Show(
            //                "非表示理由が入力されていませんが非表示にしますか？",
            //                "確認",
            //                MessageBoxButtons.YesNo,
            //                MessageBoxIcon.Question
            //                );

            //                // 「はい」が選択された場合のみ処理を実行
            //                if (result == DialogResult.Yes)
            //                {
            //                    string orhide = tbxOrHidden.Text;
            //                    // 更新する新しい値
            //                    int newOrflg = 2;

            //                    // データベースの値を更新
            //                    bool changeOrflg = orderDataAccess.ChangeOrhideflg(eiid, newOrflg, orhide);

            //                    // 更新結果を通知
            //                    if (changeOrflg)
            //                    {
            //                        MessageBox.Show("受注情報を非表示にしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    }
            //                    else
            //                    {
            //                        MessageBox.Show("情報の更新に失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                    }
            //                    HdUpdating = false;
            //                    return;
            //                }
            //                //「いいえ」が選択された時
            //                else
            //                {
            //                    cbxOrFlag.SelectedIndex = 0;
            //                    HdUpdating = false;
            //                    return;
            //                }
            //            }

            //        }
            //        else if (hiflgindex == 0 && orflg == 2)
            //        {
            //            var result = MessageBox.Show(
            //            "受注情報を表示状態にしますか？" +
            //            "(入力されている非表示理由は削除されます。)",
            //            "確認",
            //            MessageBoxButtons.YesNo,
            //            MessageBoxIcon.Question
            //            );

            //            // 「はい」が選択された場合のみ処理を実行
            //            if (result == DialogResult.Yes)
            //            {
            //                // 更新する新しい値
            //                int newOrflg = 0;
            //                string orhide = "";
            //                // データベースの値を更新
            //                bool changeOrflg = orderDataAccess.ChangeOrhideflg(eiid, newOrflg, orhide);

            //                // 更新結果を通知
            //                if (changeOrflg)
            //                {
            //                    MessageBox.Show("受注情報を表示状態にしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                }
            //                else
            //                {
            //                    MessageBox.Show("情報の更新に失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                }
            //                HdUpdating = false;
            //                return;
            //            }
            //            //「いいえ」が選択された時
            //            else
            //            {
            //                cbxOrFlag.SelectedIndex = 1;
            //                HdUpdating = false;
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            MessageBox.Show("エラーが発生しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            HdUpdating = false;
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("該当するデータがありません。", "確認結果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
            //catch (FormatException ex)
            //{
            //    MessageBox.Show("数値形式が正しくありません: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    HdUpdating = false;
            //    return;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    HdUpdating = false;
            //    return;
            //}
        }

        private void InitializeOrFlagComboBox()
        {
            cbxOrFlag.Items.Clear(); // 初期化
            cbxOrFlag.Items.Add("表示");
            cbxOrFlag.Items.Add("非表示");
            cbxOrFlag.SelectedIndex = 0; // 初期状態を「表示」に設定
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            //メニュー画面を表示する
            Menu menu = new Menu();
            menu.Show();

            //〇〇画面を閉じる
            this.Hide();
        }

        private void CalculateTotalPrice()
        {
            // 金額と数量が正しい値かをチェック
            if (decimal.TryParse(tbxOrPrice.Text.Trim(), out decimal price) &&
                int.TryParse(tbxOrQuantity.Text.Trim(), out int quantity))
            {
                // 合計金額を計算
                decimal totalPrice = price * quantity;
                //intの上限　2,147,483,647
                //合計金額の上限　10桁
                if (totalPrice > 2147483647)
                {
                    // 入力が不正な場合は合計金額をクリア
                    tbxOrQuantity.Text = string.Empty;
                    tbxTotalPrice.Text = string.Empty;
                }
                else
                {
                    tbxTotalPrice.Text = ((int)totalPrice).ToString(); // 小数点以下を切り捨て
                }
            }
            else
            {
                // 入力が不正な場合は合計金額をクリア
                tbxTotalPrice.Text = string.Empty;
            }
        }

        private void tbxOrPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void tbxOrQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void dgvOr_SelectionChanged(object sender, EventArgs e)
        {
            // 自動入力のメッセージ対策
            if (HdUpdating) return;
            HdUpdating = true;

            if (dgvOr.SelectedRows.Count > 0) // 選択された行があるか確認
            {
                // 選択された行の最初の行を取得
                var selectedRow = dgvOr.SelectedRows[0];

                // 各テキストボックスに選択された行のデータを設定
                tbxOrID.Text = selectedRow.Cells["OrId"].Value.ToString(); // 受注ID
                tbxSoID.Text = selectedRow.Cells["SoId"].Value.ToString(); // 営業所ID
                tbxEmID.Text = selectedRow.Cells["EmId"].Value.ToString(); // 社員ID
                tbxClID.Text = selectedRow.Cells["ClId"].Value.ToString(); // 顧客ID
                tbxClCharge.Text = selectedRow.Cells["ClCharge"].Value.ToString(); // 顧客担当者
                dtpOrDate.Text = selectedRow.Cells["OrDate"].Value.ToString(); // 受注日
                cbxOrStateFlag.Text = selectedRow.Cells["OrStateFlag"].Value?.ToString() ?? ""; // 状態フラグ
                tbxOrHidden.Text = selectedRow.Cells["OrHidden"].Value?.ToString() ?? ""; // 非表示理由
            }

            if (dgvOr.SelectedRows.Count > 0) // 選択された行があるか確認
            {
                // 選択された行のフラグ列の値を取得
                var selectedRow = dgvOr.SelectedRows[0];
                var flagState = selectedRow.Cells["OrStateFlag"].Value;

                if (flagState != null && int.TryParse(flagState.ToString(), out int stateFlag))
                {
                    // フラグ値に応じてコンボボックスの選択を変更
                    if (stateFlag == 1)
                    {
                        cbxOrStateFlag.SelectedIndex = 0; // 確定
                    }
                    else if (stateFlag == 0)
                    {
                        cbxOrStateFlag.SelectedIndex = 1; // 未確定
                    }
                    else
                    {
                        cbxOrStateFlag.SelectedItem = null; // 不明な値の場合は未選択状態にする
                    }
                }
                else
                {
                    cbxOrStateFlag.SelectedItem = null; // フラグ値が取得できない場合は未選択状態
                }
            }

            if (dgvOr.SelectedRows.Count > 0) // 行が選択されている場合
            {
                var selectedRow = dgvOr.SelectedRows[0];

                // 非表示の列からフラグ値を取得
                var flagOrder = selectedRow.Cells["OrFlag"].Value;
                if (flagOrder != null && int.TryParse(flagOrder.ToString(), out int orderFlag))
                {
                    if (orderFlag == 2)
                    {
                        cbxOrFlag.SelectedIndex = 1; // 特定フラグ（例: 隠し設定など）
                    }
                    else if (orderFlag == 0)
                    {
                        cbxOrFlag.SelectedIndex = 0; // 標準フラグ
                    }
                    else
                    {
                        cbxOrFlag.SelectedItem = null; // 不明な値の場合
                    }
                }
                else
                {
                    cbxOrFlag.SelectedItem = null; // フラグ値が取得できない場合
                }

                orderDetail = orderDetailDataAccess.GetOrderDetailSelectionData(selectedRow.Cells["OrId"].Value.ToString());
                SetDataGridView();
            }

            HdUpdating = false;

        }

        private void dgvOrDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrDetail.SelectedRows.Count > 0)  // 選択された行があるか確認
            {
                // 選択された行の最初の行を取得
                var selectedRow = dgvOrDetail.SelectedRows[0];

                // 各テキストボックスに選択された行のデータを設定
                tbxOrDetailID.Text = selectedRow.Cells["OrDetailID"].Value.ToString();
                tbxPrID.Text = selectedRow.Cells["PrID"].Value.ToString();
                tbxOrQuantity.Text = selectedRow.Cells["OrQuantity"].Value.ToString();
            }
        }

        // 受注状態フラグを更新するメソッド
        private bool UpdateOrderStateFlag(int orderId, int newFlag)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 指定された受注を取得
                    var order = context.TOrders.FirstOrDefault(o => o.OrId == orderId);

                    if (order != null)
                    {
                        // 受注状態フラグを更新
                        order.OrStateFlag = newFlag;

                        // データベースを保存
                        context.SaveChanges();
                        return true; // 更新成功
                    }
                    else
                    {
                        MessageBox.Show("指定された受注が見つかりません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; // データが見つからない
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラー: {ex.Message}", "データベースエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // エラー発生
            }
        }

        // DataGridViewをリフレッシュするメソッド
        private void RefreshOrderList()
        {
            // 受注一覧を再読み込み
            var orderData = OrderDataAccess.GetOrderData(); // 必要に応じてフィルタリング
            dgvOr.DataSource = orderData;
        }

        //表示非表示コンボボックス選択時のプログラム(フラグ)
        private int OrFlgNum()
        {
            int flg;
            if (cbxOrFlag.SelectedIndex == 0)
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

        private string OrHidden()
        {
            string hidden;
            if (cbxOrFlag.SelectedIndex == 0)
            {
                hidden = null;
            }
            else
            {
                hidden = tbxOrHidden.Text.Trim();
            }

            return hidden;
        }

        private string OrHiddenConfirm()
        {
            var selectedRow = dgvOr.SelectedRows[0];
            int Orflg = int.Parse(selectedRow.Cells["OrFlag"].Value.ToString());
            string hidden;
            if (Orflg == 0)
            {
                hidden = null;
            }
            else
            {
                hidden = selectedRow.Cells["OrHidden"].Value.ToString();
            }

            return hidden;
        }
    }
}





