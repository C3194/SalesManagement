using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement_SysDev.DateAccess
{
    internal class MajorClassificationDataAccess
    {
        public List<string> McGetComboboxText()
        {
            List<string> cmbTextList = new List<string>();
            try
            {
                using (var context = new SalesManagementContext())
                {
                    cmbTextList = context.MMajorClassifications.Select(x => x.McName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cmbTextList;
        }


        // McName から McID を取得するメソッド
        public int? GetMcIdByMcName(string mcName)
        {
            using (var context = new SalesManagementContext())
            {
                var majorClassification = context.MMajorClassifications
                    .FirstOrDefault(m => m.McName == mcName);

                return majorClassification?.McId;  // McID を返す。見つからなければ null を返す
            }
        }

        // 大分類ID から大分類名を取得するメソッド
        public string GetMcNameByMcId(string mcId)
        {
            using (var context = new SalesManagementContext())
            {
                var majorClassification = context.MMajorClassifications
                                                .FirstOrDefault(mc => mc.McId.ToString() == mcId);

                return majorClassification?.McName; // 大分類名を返す
            }
        }

        ///////////////////////////////
        //メソッド名：GetMajorClassDspData()
        //引　数   ：なし
        //戻り値   ：List<MMajorClassification>　大分類情報
        //機　能   ：表示用大分類データの取得(コンボボックス)
        ///////////////////////////////
        public List<MMajorClassification> GetMajorClassDspData()
        {
            List<MMajorClassification> MajorClass = new List<MMajorClassification>();
            try
            {
                var context = new SalesManagementContext();
                //非表示フラグが0のデータを抜き取る
                MajorClass = context.MMajorClassifications.Where(x => x.McFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return MajorClass;
        }
    }

}
