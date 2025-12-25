using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement_SysDev.DataAccess
{
    internal class LoginDataAccess
    {
        public List<MEmployee> GetEmployeeData(string emID, string emPasword)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 入力された社員IDを int に変換
                    if (int.TryParse(emID, out int parsedEmID))
                    {
                        // クエリを作成
                        var query = context.MEmployees.AsQueryable();

                        //社員IDとパスワードでフィルタリング
                        query = query.Where(emp => emp.EmId == parsedEmID && emp.EmPassword == emPasword);

                        // フィルタリングされた結果をリストで返す
                        return query.ToList();
                    }
                    else
                    {
                        // 入力された社員IDが整数に変換できない場合はエラーメッセージを表示
                        MessageBox.Show("社員IDは整数で入力してください。", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        // 空のリストを返す
                        return new List<MEmployee>();
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合はログやメッセージを表示する
                MessageBox.Show(ex.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MEmployee>(); // 空のリストを返す
            }
        }
    }
}
