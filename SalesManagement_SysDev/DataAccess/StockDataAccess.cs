using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement_SysDev.DataAccess
{
    internal class StockDataAccess
    {
        public void SubStockQuantity(int chId)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 注文詳細テーブルから商品の数量を取得
                    var chumonDetails = context.TChumonDetails.Where(detail => detail.ChId == chId).ToList();

                    if (!chumonDetails.Any())
                    {
                        throw new InvalidOperationException("注文詳細が見つかりません。");
                    }

                    foreach (var detail in chumonDetails)
                    {
                        // 在庫テーブルから該当商品の在庫を取得
                        var stock = context.TStocks.FirstOrDefault(stock => stock.PrId == detail.PrId);

                        if (stock == null)
                        {
                            throw new InvalidOperationException($"商品ID {detail.PrId} の在庫が見つかりません。");
                        }

                        // 在庫数を減らす
                        stock.StQuantity -= detail.ChQuantity;

                        if (stock.StQuantity < 0)
                        {
                            throw new InvalidOperationException($"商品ID {detail.PrId} の在庫が不足しています。");
                        }
                    }

                    // 注文テーブルの注文確定フラグを変更
                    var chumon = context.TChumons.FirstOrDefault(ch => ch.ChId == chId);

                    if (chumon == null)
                    {
                        throw new InvalidOperationException("注文が見つかりません。");
                    }

                    chumon.ChStateFlag = 1; // 確定フラグに変更（例: 1が確定状態を表す場合）

                    // データベースを保存
                    context.SaveChanges();
                }

                MessageBox.Show("注文が確定され、在庫が更新されました。", "成功",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        
    }
}
