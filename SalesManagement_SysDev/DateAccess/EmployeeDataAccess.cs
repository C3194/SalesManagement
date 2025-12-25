using Product_Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesManagement_SysDev.DateAccess
{
    //internal class EmployeeDataAccess
    //{

       
        //public List<string> EmGetComboboxText()
        //{
        //    List<string> cmbTextList = new List<string>();
        //    try
        //    {
        //        using (var context = new SalesManagementContext())
        //        {
        //            cmbTextList = context.MEmployees.Select(x => x.EmName).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    return cmbTextList;
        //}

        //public List<MEmployee> GetAllEmployee()
        //{
        //    using (var context = new SalesManagementContext())
        //    {
        //        return context.MEmployees.Select(x => new MEmployee
        //        {
        //            EmName = x.EmName,
        //            EmId = x.EmId
        //        }).ToList();
        //    }
        //}

        //public int? GetEmIdByName(string name)
        //{
        //    using (var context = new SalesManagementContext())
        //    {
        //        var employees = context.MEmployees
        //                              .Where(x => x.EmName == name)
        //                              .OrderBy(x => x.EmId) // IDが小さい順にソート
        //                              .FirstOrDefault();
        //        return employees.EmId;
        //        // 見つかった場合はIDを返し、見つからなければnullを返す
        //    }
        //}

        //public string GetEmNameById(int id)
        //{
        //    using (var context = new SalesManagementContext())
        //    {
        //        var employees = context.MEmployees
        //        .Where(x => x.EmId == id) // 社員IDが一致するものを検索
        //        .FirstOrDefault(); // 最初の一致するデータを取得
        //        if (employees != null)
        //        {
        //            return employees.EmName; // 社員名を返す
        //        }
        //        else
        //        {
        //            return null; // 見つからなかった場合はnullを返す
        //        }
        //    }
        //}
        //public List<MEmployee> GetEmployeeData(int? emFlag = null)
        //{
        //    try
        //    {
        //        using (var context = new SalesManagementContext())
        //        {
        //            // クエリを作成
        //            var query = context.MEmployees.AsQueryable();

        //            // フィルタリング条件を追加
        //            if (emFlag.HasValue)
        //            {
        //                query = query.Where(em => em.EmFlag == emFlag.Value);
        //            }

        //            // フィルタリングされた結果をリストで返す
        //            return query.ToList();


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // エラーが発生した場合はログやメッセージを表示する
        //        MessageBox.Show(ex.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return new List<MEmployee>(); // 空のリストを返す
        //    }

        //}


        //public bool CheckDataE(int Emid, int Soid, int Poid, int Emps, int Empn, DateTime Emdate)
        //{
        //    try
        //    {
        //       using (var context = new SalesManagementContext())
        //       {
        //             //データベース内に条件に合うデータが存在するか確認
        //            return context.MEmployees.Any(data =>
        //                 data.PoId == Poid &&
        //                 data.SoId == Soid &&
        //                 data.EmId == Emid &&
                         
        //                 data.EmHiredate == Emdate
        //                 );
        //       }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"データベースエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false; // エラー時はfalseを返す
        //    }
        //}
           
      
        ////表示・非表示
        //public int? GetEmFlag(int id)
        //{
        //    try
        //    {
        //        using (var context = new SalesManagementContext())
        //        {
        //            // 条件に一致するレコードのflgを取得
        //            var flgValue = context.MEmployees
        //                .Where(x => x.EmId == id) // 条件を指定 (例: 主キーがidと一致)
        //                .Select(x => x.EmFlag)     // flg値だけを取得
        //                .FirstOrDefault();      // 最初の値を取得（ない場合はデフォルト値）

        //            return flgValue;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"エラー: {ex.Message}", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null; // エラー時はnullを返す
        //    }
        //}
        //public bool ChangeEmhideflg(int id, int newflg, string emhide)
        //{
        //    try
        //    {
        //        using (var context = new SalesManagementContext())
        //        {
        //            // 更新対象の注文情報を取得
        //            var chdata = context.MEmployees.FirstOrDefault(x => x.EmId == id);

        //            if (chdata != null)
        //            {
        //                // 値を更新
        //                chdata.EmFlag = newflg;
        //                //非表示理由を更新
        //                chdata.EmHidden = emhide;
        //                // データベースに保存
        //                context.SaveChanges();

        //                return true; // 更新成功
        //            }
        //            else
        //            {
        //                MessageBox.Show("対象のデータが見つかりませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                return false; // 対象データなし
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"データベース更新エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false; // エラー発生
        //    }
        //}
        ////検索
        //public List<MEmployee> SearchEmData(int? emId, int? soId, int? poId,int? emPhone,int? emPassword,string? emName, DateTime? emhireDate)
        //{
        //    try
        //    {
        //        using (var context = new SalesManagementContext())
        //        {
        //            var query = context.MEmployees.AsQueryable(); // 初期状態としてMEmployeesテーブルの全てのデータを取得

        //            // emIdが指定されていれば、emIdでフィルタリング
        //            if (emId.HasValue)
        //                query = query.Where(employee => employee.EmId == emId.Value);

        //            // soIdが指定されていれば、soIdでフィルタリング
        //            if (soId.HasValue)
        //                query = query.Where(employee => employee.SoId == soId.Value);

        //            // poIdが指定されていれば、poIdでフィルタリング
        //            if (poId.HasValue)
        //                query = query.Where(employee => employee.PoId == poId.Value);

        //            // emNameがnullまたは空文字列でない場合にフィルタリング
        //            if (!string.IsNullOrEmpty(emName))
        //                query = query.Where(product => product.EmName == emName);

        //            // chDateが指定されていれば、chDateでフィルタリング
        //            if (emhireDate.HasValue)
        //                query = query.Where(employee => employee.EmHiredate == emhireDate.Value.Date); // 時間部分は無視して比較



        //            // 検索結果をリストとして返す
        //            return query.ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"検索中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return new List<MEmployee>(); // エラーが発生した場合は空のリストを返す
              
        //    }
        //}

        ////登録
        //public MEmployee? RegisterEmData(
        //int? emId, int soId, int poId, string emName, DateTime emHiredate, string emPassword, string emPhone, int emFlg)
        //{
        //    try
        //    {
        //        using (var context = new SalesManagementContext())
        //        {
        //            // 新しい社員データを作成
        //            var newEmployee = new MEmployee
        //            {
        //                EmId = emId ?? 0, // 自動生成の場合は不要
        //                SoId = soId,
        //                PoId = poId,
        //                EmName = emName,
        //                EmHiredate = emHiredate,
        //                EmPassword = emPassword,
        //                EmPhone = emPhone,
        //                EmFlag = 0, // 初期値として表示状態
        //                EmHidden = null // 初期値として非表示理由なし
        //            };

        //            // データベースに追加
        //            context.MEmployees.Add(newEmployee);
        //            context.SaveChanges(); // データベースに保存
        //            return newEmployee;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"登録中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null;
        //    }
        //}
        //public bool CheckData(int? Emid, string Emps, string Empn, string Emname)
        //{
        //    try
        //    {
        //        using (var context = new SalesManagementContext())
        //        {
        //            //データベース内に条件に合うデータが存在するか確認
        //            return context.MEmployees.Any(data =>
                        
        //                 data.EmId == Emid &&
        //                 data.EmName == Emname &&
        //                 data.EmPhone ==Empn &&
        //                 data.EmPassword == Emps 
                        
        //                 );
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"データベースエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false; // エラー時はfalseを返す
        //    }
        //}

        ////更新
        //public MEmployee? UpdateEmployee(int emid, string emnames, int soid, int poid, string emPhone, string emPass, DateTime emhidate, int hiflgindex)
        //{
        //    try
        //    {
        //        using (var context = new SalesManagementContext())
        //        {
        //            // 更新対象の注文情報を取得
        //            var employee = context.MEmployees.FirstOrDefault(x => x.EmId == emid);
        //            if (employee != null)
        //            {
        //                employee.EmId = emid;
        //                employee.EmName = emnames;
        //                employee.SoId = soid;
        //                employee.PoId = poid;
        //                employee.EmPhone = emPhone;
        //                employee.EmPassword = emPass; // int? 型なのでそのままセット
        //                employee.EmHiredate = emhidate;
        //                employee.EmFlag = hiflgindex;

        //                context.SaveChanges(); // 更新をデータベースに反映
        //                return employee;
        //            }
        //        }
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"データベース更新エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return null; // エラー発生
        //    }
        //}

        //public MEmployee? GetEmployee(int emid)
        //{
        //    try
        //    {
        //        using (var context = new SalesManagementContext())
        //        {
        //            // 社員IDで商品情報を検索
        //            var employee = context.MEmployees.FirstOrDefault(x => x.EmId == emid);

        //            // 社員が見つかれば返す
        //            return employee;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // エラーハンドリング: 必要に応じてログに記録
        //        // ここではエラーを再スローして上位の呼び出し元に伝えることが一般的
        //        throw new InvalidOperationException("社員情報の取得に失敗しました。", ex);
        //    }
        //}

        //internal string GetEmNameById(string emId)
        //{
        //    throw new NotImplementedException();
        //}

        /////////////////////////////////
        ////メソッド名：CheckEmployeeIDExistence()
        ////引　数   ：int 検索する社員ID
        ////戻り値   ：bool　結果（True：一致あり、False：一致なし）
        ////機　能   ：一致する社員IDの有無を確認(重複チェック)
        /////////////////////////////////
        //public bool CheckEmployeeIDExistence(int EmployeeID)
        //{
        //    bool flg = false;

        //    try
        //    {
        //        var context = new SalesManagementContext();
        //        //社員IDで一致するデータが存在するか
        //        flg = context.MEmployees.Any(x => x.EmId == EmployeeID);
        //        context.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return flg;
        //}

        /////////////////////////////////
        ////メソッド名：GetEmployeeDspData()
        ////引　数   ：なし
        ////戻り値   ：List<MEmployee>　社員情報
        ////機　能   ：表示用社員データの取得(コンボボックス)
        /////////////////////////////////
        //public List<MEmployee> GetEmployeeDspData()
        //{
        //    List<MEmployee> employees = new List<MEmployee>();
        //    try
        //    {
        //        var context = new SalesManagementContext();
        //        //非表示フラグが0のデータを抜き取る
        //        employees = context.MEmployees.Where(x => x.EmFlag == 0).ToList();
        //        context.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return employees;
        //}
    //}
}

                
    
    
