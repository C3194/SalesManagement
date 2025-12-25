using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace SalesManagement_SysDev.DataAccess
{
    internal class ChumonDetailDataAccess
    {
        public List<TChumonDetail> GetChumonDetails(int chFlag = 0)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // ChFlag が指定された値に一致する注文データと対応する明細データを取得
                    var filteredDetails = context.TChumons
                        .Where(ch => ch.ChFlag == chFlag) // ChFlag でフィルタリング
                        .Join(context.TChumonDetails,          // TDetails と結合
                              ch => ch.ChId,             // TChumons の ChID
                              dt => dt.ChId,             // TDetails の ChID
                              (ch, dt) => dt)            // 結果として TDetails を取得
                        .ToList();

                    return filteredDetails;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TChumonDetail>(); // 空のリストを返す
            }
        }

        public List<TChumonDetail> SearchChDetailData(int? chdeId, int? chId, int? prId, List<int> validChIds)
        {
            try
            {
                // 条件がすべてnullかつ有効な注文IDリストが空の場合、空リストを返す
                if (chdeId == null && chId == null && prId == null && (validChIds == null || !validChIds.Any()))
                {
                    return new List<TChumonDetail>();
                }

                using (var context = new SalesManagementContext())
                {
                    var query = context.TChumonDetails.AsQueryable();

                    // validChIdsでフィルタリング（注文情報の結果を基に注文詳細を絞り込む）
                    if (validChIds != null && validChIds.Any())
                    {
                        query = query.Where(chumondetail => validChIds.Contains(chumondetail.ChId));
                    }

                    // その他の条件でフィルタリング
                    if (chdeId.HasValue)
                        query = query.Where(chumondetail => chumondetail.ChDetailId == chdeId.Value);

                    if (chId.HasValue)
                        query = query.Where(chumondetail => chumondetail.ChId == chId.Value);

                    if (prId.HasValue)
                        query = query.Where(chumondetail => chumondetail.PrId == prId.Value);

                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"検索中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<TChumonDetail>();
            }
        }

        ///////////////////////////////
        //メソッド名：AddChumonDetailData()
        //引　数   ：List<TChumonDetail> 注文詳細情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：注文詳細データの登録
        ///////////////////////////////
        public bool AddChumonDetailData(List<TChumonDetail> RegChumonDetail)
        {
            try
            {
                var context = new SalesManagementContext();
                //複数のデータを一気に追加したいとき[AddRange]
                context.TChumonDetails.AddRange(RegChumonDetail);
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
