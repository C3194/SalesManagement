using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SalesManagement_SysDev.DateAccess
{
    internal class MakerDataAccess
    {
        public List<string> MaGetComboboxText()
        {
            List<string> cmbTextList = new List<string>();
            try
            {
                using (var context = new SalesManagementContext())
                {
                    cmbTextList = context.MMakers.Select(x => x.MaName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cmbTextList;
        }


        public List<MMaker> GetAllMakers()
        {
            using (var context = new SalesManagementContext())
            {
                return context.MMakers.Select(x => new MMaker
                {
                    MaName = x.MaName,
                    MaId = x.MaId
                }).ToList();
            }
        }

        public int? GetMaIdByName(string name)
        {
            using (var context = new SalesManagementContext())
            {
                var maker = context.MMakers
                                      .Where(x => x.MaName == name)
                                      .OrderBy(x => x.MaId) // IDが小さい順にソート
                                      .FirstOrDefault();
                return maker?.MaId;
                // 見つかった場合はIDを返し、見つからなければnullを返す
            }
        }

        public string GetMaNameById(int id)
        {
            using (var context = new SalesManagementContext())
            {
                var makerss = context.MMakers
                .Where(x => x.MaId == id) // 営業所IDが一致するものを検索
                .FirstOrDefault(); // 最初の一致するデータを取得
                if (makerss != null)
                {
                    return makerss.MaName; // 営業所名を返す
                }
                else
                {
                    return null; // 見つからなかった場合はnullを返す
                }
            }
        }

        ///////////////////////////////
        //メソッド名：CheckMakerIDExistence()
        //引　数   ：int 検索するメーカーID
        //戻り値   ：bool　結果（True：一致あり、False：一致なし）
        //機　能   ：一致するメーカーIDの有無を確認(重複チェック)
        ///////////////////////////////
        public bool CheckMakerIDExistence(int MakerID)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                //メーカーIDで一致するデータが存在するか
                flg = context.MMakers.Any(x => x.MaId == MakerID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetMakerDspData()
        //引　数   ：なし
        //戻り値   ：List<MMaker>　メーカー情報
        //機　能   ：表示用メーカーデータの取得(コンボボックス)
        ///////////////////////////////
        public List<MMaker> GetMakerDspData()
        {
            List<MMaker> maker = new List<MMaker>();
            try
            {
                var context = new SalesManagementContext();
                //非表示フラグが0のデータを抜き取る
                maker = context.MMakers.Where(x => x.MaFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return maker;
        }
    }
}
