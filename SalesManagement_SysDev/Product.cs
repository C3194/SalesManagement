using a;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using SalesManagement_SysDev;
using SalesManagement_SysDev.Common;
using SalesManagement_SysDev.DataAccess;
using SalesManagement_SysDev.DateAccess;
using SalesManagement_SysDev.新しいフォルダー;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Xml.Linq;
using 画面設計用9._0注文管理;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.CompilerServices.RuntimeHelpers;


namespace Product_Management
{
    public partial class Product : Form
    {
        //クラスのインスタンスを生成
        MakerDataAccess makerDataAccess = new MakerDataAccess();
        MajorClassificationDataAccess majorClassificationDataAccess = new MajorClassificationDataAccess();
        SmallClassificationDataAccess smallClassificationDataAccess = new SmallClassificationDataAccess();
        ProductDataAccess productDataAccess = new ProductDataAccess();

        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の顧客データ
        private static List<MProduct> product;
        private static List<MProductDsp> productDsp;
        //コンボボックス用のメーカーデータ
        private static List<MMaker> maker;
        //コンボボックス用の大分類データ
        private static List<MMajorClassification> Majorclass;
        //コンボボックス用の小分類データ
        private static List<MSmallClassification> Smallclass;

        //自動入力のループ回避変数
        private bool MaUpdating = false;
        private bool McUpdating = false;
        private bool ScUpdating = false;
        private bool PrUpdating = false;
        private bool clear = false;
        private bool isSearching = false;
        private bool isErrorDisplayed = false;
        private bool hihyouji = true;



        public Product()
        {
            InitializeComponent();
        }


        //4.0商品管理画面
        private void Product_Load(object sender, EventArgs e)
        {
            //コンボボックスの設定
            SetFormComboBox();

            //データグリッドビューの表示
            SetFormDataGridView();

            cbxPrFlag.SelectedIndex = 0; // 表示を選択する
            cbxPrFlag.Enabled = false;

            ClearPr();
        }

        //コンボボックスのデータ設定
        private void SetFormComboBox()
        {
            // コンボボックスを読み取り専用
            cbxMaName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxMcName.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxScName.DropDownStyle = ComboBoxStyle.DropDownList;

            //メーカー名の取得
            maker = makerDataAccess.GetMakerDspData();
            //メーカーデータとコンボボックスをリンクさせる
            cbxMaName.DataSource = maker;
            //[DisplayMember]表示する名前 = MaName(メーカー名)
            cbxMaName.DisplayMember = "MaName";
            //[ValueMember]裏でリンクしているデータ = MaID(メーカーID)
            cbxMaName.ValueMember = "MaID";
            //コンボボックスをデフォルト状態に設定
            cbxMaName.SelectedIndex = -1;

            //大分類名の取得
            Majorclass = majorClassificationDataAccess.GetMajorClassDspData();
            //大分類データとコンボボックスをリンクさせる
            cbxMcName.DataSource = Majorclass;
            //[DisplayMember]表示する名前 = McName(大分類名)
            cbxMcName.DisplayMember = "McName";
            //[ValueMember]裏でリンクしているデータ = McID(大分類ID)
            cbxMcName.ValueMember = "McID";
            //コンボボックスをデフォルト状態に設定
            cbxMcName.SelectedIndex = -1;

            int McID;
            if (cbxMcName.SelectedValue == null)
            {
                McID = -1;
            }
            else
            {
                McID = int.Parse(cbxMcName.SelectedValue.ToString());
            }
            //小分類名の取得
            Smallclass = smallClassificationDataAccess.GetSmallClassDspData(McID);
            //小分類データとコンボボックスをリンクさせる
            cbxScName.DataSource = Smallclass;
            //[DisplayMember]表示する名前 = ScName(小分類名)
            cbxScName.DisplayMember = "ScName";
            //[ValueMember]裏でリンクしているデータ = McID(大分類ID)
            cbxScName.ValueMember = "McID";
            //コンボボックスをデフォルト状態に設定
            cbxScName.SelectedIndex = -1;


            /////////////////////////////////////////
            //try
            //{
            //    using (SalesManagementContext context = new SalesManagementContext())
            //    {
            //        // コンボボックスを読み取り専用
            //        cbxMaName.DropDownStyle = ComboBoxStyle.DropDownList;
            //        cbxMcName.DropDownStyle = ComboBoxStyle.DropDownList;
            //        cbxScName.DropDownStyle = ComboBoxStyle.DropDownList;

            //        //コンボボックスをデフォルト状態に設定
            //        cbxMaName.SelectedIndex = -1;
            //        cbxMcName.SelectedIndex = -1;
            //        cbxScName.SelectedIndex = -1;


            //        // データベースからコンボボックスに設定するデータを取得
            //        List<string> makers = makerDataAccess.MaGetComboboxText();

            //        // コンボボックスにデータを追加
            //        foreach (var maname in makers)
            //        {
            //            cbxMaName.Items.Add(maname);
            //        }

            //        //MajorClassificationDataAccessクラスのインスタンスを作成
            //        MajorClassificationDataAccess majorClassificationDataAccess = new MajorClassificationDataAccess();
            //        // データベースからコンボボックスに設定するデータを取得
            //        List<string> majorclassification = majorClassificationDataAccess.McGetComboboxText();

            //        // コンボボックスにデータを追加
            //        foreach (var mcname in majorclassification)
            //        {
            //            cbxMcName.Items.Add(mcname);
            //        }

            //        // SmallClassificationDataAccessクラスのインスタンスを作成
            //        SmallClassificationDataAccess smallClassificationDataAccess = new SmallClassificationDataAccess();
            //        // データベースからコンボボックスに設定するデータを取得
            //        List<string> smallclassification = smallClassificationDataAccess.ScGetComboboxText();

            //        // コンボボックスにデータを追加
            //        foreach (var scname in smallclassification)
            //        {
            //            cbxScName.Items.Add(scname);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"データベース接続エラー。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }


        //商品名tbxと商品IDtbxの紐づけ（商品名入力時、IDtbxに表示）
        private void tbxPrName_TextChanged(object sender, EventArgs e)
        {
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(tbxPrName.Text))
            {
                return;
            }

            if (PrUpdating) return;
            PrUpdating = true;

            // テキストボックスに入力された商品名を取得
            string prName = tbxPrName.Text.Trim();

            // 商品名に対応する商品IDを取得
            int? prId = productDataAccess.GetPrIdByName(prName);

            if (prId.HasValue)
            {
                // 商品IDが見つかった場合、tbxPrIDに表示
                tbxPrID.Text = prId.Value.ToString();
            }
            else
            {
                // 商品名に対応する商品IDが見つからなかった場合
                tbxPrID.Clear(); // 商品IDが見つからない場合はクリア
            }

            PrUpdating = false;
        }


        //商品名tbxと商品IDtbxの紐づけ（商品名入力時、IDtbxに表示）
        private void tbxPrID_TextChanged(object sender, EventArgs e)
        {
            //入力クリア時に、エラーメッセージを表示させないようにする
            if (string.IsNullOrWhiteSpace(tbxPrID.Text))
            {
                return;
            }

            if (PrUpdating) return;
            PrUpdating = true;

            // テキストボックスに入力された商品名を取得
            string prId = tbxPrID.Text.Trim();

            // 商品名に対応する商品IDを取得
            string prName = productDataAccess.GetPrNameByPrId(prId);

            if (!string.IsNullOrEmpty(prName))
            {
                // コンボボックスの選択肢にメーカ名を設定
                tbxPrName.Text = prName.ToString();
            }
            else
            {
                // 商品名に対応する商品IDが見つからなかった場合
                tbxPrName.Clear(); // 商品IDが見つからない場合はクリア
            }

            PrUpdating = false;
        }


        //メーカ名cbxとメーカIDtbxの紐づけ（メーカ名cbxで選択されたメーカIDをtbxに表示）
        private void cbxMaName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxMaName.SelectedIndex >= 0)
            {
                tbxMaID.Text = cbxMaName.SelectedValue.ToString();
            }
            else
            {
                tbxMaID.Text = "";
            }

            ////入力クリア時に、エラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(cbxMaName.Text))
            //{
            //    return;
            //}
            //// コンボボックスで選択されたアイテムがnullでないこと(選択されているか)を確認
            //if (cbxMaName.SelectedIndex != -1)
            //{
            //    //自動入力のループ対策
            //    if (MaUpdating) return;
            //    MaUpdating = true;

            //    // コンボボックスで選択されたメーカ名を取得
            //    string selectedMaName = cbxMaName.SelectedItem.ToString();

            //    //メーカIDを取得してテキストボックスに表示する
            //    int? Maid = makerDataAccess.GetMaIdByName(selectedMaName);
            //    if (Maid.HasValue)
            //    {
            //        tbxMaID.Text = Maid.Value.ToString();
            //    }
            //    else
            //    {
            //        MessageBox.Show($"メーカIDを取得できませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

            //MaUpdating = false;
        }


        //メーカ名cbxとメーカIDtbxの紐づけ（入力されたメーカIDに対応するメーカ名をメーカ名cbxに表示）
        private void tbxMaID_TextChanged(object sender, EventArgs e)
        {
            string text = tbxMaID.Text;
            bool flg = false;

            //コンボボックスに入っているデータ全部を調べることが出来る
            foreach (var item in cbxMaName.Items)
            {
                if (item is SalesManagement_SysDev.MMaker mMaker)
                {
                    if (mMaker.MaId.ToString() == text)
                    {
                        cbxMaName.SelectedItem = mMaker;
                        flg = true;
                        break;
                    }
                }
            }

            //一致しなかった場合
            if (!flg)
            {
                cbxMaName.SelectedIndex = -1;
            }

            ////入力クリア時に、エラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(tbxMaID.Text))
            //{
            //    cbxMaName.SelectedIndex = -1;
            //    return;
            //}

            //// 自動入力のループ対策
            //if (MaUpdating) return;
            //MaUpdating = true;

            //// テキストボックスに入力されたメーカIDを取得
            //if (int.TryParse(tbxMaID.Text, out int maID))
            //{
            //    // メーカIDに対応するメーカ名を取得
            //    string maName = makerDataAccess.GetMaNameById(maID);

            //    if (!string.IsNullOrEmpty(maName))
            //    {
            //        // コンボボックスの選択肢にメーカ名を設定
            //        cbxMaName.SelectedItem = maName;
            //    }
            //    else
            //    {
            //        cbxMaName.SelectedIndex = -1;
            //        MessageBox.Show($"入力されたメーカは存在しません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        tbxMaID.Clear();
            //    }
            //}
            //MaUpdating = false;
        }



        //大分類・小分類cbxの設定（小分類を選択すると、自動的に大分類コンボボックス項目を表示する）
        private void cbxScName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value;
            string select = cbxScName.Text;

            if (cbxScName.SelectedIndex >= 0)
            {
                value = cbxScName.SelectedValue.ToString();

                foreach (var item in cbxMcName.Items)
                {
                    if (item is SalesManagement_SysDev.MMajorClassification mMajorClass)
                    {
                        if (mMajorClass.McId.ToString() == value)
                        {
                            cbxMcName.SelectedItem = mMajorClass;
                            cbxScName.Text = select;
                            break;
                        }
                    }
                }
            }
            else
            {
                cbxMcName.SelectedIndex = -1;
            }

            //// 入力クリア時にエラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(cbxScName.Text))
            //{
            //    return;
            //}

            //// コンボボックスで選択されたアイテムが null でないこと(選択されているか)を確認
            //if (cbxScName.SelectedIndex != -1)
            //{
            //    // 自動入力のループ対策
            //    if (ScUpdating) return;
            //    ScUpdating = true;

            //    // コンボボックスで選択された小分類名を取得
            //    string selectedScName = cbxScName.SelectedItem.ToString();

            //    // 小分類名（ScName）から McID を取得
            //    var mcid = smallClassificationDataAccess.GetMcIdByScName(selectedScName);

            //    if (mcid != null)
            //    {
            //        // McID に基づいて大分類名（McName）を取得
            //        string mcName = smallClassificationDataAccess.GetMcNameByMcId(mcid.Value);

            //        // cbxMcName に大分類名（McName）を表示
            //        cbxMcName.SelectedItem = mcName;
            //    }
            //    else
            //    {
            //        // McID が見つからない場合
            //        cbxMcName.SelectedItem = null;  // 選択肢が見つからなかった場合、コンボボックスを空にする
            //    }
            //}

            //ScUpdating = false;
        }

        //大分類・小分類cbxの設定（大分類項目を選択すると、選択できる小分類項目が絞り込まれる機能）
        private void cbxMcName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int McID = -1;

            if (cbxMcName.SelectedIndex >= 0)
            {
                if (cbxMcName.SelectedValue == null)
                {
                    McID = -1;
                }
                else
                {
                    if (int.TryParse(cbxMcName.SelectedValue.ToString(), out McID))
                    {
                        // 変換成功
                    }
                    else
                    {
                        // 変換失敗時の処理
                        McID = -1;
                    }
                }
                Smallclass = smallClassificationDataAccess.GetSmallClassDspData(McID);
                cbxScName.DataSource = Smallclass;
                //cbxScName.SelectedIndex = -1;
            }
            else
            {
                Smallclass = smallClassificationDataAccess.GetSmallClassDspData(McID);
                cbxScName.SelectedIndex = -1;
            }

            //// 入力クリア時にエラーメッセージを表示させないようにする
            //if (string.IsNullOrWhiteSpace(cbxMcName.Text))
            //{
            //    return;
            //}

            //// 大分類名が選択されたか確認
            //if (cbxMcName.SelectedIndex != -1)
            //{
            //    if (McUpdating) return;
            //    McUpdating = true;

            //    // 選択された大分類名を取得
            //    string selectedMcName = cbxMcName.SelectedItem.ToString();

            //    // MajorClassificationDataAccess を使って McID を取得
            //    var mcid = majorClassificationDataAccess.GetMcIdByMcName(selectedMcName);

            //    if (mcid != null)
            //    {
            //        // 小分類データを取得
            //        var smallClassifications = smallClassificationDataAccess.GetSmallClassificationsByMcId(mcid.Value);

            //        // 現在選択されている小分類名を保持
            //        var selectedScName = cbxScName.SelectedItem?.ToString();

            //        // cbxScName をクリア
            //        cbxScName.Items.Clear();

            //        // 小分類コンボボックスに小分類名を追加
            //        foreach (var smallClass in smallClassifications)
            //        {
            //            cbxScName.Items.Add(smallClass.ScName);
            //        }

            //        // 小分類の選択状態を復元（選択されていた小分類があれば再選択）
            //        if (!string.IsNullOrWhiteSpace(selectedScName))
            //        {
            //            var index = cbxScName.Items.IndexOf(selectedScName);
            //            if (index != -1)
            //            {
            //                cbxScName.SelectedIndex = index; // 再選択
            //            }
            //        }
            //    }
            //}

            //McUpdating = false;
        }


        //各テキストボックスの入力チェック
        //半角数字チェック（商品ID）
        private void tbxPrID_KeyPress(object sender, KeyPressEventArgs e)
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
                    MessageBox.Show("商品IDは半角数字です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //半角数字チェック（メーカID）
        private void tbxMaID_KeyPress(object sender, KeyPressEventArgs e)
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
                    MessageBox.Show("メーカIDは半角数字です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //半角数字チェック（価格）
        private void tbxPrice_KeyPress(object sender, KeyPressEventArgs e)
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
                    MessageBox.Show("価格は半角数字です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //半角数字チェック（安全在庫数）
        private void tbxPrSafetyStock_KeyPress(object sender, KeyPressEventArgs e)
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
                    MessageBox.Show("安全在庫数は半角数字です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //半角数字チェック（型番）
        private void tbxPrModelNumber_KeyPress(object sender, KeyPressEventArgs e)
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
                    MessageBox.Show("型番は半角数字です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //全角文字チェック（色）
        private void tbxColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // バックスペースは許可
            if (char.IsControl(e.KeyChar))
                return;

            // 全角文字（ひらがな、カタカナ、漢字など）の範囲をチェック
            if ((e.KeyChar >= 0x3040 && e.KeyChar <= 0x309F) ||  // ひらがな
                (e.KeyChar >= 0x30A0 && e.KeyChar <= 0x30FF) ||  // カタカナ
                (e.KeyChar >= 0x4E00 && e.KeyChar <= 0x9FFF))    // 漢字（基本的な範囲）
            {
                return; // 全角文字（ひらがな、カタカナ、漢字）は許可
            }
            else
            {
                // 全角数字は無効なのでエラーメッセージを表示
                MessageBox.Show("色は全角文字のみ入力可能です。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true; // 無効な入力をキャンセル
                PrUpdating = false;
                return;
            }
        }


        //4.1 登録機能
        private void btnRegist_Click(object sender, EventArgs e)
        {
            // 商品情報を得る
            if (!GetValidDataAtRegistration())
            {
                return;
            }

            // 商品IDが既にデータベースに存在するかチェック
            bool isProductIdExists = productDataAccess.IsProductIdExists(tbxPrID.Text);
            if (isProductIdExists)
            {
                MessageBox.Show("入力された商品ID、商品名は既に存在しています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                PrUpdating = false;
                return;
            }

            // 商品情報作成
            var regProduct = GenerateDataAtRegistration();

            // 商品情報登録
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "入力されている商品情報を登録しますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );
            if (result == DialogResult.No)
            {
                return;
            }

            bool flg = productDataAccess.AddProductData(regProduct);
            if (flg == true)
            {
                MessageBox.Show("データの登録に成功しました", "登録成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearPr();
            }
            else
            {
                MessageBox.Show("データの登録に失敗しました", "登録失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            GetDataGridView();

            //try
            //{
            //    //自動入力のメッセージ対策
            //    if (PrUpdating) return;
            //    PrUpdating = true;

            //    //入力された商品情報を取得
            //    string prids = tbxPrID.Text;
            //    string prnames = tbxPrName.Text;
            //    string maids = tbxMaID.Text;
            //    string prices = tbxPrice.Text;
            //    string colors = tbxColor.Text;
            //    //string prjcodes = tbxPrJCode.Text;
            //    string prmodelnumbers = tbxPrModelNumber.Text;
            //    string prsafetystocks = tbxPrSafetyStock.Text;


            //    int hiflgindex = PrFlgNum();

            //    int mcname = int.Parse(cbxMcName.SelectedValue.ToString());
            //    int maname = int.Parse(tbxMaID.Text);

            //    DateTime prdate = dtpReleaseDate.Value;

            //    // コンボボックスで選択された小分類名を取得
            //    //string selectedScName = cbxScName.SelectedItem.ToString();
            //    // 小分類名（ScName）から ScID を取得
            //    var scids = smallClassificationDataAccess.GetSmallClassIDData(int.Parse(cbxScName.SelectedValue.ToString()), cbxScName.SelectedItem.ToString());

            //    //商品IDtbxが空白の場合処理を実行
            //    if (string.IsNullOrEmpty(prids))
            //    {
            //        //必須項目に空白がある場合、エラー表示
            //        if (string.IsNullOrWhiteSpace(prnames) ||
            //        string.IsNullOrWhiteSpace(maids) ||
            //        string.IsNullOrWhiteSpace(prices) ||
            //        string.IsNullOrWhiteSpace(colors) ||
            //        //(prjcodes != "") ||
            //        //string.IsNullOrWhiteSpace(prjcodes) ||
            //        !scids.HasValue || scids.Value == 0 ||
            //        string.IsNullOrWhiteSpace(prmodelnumbers) ||
            //        string.IsNullOrWhiteSpace(prsafetystocks) ||
            //        hiflgindex == -1 ||
            //        mcname == -1 || maname == -1 || clear != false)
            //        {
            //            MessageBox.Show("必須項目を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //            cbxPrFlag.SelectedIndex = -1;
            //            PrUpdating = false;
            //            return;
            //        }



            //        //商品情報をint型に変換          
            //        //int prid = int.Parse(prids);
            //        int maid = int.Parse(maids);
            //        int price = int.Parse(prices);

            //        int scid = (int)scids;

            //        string color = colors;
            //        //string prjcode = prjcodes
            //        string prmodelnumber = prmodelnumbers;
            //        int prsafetystock = int.Parse(prsafetystocks);

            //        //商品テーブルに新規登録
            //        var newProduct = productDataAccess.AddNewProduct(prnames, maid, price, color, scid, prmodelnumber,
            //                                                        prsafetystock, prdate, hiflgindex);

            //        //データグリッドビューに表示
            //        if (newProduct != null)
            //        {
            //            try
            //            {
            //                // 新規商品を BindingList に追加
            //                BindingList<MProduct> newProductList = new BindingList<MProduct> { newProduct };

            //                // DataGridView のデータソースを BindingList に設定
            //                dgvPr.DataSource = newProductList;

            //                // データグリッドビューの列ヘッダー名を変更
            //                dgvPr.Columns["PrID"].HeaderText = "商品ID";
            //                dgvPr.Columns["MaID"].HeaderText = "メーカID";
            //                dgvPr.Columns["PrName"].HeaderText = "商品名";
            //                dgvPr.Columns["Price"].HeaderText = "価格";
            //                dgvPr.Columns["PrJCode"].HeaderText = "JANコード";
            //                dgvPr.Columns["PrSafetyStock"].HeaderText = "安全在庫数";
            //                dgvPr.Columns["ScID"].HeaderText = "小分類ID";
            //                dgvPr.Columns["PrModelNumber"].HeaderText = "型番";
            //                dgvPr.Columns["PrColor"].HeaderText = "色";
            //                dgvPr.Columns["PrReleaseDate"].HeaderText = "発売日";
            //                dgvPr.Columns["PrHidden"].HeaderText = "非表示理由";

            //                //PrFlag(表示/非表示)の列を非表示にする
            //                dgvPr.Columns["PrFlag"].Visible = false;

            //                // 列幅を設定
            //                dgvPr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //                dgvPr.Columns["PrHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //                dgvPr.Columns["PrHidden"].Width = 500;

            //                // ユーザーが編集や削除をできないように設定
            //                dgvPr.ReadOnly = true;  // 編集不可
            //                dgvPr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //                dgvPr.MultiSelect = false;  // 複数行選択を無効
            //                dgvPr.AllowUserToAddRows = false;  // 行の追加を無効
            //                dgvPr.AllowUserToDeleteRows = false;  // 行の削除を無効
            //                dgvPr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //                dgvPr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //                // 行番号の列や選択列のサイズ変更を無効にする
            //                dgvPr.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //                //不要な要素を非表示
            //                dgvPr.Columns["Sc"].Visible = false;
            //                dgvPr.Columns["Ma"].Visible = false;
            //                dgvPr.Columns["TArrivalDetails"].Visible = false;
            //                dgvPr.Columns["TChumonDetails"].Visible = false;
            //                dgvPr.Columns["THattyuDetails"].Visible = false;
            //                dgvPr.Columns["TOrderDetails"].Visible = false;
            //                dgvPr.Columns["TSaleDetails"].Visible = false;
            //                dgvPr.Columns["TShipmentDetails"].Visible = false;
            //                dgvPr.Columns["TStocks"].Visible = false;
            //                dgvPr.Columns["TSyukkoDetails"].Visible = false;
            //                dgvPr.Columns["TWarehousingDetails"].Visible = false;
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBox.Show($"データの表示中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //        }

            //        PrUpdating = false;
            //        return;
            //    }



            //    // 商品IDは自動生成する旨のメッセージ
            //    else
            //    {
            //        //商品IDが入力されており、データベースに存在していない場合
            //        MessageBox.Show("商品IDは自動で振り分けられます。");
            //        PrUpdating = false;
            //        return;
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("すべての項目を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    PrUpdating = false;
            //    return;
            //}
        }

        //商品情報を得る
        private bool GetValidDataAtRegistration()
        {
            //商品名
            if (!String.IsNullOrEmpty(tbxPrName.Text.Trim()))
            {
                //文字数チェック
                //50文字以下
                if (tbxPrName.Text.Length > 50)
                {
                    MessageBox.Show("商品名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrName.Focus();
                    return false;
                }
            }
            else
            {
                //商品名テキストボックスが空
                MessageBox.Show("商品名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrName.Focus();
                return false;
            }

            //メーカーID
            if (!String.IsNullOrEmpty(tbxMaID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxMaID.Text.Trim()))
                {
                    MessageBox.Show("メーカーIDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxMaID.Focus();
                    return false;
                }
                //メーカーIDの存在チェック
                if (!makerDataAccess.CheckMakerIDExistence(int.Parse(tbxMaID.Text.Trim())))
                {
                    MessageBox.Show("入力されたメーカーIDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxMaID.Focus();
                    return false;
                }
            }
            else
            {
                //商品名テキストボックスが空
                MessageBox.Show("メーカーIDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxMaID.Focus();
                return false;
            }
            //メーカー名
            if (cbxMaName.SelectedIndex == -1)
            {
                MessageBox.Show("メーカー名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxMaName.Focus();
                return false;
            }

            //価格
            if (!String.IsNullOrEmpty(tbxPrice.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrice.Text.Trim()))
                {
                    MessageBox.Show("価格は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrice.Focus();
                    return false;
                }
                //桁数チェック
                //9桁
                if (tbxPrice.Text.Length > 9)
                {
                    MessageBox.Show("価格は9桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrice.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("価格を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrice.Focus();
                return false;
            }

            //色
            if (!String.IsNullOrEmpty(tbxColor.Text.Trim()))
            {
                //全角チェック
                if (!dataInputFormCheck.CheckFullWidth(tbxColor.Text.Trim()))
                {
                    MessageBox.Show("色は全角で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxColor.Focus();
                    return false;
                }
                //文字数チェック
                //20文字以下
                if (tbxColor.Text.Length > 20)
                {
                    MessageBox.Show("色は20文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxColor.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("色を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxColor.Focus();
                return false;
            }

            //型番
            if (!String.IsNullOrEmpty(tbxPrModelNumber.Text.Trim()))
            {
                //数値チェック
                //if (!dataInputFormCheck.CheckNumeric(tbxPrModelNumber.Text.Trim()))
                //{
                //    MessageBox.Show("型番は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    tbxPrModelNumber.Focus();
                //    return false;
                //}
                //文字数チェック
                //20文字以下
                if (tbxPrModelNumber.Text.Length > 20)
                {
                    MessageBox.Show("型番は20文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrModelNumber.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("型番を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrModelNumber.Focus();
                return false;
            }

            //安全在庫数
            if (!String.IsNullOrEmpty(tbxPrSafetyStock.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrSafetyStock.Text.Trim()))
                {
                    MessageBox.Show("安全在庫数は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrSafetyStock.Focus();
                    return false;
                }
                //4桁以下
                if (tbxPrSafetyStock.Text.Length > 4)
                {
                    MessageBox.Show("安全在庫数は4桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrSafetyStock.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("安全在庫数を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrSafetyStock.Focus();
                return false;
            }

            //小分類名
            if (cbxScName.SelectedIndex == -1)
            {
                MessageBox.Show("小分類名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxScName.Focus();
                return false;
            }

            //発売日
            if (dtpReleaseDate.CustomFormat == " ")
            {
                MessageBox.Show("発売日を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpReleaseDate.Focus();
                return false;
            }

            return true;
        }

        // 商品情報作成
        private MProduct GenerateDataAtRegistration()
        {
            return new MProduct
            {
                PrName = tbxPrName.Text.Trim(),
                MaId = int.Parse(tbxMaID.Text.Trim()),
                Price = decimal.Parse(tbxPrice.Text.Trim()),
                PrJcode = null,
                PrColor = tbxColor.Text.Trim(),
                PrModelNumber = tbxPrModelNumber.Text.Trim(),
                PrSafetyStock = int.Parse(tbxPrSafetyStock.Text.Trim()),
                ScId = smallClassificationDataAccess.GetSmallClassIDData(int.Parse(cbxScName.SelectedValue.ToString()), cbxScName.Text),
                PrReleaseDate = dtpReleaseDate.Value,
                PrFlag = 0,
                PrHidden = null
            };
        }

        //4.2 更新機能
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 商品情報を得る
            if (!GetValidDataAtUpdate())
            {
                return;
            }

            //商品情報作成
            var updProduct = GenerateDataAtUpdate();

            // 現在の情報と入力された情報を比較して変更がないか確認
            bool isChanged = false;

            // 商品の現在の情報を取得
            var currentProduct = productDataAccess.GetProduct(int.Parse(tbxPrID.Text));

            if (currentProduct.PrName != tbxPrName.Text) isChanged = true;
            if (currentProduct.MaId != int.Parse(tbxMaID.Text)) isChanged = true;
            if (currentProduct.Price != decimal.Parse(tbxPrice.Text)) isChanged = true;
            if (currentProduct.PrColor != tbxColor.Text) isChanged = true;
            if (currentProduct.PrModelNumber != tbxPrModelNumber.Text) isChanged = true;
            if (currentProduct.PrSafetyStock != int.Parse(tbxPrSafetyStock.Text)) isChanged = true;
            if (currentProduct.ScId != smallClassificationDataAccess.GetSmallClassIDData
                (int.Parse(cbxScName.SelectedValue.ToString()), cbxScName.Text)) isChanged = true;
            if (currentProduct.PrReleaseDate != dtpReleaseDate.Value) isChanged = true;
            if (currentProduct.PrFlag != PrFlgNum()) isChanged = true;
            if (currentProduct.PrHidden != PrHidden()) isChanged = true;

            if (!isChanged)
            {
                MessageBox.Show("変更された項目がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PrUpdating = false;
                return;
            }

            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "商品情報を更新しますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );
            if (result == DialogResult.No)
            {
                return;
            }

            bool flg = productDataAccess.UpdateProductData(updProduct);
            if (flg == true)
            {
                MessageBox.Show("データの更新に成功しました", "更新成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearPr();
            }
            else
            {
                MessageBox.Show("データの更新に失敗しました", "更新失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            GetDataGridView();

            //try
            //{
            //    //自動入力のメッセージ対策
            //    if (PrUpdating) return;
            //    PrUpdating = true;

            //    //入力された商品情報を取得
            //    string prids = tbxPrID.Text;
            //    string prnames = tbxPrName.Text;
            //    string maids = tbxMaID.Text;
            //    string prices = tbxPrice.Text;
            //    string colors = tbxColor.Text;
            //    //string prjcodes = tbxPrJCode.Text;
            //    string prmodelnumbers = tbxPrModelNumber.Text;
            //    string prsafetystocks = tbxPrSafetyStock.Text;
            //    string prhidden = tbxPrHidden.Text;

            //    int hiflgindex;
            //    if (cbxPrFlag.SelectedIndex == 0)
            //    {
            //        hiflgindex = 0;
            //    }
            //    else
            //    {
            //        hiflgindex = 2;
            //    }

            //    DateTime prdate = dtpReleaseDate.Value;



            //    // コンボボックスで選択された小分類名を取得
            //    string selectedScName = cbxScName.SelectedItem.ToString();
            //    // 小分類名（ScName）から ScID を取得
            //    var scids = smallClassificationDataAccess.GetScIdByScName(selectedScName);

            //    int mcname = cbxMcName.SelectedIndex;
            //    int maname = cbxMaName.SelectedIndex;

            //    // 空白チェック
            //    if (string.IsNullOrWhiteSpace(prids) ||
            //        string.IsNullOrWhiteSpace(prnames) ||
            //        string.IsNullOrWhiteSpace(maids) ||
            //        string.IsNullOrWhiteSpace(prices) ||
            //        string.IsNullOrWhiteSpace(colors) ||
            //        //(prjcodes != "") ||
            //        //string.IsNullOrWhiteSpace(prjcodes) ||
            //        !scids.HasValue || scids.Value == 0 ||
            //        string.IsNullOrWhiteSpace(prmodelnumbers) ||
            //        string.IsNullOrWhiteSpace(prsafetystocks) ||
            //        hiflgindex == -1 ||
            //        mcname == -1 || maname == -1 || clear != false)
            //    {
            //        MessageBox.Show("すべての項目を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        PrUpdating = false;
            //        return;
            //    }

            //    //注文情報をint型に変換          
            //    int prid = int.Parse(prids);
            //    int maid = int.Parse(maids);
            //    int price = int.Parse(prices);
            //    int scid = (int)scids; // ただし、nullableIntがnullの場合は例外が発生します

            //    string color = colors;
            //    //string prjcode = prjcodes
            //    string prmodelnumber = prmodelnumbers;
            //    int prsafetystock = int.Parse(prsafetystocks);

            //    // 更新対象の商品情報をデータベースに反映
            //    var newProduct = productDataAccess.UpdateProduct(prid, prnames, maid, price, color, scid, prmodelnumber,
            //                                                    prsafetystock, prdate, hiflgindex, prhidden);


            //    MessageBox.Show("商品情報が更新されました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //データグリッドビューに表示
            //    if (newProduct != null)
            //    {
            //        try
            //        {
            //            // 新規商品を BindingList に追加
            //            BindingList<MProduct> newProductList = new BindingList<MProduct> { newProduct };

            //            // DataGridView のデータソースを BindingList に設定
            //            dgvPr.DataSource = newProductList;

            //            // データグリッドビューの列ヘッダー名を変更
            //            dgvPr.Columns["PrID"].HeaderText = "商品ID";
            //            dgvPr.Columns["MaID"].HeaderText = "メーカID";
            //            dgvPr.Columns["PrName"].HeaderText = "商品名";
            //            dgvPr.Columns["Price"].HeaderText = "価格";
            //            dgvPr.Columns["PrJCode"].HeaderText = "JANコード";
            //            dgvPr.Columns["PrSafetyStock"].HeaderText = "安全在庫数";
            //            dgvPr.Columns["ScID"].HeaderText = "小分類ID";
            //            dgvPr.Columns["PrModelNumber"].HeaderText = "型番";
            //            dgvPr.Columns["PrColor"].HeaderText = "色";
            //            dgvPr.Columns["PrReleaseDate"].HeaderText = "発売日";
            //            dgvPr.Columns["PrHidden"].HeaderText = "非表示理由";

            //            //PrFlag(表示/非表示)の列を非表示にする
            //            dgvPr.Columns["PrFlag"].Visible = false;

            //            // 列幅を設定
            //            dgvPr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //            dgvPr.Columns["PrHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //            dgvPr.Columns["PrHidden"].Width = 500;

            //            // ユーザーが編集や削除をできないように設定
            //            dgvPr.ReadOnly = true;  // 編集不可
            //            dgvPr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //            dgvPr.MultiSelect = false;  // 複数行選択を無効
            //            dgvPr.AllowUserToAddRows = false;  // 行の追加を無効
            //            dgvPr.AllowUserToDeleteRows = false;  // 行の削除を無効
            //            dgvPr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //            dgvPr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //            // 行番号の列や選択列のサイズ変更を無効にする
            //            dgvPr.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //            //不要な要素を非表示
            //            dgvPr.Columns["Sc"].Visible = false;
            //            dgvPr.Columns["Ma"].Visible = false;
            //            dgvPr.Columns["TArrivalDetails"].Visible = false;
            //            dgvPr.Columns["TChumonDetails"].Visible = false;
            //            dgvPr.Columns["THattyuDetails"].Visible = false;
            //            dgvPr.Columns["TOrderDetails"].Visible = false;
            //            dgvPr.Columns["TSaleDetails"].Visible = false;
            //            dgvPr.Columns["TShipmentDetails"].Visible = false;
            //            dgvPr.Columns["TStocks"].Visible = false;
            //            dgvPr.Columns["TSyukkoDetails"].Visible = false;
            //            dgvPr.Columns["TWarehousingDetails"].Visible = false;

            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show($"更新情報が表示できません ", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //        PrUpdating = false;
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //PrUpdating = false;
            //return;
        }

        //4.3 一覧表示機能
        private void btnList_Click(object sender, EventArgs e)
        {
            //データグリッドビューの表示
            SetFormDataGridView();

            //// 全商品データを取得
            //var allProductData = productDataAccess.GetProductData();  // 全商品情報を取得

            //// 取得したデータ（例えばPrFlag == 2）の商品データを取得
            //var selectedProductData = productDataAccess.GetProductData(2);  // 例えば PrFlag が 2 の商品データを取得

            //// selectedProductDataのPrIDを抽出
            //var selectedProductIds = selectedProductData.Select(p => p.PrId).ToList();

            //// selectedProductIdsに含まれない商品をフィルタリング
            //var filteredProduct = allProductData.Where(p => !selectedProductIds.Contains(p.PrId)).ToList();

            //// 選択を解除
            //foreach (DataGridViewRow row in dgvPr.SelectedRows)
            //{
            //    row.Selected = false;
            //}

            //// 取得したデータをDataGridViewに表示
            //if (filteredProduct != null && filteredProduct.Count > 0)
            //{
            //    dgvPr.DataSource = filteredProduct;
            //    // データグリッドビューの列ヘッダー名を変更
            //    dgvPr.Columns["PrID"].HeaderText = "商品ID";
            //    dgvPr.Columns["MaID"].HeaderText = "メーカID";
            //    dgvPr.Columns["PrName"].HeaderText = "商品名";
            //    dgvPr.Columns["Price"].HeaderText = "価格";
            //    dgvPr.Columns["PrJCode"].HeaderText = "JANコード";
            //    dgvPr.Columns["PrSafetyStock"].HeaderText = "安全在庫数";
            //    dgvPr.Columns["ScID"].HeaderText = "小分類ID";
            //    dgvPr.Columns["PrModelNumber"].HeaderText = "型番";
            //    dgvPr.Columns["PrColor"].HeaderText = "色";
            //    dgvPr.Columns["PrReleaseDate"].HeaderText = "発売日";
            //    dgvPr.Columns["PrHidden"].HeaderText = "非表示理由";

            //    //PrFlag(表示/非表示)の列を非表示にする
            //    dgvPr.Columns["PrFlag"].Visible = false;

            //    // 列幅を設定
            //    dgvPr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //    dgvPr.Columns["PrHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //    dgvPr.Columns["PrHidden"].Width = 500;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvPr.ReadOnly = true;  // 編集不可
            //    dgvPr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //    dgvPr.MultiSelect = false;  // 複数行選択を無効
            //    dgvPr.AllowUserToAddRows = false;  // 行の追加を無効
            //    dgvPr.AllowUserToDeleteRows = false;  // 行の削除を無効
            //    dgvPr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //    dgvPr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvPr.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //    //不要な要素を非表示
            //    dgvPr.Columns["Sc"].Visible = false;
            //    dgvPr.Columns["Ma"].Visible = false;
            //    dgvPr.Columns["TArrivalDetails"].Visible = false;
            //    dgvPr.Columns["TChumonDetails"].Visible = false;
            //    dgvPr.Columns["THattyuDetails"].Visible = false;
            //    dgvPr.Columns["TOrderDetails"].Visible = false;
            //    dgvPr.Columns["TSaleDetails"].Visible = false;
            //    dgvPr.Columns["TShipmentDetails"].Visible = false;
            //    dgvPr.Columns["TStocks"].Visible = false;
            //    dgvPr.Columns["TSyukkoDetails"].Visible = false;
            //    dgvPr.Columns["TWarehousingDetails"].Visible = false;
            //}
            //else
            //{
            //    MessageBox.Show("表示するデータがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}


        }

        // 商品情報を得る
        private bool GetValidDataAtUpdate()
        {
            //商品ID
            if (!String.IsNullOrEmpty(tbxPrID.Text.Trim()))
            {
                //文字チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrID.Text.Trim()))
                {
                    MessageBox.Show("商品IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrID.Focus();
                    return false;
                }
                //6桁以内
                if (tbxPrID.Text.Length > 6)
                {
                    MessageBox.Show("商品IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            //商品名
            if (!String.IsNullOrEmpty(tbxPrName.Text.Trim()))
            {
                //文字数チェック
                //50文字以下
                if (tbxPrName.Text.Length > 50)
                {
                    MessageBox.Show("商品名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrName.Focus();
                    return false;
                }
            }
            else
            {
                //商品名テキストボックスが空
                MessageBox.Show("商品名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrName.Focus();
                return false;
            }

            //メーカーID
            if (!String.IsNullOrEmpty(tbxMaID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxMaID.Text.Trim()))
                {
                    MessageBox.Show("メーカーIDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxMaID.Focus();
                    return false;
                }
                //メーカーIDの存在チェック
                if (!makerDataAccess.CheckMakerIDExistence(int.Parse(tbxMaID.Text.Trim())))
                {
                    MessageBox.Show("入力されたメーカーIDは存在しません", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxMaID.Focus();
                    return false;
                }
            }
            else
            {
                //商品名テキストボックスが空
                MessageBox.Show("メーカーIDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxMaID.Focus();
                return false;
            }
            //メーカー名
            if (cbxMaName.SelectedIndex == -1)
            {
                MessageBox.Show("メーカー名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxMaName.Focus();
                return false;
            }

            //価格
            if (!String.IsNullOrEmpty(tbxPrice.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrice.Text.Trim()))
                {
                    MessageBox.Show("価格は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrice.Focus();
                    return false;
                }
                //桁数チェック
                //9桁
                if (tbxPrice.Text.Length > 9)
                {
                    MessageBox.Show("価格は9桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrice.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("価格を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrice.Focus();
                return false;
            }

            //色
            if (!String.IsNullOrEmpty(tbxColor.Text.Trim()))
            {
                //全角チェック
                if (!dataInputFormCheck.CheckFullWidth(tbxColor.Text.Trim()))
                {
                    MessageBox.Show("色は全角で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxColor.Focus();
                    return false;
                }
                //文字数チェック
                //20文字以下
                if (tbxColor.Text.Length > 20)
                {
                    MessageBox.Show("色は20文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxColor.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("色を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxColor.Focus();
                return false;
            }

            //型番
            if (!String.IsNullOrEmpty(tbxPrModelNumber.Text.Trim()))
            {
                //数値チェック
                //if (!dataInputFormCheck.CheckNumeric(tbxPrModelNumber.Text.Trim()))
                //{
                //    MessageBox.Show("型番は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    tbxPrModelNumber.Focus();
                //    return false;
                //}
                //文字数チェック
                //20文字以下
                if (tbxPrModelNumber.Text.Length > 20)
                {
                    MessageBox.Show("型番は20文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrModelNumber.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("型番を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrModelNumber.Focus();
                return false;
            }

            //安全在庫数
            if (!String.IsNullOrEmpty(tbxPrSafetyStock.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrSafetyStock.Text.Trim()))
                {
                    MessageBox.Show("安全在庫数は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrSafetyStock.Focus();
                    return false;
                }
                //4桁以下
                if (tbxPrSafetyStock.Text.Length > 4)
                {
                    MessageBox.Show("安全在庫数は4桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrSafetyStock.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("安全在庫数を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxPrSafetyStock.Focus();
                return false;
            }

            //小分類名
            if (cbxScName.SelectedIndex == -1)
            {
                MessageBox.Show("小分類名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxScName.Focus();
                return false;
            }

            //発売日
            if (dtpReleaseDate.CustomFormat == " ")
            {
                MessageBox.Show("発売日を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpReleaseDate.Focus();
                return false;
            }

            //非表示理由
            //if (String.IsNullOrEmpty(tbxPrHidden.Text.Trim()))
            //{
            //    if(cbxPrFlag.SelectedIndex == 1)
            //    {
            //        MessageBox.Show("非表示理由を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        tbxPrHidden.Focus();
            //        return false;
            //    }
            //}

            return true;
        }

        //商品情報作成
        private MProduct GenerateDataAtUpdate()
        {
            //更新データのセット
            return new MProduct
            {
                PrId = int.Parse(tbxPrID.Text.Trim()),
                PrName = tbxPrName.Text.Trim(),
                MaId = int.Parse(tbxMaID.Text.Trim()),
                Price = decimal.Parse(tbxPrice.Text.Trim()),
                PrJcode = null,
                PrColor = tbxColor.Text.Trim(),
                PrModelNumber = tbxPrModelNumber.Text.Trim(),
                PrSafetyStock = int.Parse(tbxPrSafetyStock.Text.Trim()),
                ScId = smallClassificationDataAccess.GetSmallClassIDData(int.Parse(cbxScName.SelectedValue.ToString()), cbxScName.Text),
                PrReleaseDate = dtpReleaseDate.Value,
                PrFlag = PrFlgNum(),
                PrHidden = PrHidden()
            };
        }

        //デーグリッドビューに表示された項目を選択するとテキストボックス等に表示される機能
        private void dgvPr_SelectionChanged(object sender, EventArgs e)
        {
            //if (isSearching)
            //    return;

            //自動入力のメッセージ対策
            if (PrUpdating) return;
            PrUpdating = true;

            ClearPr();


            if (dgvPr.SelectedRows.Count > 0)  // 選択された行があるか確認
            {
                // 選択された行の最初の行を取得
                var selectedRow = dgvPr.SelectedRows[0];

                // 各テキストボックスに選択された行のデータを設定
                tbxPrID.Text = selectedRow.Cells["PrID"].Value.ToString();
                tbxMaID.Text = selectedRow.Cells["MaID"].Value.ToString();
                tbxPrName.Text = selectedRow.Cells["PrName"].Value.ToString();
                tbxPrice.Text = selectedRow.Cells["Price"].Value.ToString();
                //tbxPrJCode.Text = selectedRow.Cells["PrJCode"].Value.ToString();
                tbxPrSafetyStock.Text = selectedRow.Cells["PrSafetyStock"].Value.ToString();
                tbxPrModelNumber.Text = selectedRow.Cells["PrModelNumber"].Value.ToString();
                //cbxScName.Text = selectedRow.Cells["ScID"].Value.ToString();
                tbxPrHidden.Text = selectedRow.Cells["PrHidden"].Value?.ToString() ?? "";
                tbxColor.Text = selectedRow.Cells["PrColor"].Value.ToString();
                dtpReleaseDate.Text = selectedRow.Cells["PrReleaseDate"].Value.ToString();

                ////大分類・小分類コンボボックス取得、表示
                ////データグリッドビューで選択された大分類IDを取得
                var mcId = selectedRow.Cells[11].Value;
                //// ScID の列名を確認してください

                ////小分類名を取得
                var scName = selectedRow.Cells[12].Value.ToString();

                //取得した小分類名をコンボボックスに表示
                if (scName != null)
                {
                    cbxMcName.SelectedValue = mcId;
                    cbxScName.Text = scName;
                }
                else
                {
                    MessageBox.Show("表示するデータがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PrUpdating = false;
                    return;
                }

                //小分類名を取得し、大分類コンボボックスに表示
                // 小分類ID を使って、大分類ID を取得
                //string mcId = smallClassificationDataAccess.GetMcIdByScId(scId);

                //// 大分類ID から大分類名を取得
                //string mcName = majorClassificationDataAccess.GetMcNameByMcId(mcId);

                //// 大分類名を cbxMcName コンボボックスに表示
                //if (mcName != null)
                //{
                //    cbxMcName.SelectedItem = mcName;
                //}
            }


            if (dgvPr.SelectedRows.Count > 0) // 行が選択されている場合
            {
                var selectedRow = dgvPr.SelectedRows[0];

                // 非表示の列からフラグ値を取得
                var flagPr = selectedRow.Cells["PrFlag"].Value;
                if (flagPr != null && int.TryParse(flagPr.ToString(), out int flgch))
                {
                    if (flgch == 2)
                    {
                        cbxPrFlag.SelectedIndex = 1;
                    }
                    else if (flgch == 0)
                    {
                        cbxPrFlag.SelectedIndex = 0;
                    }
                    else
                    {
                        cbxPrFlag.SelectedItem = null;
                    }
                }
                else
                {
                    cbxPrFlag.SelectedItem = null;
                }
                // ComboBox を変更可能にする
                cbxPrFlag.Enabled = true;
            }
            PrUpdating = false;
        }


        //4.4 検索機能
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 商品情報を得る
            if (!GetValidDataAtSlect())
            {
                return;
            }

            // 商品情報抽出
            GenerateDataAtSelect();

            // 商品抽出結果表示
            SetSelectData();

            //isSearching = true;

            //// 商品情報での入力値を取得
            //int? prId = string.IsNullOrWhiteSpace(tbxPrID.Text) ? (int?)null : int.Parse(tbxPrID.Text);
            //int? maId = string.IsNullOrWhiteSpace(tbxMaID.Text) ? (int?)null : int.Parse(tbxMaID.Text);
            //string prName = string.IsNullOrWhiteSpace(tbxPrName.Text) ? null : tbxPrName.Text;
            //int? price = string.IsNullOrWhiteSpace(tbxPrice.Text) ? (int?)null : int.Parse(tbxPrice.Text);
            //int? prSafetyStock = string.IsNullOrWhiteSpace(tbxPrSafetyStock.Text) ? (int?)null : int.Parse(tbxPrSafetyStock.Text);
            //string prModelNumber = string.IsNullOrWhiteSpace(tbxPrModelNumber.Text) ? null : tbxPrModelNumber.Text;
            //string prColor = string.IsNullOrWhiteSpace(tbxColor.Text) ? null : tbxColor.Text;
            //DateTime? prReleaseDate = dtpReleaseDate.Checked ? dtpReleaseDate.Value.Date : (DateTime?)null;

            //// コンボボックスで選択された小分類名を取得
            //string selectedScName = cbxScName.SelectedItem?.ToString();
            //var scIds = selectedScName != null ? smallClassificationDataAccess.GetScIdByScName2(selectedScName) : null;

            //// コンボボックスで選択された大分類名を取得
            //string selectedMcName = cbxMcName.SelectedItem?.ToString();

            //// 大分類名から大分類IDを取得
            //var mcid = string.IsNullOrWhiteSpace(selectedMcName) ? (int?)null : majorClassificationDataAccess.GetMcIdByMcName(selectedMcName);

            //// mcidが取得できた場合、そのmcidを使って小分類IDを取得
            //List<int> combinedScIds = new List<int>();

            //if (mcid != null)
            //{
            //    // 大分類IDから小分類IDを取得
            //    var smallClassifications = smallClassificationDataAccess.GetSmallClassificationsByMcId(mcid.Value);
            //    // 小分類IDをリストに格納
            //    combinedScIds.AddRange(smallClassifications.Select(sc => sc.ScId));
            //}

            //// 小分類IDが選択されている場合、それを追加
            //if (scIds != null)
            //{
            //    combinedScIds = scIds.ToList();
            //}

            //// 入力が全て空の場合は検索を実行しない
            //if (prId == null && maId == null && prName == null && price == null && prSafetyStock == null &&
            //    prModelNumber == null && prColor == null && prReleaseDate == null && !combinedScIds.Any())
            //{
            //    MessageBox.Show("少なくとも1つの条件を入力してください。", "注意", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    return;
            //}

            //// 商品データを検索（小分類IDを使ってフィルタリング）
            //var searchresult = productDataAccess.SearchPrData(prId, prName, maId, price, prColor, combinedScIds,
            //                                                   prModelNumber, prSafetyStock, prReleaseDate);

            //// 注文情報の結果から有効な注文IDリストを作成
            //List<int> validPrIds = searchresult.Select(pr => pr.PrId).ToList();

            //// 選択を解除
            //foreach (DataGridViewRow row in dgvPr.SelectedRows)
            //{
            //    row.Selected = false;
            //}

            //// 検索結果があれば DataGridView に表示
            //if (searchresult.Any())
            //{
            //    dgvPr.DataSource = searchresult;
            //    // データグリッドビューの列ヘッダー名を変更
            //    dgvPr.Columns["PrID"].HeaderText = "商品ID";
            //    dgvPr.Columns["MaID"].HeaderText = "メーカID";
            //    dgvPr.Columns["PrName"].HeaderText = "商品名";
            //    dgvPr.Columns["Price"].HeaderText = "価格";
            //    dgvPr.Columns["PrJCode"].HeaderText = "JANコード";
            //    dgvPr.Columns["PrSafetyStock"].HeaderText = "安全在庫数";
            //    dgvPr.Columns["ScID"].HeaderText = "小分類ID";
            //    dgvPr.Columns["PrModelNumber"].HeaderText = "型番";
            //    dgvPr.Columns["PrColor"].HeaderText = "色";
            //    dgvPr.Columns["PrReleaseDate"].HeaderText = "発売日";
            //    dgvPr.Columns["PrHidden"].HeaderText = "非表示理由";

            //    //PrFlag(表示/非表示)の列を非表示にする
            //    dgvPr.Columns["PrFlag"].Visible = false;

            //    // 列幅を設定
            //    dgvPr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //    dgvPr.Columns["PrHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //    dgvPr.Columns["PrHidden"].Width = 500;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvPr.ReadOnly = true;  // 編集不可
            //    dgvPr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //    dgvPr.MultiSelect = false;  // 複数行選択を無効
            //    dgvPr.AllowUserToAddRows = false;  // 行の追加を無効
            //    dgvPr.AllowUserToDeleteRows = false;  // 行の削除を無効
            //    dgvPr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //    dgvPr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvPr.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //    //不要な要素を非表示
            //    dgvPr.Columns["Sc"].Visible = false;
            //    dgvPr.Columns["Ma"].Visible = false;
            //    dgvPr.Columns["TArrivalDetails"].Visible = false;
            //    dgvPr.Columns["TChumonDetails"].Visible = false;
            //    dgvPr.Columns["THattyuDetails"].Visible = false;
            //    dgvPr.Columns["TOrderDetails"].Visible = false;
            //    dgvPr.Columns["TSaleDetails"].Visible = false;
            //    dgvPr.Columns["TShipmentDetails"].Visible = false;
            //    dgvPr.Columns["TStocks"].Visible = false;
            //    dgvPr.Columns["TSyukkoDetails"].Visible = false;
            //    dgvPr.Columns["TWarehousingDetails"].Visible = false;
            //}
            //else
            //{
            //    MessageBox.Show("条件に合うデータがありませんでした。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}
            //isSearching = false;
        }
        // 商品情報を得る
        private bool GetValidDataAtSlect()
        {
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

            //商品名
            if (!String.IsNullOrEmpty(tbxPrName.Text.Trim()))
            {
                //50文字以下
                if (tbxPrName.TextLength > 50)
                {
                    MessageBox.Show("商品名は50桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrName.Focus();
                    return false;
                }
            }

            //メーカーID
            if (!String.IsNullOrEmpty(tbxMaID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxMaID.Text.Trim()))
                {
                    MessageBox.Show("メーカーIDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxMaID.Focus();
                    return false;
                }
                //4桁以内
                if (tbxMaID.TextLength > 4)
                {
                    MessageBox.Show("メーカーIDは4桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxMaID.Focus();
                    return false;
                }
            }

            //価格
            if (!String.IsNullOrEmpty(tbxPrice.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrice.Text.Trim()))
                {
                    MessageBox.Show("価格は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrice.Focus();
                    return false;
                }
                //9桁以内
                if (tbxPrice.TextLength > 9)
                {
                    MessageBox.Show("価格は9桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrice.Focus();
                    return false;
                }
            }

            //色
            if (!String.IsNullOrEmpty(tbxColor.Text.Trim()))
            {
                //全角チェック
                if (!dataInputFormCheck.CheckFullWidth(tbxColor.Text.Trim()))
                {
                    MessageBox.Show("色は全角で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxColor.Focus();
                    return false;
                }
                //20文字以下
                if (tbxColor.TextLength > 20)
                {
                    MessageBox.Show("色は20文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxColor.Focus();
                    return false;
                }
            }

            //型番
            if (!String.IsNullOrEmpty(tbxPrModelNumber.Text.Trim()))
            {
                //20文字以下
                if (tbxColor.TextLength > 20)
                {
                    MessageBox.Show("型番は20文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxColor.Focus();
                    return false;
                }
            }

            //安全在庫数
            if (!String.IsNullOrEmpty(tbxPrSafetyStock.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxPrSafetyStock.Text.Trim()))
                {
                    MessageBox.Show("安全在庫数は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrSafetyStock.Focus();
                    return false;
                }
                //4桁以内
                if (tbxPrSafetyStock.TextLength > 4)
                {
                    MessageBox.Show("安全在庫数は4文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxPrSafetyStock.Focus();
                    return false;
                }
            }
            return true;
        }
        //商品情報抽出
        private void GenerateDataAtSelect()
        {
            //コンボボックスが未選択の場合Emptyを設定
            int cSmallClass;
            string Smallclass = "";
            string Majorclass = "";
            if (cbxScName.SelectedIndex != -1)
            {
                cSmallClass = smallClassificationDataAccess.GetSmallClassIDData
                    (int.Parse(cbxScName.SelectedValue.ToString()), cbxScName.Text);
                Smallclass = cSmallClass.ToString();
            }
            if (cbxMcName.SelectedIndex != -1)
            {
                Majorclass = cbxMcName.SelectedValue.ToString();
            }

            DateTime? time = dtpReleaseDate.Value.Date;

            if (dtpReleaseDate.CustomFormat == " ")
            {
                time = null;
            }

            //検索条件のセット
            MProductDsp selectCondition = new MProductDsp()
            {
                PrId = tbxPrID.Text,
                PrName = tbxPrName.Text,
                MaId = tbxMaID.Text,
                Price = tbxPrice.Text,
                PrColor = tbxColor.Text,
                PrModelNumber = tbxPrModelNumber.Text,
                PrSafetyStock = tbxPrSafetyStock.Text,
                //PrReleaseDate = time,
                ScId = Smallclass,
                McId = Majorclass
            };
            //商品データの抽出
            productDsp = productDataAccess.GetProductData(selectCondition);
        }
        //商品抽出結果表示
        private void SetSelectData()
        {
            dgvPr.DataSource = productDsp;
            dgvPr.Refresh();
        }

        //非表示機能関係（非表示コンボボックスの変更処理）
        private void cbxPrFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPrFlag.SelectedIndex == 0)
            {
                //表示
                tbxPrHidden.Text = "";
                tbxPrHidden.Enabled = false;
            }
            else
            {
                //非表示
                tbxPrHidden.Enabled = true;
            }

            //if (hihyouji)
            //{
            //    //デーグリッドビューに表
            //    //示された項目を選択している場合のみ行う
            //    if (dgvPr.SelectedRows.Count > 0)
            //    {
            //        //自動入力のメッセージ対策
            //        if (PrUpdating) return;
            //        PrUpdating = true;

            //        //選択jされた注文情報を取得
            //        string prids = tbxPrID.Text;
            //        string prnames = tbxPrName.Text;
            //        string maids = tbxMaID.Text;
            //        string prices = tbxPrice.Text;
            //        string colors = tbxColor.Text;
            //        //string prjcodes = tbxPrJCode.Text;
            //        string prmodelnumbers = tbxPrModelNumber.Text;
            //        string prsafetystocks = tbxPrSafetyStock.Text;


            //        int hiflgindex = cbxPrFlag.SelectedIndex;
            //        int hiflg;

            //        DateTime prdate = dtpReleaseDate.Value;

            //        // フラグが未選択(-1)の場合は何も処理せずに戻る
            //        if (hiflgindex == -1)
            //        {
            //            PrUpdating = false;
            //            return;
            //        }



            //        try
            //        {
            //            // コンボボックスで選択された小分類名を取得
            //            string selectedScName = cbxScName.SelectedItem.ToString();
            //            // 小分類名（ScName）から ScID を取得
            //            var scids = smallClassificationDataAccess.GetScIdByScName(selectedScName);


            //            int mcname = cbxMcName.SelectedIndex;
            //            int maname = cbxMaName.SelectedIndex;

            //            // 空白チェック
            //            if (string.IsNullOrWhiteSpace(prids) ||
            //                string.IsNullOrWhiteSpace(prnames) ||
            //                string.IsNullOrWhiteSpace(maids) ||
            //                string.IsNullOrWhiteSpace(prices) ||
            //                string.IsNullOrWhiteSpace(colors) ||
            //                //(prjcodes != "") ||
            //                //string.IsNullOrWhiteSpace(prjcodes) ||
            //                !scids.HasValue || scids.Value == 0 ||
            //                string.IsNullOrWhiteSpace(prmodelnumbers) ||
            //                string.IsNullOrWhiteSpace(prsafetystocks) ||
            //                hiflgindex == -1 ||
            //                mcname == -1 || maname == -1 || clear != false)
            //            {
            //                MessageBox.Show("すべての項目を入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                cbxPrFlag.SelectedIndex = -1;
            //                PrUpdating = false;
            //                return;
            //            }



            //            //注文情報をint型に変換          
            //            int prid = int.Parse(prids);
            //            int maid = int.Parse(maids);
            //            int price = int.Parse(prices);

            //            string color = colors;
            //            //string prjcode = prjcodes;
            //            string prmodelnumber = prmodelnumbers;
            //            int prsafetystock = int.Parse(prsafetystocks);

            //            //データベースで条件をチェック
            //            bool exists = productDataAccess.CheckDataExistence(prid, prnames, maid, price, color, scids, prmodelnumber,
            //                                                                prsafetystock, prdate);

            //            //フラグ変更処理
            //            if (exists)
            //            {
            //                int? prflg = productDataAccess.GetPrFlag(prid);
            //                if (hiflgindex == 1 && prflg == 0)
            //                {
            //                    //if (tbxPrHidden.Text != "")
            //                    //{
            //                    var result = MessageBox.Show(
            //                    "商品情報を非表示にしますか？",
            //                    "確認",
            //                    MessageBoxButtons.YesNo,
            //                    MessageBoxIcon.Question
            //                    );

            //                    // 「はい」が選択された場合のみ処理を実行
            //                    if (result == DialogResult.Yes)
            //                    {
            //                        string prhide = tbxPrHidden.Text;
            //                        // 更新する新しい値
            //                        int newprflg = 2;

            //                        // データベースの値を更新
            //                        bool changeprflg = productDataAccess.ChangePrhideflg(prid, newprflg, prhide);

            //                        // 更新結果を通知
            //                        if (changeprflg)
            //                        {
            //                            MessageBox.Show("商品情報を非表示にしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        }
            //                        else
            //                        {
            //                            MessageBox.Show("情報の更新に失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                        }
            //                        //データグリッドビューをクリア
            //                        dgvPr.DataSource = null; // データソースを解除

            //                        // 列をクリアする場合（必要に応じて）
            //                        dgvPr.Columns.Clear();

            //                        // 行をクリアする場合（必要に応じて）
            //                        dgvPr.Rows.Clear();


            //                        PrUpdating = false;
            //                        return;
            //                    }
            //                    //「いいえ」が選択された時
            //                    else
            //                    {
            //                        cbxPrFlag.SelectedIndex = 0;
            //                        PrUpdating = false;
            //                        return;
            //                    }
            //                    //}
            //                    //else
            //                    //{
            //                    //    MessageBox.Show("非表示理由を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                    //    cbxPrFlag.SelectedIndex = 0;
            //                    //    PrUpdating = false;
            //                    //    return;
            //                    //}

            //                }
            //                else if (hiflgindex == 0 && prflg == 2)
            //                {
            //                    var result = MessageBox.Show(
            //                    "商品情報を表示状態にしますか？" +
            //                    "(入力されている非表示理由は削除されます。)",
            //                    "確認",
            //                    MessageBoxButtons.YesNo,
            //                    MessageBoxIcon.Question
            //                    );

            //                    // 「はい」が選択された場合のみ処理を実行
            //                    if (result == DialogResult.Yes)
            //                    {
            //                        // 更新する新しい値
            //                        int newprflg = 0;
            //                        string prhide = "";
            //                        // データベースの値を更新
            //                        bool changeprflg = productDataAccess.ChangePrhideflg(prid, newprflg, prhide);

            //                        // 更新結果を通知
            //                        if (changeprflg)
            //                        {
            //                            MessageBox.Show("商品情報を表示状態にしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                        }
            //                        else
            //                        {
            //                            MessageBox.Show("情報の更新に失敗しました。", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //                        }
            //                        PrUpdating = false;
            //                        return;
            //                    }
            //                    //「いいえ」が選択された時
            //                    else
            //                    {
            //                        cbxPrFlag.SelectedIndex = 1;
            //                        PrUpdating = false;
            //                        return;
            //                    }
            //                }
            //                else
            //                {
            //                    MessageBox.Show("エラーが発生しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    PrUpdating = false;
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
            //            cbxPrFlag.SelectedIndex = -1;
            //            PrUpdating = false;
            //            return;
            //        }
            //    }
            //}
        }

        //非表示機能関係（非表示リスト表示）
        private void btnHidden_Click(object sender, EventArgs e)
        {
            //非表示データグリッドビューの表示
            SetFormHiddenDataGridView();

            ////取得したデータをフィルタリング
            //var filteredProduct = productDataAccess.GetProductData(2);

            //// 取得したデータをDataGridViewに表示
            //if (filteredProduct != null && filteredProduct.Count > 0)
            //{
            //    dgvPr.DataSource = filteredProduct;
            //    // データグリッドビューの列ヘッダー名を変更
            //    dgvPr.Columns["PrID"].HeaderText = "商品ID";
            //    dgvPr.Columns["MaID"].HeaderText = "メーカID";
            //    dgvPr.Columns["PrName"].HeaderText = "商品名";
            //    dgvPr.Columns["Price"].HeaderText = "価格";
            //    dgvPr.Columns["PrJCode"].HeaderText = "JANコード";
            //    dgvPr.Columns["PrSafetyStock"].HeaderText = "安全在庫数";
            //    dgvPr.Columns["ScID"].HeaderText = "小分類ID";
            //    dgvPr.Columns["PrModelNumber"].HeaderText = "型番";
            //    dgvPr.Columns["PrColor"].HeaderText = "色";
            //    dgvPr.Columns["PrReleaseDate"].HeaderText = "発売日";
            //    dgvPr.Columns["PrHidden"].HeaderText = "非表示理由";

            //    //PrFlag(表示/非表示)の列を非表示にする
            //    dgvPr.Columns["PrFlag"].Visible = false;

            //    // 列幅を設定
            //    dgvPr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //    dgvPr.Columns["PrHidden"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //    dgvPr.Columns["PrHidden"].Width = 500;

            //    // ユーザーが編集や削除をできないように設定
            //    dgvPr.ReadOnly = true;  // 編集不可
            //    dgvPr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            //    dgvPr.MultiSelect = false;  // 複数行選択を無効
            //    dgvPr.AllowUserToAddRows = false;  // 行の追加を無効
            //    dgvPr.AllowUserToDeleteRows = false;  // 行の削除を無効
            //    dgvPr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            //    dgvPr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            //    // 行番号の列や選択列のサイズ変更を無効にする
            //    dgvPr.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //    //不要な要素を非表示
            //    dgvPr.Columns["Sc"].Visible = false;
            //    dgvPr.Columns["Ma"].Visible = false;
            //    dgvPr.Columns["TArrivalDetails"].Visible = false;
            //    dgvPr.Columns["TChumonDetails"].Visible = false;
            //    dgvPr.Columns["THattyuDetails"].Visible = false;
            //    dgvPr.Columns["TOrderDetails"].Visible = false;
            //    dgvPr.Columns["TSaleDetails"].Visible = false;
            //    dgvPr.Columns["TShipmentDetails"].Visible = false;
            //    dgvPr.Columns["TStocks"].Visible = false;
            //    dgvPr.Columns["TSyukkoDetails"].Visible = false;
            //    dgvPr.Columns["TWarehousingDetails"].Visible = false;
            //}
            //else
            //{
            //    MessageBox.Show("表示する商品情報がありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dgvPr.DataSource = null; // データソースを解除

            //    // 列をクリアする場合（必要に応じて）
            //    dgvPr.Columns.Clear();

            //    // 行をクリアする場合（必要に応じて）
            //    dgvPr.Rows.Clear();

            //}
            //// 選択を解除
            //foreach (DataGridViewRow row in dgvPr.SelectedRows)
            //{
            //    row.Selected = false;
            //}
        }


        //4.6 商品情報クリア機能
        private void btnClearInput_Click(object sender, EventArgs e)
        {
            hihyouji = false;

            //クリアボタン
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "入力されている商品情報をクリアしますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            // 「はい」が選ばれた場合のみ処理を続行
            if (result == DialogResult.Yes)
            {

                ClearPr();
                dtpReleaseDate.Checked = false;
                // 選択を解除
                foreach (DataGridViewRow row in dgvPr.SelectedRows)
                {
                    row.Selected = false;
                }
            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
        }


        //商品情報クリア機能関係
        private void ClearPr()
        {
            //商品詳細
            tbxPrID.Text = "";
            tbxMaID.Text = "";
            tbxPrice.Text = "";
            tbxColor.Text = "";
            tbxPrSafetyStock.Text = "";
            tbxPrName.Text = "";
            cbxMaName.SelectedIndex = -1;
            tbxPrJCode.Text = "";
            tbxPrModelNumber.Text = "";
            cbxPrFlag.SelectedIndex = 0;
            cbxPrFlag.Enabled = false;
            dtpReleaseDate.Format = DateTimePickerFormat.Custom;
            dtpReleaseDate.CustomFormat = " ";
            dtpReleaseDate.ValueChanged += (s, e) =>
            {
                dtpReleaseDate.Format = DateTimePickerFormat.Custom;
                dtpReleaseDate.CustomFormat = "yyyy年MM月dd日";
            };
            tbxPrHidden.Text = "";

            //大分類・小分類
            cbxMcName.SelectedIndex = -1;
            cbxScName.SelectedIndex = -1;

            // 小分類コンボボックスの項目をリセット（絞り込みを解除）
            //cbxScName.Items.Clear();
            SetFormComboBox();

            //// 選択を解除
            //foreach (DataGridViewRow row in dgvPr.SelectedRows)
            //{
            //    row.Selected = false;
            //}

            //// SalesOfficeDataAccess クラスのインスタンスを作成
            //SmallClassificationDataAccess smallClassificationDataAccess = new SmallClassificationDataAccess();
            //// データベースからコンボボックスに設定するデータを取得
            //List<string> smallclassification = smallClassificationDataAccess.ScGetComboboxText();

            //// コンボボックスにデータを追加
            //foreach (var scname in smallclassification)
            //{
            //    cbxScName.Items.Add(scname);
            //}

        }

        //4,7 商品管理閉じる機能
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

        ///////////////////////////////
        //メソッド名：SetFormDataGridView()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューの設定
        ///////////////////////////////
        private void SetFormDataGridView()
        {
            // ユーザーが編集や削除をできないように設定
            dgvPr.ReadOnly = true;  // 編集不可
            dgvPr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            dgvPr.MultiSelect = false;  // 複数行選択を無効
            dgvPr.AllowUserToAddRows = false;  // 行の追加を無効
            dgvPr.AllowUserToDeleteRows = false;  // 行の削除を無効
            dgvPr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            dgvPr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            // 行番号の列や選択列のサイズ変更を無効にする
            dgvPr.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //ヘッダー位置の設定
            dgvPr.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            dgvPr.ReadOnly = true;  // 編集不可
            dgvPr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;  // 行全体を選択可能
            dgvPr.MultiSelect = false;  // 複数行選択を無効
            dgvPr.AllowUserToAddRows = false;  // 行の追加を無効
            dgvPr.AllowUserToDeleteRows = false;  // 行の削除を無効
            dgvPr.AllowUserToResizeColumns = false;  // 列のサイズ変更を無効
            dgvPr.AllowUserToResizeRows = false;  // 行のサイズ変更を無効

            // 行番号の列や選択列のサイズ変更を無効にする
            dgvPr.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            //ヘッダー位置の設定
            dgvPr.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            productDsp = productDataAccess.GetProductData();

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
            productDsp = productDataAccess.GetProductHiddenData();

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
            dgvPr.DataSource = productDsp;
            // データグリッドビューの列ヘッダー名を変更
            //dgvPr.Columns["PrID"].HeaderText = "商品ID";
            //dgvPr.Columns["MaID"].HeaderText = "メーカID";
            //dgvPr.Columns["PrName"].HeaderText = "商品名";
            //dgvPr.Columns["Price"].HeaderText = "価格";
            //dgvPr.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //dgvPr.Columns["PrJCode"].HeaderText = "JANコード";
            //dgvPr.Columns["PrSafetyStock"].HeaderText = "安全在庫数";
            //dgvPr.Columns["ScID"].HeaderText = "小分類ID";
            //dgvPr.Columns["PrModelNumber"].HeaderText = "型番";
            //dgvPr.Columns["PrColor"].HeaderText = "色";
            //dgvPr.Columns["PrReleaseDate"].HeaderText = "発売日";
            //dgvPr.Columns["PrHidden"].HeaderText = "非表示理由";

            //PrFlag(表示/非表示)の列を非表示にする
            //dgvPr.Columns["PrFlag"].Visible = false;

            // 列幅を設定
            dgvPr.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //[9]発売日
            dgvPr.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPr.Columns[9].Width = 100;
            //[14]非表示理由
            dgvPr.Columns[14].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvPr.Columns[14].Width = 500;

            //不要な要素を非表示
            //[2]メーカーID
            dgvPr.Columns[2].Visible = false;
            //[10]小分類ID
            dgvPr.Columns[10].Visible = false;
            //[11]大分類ID
            dgvPr.Columns[11].Visible = false;
            //[13]非表示フラグ
            dgvPr.Columns[13].Visible = false;
            //dgvPr.Columns["Sc"].Visible = false;
            //dgvPr.Columns["Ma"].Visible = false;
            //dgvPr.Columns["TArrivalDetails"].Visible = false;
            //dgvPr.Columns["TChumonDetails"].Visible = false;
            //dgvPr.Columns["THattyuDetails"].Visible = false;
            //dgvPr.Columns["TOrderDetails"].Visible = false;
            //dgvPr.Columns["TSaleDetails"].Visible = false;
            //dgvPr.Columns["TShipmentDetails"].Visible = false;
            //dgvPr.Columns["TStocks"].Visible = false;
            //dgvPr.Columns["TSyukkoDetails"].Visible = false;
            //dgvPr.Columns["TWarehousingDetails"].Visible = false;

            //各列の文字位置の指定
            dgvPr.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvPr.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPr.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            dgvPr.Refresh();
        }

        //表示非表示コンボボックス選択時のプログラム(フラグ)
        private int PrFlgNum()
        {
            int flg;
            if (cbxPrFlag.SelectedIndex == 0)
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

        private string PrHidden()
        {
            string hidden;
            if (cbxPrFlag.SelectedIndex == 0)
            {
                hidden = null;
            }
            else
            {
                hidden = tbxPrHidden.Text.Trim();
            }

            return hidden;
        }
    }
}
