using a;
using Microsoft.Data.SqlClient;
using Product_Management;
using SalesManagement_SysDev;
using SalesManagement_SysDev.Entity;
using 注文管理画面;

namespace 画面設計用9._0注文管理
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        // PositionPermissions の定義
        private static readonly Dictionary<int, string> PositionPermissions = new Dictionary<int, string>
        {
            { 1, "管理者" },
            { 2, "物流" },
            { 3, "営業" }
        };

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //ユーザーの認証情報をクリア

            // 確認ダイアログを表示
            var result = MessageBox.Show(
                "ログアウトしますか？",   // メッセージ
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

        private void btnProduct_Click(object sender, EventArgs e)
        {
            Product product = new Product();

            // 社員管理画面を表示
            product.Show();

            // メニュー画面を非表示にする
            this.Hide();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();

            // 社員管理画面を表示
            employee.Show();

            // メニュー画面を非表示にする
            this.Hide();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            Client client = new Client();

            // 社員管理画面を表示
            client.Show();

            // メニュー画面を非表示にする
            this.Hide();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            Order order = new Order();


            // 社員管理画面を表示
            order.Show();

            // メニュー画面を非表示にする
            this.Hide();
        }

        private void btnChumon_Click(object sender, EventArgs e)
        {
            Chumon chumon = new Chumon();

            // 社員管理画面を表示
            chumon.Show();

            // メニュー画面を非表示にする
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

      
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 現在のログインIDを取得
                    int employeeID = SessionManager.LoggedInEmployeeID;

                    // 社員情報を取得
                    var employee = context.MEmployees.FirstOrDefault(emp => emp.EmId == employeeID);
                    if (employee != null)
                    {
                        
                    }
                    else
                    {
                        MessageBox.Show("該当する社員が見つかりません。");
                    }




                    if (employee != null)
                    {
                        // 社員の役職ID[PoId]を取得
                        int positionID = employee.PoId;

                        // 役職IDに基づいてボタンを有効/無効にする
                        SetButtonPermissions(positionID);

                        // PoIDを日本語に変換
                        if (PositionPermissions.TryGetValue(positionID, out string permission))
                        {
                            // 権限情報をラベルに表示
                            lblPermission.Text = $"権限：{permission}";
                        }
                        else
                        {
                            // PoID がマッピングにない場合
                            lblPermission.Text = "権限：不明な役職";
                            MessageBox.Show($"Position ID: {positionID} はマッピングに存在しません。");
                        }
                        //確認用
                        //if (PositionPermissions.TryGetValue(positionID, out string permission))
                        
                    }
                    else
                    {
                        
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void SetButtonPermissions(int positionID)
        {
            // 役職IDごとにボタンの有効/無効を設定
            switch (positionID)
            {
                case 1: // 管理者
                    break;

                case 2: // 物流担当
                    btnClient.Enabled = false;
                    btnOrder.Enabled = false;
                    btnChumon.Enabled = false;
                    btnArrival.Enabled = false;
                    btnSale.Enabled = false;
                    btnShipment.Enabled = false;
                    btnEmployee.Enabled = false;
                    break;

                case 3: // 営業担当
                    btnHattyu.Enabled = false;
                    btnSyukko.Enabled = false;
                    btnWarehousing.Enabled = false;
                    btnProduct.Enabled = false;
                    btnStock.Enabled = false;
                    btnEmployee.Enabled = false;
                    break;

                default: // 不明な役職
                    btnClient.Enabled = false;
                    btnOrder.Enabled = false;
                    btnChumon.Enabled = false;
                    btnArrival.Enabled = false;
                    btnSale.Enabled = false;
                    btnShipment.Enabled = false;
                    btnStock.Enabled = false;
                    btnProduct.Enabled = false;
                    btnWarehousing.Enabled = false;
                    btnSyukko.Enabled = false;
                    btnHattyu.Enabled = false;
                    btnEmployee.Enabled = false;
                    break;
            }
        }

    }
}
