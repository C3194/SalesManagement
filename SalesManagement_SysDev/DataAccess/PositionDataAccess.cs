using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SalesManagement_SysDev.DateAccess
{
    internal class PositionDataAccess
    {
        public List<string> PoGetComboboxText()
        {
            List<string> cmbTextList = new List<string>();
            try
            {
                using (var context = new SalesManagementContext())
                {
                    cmbTextList = context.MPositions.Select(x => x.PoName).ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cmbTextList;
        }
        public List<MPosition> GetAllPoient()
        {
            using (var context = new SalesManagementContext())
            {
                return context.MPositions.Select(x => new MPosition
                {
                    PoName = x.PoName,
                    PoId = x.PoId,
                }).ToList();
            }
        }

        public int? GetPoIdByName(string name)
        {
            using (var context = new SalesManagementContext())
            {
                var position = context.MPositions
                                  .Where(x => x.PoName == name)
                                      .OrderBy(x => x.PoId) // IDが小さい順にソート
                                      .FirstOrDefault();
                return position?.PoId;
                // 見つかった場合はIDを返し、見つからなければnullを返す
            }
        }


        public string GetPoNameById(int id)
        {
            using (var context = new SalesManagementContext())
            {
                var product = context.MPositions
                .Where(x => x.PoId == id) // 商品IDが一致するものを検索
                .FirstOrDefault(); // 最初の一致するデータを取得
                if (product != null)
                {
                    return product.PoName; // 商品名を返す
                }
                else
                {
                    return null; // 見つからなかった場合はnullを返す
                }
            }
        }


        ///////////////////////////////
        //メソッド名：CheckPositionIDExistence()
        //引　数   ：int 検索するメーカーID
        //戻り値   ：bool　結果（True：一致あり、False：一致なし）
        //機　能   ：一致するメーカーIDの有無を確認(重複チェック)
        ///////////////////////////////
        public bool CheckPositionIDExistence(int PoID)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                //メーカーIDで一致するデータが存在するか
                flg = context.MEmployees.Any(x => x.PoId == PoID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }
    }
}