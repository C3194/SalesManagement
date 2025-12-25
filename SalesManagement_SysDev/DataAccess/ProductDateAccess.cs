using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement_SysDev.DateAccess
{
    internal class ProductDateAccess
    {
        public List<string> PrGetComboboxText()
        {
            List<string> cmbTextList = new List<string>();
            try
            {
                using (var context = new SalesManagementContext())
                {
                    cmbTextList = context.MProducts.Select(x => x.PrName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cmbTextList;
        }

        public List<MProduct> GetAllProduct()
        {
            using (var context = new SalesManagementContext())
            {
                return context.MProducts.Select(x => new MProduct
                {
                    PrName = x.PrName,
                    PrId = x.PrId
                }).ToList();
            }
        }

        public int? GetPrIdByName(string name)
        {
            using (var context = new SalesManagementContext())
            {
                var product = context.MProducts
                                      .Where(x => x.PrName == name)
                                      .OrderBy(x => x.PrId) // IDが小さい順にソート
                                      .FirstOrDefault();
                return product?.PrId;
                // 見つかった場合はIDを返し、見つからなければnullを返す
            }
        }

        public string GetPrNameById(int id)
        {
            using (var context = new SalesManagementContext())
            {
                var product = context.MProducts
                .Where(x => x.PrId == id) // 商品IDが一致するものを検索
                .FirstOrDefault(); // 最初の一致するデータを取得
                if (product != null)
                {
                    return product.PrName; // 商品名を返す
                }
                else
                {
                    return null; // 見つからなかった場合はnullを返す
                }
            }
        }
    }
}
