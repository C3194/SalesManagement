using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using a;
using Microsoft.EntityFrameworkCore;
using Product_Management;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesManagement_SysDev.DataAccess
{
    internal class OrderDataAccess
    {
        //public List<string> OrGetComboboxText()
        //{
        //    List<string> cmbTextList = new List<string>();
        //    try
        //    {
        //        using (var context = new SalesManagementContext())
        //        {
        //            cmbTextList = context.TOrders.Select(x => x.OrName).ToList();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    return cmbTextList;
        //}

        public static List<TOrder> GetOrderData(int? orFlag = null)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // クエリを作成
                    var query = context.TOrders.AsQueryable();

                    // フィルタリング条件が指定されている場合は条件を追加
                    if (orFlag.HasValue)
                    {
                        query = query.Where(or => or.OrFlag == orFlag.Value);
                    }

                    // フィルタリングされた結果をリストで返す
                    return query.ToList();
                }
            }

            catch (Exception ex)
            {
                // エラーが発生した場合はログやメッセージを表示する
                MessageBox.Show(ex.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TOrder>(); // 空のリストを返す
            }
        }

        public bool CheckDataExistence( int Soid, int Emid, int Clid, int Orstflg, DateTime Ordate)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // データベース内に条件に合うデータが存在するか確認
                    return context.TOrders.Any(data =>
                     
                        data.SoId == Soid &&
                        data.EmId == Emid &&
                        data.ClId == Clid &&
                        data.OrStateFlag == Orstflg && // 状態フラグも条件に含む
                         data.OrDate == Ordate  // 日付も条件に含む
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データベースエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // エラー時はfalseを返す
            }
        }


        public int? GetOrFlag(int id)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 条件に一致するレコードのflgを取得
                    var flgValue = context.TOrders
                        .Where(x => x.OrId == id) // 条件を指定 (例: 主キーがidと一致)
                        .Select(x => x.OrFlag)     // flg値だけを取得
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

        public bool ChangeOrhideflg(int id, int newflg, string orhide)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 更新対象の注文情報を取得
                    var ordata = context.TOrders.FirstOrDefault(x => x.OrId == id);

                    if (ordata != null)
                    {
                        // 値を更新
                        ordata.OrFlag = newflg;
                        //非表示理由を更新
                        ordata.OrHidden = orhide;
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

        public List<TOrder> SearchOrData(int? orId, int? soId, int? emId, int? clId, DateTime? orDate, int? stflg)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    var query = context.TOrders.AsQueryable(); // 初期状態としてTOrderテーブルの全てのデータを取得

                    
                    // orIdが指定されていれば、orIdでフィルタリング
                    if (orId.HasValue)
                        query = query.Where(order => order.OrId == orId.Value);

                    // soIdが指定されていれば、soIdでフィルタリング
                    if (soId.HasValue)
                        query = query.Where(order => order.SoId == soId.Value);

                    // emIdが指定されていれば、emIdでフィルタリング
                    if (emId.HasValue)
                        query = query.Where(order => order.EmId == emId.Value);

                    // clIdが指定されていれば、clIdでフィルタリング
                    if (clId.HasValue)
                        query = query.Where(order => order.ClId == clId.Value);


                    // orDateが指定されていれば、orDateでフィルタリング（日時部分は無視して比較）
                    if (orDate.HasValue)
                        query = query.Where(order => order.OrDate.Date == orDate.Value.Date);

                    // stflgが-1以外（有効な選択肢）が指定されていれば、stflgでフィルタリング
                    if (stflg != -1)
                        query = query.Where(order => order.OrStateFlag == stflg);

                    // 検索結果をリストとして返す
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"検索中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TOrder>(); // エラーが発生した場合は空のリストを返す
            }
        }

        public List<int> GetHiddenOrIds(List<int> orIdList)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 注文IDリストをフィルタリングして非表示の注文IDだけを取得
                    var hiddenOrIds = context.TOrders
                        .Where(order => orIdList.Contains(order.OrId) && order.OrFlag == 2) // ChFlagが2のものを絞り込む
                        .Select(order => order.OrId) // OrIdだけを取得
                        .ToList(); // 結果をリストに変換

                    return hiddenOrIds; // 非表示の受注IDリストを返す
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラー: {ex.Message}", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<int>(); // エラー時は空のリストを返す
            }
        }
        //    catch (Exception ex)
        //    {
        //        // 例外発生時の処理（ログ出力やエラーメッセージの通知など）
        //        Console.WriteLine($"エラーが発生しました: {ex.Message}");
        //        throw; // 必要に応じて例外を再スロー
        //    }
        //    finally
        //    {
        //        // finally ブロックは通常、リソース解放などの後処理に使用します。
        //        // この例では using ステートメントでリソース管理を行っているため、追加の処理は不要です。
        //        Console.WriteLine("GetOrderData処理が終了しました。");
        //    }
        //}

        ///////////////////////////////
        //メソッド名：IsOrderIdExists
        //引　数   ：TOrder 受注データ
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：受注データで既に存在するか確認
        ///////////////////////////////
        public bool IsOrderIdExists(TOrder order)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                flg = context.TOrders.Any(x => x.ClCharge == order.ClCharge &&
                                            x.ClId == order.ClId && x.EmId == order.EmId &&
                                            x.SoId == order.SoId && x.OrDate == order.OrDate);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetOrderId()
        //引　数   ：なし
        //戻り値   ：受注ID（OrId）、受注IDが見つからなければ0
        //機　能   ：受注ID（OrId）を取得する
        ///////////////////////////////
        public int GetOrderId()
        {
            int OrderId = 0;
            try
            {
                var context = new SalesManagementContext();
                OrderId = context.TOrders.OrderByDescending(x => x.OrId).Select(x => x.OrId).FirstOrDefault();

                context.Dispose();
                return OrderId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return OrderId;
            }
        }

        ///////////////////////////////
        //メソッド名：AddOrderData()
        //引　数   ：TOrder 受注情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：受注データの登録
        ///////////////////////////////
        public bool AddOrderData(TOrder RegOrder)
        {
            try
            {
                var context = new SalesManagementContext();
                context.TOrders.Add(RegOrder);
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
        //メソッド名：UpdateOrderData()
        //引　数   ：TOrder 受注情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：受注データの更新
        ///////////////////////////////
        public bool UpdateOrderData(TOrder UpdOrder)
        {
            try
            {
                var context = new SalesManagementContext();
                var order = context.TOrders.Single(x => x.OrId == UpdOrder.OrId);
                order.ClCharge = UpdOrder.ClCharge;
                order.ClId = UpdOrder.ClId;
                order.EmId = UpdOrder.EmId;
                order.SoId = UpdOrder.SoId;
                order.OrDate = UpdOrder.OrDate;
                order.OrStateFlag = UpdOrder.OrStateFlag;
                order.OrFlag = UpdOrder.OrFlag;
                order.OrHidden = UpdOrder.OrHidden;
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
        //メソッド名：GetOrderData()
        //引　数   ：なし
        //戻り値   ：List<TOrderDsp>　受注情報
        //機　能   ：受注データの取得(一覧表示)
        ///////////////////////////////
        public List<TOrderDsp> GetOrderData()
        {
            List<TOrderDsp> order = new List<TOrderDsp>();
            try
            {
                var context = new SalesManagementContext();
                //IDを繋げて名前を表示する
                //結合・セレクト
                var tb = from or in context.TOrders
                         join cl in context.MClients
                         on or.ClId equals cl.ClId
                         join em in context.MEmployees
                         on or.EmId equals em.EmId
                         join so in context.MSalesOffices
                         on or.SoId equals so.SoId
                         where or.OrFlag == 0
                         select new
                         {
                             or.OrId,
                             or.ClCharge,
                             or.ClId,
                             cl.ClName,
                             or.EmId,
                             em.EmName,
                             or.SoId,
                             so.SoName,
                             or.OrDate,
                             or.OrStateFlag,
                             or.OrFlag,
                             or.OrHidden
                         };

                //tbをorderに入れる
                foreach(var p in tb)
                {
                    order.Add(new TOrderDsp()
                    {
                        OrId = p.OrId.ToString(),
                        ClCharge = p.ClCharge,
                        ClId = p.ClId.ToString(),
                        ClName = p.ClName,
                        EmId = p.EmId.ToString(),
                        EmName = p.EmName,
                        SoId = p.SoId.ToString(),
                        SoName = p.SoName,
                        OrDate = p.OrDate,
                        OrStateFlag = p.OrStateFlag.ToString(),
                        OrFlag = p.OrFlag.ToString(),
                        OrHidden = p.OrHidden
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return order;
        }

        ///////////////////////////////
        //メソッド名：GetOrderHiddenData()
        //引　数   ：なし
        //戻り値   ：List<TOrderDsp>　受注情報
        //機　能   ：受注データの取得(非表示リスト)
        ///////////////////////////////
        public List<TOrderDsp> GetOrderHiddenData()
        {
            List<TOrderDsp> order = new List<TOrderDsp>();
            try
            {
                var context = new SalesManagementContext();
                //非表示フラグが2のものを表示
                //IDを繋げて名前を表示する
                //結合・セレクト
                var tb = from or in context.TOrders
                         join cl in context.MClients
                         on or.ClId equals cl.ClId
                         join em in context.MEmployees
                         on or.EmId equals em.EmId
                         join so in context.MSalesOffices
                         on or.SoId equals so.SoId
                         where or.OrFlag == 2
                         select new
                         {
                             or.OrId,
                             or.ClCharge,
                             or.ClId,
                             cl.ClName,
                             or.EmId,
                             em.EmName,
                             or.SoId,
                             so.SoName,
                             or.OrDate,
                             or.OrStateFlag,
                             or.OrFlag,
                             or.OrHidden
                         };

                //tbをorderに入れる
                foreach (var p in tb)
                {
                    order.Add(new TOrderDsp()
                    {
                        OrId = p.OrId.ToString(),
                        ClCharge = p.ClCharge,
                        ClId = p.ClId.ToString(),
                        ClName = p.ClName,
                        EmId = p.EmId.ToString(),
                        EmName = p.EmName,
                        SoId = p.SoId.ToString(),
                        SoName = p.SoName,
                        OrDate = p.OrDate,
                        OrStateFlag = p.OrStateFlag.ToString(),
                        OrFlag = p.OrFlag.ToString(),
                        OrHidden = p.OrHidden
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return order;
        }

        ///////////////////////////////
        //メソッド名：CheckOrderIDExistence()
        //引　数   ：int 検索する受注ID
        //戻り値   ：bool　結果（True：一致あり、False：一致なし）
        //機　能   ：一致する受注IDの有無を確認(重複チェック)
        ///////////////////////////////
        public bool CheckOrderIDExistence(int OrderID)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                //受注IDで一致するデータが存在するか
                flg = context.TOrders.Any(x => x.OrId == OrderID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetOrderData() オーバーロード
        //引　数   ：TOrderDsp 受注情報
        //戻り値   ：List<TOrderDsp>　受注情報
        //機　能   ：条件部分一致受注データの取得(検索)
        ///////////////////////////////
        public List<TOrderDsp> GetOrderData(TOrderDsp selectCondition)
        {
            List<TOrderDsp> order = new List<TOrderDsp>();
            try
            {
                var context = new SalesManagementContext();
                var tb = from or in context.TOrders
                         join cl in context.MClients
                         on or.ClId equals cl.ClId
                         join em in context.MEmployees
                         on or.EmId equals em.EmId
                         join so in context.MSalesOffices
                         on or.SoId equals so.SoId
                         where or.OrId.ToString().Contains(selectCondition.OrId.ToString()) && 
                               or.ClCharge.Contains(selectCondition.ClCharge) && 
                               or.ClId.ToString().Contains(selectCondition.ClId.ToString()) && 
                               or.EmId.ToString().Contains(selectCondition.EmId.ToString()) && 
                               or.SoId.ToString().Contains(selectCondition.SoId.ToString())
                         select new
                         {
                             or.OrId,
                             or.ClCharge,
                             or.ClId,
                             cl.ClName,
                             or.EmId,
                             em.EmName,
                             or.SoId,
                             so.SoName,
                             or.OrDate,
                             or.OrStateFlag,
                             or.OrFlag,
                             or.OrHidden
                         };

                //tbをorderに入れる
                foreach (var p in tb)
                {
                    order.Add(new TOrderDsp()
                    {
                        OrId = p.OrId.ToString(),
                        ClCharge = p.ClCharge,
                        ClId = p.ClId.ToString(),
                        ClName = p.ClName,
                        EmId = p.EmId.ToString(),
                        EmName = p.EmName,
                        SoId = p.SoId.ToString(),
                        SoName = p.SoName,
                        OrDate = p.OrDate,
                        OrStateFlag = p.OrStateFlag.ToString(),
                        OrFlag = p.OrFlag.ToString(),
                        OrHidden = p.OrHidden
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return order;
        }

        ///////////////////////////////
        //メソッド名：GetOrderConfirmData()
        //引　数   ：string 受注ID
        //戻り値   ：int?　受注確定フラグ
        //機　能   ：受注IDに一致する受注データの取得
        ///////////////////////////////
        public int? GetOrderConfirmData(string OrderID)
        {
            int? order = 0;
            try
            {
                var context = new SalesManagementContext();
                order = context.TOrders.Where(x => x.OrId.ToString() == OrderID).Select(x => x.OrStateFlag).FirstOrDefault();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return order;
        }
    }
}


