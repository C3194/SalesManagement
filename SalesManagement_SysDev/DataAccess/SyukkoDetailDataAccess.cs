using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement_SysDev.DataAccess
{
    internal class SyukkoDetailDataAccess
    {
        public void ChumonDetailConfirm(int syukkoId, int chId)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 注文詳細情報の取得
                    var chumonDetails = context.TChumonDetails.Where(detail => detail.ChId == chId).ToList();
                    if (!chumonDetails.Any())
                    {
                        throw new InvalidOperationException("注文詳細が見つかりません。");
                    }

                    // 出庫詳細テーブルの登録
                    foreach (var detail in chumonDetails)
                    {
                        var syukkoDetail = new TSyukkoDetail
                        {
                            SyId = syukkoId, // 出庫ID
                            PrId = detail.PrId,     // 商品ID
                            SyQuantity = detail.ChQuantity // 数量
                        };

                        context.TSyukkoDetails.Add(syukkoDetail);
                    }

                    context.SaveChanges(); // データベース保存
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"出庫詳細情報の登録中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }
    }
}
