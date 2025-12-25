using SalesManagement_SysDev;
using SalesManagement_SysDev.Common;
using SalesManagement_SysDev.DateAccess;
using SalesManagement_SysDev.新しいフォルダー;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using 画面設計用9._0注文管理;

namespace a
{
    public partial class Client : Form
    {
        //顧客テーブルアクセス用クラスのインスタンス化
        ClientDataAccess clientDataAccess = new ClientDataAccess();
        //営業所テーブルアクセス用クラスのインスタンス化
        SalesOfficeDataAccess salesOfficeDataAccess = new SalesOfficeDataAccess();

        //入力形式チェック用クラスのインスタンス化
        DataInputFormCheck dataInputFormCheck = new DataInputFormCheck();
        //データグリッドビュー用の顧客データ
        private static List<MClientDsp> client;
        //コンボボックス用の顧客データ
        private static List<MClient> cbxClient;
        //コンボボックス用の営業所データ
        private static List<MSalesOffice> salesOffice;

        public Client()
        {
            InitializeComponent();
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
                Dispose();
            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
        }

        private void Client_Load(object sender, EventArgs e)
        {
            //入力初期設定
            cbxClFlag.SelectedIndex = 0;

            //コンボボックスの設定
            SetFormComboBox();

            //データグリッドビューの表示
            SetFormDataGridView();
        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            //3.1.1　顧客情報を得る(妥当な顧客データ取得)
            if (!GetValidDataAtRegistration())
            {
                return;
            }

            //3.1.3 顧客情報作成
            var regClient = GenerateDataAtRegistration();

            //3.1.5 顧客情報登録
            RegistrationClient(regClient);
        }
        ///////////////////////////////
        //　3.1.1 妥当な顧客データ取得
        //メソッド名：GetValidDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtRegistration()
        {

            //顧客名の適否
            if (!String.IsNullOrEmpty(cbxClName.Text.Trim()))
            {
                //顧客名の文字数チェック
                //50文字以下
                if (cbxClName.Text.Length > 50)
                {
                    MessageBox.Show("顧客名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxClName.Focus();
                    return false;
                }
            }
            else
            {
                //顧客名テキストボックスが空
                MessageBox.Show("顧客名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxClName.Focus();
                return false;
            }
            //郵便番号の適否
            if (!String.IsNullOrEmpty(tbxClPostal.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxClPostal.Text.Trim()))
                {
                    MessageBox.Show("郵便番号は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClPostal.Focus();
                    return false;
                }
                //文字数チェック
                if (!dataInputFormCheck.CheckPostalDigit(tbxClPostal.Text.Trim()))
                {
                    MessageBox.Show("郵便番号は7桁で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClPostal.Focus();
                    return false;
                }
            }
            else
            {
                //郵便番号テキストボックスが空
                MessageBox.Show("郵便番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClPostal.Focus();
                return false;
            }

            //住所の文字数チェック
            if (!String.IsNullOrEmpty(tbxClAddress.Text.Trim()))
            {
                //50文字以下
                if (tbxClAddress.TextLength > 50)
                {
                    MessageBox.Show("住所は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClAddress.Focus();
                    return false;
                }
            }
            else
            {
                //住所テキストボックスが空
                MessageBox.Show("住所を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClAddress.Focus();
                return false;
            }

            //電話番号の適否
            if (!String.IsNullOrEmpty(tbxClPhone.Text.Trim()))
            {
                //電話番号の形式チェック
                if (!dataInputFormCheck.CheckPhoneDigitHyphen(tbxClPhone.Text.Trim()))
                {
                    MessageBox.Show("電話番号はハイフン(-)を含み\r9桁以上11桁以内で半角入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClPhone.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("電話番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClPhone.Focus();
                return false;
            }

            //FAXの適否
            if (!String.IsNullOrEmpty(tbxClFAX.Text.Trim()))
            {
                //形式チェック
                if (!dataInputFormCheck.CheckFaxDigitHyphen(tbxClFAX.Text.Trim()))
                {
                    MessageBox.Show("FAXはハイフン(-)を含み\r9桁以上11桁以内で半角入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClFAX.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("FAXを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClFAX.Focus();
                return false;
            }

            //営業所ID適否
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
                MessageBox.Show("営業所IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxSoID.Focus();
                return false;
            }

            //営業所名の適否
            if (cbxSoName.SelectedIndex == -1)
            {
                MessageBox.Show("営業所名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxSoName.Focus();
                return false;
            }

            //非表示フラグ

            //非表示理由
            if (String.IsNullOrEmpty(tbxClHidden.Text.Trim()))
            {
                if (cbxClFlag.SelectedIndex == 1)
                {
                    MessageBox.Show("非表示理由を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClHidden.Focus();
                    return false;
                }
            }

            string hidden;
            if (cbxClFlag.SelectedIndex == 0)
            {
                hidden = null;
            }
            else
            {
                hidden = tbxClHidden.Text.Trim();
            }

            //別データの存在確認
            if (!String.IsNullOrEmpty(tbxClID.Text.Trim()))
            {
                //顧客IDの存在チェック
                if (clientDataAccess.SelectClientExistenceCheck
                    (int.Parse(tbxClID.Text.Trim()), cbxClName.Text.Trim(),
                    tbxClPostal.Text.Trim(),tbxClAddress.Text.Trim(),
                    tbxClPhone.Text.Trim(),tbxClFAX.Text.Trim(),
                    int.Parse(tbxSoID.Text.Trim()),
                    ClFlgNum(),hidden))
                {
                    MessageBox.Show("同じ顧客名のデータがあります", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbxClID.Focus();
                    return false;
                }
            }

            return true;
        }
        ///////////////////////////////
        //　3.1.3 顧客情報作成
        //メソッド名：GenerateDataAtRegistration()
        //引　数   ：なし
        //戻り値   ：顧客登録情報
        //機　能   ：登録データのセット
        ///////////////////////////////
        private MClient GenerateDataAtRegistration()
        {
            return new MClient
            {
                ClName = cbxClName.Text.Trim(),
                ClPostal = tbxClPostal.Text.Trim(),
                ClAddress = tbxClAddress.Text.Trim(),
                ClPhone = tbxClPhone.Text.Trim(),
                ClFax = tbxClFAX.Text.Trim(),
                SoId = int.Parse(tbxSoID.Text.Trim()),
                ClFlag = ClFlgNum(),
                ClHidden = ClHidden()
            };

        }
        ///////////////////////////////
        //　3.1.5 顧客情報登録
        //メソッド名：RegistrationClient()
        //引　数   ：顧客情報
        //戻り値   ：なし
        //機　能   ：顧客情報の登録
        ///////////////////////////////
        private void RegistrationClient(MClient regClient)
        {
            //登録確認メッセージ
            DialogResult result = MessageBox.Show("顧客データを登録します\rよろしいですか？", "登録確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }

            //顧客情報の登録
            bool flg = clientDataAccess.AddClientData(regClient);
            if (flg == true)
            {
                MessageBox.Show("データの登録に成功しました", "登録成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearInput();
            }
            else
            {
                MessageBox.Show("データの登録に失敗しました", "登録失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            GetDataGridView();
            //コンボボックスの更新
            SetFormComboBox();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //3.2.1 顧客情報を得る
            if (!GetValidDataAtUpdate())
            {
                return;
            }

            //3.2.3 顧客情報作成
            var updClient = GenerateDataAtUpdate();

            //3.2.5 顧客情報更新
            UpdateClient(updClient);
        }
        ///////////////////////////////
        //3.2.1 顧客情報を得る
        //メソッド名：GetValidDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtUpdate()
        {
            //顧客IDの適否
            if(!String.IsNullOrEmpty(tbxClID.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxClID.Text.Trim()))
                {
                    MessageBox.Show("顧客IDは半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClID.Focus();
                    return false;
                }
                //6桁以内
                if (tbxClID.TextLength > 6)
                {
                    MessageBox.Show("顧客IDは6桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            //顧客名の適否
            if (!String.IsNullOrEmpty(cbxClName.Text.Trim()))
            {
                //顧客名の文字数チェック
                //50文字以下
                if (cbxClName.Text.Length > 50)
                {
                    MessageBox.Show("顧客名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxClName.Focus();
                    return false;
                }
            }
            else
            {
                //顧客名テキストボックスが空
                MessageBox.Show("顧客名を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxClName.Focus();
                return false;
            }
            //郵便番号の適否
            if (!String.IsNullOrEmpty(tbxClPostal.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxClPostal.Text.Trim()))
                {
                    MessageBox.Show("郵便番号は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClPostal.Focus();
                    return false;
                }
                //文字数チェック
                if (!dataInputFormCheck.CheckPostalDigit(tbxClPostal.Text.Trim()))
                {
                    MessageBox.Show("郵便番号は7桁で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClPostal.Focus();
                    return false;
                }
            }
            else
            {
                //郵便番号テキストボックスが空
                MessageBox.Show("郵便番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClPostal.Focus();
                return false;
            }

            //住所の文字数チェック
            if (!String.IsNullOrEmpty(tbxClAddress.Text.Trim()))
            {
                //50文字以下
                if (tbxClAddress.TextLength > 50)
                {
                    MessageBox.Show("住所は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClAddress.Focus();
                    return false;
                }
            }
            else
            {
                //住所テキストボックスが空
                MessageBox.Show("住所を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClAddress.Focus();
                return false;
            }

            //電話番号の適否
            if (!String.IsNullOrEmpty(tbxClPhone.Text.Trim()))
            {
                //電話番号の形式チェック
                if (!dataInputFormCheck.CheckPhoneDigitHyphen(tbxClPhone.Text.Trim()))
                {
                    MessageBox.Show("電話番号はハイフン(-)を含み\r9桁以上12桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClPhone.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("電話番号を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClPhone.Focus();
                return false;
            }

            //FAXの適否
            if (!String.IsNullOrEmpty(tbxClFAX.Text.Trim()))
            {
                //形式チェック
                if (!dataInputFormCheck.CheckFaxDigitHyphen(tbxClFAX.Text.Trim()))
                {
                    MessageBox.Show("FAXはハイフン(-)を含み\r9桁以上12桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClFAX.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("FAXを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxClFAX.Focus();
                return false;
            }

            //営業所ID適否
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
                MessageBox.Show("営業所IDを入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbxSoID.Focus();
                return false;
            }

            //営業所名の適否
            if (cbxSoName.SelectedIndex == -1)
            {
                MessageBox.Show("営業所名を選択してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxSoName.Focus();
                return false;
            }

            //非表示フラグ

            //非表示理由
            if (String.IsNullOrEmpty(tbxClHidden.Text.Trim()))
            {
                if (cbxClFlag.SelectedIndex == 1)
                {
                    MessageBox.Show("非表示理由を入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClHidden.Focus();
                    return false;
                }
            }

            string hidden;
            if (cbxClFlag.SelectedIndex == 0)
            {
                hidden = null;
            }
            else
            {
                hidden = tbxClHidden.Text.Trim();
            }

            //別データの存在確認
            if (!String.IsNullOrEmpty(tbxClID.Text.Trim()))
            {
                //顧客IDの存在チェック
                if (clientDataAccess.SelectClientExistenceCheck
                    (int.Parse(tbxClID.Text.Trim()), cbxClName.Text.Trim(),
                    tbxClPostal.Text.Trim(), tbxClAddress.Text.Trim(),
                    tbxClPhone.Text.Trim(), tbxClFAX.Text.Trim(),
                    int.Parse(tbxSoID.Text.Trim()),
                    ClFlgNum(), hidden))
                {
                    MessageBox.Show("変更されていないデータです", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tbxClID.Focus();
                    return false;
                }
            }

            return true;
        }
        ///////////////////////////////
        //3.2.3 顧客情報作成
        //メソッド名：GenerateDataAtUpdate()
        //引　数   ：なし
        //戻り値   ：顧客更新情報
        //機　能   ：更新データのセット
        ///////////////////////////////
        private MClient GenerateDataAtUpdate()
        {
            //更新データのセット
            return new MClient
            {
                ClId = int.Parse(tbxClID.Text.Trim()),
                ClName = cbxClName.Text.Trim(),
                ClPostal = tbxClPostal.Text.Trim(),
                ClAddress = tbxClAddress.Text.Trim(),
                ClPhone = tbxClPhone.Text.Trim(),
                ClFax = tbxClFAX.Text.Trim(),
                SoId = int.Parse(tbxSoID.Text.Trim()),
                ClFlag = ClFlgNum(),
                ClHidden = ClHidden()
            };
        }
        ///////////////////////////////
        //3.2.5 顧客情報更新
        //メソッド名：UpdateClient()
        //引　数   ：顧客情報
        //戻り値   ：なし
        //機　能   ：顧客情報の更新
        ///////////////////////////////
        private void UpdateClient(MClient updClient)
        {
            DialogResult result = MessageBox.Show("顧客データを更新します\rよろしいですか？", "更新確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                return;
            }

            bool flg = clientDataAccess.UpdateClientData(updClient);
            if (flg == true)
            {
                MessageBox.Show("データの更新に成功しました", "更新成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //入力エリアのクリア
                ClearInput();
            }
            else
            {
                MessageBox.Show("データの更新に失敗しました", "更新失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //データグリッドビューの表示
            GetDataGridView();
            //コンボボックスの更新
            SetFormComboBox();
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            //データグリッドビューの表示
            SetFormDataGridView();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //3.4.1 顧客情報を得る
            if (!GetValidDataAtSlect())
            {
                return;
            }

            //3.4.3 顧客情報抽出
            GenerateDataAtSelect();

            //3.4.5 顧客抽出結果表示
            SetSelectData();
        }
        ///////////////////////////////
        //3.4.1 顧客情報を得る
        //メソッド名：GetValidDataAtSlect()
        //引　数   ：なし
        //戻り値   ：true or false
        //機　能   ：入力データの形式チェック
        //          ：エラーがない場合True
        //          ：エラーがある場合False
        ///////////////////////////////
        private bool GetValidDataAtSlect()
        {
            //顧客IDの適否
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

            //顧客名の適否
            if (!String.IsNullOrEmpty(cbxClName.Text.Trim()))
            {
                //顧客名の文字数チェック
                //50文字以下
                if (cbxClName.Text.Length > 50)
                {
                    MessageBox.Show("顧客名は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxClName.Focus();
                    return false;
                }
            }

            //郵便番号の適否
            if (!String.IsNullOrEmpty(tbxClPostal.Text.Trim()))
            {
                //数値チェック
                if (!dataInputFormCheck.CheckNumeric(tbxClPostal.Text.Trim()))
                {
                    MessageBox.Show("郵便番号は半角数字で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClPostal.Focus();
                    return false;
                }
                //文字数チェック
                if (tbxClPostal.TextLength > 7)
                {
                    MessageBox.Show("郵便番号は7桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClPostal.Focus();
                    return false;
                }
            }

            //住所の文字数チェック
            if (!String.IsNullOrEmpty(tbxClAddress.Text.Trim()))
            {
                //50文字以下
                if (tbxClAddress.TextLength > 50)
                {
                    MessageBox.Show("住所は50文字以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbxClAddress.Focus();
                    return false;
                }
            }

            //電話番号の適否
            if (!String.IsNullOrEmpty(tbxClPhone.Text.Trim()))
            {
                ////電話番号の形式チェック
                //if (!dataInputFormCheck.CheckPhoneDigitHyphen(tbxClPhone.Text.Trim()))
                //{
                //    MessageBox.Show("電話番号はハイフン(-)を含み\r10桁以上11桁以内で入力してください", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    tbxClPhone.Focus();
                //    return false;
                //}
            }

            //営業所ID適否
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
            return true;
        }
        ///////////////////////////////
        //3.4.3 顧客情報抽出
        //メソッド名：GenerateDataAtSelect()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：検索データの取得
        ///////////////////////////////
        private void GenerateDataAtSelect()
        {
            //コンボボックスが未選択の場合Emptyを設定
            string cClient = "";
            string cSalesOffice = "";
            if (cbxClName.SelectedIndex != -1)
            {
                cClient = cbxClName.SelectedValue.ToString();
            }
            if (cbxSoName.SelectedIndex != -1)
            {
                cSalesOffice = cbxSoName.SelectedValue.ToString();
            }

            //検索条件のセット
            MClientDsp selectCondition = new MClientDsp()
            {
                ClId = cClient,
                ClName = cbxClName.Text,
                ClPostal = tbxClPostal.Text,
                ClAddress = tbxClAddress.Text,
                ClPhone = tbxClPhone.Text,
                ClFAX = tbxClFAX.Text,
                SoId = cSalesOffice,
            };
            //顧客データの抽出
            client = clientDataAccess.GetClientData(selectCondition);

        }
        ///////////////////////////////
        //3.4.5 顧客抽出結果表示
        //メソッド名：SetSelectData()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：顧客情報の表示
        ///////////////////////////////
        private void SetSelectData()
        {
            dgvCl.DataSource = client;
            dgvCl.Refresh();
        }

        private void btnHidden_Click(object sender, EventArgs e)
        {
            //非表示データグリッドビューの表示
            SetFormHiddenDataGridView();
        }

        private void btnClearlnput_Click(object sender, EventArgs e)
        {
            //入力エリアのクリア
            ClearInput();
        }

        ///////////////////////////////
        //メソッド名：SetFormComboBox()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：コンボボックスのデータ設定
        ///////////////////////////////
        private void SetFormComboBox()
        {
            //顧客名データの取得
            cbxClient = clientDataAccess.GetClientDspData();
            //顧客データとコンボボックスをリンクさせる
            cbxClName.DataSource = cbxClient;
            //[DisplayMember]表示する名前 = ClName(顧客名)
            cbxClName.DisplayMember = "ClName";
            //[ValueMember]裏でリンクしているデータ = ClID(顧客ID)
            cbxClName.ValueMember = "ClID";
            //何も表示しない
            cbxClName.SelectedIndex = -1;

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
            dgvCl.ReadOnly = true;
            //行内をクリックすることで行を選択
            dgvCl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の設定
            dgvCl.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            dgvCl.ReadOnly = true;
            //行内をクリックすることで行を選択
            dgvCl.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //ヘッダー位置の設定
            dgvCl.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
            //顧客データの取得
            client = clientDataAccess.GetClientData();

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
            client = clientDataAccess.GetClientHiddenData();

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
            dgvCl.DataSource = client;
            //各列幅の設定
            //[0]ClID・顧客ID
            dgvCl.Columns[0].Width = 90;
            //[1]ClName・顧客名
            dgvCl.Columns[1].Width = 130;
            //[2]ClPostal・郵便番号
            dgvCl.Columns[2].Width = 130;
            //[3]ClAddress・住所
            dgvCl.Columns[3].Width = 250;
            //[4]ClPhone・電話番号
            dgvCl.Columns[4].Width = 130;
            //[5]ClFAX・FAX
            dgvCl.Columns[5].Width = 130;
            //[6]SoID・営業所ID
            dgvCl.Columns[6].Width = 90;
            dgvCl.Columns[6].Visible = false;
            //[7]SoName・営業所名
            dgvCl.Columns[7].Width = 130;
            //[8]ClFlag・非表示フラグ
            dgvCl.Columns[8].Width = 90;
            //[9]ClHidden・非表示理由
            dgvCl.Columns[9].Width = 250;

            //各列の文字位置の指定
            dgvCl.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCl.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCl.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCl.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCl.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCl.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCl.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCl.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCl.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgvCl.Refresh();
        }

        ///////////////////////////////
        //メソッド名：dataGridViewDsp_CellClick()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：データグリッドビューから選択された情報を各入力エリアにセット
        ///////////////////////////////
        private void dgvCl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //データグリッドビューからクリックされたデータを各入力エリアへ
            //[0]ClID・顧客ID
            tbxClID.Text = dgvCl.Rows[dgvCl.CurrentRow.Index].Cells[0].Value.ToString();
            //[1]ClName・顧客名
            cbxClName.Text = dgvCl.Rows[dgvCl.CurrentRow.Index].Cells[1].Value.ToString();
            //[2]ClPostal・郵便番号
            tbxClPostal.Text = dgvCl.Rows[dgvCl.CurrentRow.Index].Cells[2].Value.ToString();
            //[3]ClAddress・住所
            tbxClAddress.Text = dgvCl.Rows[dgvCl.CurrentRow.Index].Cells[3].Value.ToString();
            //[4]ClPhone・電話番号
            tbxClPhone.Text = dgvCl.Rows[dgvCl.CurrentRow.Index].Cells[4].Value.ToString();
            //[5]ClFAX・FAX
            tbxClFAX.Text = dgvCl.Rows[dgvCl.CurrentRow.Index].Cells[5].Value.ToString();
            //[6]SoID・営業所ID
            tbxSoID.Text = dgvCl.Rows[dgvCl.CurrentRow.Index].Cells[6].Value.ToString();
            //[7]SoName・営業所名
            //[8]ClFlag・非表示フラグ
            if (dgvCl.Rows[dgvCl.CurrentRow.Index].Cells[8].Value.ToString() == "0")
            {
                //表示
                cbxClFlag.SelectedIndex = 0;
                tbxClHidden.Text = "";
            }
            else
            {
                //非表示
                cbxClFlag.SelectedIndex = 1;
                //[9]ClHidden・非表示理由
                tbxClHidden.Text = dgvCl.Rows[dgvCl.CurrentRow.Index].Cells[9].Value.ToString();
            }
        }

        ///////////////////////////////
        //メソッド名：ClearInput()
        //引　数   ：なし
        //戻り値   ：なし
        //機　能   ：入力エリアをクリア
        ///////////////////////////////
        private void ClearInput()
        {
            tbxClID.Text = "";
            tbxClPostal.Text = "";
            tbxClAddress.Text = "";
            tbxClPhone.Text = "";
            tbxClFAX.Text = "";
            tbxSoID.Text = "";
            tbxClHidden.Text = "";
            cbxClName.SelectedIndex = -1;
            cbxClName.Text = "";
            cbxSoName.SelectedIndex = -1;
            cbxClFlag.SelectedIndex = 0;
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
        }

        //表示非表示コンボボックス選択時のプログラム(非表示理由テキストボックスの開放)
        private void cbxClFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxClFlag.SelectedIndex == 0)
            {
                //表示
                tbxClHidden.Text = "";
                tbxClHidden.Enabled = false;
            }
            else
            {
                //非表示
                tbxClHidden.Enabled = true;
            }
        }

        //表示非表示コンボボックス選択時のプログラム(フラグ)
        private int ClFlgNum()
        {
            int flg;
            if (cbxClFlag.SelectedIndex == 0)
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

        private string ClHidden()
        {
            string hidden;
            if(cbxClFlag.SelectedIndex == 0)
            {
                hidden = null;
            }
            else
            {
                hidden = tbxClHidden.Text.Trim();
            }

            return hidden;
        }
    }
}