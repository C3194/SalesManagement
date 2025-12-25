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

namespace SalesManagement_SysDev.DataAccess
{
    internal class EmployeeDataAccess
    {


        public List<string> EmGetComboboxText()
        {
            List<string> cmbTextList = new List<string>();
            try
            {
                using (var context = new SalesManagementContext())
                {
                    cmbTextList = context.MEmployees.Select(x => x.EmName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cmbTextList;
        }

        public int? GetEmIdByName(string name)
        {
            using (var context = new SalesManagementContext())
            {
                var employee = context.MEmployees
                                      .Where(x => x.EmName == name)
                                      .OrderBy(x => x.EmId) // IDが小さい順にソート
                                      .FirstOrDefault();
                return employee.EmId;
                // 見つかった場合はIDを返し、見つからなければnullを返す
            }
        }

        public string GetEmNameById(int id)
        {
            using (var context = new SalesManagementContext())
            {
                var employee = context.MEmployees
                .Where(x => x.EmId == id) // 営業所IDが一致するものを検索
                .FirstOrDefault(); // 最初の一致するデータを取得
                if (employee != null)
                {
                    return employee.EmName; // 営業所名を返す
                }
                else
                {
                    return null; // 見つからなかった場合はnullを返す
                }
            }
        }


        public List<MEmployee> GetAllEmployee()
        {
            using (var context = new SalesManagementContext())
            {
                return context.MEmployees.Select(x => new MEmployee
                {
                    EmName = x.EmName,
                    EmId = x.EmId
                }).ToList();
            }
        }

        public int? EmIdByName(string emname)
        {
            using (var context = new SalesManagementContext())
            {
                var employee = context.MEmployees
                    .FirstOrDefault(m => m.EmName == emname);
                return employee?.EmId;
                // 見つかった場合はIDを返し、見つからなければnullを返す

            }
        }

        public string EmNameById(string emid)
        {
            using (var context = new SalesManagementContext())
            {
                var employees = context.MEmployees
                     .FirstOrDefault(m => m.EmId.ToString() == emid); // 最初の一致するデータを取得

                return employees?.EmName; // 社員名を返す

            }
        }

        public bool CheckDataE(int Emid, int Soid, int Poid, int Emps, int Empn, DateTime Emdate)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    //データベース内に条件に合うデータが存在するか確認
                    return context.MEmployees.Any(data =>
                         data.PoId == Poid &&
                         data.SoId == Soid &&
                         data.EmId == Emid &&

                         data.EmHiredate == Emdate
                         );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データベースエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // エラー時はfalseを返す
            }
        }


        //表示・非表示
        public int? GetEmFlag(int id)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 条件に一致するレコードのflgを取得
                    var flgValue = context.MEmployees
                        .Where(x => x.EmId == id) // 条件を指定 (例: 主キーがidと一致)
                        .Select(x => x.EmFlag)     // flg値だけを取得
                        .FirstOrDefault();      // 最初の値を取得（ない場合はデフォルト値）

                    return flgValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラー: {ex.Message}", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // エラー時はnullを返す
            }
        }
        public bool ChangeEmhideflg(int id, int newflg, string emhide)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 更新対象の注文情報を取得
                    var chdata = context.MEmployees.FirstOrDefault(x => x.EmId == id);

                    if (chdata != null)
                    {
                        // 値を更新
                        chdata.EmFlag = newflg;
                        //非表示理由を更新
                        chdata.EmHidden = emhide;
                        // データベースに保存
                        context.SaveChanges();

                        return true; // 更新成功
                    }
                    else
                    {
                        MessageBox.Show("対象のデータが見つかりませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; // 対象データなし
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データベース更新エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // エラー発生
            }
        }
        //検索
        public List<MEmployee> SearchEmData(int? emId, int? soId, int? poId, int? emPhone, int? emPassword, string? emName, DateTime? emhireDate)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    var query = context.MEmployees.AsQueryable(); // 初期状態としてMEmployeesテーブルの全てのデータを取得

                    // emIdが指定されていれば、emIdでフィルタリング
                    if (emId.HasValue)
                        query = query.Where(employee => employee.EmId == emId.Value);

                    // soIdが指定されていれば、soIdでフィルタリング
                    if (soId.HasValue)
                        query = query.Where(employee => employee.SoId == soId.Value);

                    // poIdが指定されていれば、poIdでフィルタリング
                    if (poId.HasValue)
                        query = query.Where(employee => employee.PoId == poId.Value);

                    // emNameがnullまたは空文字列でない場合にフィルタリング
                    if (!string.IsNullOrEmpty(emName))
                        query = query.Where(product => product.EmName == emName);

                    // chDateが指定されていれば、chDateでフィルタリング
                    if (emhireDate.HasValue)
                        query = query.Where(employee => employee.EmHiredate == emhireDate.Value.Date); // 時間部分は無視して比較



                    // 検索結果をリストとして返す
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"検索中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MEmployee>(); // エラーが発生した場合は空のリストを返す

            }
        }

        //登録
        public MEmployee? RegisterEmData(
        int? emId, int soId, int poId, string emName, DateTime emHiredate, string emPassword, string emPhone, int emFlg)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 新しい社員データを作成
                    var newEmployee = new MEmployee
                    {
                        EmId = emId ?? 0, // 自動生成の場合は不要
                        SoId = soId,
                        PoId = poId,
                        EmName = emName,
                        EmHiredate = emHiredate,
                        EmPassword = emPassword,
                        EmPhone = emPhone,
                        EmFlag = 0, // 初期値として表示状態
                        EmHidden = null // 初期値として非表示理由なし
                    };

                    // データベースに追加
                    context.MEmployees.Add(newEmployee);
                    context.SaveChanges(); // データベースに保存
                    return newEmployee;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"登録中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public bool CheckData(int? Emid, string Emps, string Empn, string Emname)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    //データベース内に条件に合うデータが存在するか確認
                    return context.MEmployees.Any(data =>

                         data.EmId == Emid &&
                         data.EmName == Emname &&
                         data.EmPhone == Empn &&
                         data.EmPassword == Emps

                         );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データベースエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // エラー時はfalseを返す
            }
        }

        //更新
        public MEmployee? UpdateEmployee(int emid, string emnames, int soid, int poid, string emPhone, string emPass, DateTime emhidate, int hiflgindex)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 更新対象の注文情報を取得
                    var employee = context.MEmployees.FirstOrDefault(x => x.EmId == emid);
                    if (employee != null)
                    {
                        employee.EmId = emid;
                        employee.EmName = emnames;
                        employee.SoId = soid;
                        employee.PoId = poid;
                        employee.EmPhone = emPhone;
                        employee.EmPassword = emPass; // int? 型なのでそのままセット
                        employee.EmHiredate = emhidate;
                        employee.EmFlag = hiflgindex;

                        context.SaveChanges(); // 更新をデータベースに反映
                        return employee;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データベース更新エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // エラー発生
            }
        }

        public MEmployee? GetEmployee(int emid)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 社員IDで商品情報を検索
                    var employee = context.MEmployees.FirstOrDefault(x => x.EmId == emid);

                    // 社員が見つかれば返す
                    return employee;
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング: 必要に応じてログに記録
                // ここではエラーを再スローして上位の呼び出し元に伝えることが一般的
                throw new InvalidOperationException("社員情報の取得に失敗しました。", ex);
            }
        }

        public bool IsEmployeeIdExists(string employeeId)
        {
            try
            {
                if (int.TryParse(employeeId, out int parsedEmployeeId))
                {
                    using (var context = new SalesManagementContext())
                    {
                        // 社員IDで社員が既に存在するか確認
                        var existingEmployee = context.MEmployees.FirstOrDefault(p => p.EmId.ToString() == employeeId);
                        return existingEmployee != null; // 存在する場合はtrueを返す
                    }
                }
                else
                {
                    // 社員IDが整数に変換できなかった場合
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool isEmPhoneExists(string emphone)
        {
            try
            {
                if (int.TryParse(emphone, out int parsedEmphone))
                {
                    using (var context = new SalesManagementContext())
                    {
                        // 電話番号が既に存在するか確認
                        var existingEmployee = context.MEmployees.FirstOrDefault(p => p.EmPhone.ToString() == emphone);
                        return existingEmployee != null; // 存在する場合はtrueを返す
                    }
                }
                else
                {
                    // 電話番号が整数に変換できなかった場合
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool isEmPasswordExists(string empassword)
        {
            try
            {
                if (int.TryParse(empassword, out int parsedEmPassword))
                {
                    using (var context = new SalesManagementContext())
                    {
                        // パスワードが既に存在するか確認
                        var existingEmployee = context.MEmployees.FirstOrDefault(p => p.EmPassword.ToString() == empassword);
                        return existingEmployee != null; // 存在する場合はtrueを返す
                    }
                }
                else
                {
                    // パスワードが整数に変換できなかった場合
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        ///////////////////////////////
        //メソッド名：AddEmployeeData()
        //引　数   ：MEmployee 社員情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：商品データの登録
        ///////////////////////////////
        public bool AddEmployeeData(MEmployee RegEmployee)
        {
            try
            {
                var context = new SalesManagementContext();
                context.MEmployees.Add(RegEmployee);
                context.SaveChanges();
                context.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        ///////////////////////////////
        //メソッド名：UpdateEmployeeData()
        //引　数   ：MEmployee 商品情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：商品データの更新
        ///////////////////////////////
        public bool UpdateEmployeeData(MEmployee UpdEmployee)
        {
            try
            {
                var context = new SalesManagementContext();
                var product = context.MEmployees.Single(x => x.EmId == UpdEmployee.EmId);
                product.EmName = UpdEmployee.EmName;
                product.PoId = UpdEmployee.PoId;
                product.SoId = UpdEmployee.SoId;
                product.EmPassword = UpdEmployee.EmPassword;
                product.EmPhone = UpdEmployee.EmPhone;
                product.EmHiredate = UpdEmployee.EmHiredate;
                product.EmFlag = UpdEmployee.EmFlag;
                product.EmHidden = UpdEmployee.EmHidden;
                context.SaveChanges();
                context.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        ///////////////////////////////
        //メソッド名：GetEmployeeData() オーバーロード
        //引　数   ：MEmployeeDsp 商品情報
        //戻り値   ：List<MEmployeeDsp>　商品情報
        //機　能   ：条件部分一致商品データの取得(検索)
        ///////////////////////////////
        public List<MEmployeeDsp> GetEmployeeData(MEmployeeDsp selectCondition)
        {
            List<MEmployeeDsp> employee = new List<MEmployeeDsp>();
            try
            {
                var context = new SalesManagementContext();
                var tb = from em in context.MEmployees
                         join po in context.MPositions
                         on em.PoId equals po.PoId
                         join so in context.MSalesOffices
                         on em.SoId equals so.SoId

                         where em.EmId.ToString().Contains(selectCondition.EmId.ToString()) &&
                               em.EmName.Contains(selectCondition.EmName.ToString()) &&
                               so.SoId.ToString().Contains(selectCondition.SoId.ToString()) &&
                               po.PoId.ToString().Contains(selectCondition.PoId.ToString()) &&
                               em.EmPassword.Contains(selectCondition.EmPassword) &&
                               em.EmPhone.Contains(selectCondition.EmPhone)

                         select new
                         {
                             em.EmId,
                             em.EmName,
                             so.SoId,
                             po.PoId,
                             em.EmPassword,
                             em.EmPhone,
                             em.EmHiredate,
                             em.EmFlag,
                             em.EmHidden,
                         };

                //tbをemployeeに
                foreach (var p in tb)
                {

                    employee.Add(new MEmployeeDsp()
                    {
                        EmId = p.EmId.ToString(),
                        EmName = p.EmName,
                        SoId = p.SoId.ToString(),
                        PoId = p.PoId.ToString(),
                        EmHiredate = p.EmHiredate,
                        EmPassword = p.EmPassword,
                        EmPhone = p.EmPhone,
                        EmFlag = p.EmFlag.ToString(),
                        EmHidden = p.EmHidden,

                    });
                }

                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return employee;
        }

        ///////////////////////////////
        //メソッド名：CheckEmployeeIDExistence()
        //引　数   ：int 検索する社員ID
        //戻り値   ：bool　結果（True：一致あり、False：一致なし）
        //機　能   ：一致する社員IDの有無を確認(重複チェック)
        ///////////////////////////////
        public bool CheckEmployeeIDExistence(int EmployeeID)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                //社員IDで一致するデータが存在するか
                flg = context.MEmployees.Any(x => x.EmId == EmployeeID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetEmployeeData()
        //引　数   ：なし
        //戻り値   ：List<MEmployee>　顧客情報
        //機　能   ：顧客データの取得(一覧表示)
        ///////////////////////////////
        public List<MEmployeeDsp> GetEmployeeData()
        {
            List<MEmployeeDsp> employees = new List<MEmployeeDsp>();
            try
            {
                var context = new SalesManagementContext();
                var tb = from em in context.MEmployees
                         //join so in context.MSalesOffices
                         //on em.SoId equals so.SoId
                         //join po in context.MPositions
                         //on em.PoId equals po.PoId
                         where em.EmFlag == 0
                         select new

                         {
                             em.EmId,
                             em.EmName,
                             em.SoId,
                             em.PoId,
                             em.EmPassword,
                             em.EmPhone,
                             em.EmHiredate,
                             em.EmFlag,
                             em.EmHidden,
                         };

                //tbをemployeeに入れる
                foreach (var p in tb)
                {
                    employees.Add(new MEmployeeDsp()
                    {
                        EmId = p.EmId.ToString(),
                        EmName = p.EmName,
                        SoId = p.SoId.ToString(),
                        PoId = p.PoId.ToString(),
                        EmHiredate = p.EmHiredate,
                        EmPassword = p.EmPassword,
                        EmPhone = p.EmPhone,
                        EmFlag = p.EmFlag.ToString(),
                        EmHidden = p.EmHidden
                    });
                }

                //int batchSize = 100; // 一度に処理するデータのサイズ
                //int totalCount = tb.Count(); // メソッドを呼び出して総数を取得

                //for (int i = 0; i < totalCount; i += batchSize)
                //{
                //    var batch = tb.Skip(i).Take(batchSize);
                //    foreach (var p in batch)
                //    {
                //        employees.Add(new MEmployeeDsp()
                //        {
                //            EmId = p.EmId.ToString(),
                //            EmName = p.EmName,
                //            SoId = p.SoId.ToString(),
                //            PoId = p.PoId.ToString(),
                //            EmHiredate = p.EmHiredate,
                //            EmPassword = p.EmPassword,
                //            EmPhone = p.EmPhone,
                //            EmFlag = p.EmFlag.ToString(),
                //            EmHidden = p.EmHidden,
                //        });
                //    }
                //}

                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return employees;
        }

        ///////////////////////////////
        //メソッド名：GetEmployeeHiddenData()
        //引　数   ：なし
        //戻り値   ：List<MEmployee>　商品情報
        //機　能   ：商品データの取得(非表示リスト)
        ///////////////////////////////
        public List<MEmployeeDsp> GetEmployeeHiddenData()
        {
            List<MEmployeeDsp> employees = new List<MEmployeeDsp>();
            try
            {
                var context = new SalesManagementContext();
                var tb = from em in context.MEmployees
                        // join so in context.MSalesOffices
                        //on em.SoId equals so.SoId
                        // join po in context.MPositions
                        // on em.PoId equals po.PoId
                         where em.EmFlag == 2
                         select new

                         {
                             em.EmId,
                             em.EmName,
                             em.SoId,
                             em.PoId,
                             em.EmPassword,
                             em.EmPhone,
                             em.EmHiredate,
                             em.EmFlag,
                             em.EmHidden,
                         };

                //tbをemployeeに入れる
                foreach (var p in tb)
                {
                    employees.Add(new MEmployeeDsp()
                    {
                        EmId = p.EmId.ToString(),
                        EmName = p.EmName,
                        SoId = p.SoId.ToString(),
                        PoId = p.PoId.ToString(),
                        EmHiredate = p.EmHiredate,
                        EmPassword = p.EmPassword,
                        EmPhone = p.EmPhone,
                        EmFlag = p.EmFlag.ToString(),
                        EmHidden = p.EmHidden,

                    });

                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return employees;
        }

        ///////////////////////////////
        //メソッド名：GetEmployeeDspData()
        //引　数   ：なし
        //戻り値   ：List<MEmployee>　社員情報
        //機　能   ：表示用社員データの取得(コンボボックス)
        ///////////////////////////////
        public List<MEmployee> GetEmployeeDspData()
        {
            List<MEmployee> employees = new List<MEmployee>();
            try
            {
                var context = new SalesManagementContext();
                //非表示フラグが0のデータを抜き取る
                employees = context.MEmployees.Where(x => x.EmFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return employees;
        }
    }
}




