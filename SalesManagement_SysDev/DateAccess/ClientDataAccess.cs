using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement_SysDev.DateAccess
{
    internal class ClientDataAccess
    {
        ///////////////////////////////
        //メソッド名：ClGetComboboxText()
        //引　数   ：なし
        //戻り値   ：顧客名（ClName）のリスト（List<string>）
        //機　能   ：顧客名リストを取得し、ComboBox に表示できる形式で返す
        ///////////////////////////////
        public List<string> ClGetComboboxText()
        {
            List<string> cmbTextList = new List<string>();
            try
            {
                using (var context = new SalesManagementContext())
                {
                    //名前だけ抜き出し、それ以外はない
                    cmbTextList = context.MClients.Select(x => x.ClName).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return cmbTextList;
        }

        ///////////////////////////////
        //メソッド名：GetAllClient()
        //引　数   ：なし
        //戻り値   ：MClient 型のリスト（List<MClient>）
        //機　能   ：すべての顧客情報（ClName と ClId）を取得して返す
        ///////////////////////////////
        public List<MClient> GetAllClient()
        {
            using (var context = new SalesManagementContext())
            {
                return context.MClients.Select(x => new MClient
                {
                    ClName = x.ClName,
                    ClId = x.ClId
                }).ToList();
            }
        }

        ///////////////////////////////
        //メソッド名：GetClIdByName()
        //引　数   ：顧客名（name）
        //戻り値   ：顧客ID（ClId）、顧客が見つからなければ null
        //機　能   ：顧客名を基に、顧客ID（ClId）を取得する
        ///////////////////////////////
        public int? GetClIdByName(string name)
        {
            using (var context = new SalesManagementContext())
            {
                var client = context.MClients
                                      .Where(x => x.ClName == name)
                                      .OrderBy(x => x.ClId) // IDが小さい順にソート
                                      .FirstOrDefault();
                return client?.ClId;
                // 見つかった場合はIDを返し、見つからなければnullを返す
            }
        }

        ///////////////////////////////
        //メソッド名：GetClNameById()
        //引　数   ：顧客ID（id）
        //戻り値   ：顧客名（ClName）、顧客が見つからなければ null
        //機　能   ：顧客IDを基に、顧客名（ClName）を取得する
        ///////////////////////////////
        public string GetClNameById(int id)
        {
            using (var context = new SalesManagementContext())
            {
                var client = context.MClients
                .Where(x => x.ClId == id) // 顧客IDが一致するものを検索
                .FirstOrDefault(); // 最初の一致するデータを取得
                if (client != null)
                {
                    return client.ClName; // 顧客名を返す
                }
                else
                {
                    return null; // 見つからなかった場合はnullを返す
                }
            }
        }

        ///////////////////////////////
        //メソッド名：CheckClientIDExistence()
        //引　数   ：int 検索する顧客ID
        //戻り値   ：bool　結果（True：一致あり、False：一致なし）
        //機　能   ：一致する顧客IDの有無を確認(重複チェック)
        ///////////////////////////////
        public bool CheckClientIDExistence(int ClientID)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                //顧客IDで一致するデータが存在するか
                flg = context.MClients.Any(x => x.ClId == ClientID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：SelectClientExistenceCheck()
        //引　数   ：int 顧客ID, string 顧客名
        //戻り値   ：bool　結果（True：一致あり、False：一致なし）
        //機　能   ：一致する顧客データの有無を確認
        ///////////////////////////////
        public bool SelectClientExistenceCheck(int ClientID, string ClientName,string ClientPostal,string ClientAddress,
                                                string ClientPhone, string ClientFAX,int SalesOfficeID, int ClientFlg, string ClientHidden)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                //顧客ID,顧客名で一致するデータが存在するか
                flg = context.MClients.Any(x => x.ClId == ClientID && x.ClName == ClientName
                                            && x.ClPostal == ClientPostal && x.ClAddress == ClientAddress
                                            && x.ClPhone == ClientPhone && x.ClFax == ClientFAX 
                                            && x.SoId == SalesOfficeID && x.ClFlag == ClientFlg && x.ClHidden == ClientHidden);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：AddClientData()
        //引　数   ：MClient 顧客情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：顧客データの登録
        ///////////////////////////////
        public bool AddClientData(MClient RegClient)
        {
            try
            {
                var context = new SalesManagementContext();
                context.MClients.Add(RegClient);
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
        //メソッド名：UpdateClientData()
        //引　数   ：MClient 顧客情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：顧客データの更新
        ///////////////////////////////
        public bool UpdateClientData(MClient UpdClient)
        {
            try
            {
                var context = new SalesManagementContext();
                var client = context.MClients.Single(x => x.ClId == UpdClient.ClId);
                client.SoId = UpdClient.SoId;
                client.ClName = UpdClient.ClName;
                client.ClAddress = UpdClient.ClAddress;
                client.ClPhone = UpdClient.ClPhone;
                client.ClPostal = UpdClient.ClPostal;
                client.ClFax = UpdClient.ClFax;
                client.ClFlag = UpdClient.ClFlag;
                client.ClHidden = UpdClient.ClHidden;
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
        //メソッド名：GetClientData()
        //引　数   ：なし
        //戻り値   ：List<MClient>　顧客情報
        //機　能   ：顧客データの取得(一覧表示)
        ///////////////////////////////
        public List<MClientDsp> GetClientData()
        {
            List<MClientDsp> client = new List<MClientDsp>();
            try
            {
                var context = new SalesManagementContext();
                //IDを繋げて名前を表示する
                //結合・セレクト
                var tb = from cl in context.MClients
                         join so in context.MSalesOffices
                         on cl.SoId equals so.SoId
                         where cl.ClFlag == 0
                         select new
                         {
                             cl.ClId,
                             cl.ClName,
                             cl.ClPostal,
                             cl.ClAddress,
                             cl.ClPhone,
                             cl.ClFax,
                             cl.SoId,
                             so.SoName,
                             cl.ClFlag,
                             cl.ClHidden,
                         };

                //tbをclientに入れる
                foreach(var p in tb)
                {
                    client.Add(new MClientDsp()
                    {
                        ClId = p.ClId.ToString(),
                        ClName = p.ClName,
                        ClPostal = p.ClPostal,
                        ClAddress = p.ClAddress,
                        ClPhone = p.ClPhone,
                        ClFAX = p.ClFax,
                        SoId = p.SoId.ToString(),
                        SoName = p.SoName,
                        ClFlag = p.ClFlag,
                        ClHidden = p.ClHidden,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return client;
        }

        ///////////////////////////////
        //メソッド名：GetClientHiddenData()
        //引　数   ：なし
        //戻り値   ：List<MClient>　顧客情報
        //機　能   ：顧客データの取得(非表示リスト)
        ///////////////////////////////
        public List<MClientDsp> GetClientHiddenData()
        {
            List<MClientDsp> client = new List<MClientDsp>();
            try
            {
                var context = new SalesManagementContext();
                //非表示フラグが2のものを表示
                //IDを繋げて名前を表示する
                //結合・セレクト
                var tb = from cl in context.MClients
                         join so in context.MSalesOffices
                         on cl.SoId equals so.SoId
                         where cl.ClFlag == 2
                         select new
                         {
                             cl.ClId,
                             cl.ClName,
                             cl.ClPostal,
                             cl.ClAddress,
                             cl.ClPhone,
                             cl.ClFax,
                             cl.SoId,
                             so.SoName,
                             cl.ClFlag,
                             cl.ClHidden,
                         };

                //tbをclientに入れる
                foreach (var p in tb)
                {
                    client.Add(new MClientDsp()
                    {
                        ClId = p.ClId.ToString(),
                        ClName = p.ClName,
                        ClPostal = p.ClPostal,
                        ClAddress = p.ClAddress,
                        ClPhone = p.ClPhone,
                        ClFAX = p.ClFax,
                        SoId = p.SoId.ToString(),
                        SoName = p.SoName,
                        ClFlag = p.ClFlag,
                        ClHidden = p.ClHidden,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return client;
        }

        ///////////////////////////////
        //メソッド名：GetClientData() オーバーロード
        //引　数   ：MClient 顧客情報
        //戻り値   ：List<MClient>　顧客情報
        //機　能   ：条件部分一致顧客データの取得(検索)
        ///////////////////////////////
        public List<MClientDsp> GetClientData(MClientDsp selectCondition)
        {
            List<MClientDsp> client = new List<MClientDsp>();
            try
            {
                var context = new SalesManagementContext();
                var tb = from cl in context.MClients
                         join so in context.MSalesOffices
                         on cl.SoId equals so.SoId
                         where cl.ClId.ToString().Contains(selectCondition.ClId.ToString()) &&
                               cl.ClName.Contains(selectCondition.ClName) &&
                               cl.ClPostal.Contains(selectCondition.ClPostal) &&
                               cl.ClAddress.Contains(selectCondition.ClAddress) &&
                               cl.ClPhone.Contains(selectCondition.ClPhone) &&
                               cl.ClFax.Contains(selectCondition.ClFAX) &&
                               cl.SoId.ToString().Contains(selectCondition.SoId.ToString())
                         select new
                         {
                             cl.ClId,
                             cl.ClName,
                             cl.ClPostal,
                             cl.ClAddress,
                             cl.ClPhone,
                             cl.ClFax,
                             cl.SoId,
                             so.SoName,
                             cl.ClFlag,
                             cl.ClHidden

                         };

                //tbをclientに
                foreach( var p in tb)
                {
                    client.Add(new MClientDsp()
                    {
                        ClId = p.ClId.ToString(),
                        ClName = p.ClName,
                        ClPostal = p.ClPostal,
                        ClAddress = p.ClAddress,
                        ClPhone = p.ClPhone,
                        ClFAX = p.ClFax,
                        SoId = p.SoId.ToString(),
                        SoName = p.SoName,
                        ClFlag = p.ClFlag,
                        ClHidden = p.ClHidden
                    });
                }

                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return client;
        }

        ///////////////////////////////
        //メソッド名：GetClientDspData()
        //引　数   ：なし
        //戻り値   ：List<MClient>　顧客情報
        //機　能   ：表示用顧客データの取得(コンボボックス)
        ///////////////////////////////
        public List<MClient> GetClientDspData()
        {
            List<MClient> client = new List<MClient>();
            try
            {
                var context = new SalesManagementContext();
                //非表示フラグが0のデータを抜き取る
                client = context.MClients.Where(x => x.ClFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return client;
        }
    }
}
