using a;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SalesManagement_SysDev.DataAccess
{
    internal class OrderDetailDataAccess
    {
        public List<TOrderDetail> GetOrderDetails(int orFlag = 0)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // OrFlag でフィルタリングされた受注詳細データを取得
                    var filteredDetails = context.TOrders
                        .Where(or => or.OrFlag == orFlag) // フィルタリング条件
                        .Join(context.TOrderDetails,       // TOrderDetails と結合
                              or => or.OrId,             // TOrders の OrId
                              dt => dt.OrId,             // TOrderDetails の OrId
                              (or, dt) => dt)            // 結果として TOrderDetails を取得
                        .ToList();

                    return filteredDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TOrderDetail>(); // エラー時に空のリストを返す
            }
        }
        public List<TOrderDetail> SearchOrDetailData(int? ordeId, int? orId, int? prId, List<int> validOrIds)
        {
            try
            {
                // 条件がすべてnullかつ有効な受注IDリストが空の場合、空リストを返す
                if (ordeId == null && orId == null && prId == null && (validOrIds == null || !validOrIds.Any()))
                {
                    return new List<TOrderDetail>();
                }

                using (var context = new SalesManagementContext())
                {
                    var query = context.TOrderDetails.AsQueryable();

                    // validOrIdsでフィルタリング（受注情報の結果を基に受注明細を絞り込む）
                    if (validOrIds != null && validOrIds.Any())
                    {
                        query = query.Where(orderdetail => validOrIds.Contains(orderdetail.OrId));
                    }

                    // その他の条件でフィルタリング
                    if (ordeId.HasValue)
                        query = query.Where(orderdetail => orderdetail.OrDetailId == ordeId.Value);

                    if (orId.HasValue)
                        query = query.Where(orderdetail => orderdetail.OrId == orId.Value);

                    if (prId.HasValue)
                        query = query.Where(orderdetail => orderdetail.PrId == prId.Value);

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"検索中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TOrderDetail>();
            }
        }

        public bool CheckData(int? OrId, int? SoId, int? EmId, int? ClId, DateTime? OrDate, int OrStateFlag)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // データベース内に条件に合う受注データが存在するか確認
                    return context.TOrders.Any(data =>
                         data.OrId == OrId &&                    // 受注ID
                         data.SoId == SoId &&                    // 営業所ID
                         data.EmId == EmId &&                    // 社員ID
                         data.ClId == ClId &&                    // 顧客ID
                         data.OrDate == OrDate &&                // 受注日
                         data.OrStateFlag == OrStateFlag        // 受注状態
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データベースエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // エラー時はfalseを返す
            }
        }

        ///////////////////////////////
        //メソッド名：IsOrderDetailIdExists
        //引　数   ：TOrder 受注データ
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：受注データで既に存在するか確認
        ///////////////////////////////
        public bool IsOrderDetailIdExists(TOrderDetail orderDetail)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                flg = context.TOrderDetails.Any(x => x.OrDetailId == orderDetail.OrDetailId && x.OrId == orderDetail.OrId && 
                                                     x.PrId == orderDetail.PrId && x.OrQuantity == orderDetail.OrQuantity &&
                                                     x.OrTotalPrice == orderDetail.OrTotalPrice);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：AddOrderDetailData()
        //引　数   ：TOrderDetail 受注詳細情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：受注詳細データの登録
        ///////////////////////////////
        public bool AddOrderDetailData(TOrderDetail RegOrderDetail)
        {
            try
            {
                var context = new SalesManagementContext();
                context.TOrderDetails.Add(RegOrderDetail);
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
        //メソッド名：UpdateOrderDetailData()
        //引　数   ：TOrderDetail 受注詳細情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：受注詳細データの更新
        ///////////////////////////////
        public bool UpdateOrderDetailData(TOrderDetail UpdOrderDetail)
        {
            try
            {
                var context = new SalesManagementContext();
                var orderDetail = context.TOrderDetails.Single(x => x.OrDetailId == UpdOrderDetail.OrDetailId);
                orderDetail.OrDetailId = UpdOrderDetail.OrDetailId;
                orderDetail.OrId = UpdOrderDetail.OrId;
                orderDetail.PrId = UpdOrderDetail.PrId;
                orderDetail.OrQuantity = UpdOrderDetail.OrQuantity;
                orderDetail.OrTotalPrice = UpdOrderDetail.OrTotalPrice;
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
        //メソッド名：GetOrderDetailData()
        //引　数   ：なし
        //戻り値   ：List<TOrderDetailDsp>　受注詳細情報
        //機　能   ：受注詳細データの取得(一覧表示)
        ///////////////////////////////
        public List<TOrderDetailDsp> GetOrderDetailData()
        {
            List<TOrderDetailDsp> orderDetail = new List<TOrderDetailDsp>();
            try
            {
                var context = new SalesManagementContext();
                //IDを繋げて名前を表示する
                //結合・セレクト
                var tb = from orDe in context.TOrderDetails
                         join or in context.TOrders
                         on orDe.OrId equals or.OrId
                         join pr in context.MProducts
                         on orDe.PrId equals pr.PrId
                         where or.OrFlag == 0 && pr.PrFlag == 0
                         select new
                         {
                             orDe.OrDetailId,
                             orDe.OrId,
                             orDe.PrId,
                             pr.PrName,
                             orDe.OrQuantity,
                             orDe.OrTotalPrice
                         };

                //tbをorderに入れる
                foreach(var p in tb)
                {
                    orderDetail.Add(new TOrderDetailDsp()
                    {
                        OrDetailId = p.OrDetailId.ToString(),
                        OrId = p.OrId.ToString(),
                        PrId = p.PrId.ToString(),
                        PrName = p.PrName,
                        OrQuantity = p.OrQuantity.ToString(),
                        OrTotalPrice = p.OrTotalPrice.ToString()
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return orderDetail;
        }

        ///////////////////////////////
        //メソッド名：GetOrderDetailHiddenData()
        //引　数   ：なし
        //戻り値   ：List<TOrderDetailDsp>　受注詳細情報
        //機　能   ：受注詳細データの取得(非表示リスト)
        ///////////////////////////////
        public List<TOrderDetailDsp> GetOrderDetailHiddenData()
        {
            List<TOrderDetailDsp> orderDetail = new List<TOrderDetailDsp>();
            try
            {
                var context = new SalesManagementContext();
                //IDを繋げて名前を表示する
                //結合・セレクト
                var tb = from orDe in context.TOrderDetails
                         join or in context.TOrders
                         on orDe.OrId equals or.OrId
                         join pr in context.MProducts
                         on orDe.PrId equals pr.PrId
                         where or.OrFlag == 2
                         select new
                         {
                             orDe.OrDetailId,
                             orDe.OrId,
                             orDe.PrId,
                             pr.PrName,
                             orDe.OrQuantity,
                             orDe.OrTotalPrice
                         };

                //tbをorderに入れる
                foreach (var p in tb)
                {
                    orderDetail.Add(new TOrderDetailDsp()
                    {
                        OrDetailId = p.OrDetailId.ToString(),
                        OrId = p.OrId.ToString(),
                        PrId = p.PrId.ToString(),
                        PrName = p.PrName,
                        OrQuantity = p.OrQuantity.ToString(),
                        OrTotalPrice = p.OrTotalPrice.ToString()
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return orderDetail;
        }

        ///////////////////////////////
        //メソッド名：GetOrderDetailSelectionData()
        //引　数   ：string 受注ID
        //戻り値   ：List<TOrderDetailDsp>　受注詳細情報
        //機　能   ：受注IDが一致する受注詳細データの取得
        ///////////////////////////////
        public List<TOrderDetailDsp> GetOrderDetailSelectionData(string OrID)
        {
            List<TOrderDetailDsp> orderDetail = new List<TOrderDetailDsp>();
            try
            {
                var context = new SalesManagementContext();
                //IDを繋げて名前を表示する
                //結合・セレクト
                var tb = from orDe in context.TOrderDetails
                         join or in context.TOrders
                         on orDe.OrId equals or.OrId
                         join pr in context.MProducts
                         on orDe.PrId equals pr.PrId
                         where orDe.OrId.ToString() == OrID && pr.PrFlag == 0
                         select new
                         {
                             orDe.OrDetailId,
                             orDe.OrId,
                             orDe.PrId,
                             pr.PrName,
                             orDe.OrQuantity,
                             orDe.OrTotalPrice
                         };

                //tbをorderに入れる
                foreach (var p in tb)
                {
                    orderDetail.Add(new TOrderDetailDsp()
                    {
                        OrDetailId = p.OrDetailId.ToString(),
                        OrId = p.OrId.ToString(),
                        PrId = p.PrId.ToString(),
                        PrName = p.PrName,
                        OrQuantity = p.OrQuantity.ToString(),
                        OrTotalPrice = p.OrTotalPrice.ToString()
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return orderDetail;
        }

        ///////////////////////////////
        //メソッド名：CheckOrderDetailIDExistence()
        //引　数   ：int 検索する受注詳細ID
        //戻り値   ：bool　結果（True：一致あり、False：一致なし）
        //機　能   ：一致する受注ID詳細の有無を確認(重複チェック)
        ///////////////////////////////
        public bool CheckOrderDetailIDExistence(int OrderDetailID)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                //受注IDで一致するデータが存在するか
                flg = context.TOrderDetails.Any(x => x.OrDetailId == OrderDetailID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetOrderDetailData() オーバーロード
        //引　数   ：TOrderDetailDsp 受注詳細情報
        //戻り値   ：List<TOrderDsp>　受注詳細情報
        //機　能   ：条件部分一致受注詳細データの取得(検索)
        ///////////////////////////////
        public List<TOrderDetailDsp> GetOrderDetailData(TOrderDetailDsp selectCondition)
        {
            List<TOrderDetailDsp> orderDetail = new List<TOrderDetailDsp>();
            try
            {
                var context = new SalesManagementContext();
                var tb = from orDe in context.TOrderDetails
                         join or in context.TOrders
                         on orDe.OrId equals or.OrId
                         join pr in context.MProducts
                         on orDe.PrId equals pr.PrId
                         where orDe.OrDetailId.ToString().Contains(selectCondition.OrDetailId.ToString()) &&
                               orDe.OrId.ToString().Contains(selectCondition.OrId.ToString()) &&
                               orDe.PrId.ToString().Contains(selectCondition.PrId.ToString()) &&
                               orDe.OrQuantity.ToString().Contains(selectCondition.OrQuantity.ToString()) &&
                               orDe.OrTotalPrice.ToString().Contains(selectCondition.OrTotalPrice.ToString())
                         select new
                         {
                             orDe.OrDetailId,
                             orDe.OrId,
                             orDe.PrId,
                             pr.PrName,
                             orDe.OrQuantity,
                             orDe.OrTotalPrice
                         };

                //tbをorderに入れる
                foreach (var p in tb)
                {
                    orderDetail.Add(new TOrderDetailDsp()
                    {
                        OrDetailId = p.OrDetailId.ToString(),
                        OrId = p.OrId.ToString(),
                        PrId = p.PrId.ToString(),
                        PrName = p.PrName,
                        OrQuantity = p.OrQuantity.ToString(),
                        OrTotalPrice = p.OrTotalPrice.ToString()
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return orderDetail;
        }

        ///////////////////////////////
        //メソッド名：GetOrderDetailData()
        //引　数   ：string 受注ID
        //戻り値   ：List<TOrderDetail>　受注詳細情報
        //機　能   ：受注IDに一致する受注データの取得
        ///////////////////////////////
        public List<TOrderDetail> GetOrderDetailData(string OrID)
        {
            List<TOrderDetail> orderDetail = new List<TOrderDetail>();
            try
            {
                var context = new SalesManagementContext();
                orderDetail = context.TOrderDetails.Where(x => x.OrId.ToString() == OrID).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return orderDetail;
        }
    }
}
