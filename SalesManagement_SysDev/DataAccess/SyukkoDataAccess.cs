using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesManagement_SysDev.DataAccess
{
    internal class SyukkoDataAccess
    {
        public int ChumonConfirm(int chId, int loggedInEmId)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 注文情報の取得
                    var chumon = context.TChumons.FirstOrDefault(ch => ch.ChId == chId);

                    if (chumon == null)
                    {
                        throw new InvalidOperationException($"指定された注文（ChId: {chId}）が見つかりません。");
                    }

                    // ログイン中の社員IDが有効か確認
                    if (loggedInEmId <= 0)
                    {
                        throw new InvalidOperationException("ログイン中の社員IDが無効です。");
                    }

                    // 出庫テーブルの新規登録
                    var syukko = new TSyukko
                    {
                        EmId = null,               // 社員IDは未設定
                        ClId = chumon.ClId,        // 顧客ID
                        SoId = chumon.SoId,        // 営業所ID
                        OrId = chumon.OrId,
                        SyDate = null,             // 出庫日付は未設定
                        SyStateFlag = 0,           // 出庫状態フラグ（0: 未出庫）
                        SyFlag = 0,                // 管理フラグ（0）
                        SyHidden = null            // 非表示理由は未設定
                    };

                    context.TSyukkos.Add(syukko);
                    context.SaveChanges(); // 一度保存してSyIdを取得

                    // 注文テーブルの社員IDを更新（ログイン中の社員IDに変更）
                    chumon.EmId = loggedInEmId; // ここで例外が発生する可能性がある
                    context.SaveChanges();

                    return syukko.SyId; // 登録された出庫IDを返す
                }
            }
            catch (Exception ex)
            {
                // 例外の詳細を表示
                MessageBox.Show($"出庫情報の登録中にエラーが発生しました: {ex.Message}\n{ex.StackTrace}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw; // 必要に応じて再スロー
            }
        }
    }
}
