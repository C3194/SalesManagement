using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SalesManagement_SysDev.新しいフォルダー
{
    internal class SalesOfficeDataAccess
    {
        public List<string> SoGetComboboxText()
        {
            List<string> cmbTextList = new List<string>();
            try
            {
                using (var context = new SalesManagementContext())
                {
                    cmbTextList = context.MSalesOffices.Select(x => x.SoName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cmbTextList;
        }

        public List<MSalesOffice> GetAllSalesOffices()
        {
            using (var context = new SalesManagementContext())
            {
                return context.MSalesOffices.Select(x => new MSalesOffice
                {
                    SoName = x.SoName,
                    SoId = x.SoId
                }).ToList();
            }
        }

        public int? GetSoIdByName(string name)
        {
            using (var context = new SalesManagementContext())
            {
                var salesOffice = context.MSalesOffices
                                      .Where(x => x.SoName == name)
                                      .OrderBy(x => x.SoId) // IDが小さい順にソート
                                      .FirstOrDefault();
                return salesOffice?.SoId; 
                // 見つかった場合はIDを返し、見つからなければnullを返す
            }
        }

        public string GetSoNameById(int id)
        {
            using ( var context = new SalesManagementContext()) 
            {
                var office = context.MSalesOffices
                .Where(x => x.SoId == id) // 営業所IDが一致するものを検索
                .FirstOrDefault(); // 最初の一致するデータを取得
                if (office != null)
                {
                    return office.SoName; // 営業所名を返す
                }
                else
                {
                    return null; // 見つからなかった場合はnullを返す
                }
            }
        }


        

        ///////////////////////////////
        //メソッド名：CheckSalesOfficeIDExistence()
        //引　数   ：int 検索する営業所ID
        //戻り値   ：bool　結果（True：一致あり、False：一致なし）
        //機　能   ：一致する営業所IDの有無を確認(重複チェック)
        ///////////////////////////////
        public bool CheckSalesOfficeIDExistence(int SalesOfficeID)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                //営業所IDで一致するデータが存在するか
                flg = context.MSalesOffices.Any(x => x.SoId == SalesOfficeID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetSalesOfficeDspData()
        //引　数   ：なし
        //戻り値   ：List<MSalesOffice>　営業所情報
        //機　能   ：表示用営業所データの取得(コンボボックス)
        ///////////////////////////////
        public List<MSalesOffice> GetSalesOfficeDspData()
        {
            List<MSalesOffice> salesOffice = new List<MSalesOffice>();
            try
            {
                var context = new SalesManagementContext();
                salesOffice = context.MSalesOffices.Where(x => x.SoFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return salesOffice;
        }
    }
}
