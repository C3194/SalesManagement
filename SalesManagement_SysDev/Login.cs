using System;
using Microsoft.Data.SqlClient;
using 画面設計用9._0注文管理;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SalesManagement_SysDev.DataAccess;
using SalesManagementB;
using SalesManagement_SysDev.Entity;

namespace SalesManagement_SysDev
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            tbxPass.PasswordChar = '*';// パスワードを隠す
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "終了しますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            // 「はい」が選ばれた場合のみ処理を続行
            if (result == DialogResult.Yes)
            {

                this.Close(); // 現在のフォームを閉じる

            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // ユーザーが入力した社員IDとパスワード
            string employeeID = tbxEmID.Text.Trim();
            string password = tbxPass.Text.Trim();

            // 入力チェック
            if (string.IsNullOrWhiteSpace(employeeID) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("社員IDとパスワードを入力してください。");
                return;
            }

            // 入力された社員IDを整数に変換
            if (!int.TryParse(employeeID, out int parsedEmID))
            {
                MessageBox.Show("社員IDは整数で入力してください。");
                return;
            }

            try
            {
                // LoginDataAccess を使用してデータを取得
                LoginDataAccess dataAccess = new LoginDataAccess();
                List<MEmployee> employees = dataAccess.GetEmployeeData(parsedEmID.ToString(), password);

                // 結果の判定
                if (employees.Any())
                {
                    // ログイン成功
                    SessionManager.LoggedInEmployeeID = parsedEmID; // 現在の社員IDを保存
                    //MessageBox.Show("ログイン成功", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 次の画面を開く
                    Menu menu = new 画面設計用9._0注文管理.Menu();
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    // ログイン失敗
                    MessageBox.Show("社員IDまたはパスワードが正しくありません。");
                }
            }
            catch (Exception ex)
            {
                // エラー処理
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chxPass_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chxPass.Checked)
            {
                // チェックが入っている場合、パスワードを表示
                tbxPass.PasswordChar = '\0';// パスワードを見えるようにする
            }
            else
            {
                // チェックが外れている場合、パスワードを隠す
                tbxPass.PasswordChar = '*';// パスワードを隠す
            }
        }

        private void btnPassChange_Click(object sender, EventArgs e)
        {
            {
                // 確認ダイアログを表示
                var result = MessageBox.Show(
                    "パスワード変更画面にいきますか？",      // メッセージ
                    "確認",                   // タイトル
                    MessageBoxButtons.YesNo,  // Yes/No ボタン
                    MessageBoxIcon.Question   // アイコン（質問）
                );

                // 「はい」が選ばれた場合のみ処理を続行
                if (result == DialogResult.Yes)
                {

                    PassLogin passLogin = new PassLogin(); // 新しいフォームを表示する
                    passLogin.Show();
                    this.Hide(); // 現在のフォームを隠す

                }
                else
                {
                    // 「いいえ」が選ばれた場合は何もしない（戻らない）
                }

            }
        }
    }
}
