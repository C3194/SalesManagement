using SalesManagement_SysDev.DataAccess;
using SalesManagement_SysDev;
using SalesManagement_SysDev.Entity;

namespace SalesManagementB
{
    public partial class PassLogin : Form
    {
        public PassLogin()
        {
            InitializeComponent();
            tbxPass.PasswordChar = '*';// パスワードを隠す
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string employeeID = tbxEmID.Text.Trim();
            string password = tbxPass.Text.Trim();

            // 入力チェック
            if (string.IsNullOrWhiteSpace(employeeID) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("社員IDとパスワードを入力してください。");
                return;
            }

            try
            {
                using (var context = new SalesManagementContext())
                {
                    // データベースから社員IDとパスワードを確認するクエリ
                    var employee = context.MEmployees
                                          .FirstOrDefault(emp => emp.EmId.ToString() == employeeID && emp.EmPassword == password);

                    if (employee != null)
                    {
                        // ログイン成功時にSessionManagerに社員IDを格納
                        SessionManager.LoggedInEmployeeID = employee.EmId;

                        // ログイン成功のメッセージ
                        MessageBox.Show("ログイン成功", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // 次の画面を開く
                        PassInput menu = new PassInput();
                        menu.Show();
                        this.Hide();
                    }
                    else
                    {
                        // ログイン失敗のメッセージ
                        MessageBox.Show("社員IDまたはパスワードが正しくありません。");
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }

        private void chxPass_CheckedChanged(object sender, EventArgs e)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "前の画面に戻りますか？", // メッセージ
                "確認",                   // タイトル
                MessageBoxButtons.YesNo,  // Yes/No ボタン
                MessageBoxIcon.Question   // アイコン（質問）
            );

            // 「はい」が選ばれた場合のみ処理を続行
            if (result == DialogResult.Yes)
            {

                Login login = new Login(); // 新しいフォームを表示する
                login.Show();
                this.Hide(); // 現在のフォームを隠す

            }
            else
            {
                // 「いいえ」が選ばれた場合は何もしない（戻らない）
            }
        }
    }
}

