using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement_SysDev.DateAccess
{
    internal class SmallClassificationDataAccess
    {
        public List<string> ScGetComboboxText()
        {
            List<string> cmbTextList = new List<string>();
            try
            {
                using (var context = new SalesManagementContext())
                {
                    cmbTextList = context.MSmallClassifications.Select(x => x.ScName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cmbTextList;
        }

        public List<MSmallClassification> GetAllSmallClassifications()
        {
            using (var context = new SalesManagementContext())
            {
                return context.MSmallClassifications.Select(x => new MSmallClassification
                {
                    ScName = x.ScName,
                    McId = x.McId
                }).ToList();
            }
        }

        public List<MMajorClassification> GetAllMajorClassifications()
        {
            using (var context = new SalesManagementContext())
            {
                return context.MMajorClassifications.Select(x => new MMajorClassification
                {
                    McName = x.McName,
                    McId = x.McId
                }).ToList();
            }
        }

        // 小分類名から McID を取得するメソッド
        public int? GetMcIdByScName(string scName)
        {
            using (var context = new SalesManagementContext())
            {
                var smallClassification = context.MSmallClassifications
                    .FirstOrDefault(s => s.ScName == scName);

                return smallClassification?.McId;  // McID を返す。見つからなければ null を返す
            }
        }

        // McID から McName を取得するメソッド
        public string GetMcNameByMcId(int mcid)
        {
            using (var context = new SalesManagementContext())
            {
                var majorClassification = context.MMajorClassifications
                    .FirstOrDefault(m => m.McId == mcid);

                return majorClassification?.McName;  // McName を返す。見つからなければ null を返す
            }
        }


        // McID に対応する小分類データを取得するメソッド
        public List<MSmallClassification> GetSmallClassificationsByMcId(int mcid)
        {
            using (var context = new SalesManagementContext())
            {
                var smallClassifications = context.MSmallClassifications
                    .Where(s => s.McId == mcid)
                    .ToList(); // 対応する小分類をリストで返す

                return smallClassifications;
            }
        }

        // ScID から ScName を取得するメソッド
        public string GetScNameByScId(string scId)
        {
            using (var context = new SalesManagementContext())
            {
                var smallClassifications = context.MSmallClassifications
                    .FirstOrDefault(sc => sc.ScId.ToString() == scId);

                return smallClassifications?.ScName;  // ScName を返す。見つからなければ null を返す
            }
        }



        // 小分類ID から大分類ID を取得するメソッド
        public string GetMcIdByScId(string scId)
        {
            using (var context = new SalesManagementContext())
            {
                var smallClassification = context.MSmallClassifications
                                                .FirstOrDefault(sc => sc.ScId.ToString() == scId);

                return smallClassification?.McId.ToString(); // 小分類の McID を返す
            }
        }

        // 小分類ID から大分類ID を取得するメソッド
        public int? GetScIdByMcId(int mcId)
        {
            using (var context = new SalesManagementContext())
            {
                var smallClassification = context.MSmallClassifications
                                                .FirstOrDefault(sc => sc.McId == mcId);

                return smallClassification?.ScId; // 小分類の ScID を返す
            }
        }


        // ScName から ScID を取得するメソッド
        public int? GetScIdByScName(string scName)
        {
            using (var context = new SalesManagementContext())
            {
                var smallClassification = context.MSmallClassifications
                    .FirstOrDefault(m => m.ScName == scName);

                return smallClassification?.ScId;  // McID を返す。見つからなければ null を返す
            }
        }

        public List<int> GetScIdByScName2(string scName)
        {
            try
            {
                using (var context = new SalesManagementContext()) // EFのDbContextを使用
                {
                    // LINQクエリを使って、ScNameに一致するScIDを取得
                    var scIds = context.MSmallClassifications
                        .Where(sc => sc.ScName == scName)  // 小分類名が一致するレコードを検索
                        .Select(sc => sc.ScId)  // ScIDのみを選択
                        .ToList();  // 結果をリストとして取得

                    return scIds;
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング（例: ログ出力など）
                MessageBox.Show(ex.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<int>();  // エラー発生時は空のリストを返す
            }
        }
        ///////////////////////////////
        //メソッド名：GetSmallClassIDData()
        //引　数   ：選択された大分類IDと小分類名
        //戻り値   ：int　小分類ID
        //機　能   ：小分類コンボボックスから小分類IDを抜き出す
        ///////////////////////////////
        public int GetSmallClassIDData(int McID, string ScName)
        {
            int ScID = -1;
            try
            {
                var context = new SalesManagementContext();
                var ID = context.MSmallClassifications.Where(x => x.McId == McID && x.ScName == ScName).Select(x => x.ScId).FirstOrDefault();
                ScID = int.Parse(ID.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("例外エラーです", "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ScID;
        }

        ///////////////////////////////
        //メソッド名：GetSmallClassDspData()
        //引　数   ：選択された大分類ID
        //戻り値   ：List<MSmallClassification>　小分類情報
        //機　能   ：表示用小分類データの取得(コンボボックス)
        ///////////////////////////////
        public List<MSmallClassification> GetSmallClassDspData(int McID)
        {
            List<MSmallClassification> SmallClass = new List<MSmallClassification>();
            try
            {
                var context = new SalesManagementContext();
                //大分類IDで絞り込んだものを抜き出す
                if(McID == -1)
                {
                    //非表示フラグが0のデータを抜き取る
                    SmallClass = context.MSmallClassifications.Where(x => x.ScFlag == 0).ToList();
                    context.Dispose();
                }
                else
                {
                    SmallClass = context.MSmallClassifications.Where(x => x.McId == McID).ToList();
                    context.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return SmallClass;
        }
    }

}

