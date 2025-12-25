using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesManagement_SysDev.DateAccess
{
    internal class ChumonDataAccess
    {
        public List<TChumon> GetChumonData(int? chFlag = null)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // クエリを作成
                    var query = context.TChumons.AsQueryable();

                    // フィルタリング条件が指定されている場合は条件を追加
                    if (chFlag.HasValue)
                    {
                        query = query.Where(ch => ch.ChFlag == chFlag.Value);
                    }

                    // フィルタリングされた結果をリストで返す
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合はログやメッセージを表示する
                MessageBox.Show(ex.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TChumon>(); // 空のリストを返す
            }
        }

        public bool CheckDataExistence(int Chid, int Soid, int Emid, int Clid, int Chstflg, DateTime Chdate)
        {
            try
            {

                using (var context = new SalesManagementContext())
                {
                    // データベース内に条件に合うデータが存在するか確認
                    return context.TChumons.Any(data =>
                        data.ChId == Chid &&  
                        data.SoId == Soid &&
                        data.EmId == Emid &&
                        data.ClId == Clid &&
                        data.ChStateFlag == Chstflg &&
                        data.ChDate == Chdate 
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データベースエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // エラー時はfalseを返す
            }
        }

        public int? GetChFlag(int id)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 条件に一致するレコードのflgを取得
                    var flgValue = context.TChumons
                        .Where(x => x.ChId == id) // 条件を指定 (例: 主キーがidと一致)
                        .Select(x => x.ChFlag)     // flg値だけを取得
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

        public bool ChangeChhideflg(int id, int newflg, string chhide)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 更新対象の注文情報を取得
                    var chdata = context.TChumons.FirstOrDefault(x => x.ChId == id);

                    if (chdata != null)
                    {
                        // 値を更新
                        chdata.ChFlag = newflg;
                        //非表示理由を更新
                        chdata.ChHidden = chhide;
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
        
        public List<TChumon> SearchChData(int? chId, int? soId, int? emId, int? clId, int? orId, DateTime? chDate, int? stflg)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    var query = context.TChumons.AsQueryable(); // 初期状態としてTChumonsテーブルの全てのデータを取得

                    // chIdが指定されていれば、chIdでフィルタリング
                    if (chId.HasValue)
                        query = query.Where(chumon => chumon.ChId == chId.Value);

                    // soIdが指定されていれば、soIdでフィルタリング
                    if (soId.HasValue)
                        query = query.Where(chumon => chumon.SoId == soId.Value);

                    // emIdが指定されていれば、emIdでフィルタリング
                    if (emId.HasValue)
                        query = query.Where(chumon => chumon.EmId == emId.Value);

                    // clIdが指定されていれば、clIdでフィルタリング
                    if (clId.HasValue)
                        query = query.Where(chumon => chumon.ClId == clId.Value);

                    // orIdが指定されていれば、orIdでフィルタリング
                    if (orId.HasValue)
                        query = query.Where(chumon => chumon.OrId == orId.Value);

                    // chDateが指定されていれば、chDateでフィルタリング
                    if (chDate.HasValue)
                        query = query.Where(chumon => chumon.ChDate == chDate.Value.Date); // 時間部分は無視して比較

                    // stflgが-1以外（有効な選択肢）が指定されていれば、stflgでフィルタリング
                    if (stflg != -1)
                        query = query.Where(chumon => chumon.ChStateFlag == stflg);

                    // 検索結果をリストとして返す
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"検索中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TChumon>(); // エラーが発生した場合は空のリストを返す
            }
        }

        public List<int> GetHiddenChIds(List<int> chIdList)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 注文IDリストをフィルタリングして非表示の注文IDだけを取得
                    var hiddenChIds = context.TChumons
                        .Where(chumon => chIdList.Contains(chumon.ChId) && chumon.ChFlag == 2) // ChFlagが2のものを絞り込む
                        .Select(chumon => chumon.ChId) // ChIdだけを取得
                        .ToList(); // 結果をリストに変換

                    return hiddenChIds; // 非表示の注文IDリストを返す
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラー: {ex.Message}", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<int>(); // エラー時は空のリストを返す
            }
        }

        ///////////////////////////////
        //メソッド名：GetChumonId()
        //引　数   ：なし
        //戻り値   ：注文ID（ChId）、注文IDが見つからなければ0
        //機　能   ：注文ID（ChId）を取得する
        ///////////////////////////////
        public int GetChumonId()
        {
            int ChumonId = 0;
            try
            {
                var context = new SalesManagementContext();
                ChumonId = context.TChumons.OrderByDescending(x => x.ChId).Select(x => x.ChId).FirstOrDefault();

                context.Dispose();
                return ChumonId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ChumonId;
            }
        }

        ///////////////////////////////
        //メソッド名：AddChumonData()
        //引　数   ：TChumon 注文情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：注文データの登録
        ///////////////////////////////
        public bool AddChumonData(TChumon RegChumon)
        {
            try
            {
                var context = new SalesManagementContext();
                context.TChumons.Add(RegChumon);
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
    }

}
