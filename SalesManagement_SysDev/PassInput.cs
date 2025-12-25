using SalesManagement_SysDev;
using SalesManagement_SysDev.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesManagementB
{
    public partial class PassInput : Form
    {
        public PassInput()
        {
            
            InitializeComponent();
            tbxNewPass.PasswordChar = '*';// パスワードを隠す
            tbxNewPass2.PasswordChar = '*';// パスワードを隠す
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "前の画面に戻りますか？",      // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            // 「はい」が選ばれた場合のみ処理を続行
            if (result == DialogResult.Yes)
            {

                PassLogin passlogin = new PassLogin(); // 新しいフォームを表示する
                passlogin.Show();
                this.Hide(); // 現在のフォームを隠す

            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {

            //SessionManagerに格納している社員IDを表示
            //MessageBox.Show(SessionManager.LoggedInEmployeeID.ToString());

            // 現在ログイン中の社員IDを取得
            int loggedInEmployeeID = SessionManager.LoggedInEmployeeID;

            // 新しいパスワードと確認用パスワードを取得
            string newPassword = tbxNewPass.Text.Trim();
            string confirmPassword = tbxNewPass2.Text.Trim();

            // 入力チェック
            if (string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("新しいパスワードと確認用パスワードを入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // パスワードの一致を確認
            if (newPassword != confirmPassword)
            {
                MessageBox.Show("新しいパスワードと確認用パスワードが一致しません。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 現在ログイン中の社員IDのレコードを取得
                    var employee = context.MEmployees.FirstOrDefault(emp => emp.EmId == loggedInEmployeeID);

                    if (employee != null)
                    {
                        // パスワードを更新
                        employee.EmPassword = newPassword;

                        // データベースに保存
                        context.SaveChanges();

                        MessageBox.Show("パスワードが変更されました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // 社員IDが見つからない場合（理論的には発生しないはず）
                        MessageBox.Show("エラー: ログイン情報が無効です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                // エラー処理
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Login login = new Login(); // 新しいフォームを表示する
            login.Show();
            this.Hide(); // 現在のフォームを隠す

        }

        private void chxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chxPass.Checked)
            {
                // チェックが入っている場合、パスワードを表示
                tbxNewPass.PasswordChar = '\0';// パスワードを見えるようにする
            }
            else
            {
                // チェックが外れている場合、パスワードを隠す
                tbxNewPass.PasswordChar = '*';// パスワードを隠す
            }

            if (chxPass.Checked)
            {
                // チェックが入っている場合、パスワードを表示
                tbxNewPass2.PasswordChar = '\0';// パスワードを見えるようにする
            }
            else
            {
                // チェックが外れている場合、パスワードを隠す
                tbxNewPass2.PasswordChar = '*';// パスワードを隠す
            }
        }
    }

}
