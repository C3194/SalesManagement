using Product_Management;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SalesManagement_SysDev.DateAccess
{
    internal class ProductDataAccess
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


        public List<MProduct> GetProductData(int? prFlag = null)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // クエリを作成
                    var query = context.MProducts.AsQueryable();

                    // フィルタリング条件が指定されている場合は条件を追加
                    if (prFlag.HasValue)
                    {
                        query = query.Where(pr => pr.PrFlag == prFlag.Value);
                    }

                    // フィルタリングされた結果をリストで返す
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合はログやメッセージを表示する
                MessageBox.Show(ex.Message, "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MProduct>(); // 空のリストを返す
            }
        }

        public List<MProduct> SearchPrData(int? prId, string? prName, int? maId, decimal? price, string? prColor,
                                           List<int> scIds, string? prModelNumber, int? prSafetyStock, DateTime? prReleaseDate)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    var query = context.MProducts.AsQueryable(); // 初期状態としてMProductテーブルの全てのデータを取得

                    // prIdが指定されていれば、chIdでフィルタリング
                    if (prId.HasValue)
                        query = query.Where(product => product.PrId == prId.Value);

                    // maIdが指定されていれば、soIdでフィルタリング
                    if (maId.HasValue)
                        query = query.Where(product => product.MaId == maId.Value);

                    // prNameがnullまたは空文字列でない場合にフィルタリング
                    if (!string.IsNullOrEmpty(prName))
                        query = query.Where(product => product.PrName == prName);

                    // clIdが指定されていれば、clIdでフィルタリング
                    if (price.HasValue)
                        query = query.Where(product => product.Price == price.Value);

                    //// prJcodeがnullまたは空文字列でない場合にフィルタリング
                    //if (!string.IsNullOrEmpty(prJcode))
                    //    query = query.Where(product => product.PrJcode == prJcode);

                    // prSafetyStockが指定されていれば、orIdでフィルタリング
                    if (prSafetyStock.HasValue)
                        query = query.Where(product => product.PrSafetyStock == prSafetyStock.Value);

                    // scIdが指定されていれば、chDateでフィルタリング
                    if (scIds != null && scIds.Any())
                    {
                        query = query.Where(p => scIds.Contains(p.ScId));  // 小分類IDでフィルタリング
                    }

                    // prModelNumberがnullまたは空文字列でない場合にフィルタリング
                    if (!string.IsNullOrEmpty(prModelNumber))
                        query = query.Where(product => product.PrModelNumber == prModelNumber);

                    // prColorがnullまたは空文字列でない場合にフィルタリング
                    if (!string.IsNullOrEmpty(prColor))
                        query = query.Where(product => product.PrColor == prColor);

                    // chDateが指定されていれば、chDateでフィルタリング
                    if (prReleaseDate.HasValue)
                        query = query.Where(product => product.PrReleaseDate == prReleaseDate.Value.Date); // 時間部分は無視して比較

                    // 検索結果をリストとして返す
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"検索中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<MProduct>(); // エラーが発生した場合は空のリストを返す
            }
        }


        

        public int? GetPrFlag(int id)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 条件に一致するレコードのflgを取得
                    var flgValue = context.MProducts
                        .Where(x => x.PrId == id) // 条件を指定 (例: 主キーがidと一致)
                        .Select(x => x.PrFlag)     // flg値だけを取得
                        .FirstOrDefault();      // 最初の値を取得（ない場合はデフォルト値）

                    return flgValue;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラー: {ex.Message}", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // エラー時はnullを返す
            }
        }

        public bool ChangePrhideflg(int id, int newflg, string prhide)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 更新対象の注文情報を取得
                    var prdata = context.MProducts.FirstOrDefault(x => x.PrId == id);

                    if (prdata != null)
                    {
                        // 値を更新
                        prdata.PrFlag = newflg;
                        //非表示理由を更新
                        prdata.PrHidden = prhide;
                        // データベースに保存
                        context.SaveChanges();

                        return true; // 更新成功
                    }
                    else
                    {
                        MessageBox.Show("対象のデータが見つかりませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false; // 対象データなし
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データベース更新エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // エラー発生
            }
        }

        public bool CheckDataExistence(int Prid, string Prname, int Maid, decimal pricee, string Color,
                                          int? Scid, string Prmodelnumber, int Prsafetystock, DateTime Prdate)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // データベース内に条件に合うデータが存在するか確認
                    return context.MProducts.Any(data =>
                        data.PrId == Prid &&
                        data.PrName == Prname &&
                        data.MaId == Maid &&
                        data.Price == pricee &&
                        data.PrColor == Color &&
                        //data.PrJcode == PrJcode &&
                        data.ScId == Scid &&
                        data.PrSafetyStock == Prsafetystock &&
                        data.PrModelNumber == Prmodelnumber &&
                        data.PrReleaseDate == Prdate
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"データベースエラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // エラー時はfalseを返す
            }
        }



        public MProduct? UpdateProduct(int prid, string prnames, int maid, decimal price, string color, int scid, string prmodelnumber,
                                                                    int prsafetystock, DateTime prdate, int hiflgindex, string prhidden)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 更新対象の注文情報を取得
                    var prdata = context.MProducts.FirstOrDefault(x => x.PrId == prid);
                    if (prdata != null)
                    {
                        prdata.PrId = prid;
                        prdata.PrName = prnames;
                        prdata.MaId = maid;
                        prdata.Price = price;
                        prdata.PrColor = color;
                        prdata.ScId = scid; // int? 型なのでそのままセット
                        prdata.PrModelNumber = prmodelnumber;
                        prdata.PrSafetyStock = prsafetystock;
                        prdata.PrReleaseDate = prdate;
                        prdata.PrFlag = hiflgindex;
                        prdata.PrHidden = prhidden;

                        context.SaveChanges(); // 更新をデータベースに反映
                        return prdata;
                    }
                }
                return null;
                }
               catch (Exception ex)
            {
                MessageBox.Show($"データベース更新エラー: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null; // エラー発生
            }
        }

        //商品名から商品IDを抽出するメソッド
        public int? GetPrIdByPrName(string prName)
        {
            using (var context = new SalesManagementContext())
            {
                var products = context.MProducts
                    .FirstOrDefault(m => m.PrName == prName);

                return products?.PrId;  // McID を返す。見つからなければ null を返す
            }
        }

        public string GetPrNameByPrId(string prId)
        {
            using (var context = new SalesManagementContext())
            {
                var products = context.MProducts
                    .FirstOrDefault(m => m.PrId.ToString() == prId);

                return products?.PrName;  // McID を返す。見つからなければ null を返す
            }
        }

        public MProduct? AddNewProduct(string prnames, int maid, decimal price, string color, int scid, string prmodelnumber,
                                                                   int prsafetystock, DateTime prdate, int hiflgindex)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 新しい商品データを作成
                    var newProduct = new MProduct
                    {
                        //PrId = emId ?? 0, // 自動生成の場合は不要
                        MaId = maid,
                        PrName = prnames,
                        Price = price,
                        PrSafetyStock = prsafetystock,
                        ScId = scid,
                        PrColor = color,
                        PrReleaseDate = prdate,
                        PrModelNumber = prmodelnumber,
                        PrFlag = hiflgindex,
                        PrHidden = null // 初期値として非表示理由なし
                    };

                    // データベースに追加
                    context.MProducts.Add(newProduct);
                    context.SaveChanges(); // データベースに保存


                    return newProduct;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"登録中にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public bool IsProductIdExists(string productId)
        {
            try
            {
                if (int.TryParse(productId, out int parsedProductId))
                {
                    using (var context = new SalesManagementContext())
                    {
                        // 商品IDで商品が既に存在するか確認
                        var existingProduct = context.MProducts.FirstOrDefault(p => p.PrId.ToString() == productId);
                        return existingProduct != null; // 存在する場合はtrueを返す
                    }
                }
                else
                {
                    // 商品IDが整数に変換できなかった場合
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "データ取得エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public MProduct? GetProduct(int prid)
        {
            try
            {
                using (var context = new SalesManagementContext())
                {
                    // 商品IDで商品情報を検索
                    var product = context.MProducts.FirstOrDefault(x => x.PrId == prid);

                    // 商品が見つかれば返す
                    return product;
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング: 必要に応じてログに記録
                // ここではエラーを再スローして上位の呼び出し元に伝えることが一般的
                throw new InvalidOperationException("商品情報の取得に失敗しました。", ex);
            }
        }

        ///////////////////////////////
        //メソッド名：GetPriceByPrId()
        //引　数   ：商品ID（id）
        //戻り値   ：金額（Price）、金額が見つからなければ null
        //機　能   ：商品IDを基に、金額（Price）を取得する
        ///////////////////////////////
        public string GetPrNameByPrice(string id)
        {
            using(var context = new SalesManagementContext())
            {
                var product = context.MProducts
                    .Where(x => x.PrId.ToString() == id)
                    .FirstOrDefault();
                if(product != null)
                {
                    return product.Price.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        ///////////////////////////////
        //メソッド名：AddProductData()
        //引　数   ：MProduct 商品情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：商品データの登録
        ///////////////////////////////
        public bool AddProductData(MProduct RegProduct)
        {
            try
            {
                var context = new SalesManagementContext();
                context.MProducts.Add(RegProduct);
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
        //メソッド名：UpdateProductData()
        //引　数   ：MProduct 商品情報
        //戻り値   ：bool　結果（True：異常なし、False：異常あり）
        //機　能   ：商品データの更新
        ///////////////////////////////
        public bool UpdateProductData(MProduct UpdProduct)
        {
            try
            {
                var context = new SalesManagementContext();
                var product = context.MProducts.Single(x => x.PrId == UpdProduct.PrId);
                product.MaId = UpdProduct.MaId;
                product.PrName = UpdProduct.PrName;
                product.Price = UpdProduct.Price;
                product.PrJcode = UpdProduct.PrJcode;
                product.PrSafetyStock = UpdProduct.PrSafetyStock;
                product.ScId = UpdProduct.ScId;
                product.PrModelNumber = UpdProduct.PrModelNumber;
                product.PrColor = UpdProduct.PrColor;
                product.PrReleaseDate = UpdProduct.PrReleaseDate;
                product.PrFlag = UpdProduct.PrFlag;
                product.PrHidden = UpdProduct.PrHidden;
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
        //メソッド名：GetProductData() オーバーロード
        //引　数   ：MProductDsp 商品情報
        //戻り値   ：List<MProductDsp>　商品情報
        //機　能   ：条件部分一致商品データの取得(検索)
        ///////////////////////////////
        public List<MProductDsp> GetProductData(MProductDsp selectCondition)
        {
            List<MProductDsp> product = new List<MProductDsp>();
            try
            {
                var context = new SalesManagementContext();
                var tb = from pr in context.MProducts
                         join ma in context.MMakers
                         on pr.MaId equals ma.MaId
                         join sc in context.MSmallClassifications
                         on pr.ScId equals sc.ScId
                         where pr.PrId.ToString().Contains(selectCondition.PrId.ToString()) &&
                               pr.PrName.Contains(selectCondition.PrName.ToString()) &&
                               pr.MaId.ToString().Contains(selectCondition.MaId.ToString()) &&
                               pr.Price.ToString().Contains(selectCondition.Price.ToString()) &&
                               pr.PrColor.Contains(selectCondition.PrColor) &&
                               pr.PrModelNumber.Contains(selectCondition.PrModelNumber) &&
                               pr.PrSafetyStock.ToString().Contains(selectCondition.PrSafetyStock.ToString()) &&
                               //pr.PrReleaseDate == selectCondition.PrReleaseDate &&
                               pr.ScId.ToString().Contains(selectCondition.ScId) &&
                               sc.McId.ToString().Contains(selectCondition.McId)
                         select new
                         {
                             pr.PrId,
                             pr.PrName,
                             pr.MaId,
                             ma.MaName,
                             pr.Price,
                             pr.PrJcode,
                             pr.PrColor,
                             pr.PrModelNumber,
                             pr.PrSafetyStock,
                             pr.PrReleaseDate,
                             pr.ScId,
                             sc.McId,
                             sc.ScName,
                             pr.PrFlag,
                             pr.PrHidden,
                         };

                //tbをproductに
                foreach (var p in tb)
                {
                    product.Add(new MProductDsp()
                    {
                        PrId = p.PrId.ToString(),
                        PrName = p.PrName,
                        MaId = p.MaId.ToString(),
                        MaName = p.MaName,
                        Price = p.Price.ToString(),
                        PrJcode = p.PrJcode,
                        PrColor = p.PrColor,
                        PrModelNumber = p.PrModelNumber,
                        PrSafetyStock = p.PrSafetyStock.ToString(),
                        PrReleaseDate = p.PrReleaseDate,
                        ScId = p.ScId.ToString(),
                        McId = p.McId.ToString(),
                        ScName = p.ScName,
                        PrFlag = p.PrFlag.ToString(),
                        PrHidden = p.PrHidden,
                    });
                }

                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return product;
        }

        ///////////////////////////////
        //メソッド名：GetProductData()
        //引　数   ：なし
        //戻り値   ：List<MProduct>　顧客情報
        //機　能   ：顧客データの取得(一覧表示)
        ///////////////////////////////
        public List<MProductDsp> GetProductData()
        {
            List<MProductDsp> product = new List<MProductDsp>();
            try
            {
                var context = new SalesManagementContext();
                var tb = from pr in context.MProducts
                         join ma in context.MMakers
                         on pr.MaId equals ma.MaId
                         join sc in context.MSmallClassifications
                         on pr.ScId equals sc.ScId
                         where pr.PrFlag == 0
                         select new
                         {
                             pr.PrId,
                             pr.PrName,
                             pr.MaId,
                             ma.MaName,
                             pr.Price,
                             pr.PrJcode,
                             pr.PrColor,
                             pr.PrModelNumber,
                             pr.PrSafetyStock,
                             pr.PrReleaseDate,
                             pr.ScId,
                             sc.McId,
                             sc.ScName,
                             pr.PrFlag,
                             pr.PrHidden,
                         };

                //tbをproductに入れる
                foreach(var p in tb)
                {
                    product.Add(new MProductDsp()
                    {
                        PrId = p.PrId.ToString(),
                        PrName = p.PrName,
                        MaId = p.MaId.ToString(),
                        MaName = p.MaName,
                        Price = p.Price.ToString(),
                        PrJcode = p.PrJcode,
                        PrColor = p.PrColor,
                        PrModelNumber = p.PrModelNumber,
                        PrSafetyStock = p.PrSafetyStock.ToString(),
                        PrReleaseDate = p.PrReleaseDate,
                        ScId = p.ScId.ToString(),
                        McId = p.McId.ToString(),
                        ScName = p.ScName,
                        PrFlag = p.PrFlag.ToString(),
                        PrHidden = p.PrHidden,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return product;
        }

        ///////////////////////////////
        //メソッド名：GetProductHiddenData()
        //引　数   ：なし
        //戻り値   ：List<MProduct>　商品情報
        //機　能   ：商品データの取得(非表示リスト)
        ///////////////////////////////
        public List<MProductDsp> GetProductHiddenData()
        {
            List<MProductDsp> product = new List<MProductDsp>();
            try
            {
                var context = new SalesManagementContext();
                var tb = from pr in context.MProducts
                         join ma in context.MMakers
                         on pr.MaId equals ma.MaId
                         join sc in context.MSmallClassifications
                         on pr.ScId equals sc.ScId
                         where pr.PrFlag == 2
                         select new
                         {
                             pr.PrId,
                             pr.PrName,
                             pr.MaId,
                             ma.MaName,
                             pr.Price,
                             pr.PrJcode,
                             pr.PrColor,
                             pr.PrModelNumber,
                             pr.PrSafetyStock,
                             pr.PrReleaseDate,
                             pr.ScId,
                             sc.McId,
                             sc.ScName,
                             pr.PrFlag,
                             pr.PrHidden,
                         };

                //tbをproductに入れる
                foreach (var p in tb)
                {
                    product.Add(new MProductDsp()
                    {
                        PrId = p.PrId.ToString(),
                        PrName = p.PrName,
                        MaId = p.MaId.ToString(),
                        MaName = p.MaName,
                        Price = p.Price.ToString(),
                        PrJcode = p.PrJcode,
                        PrColor = p.PrColor,
                        PrModelNumber = p.PrModelNumber,
                        PrSafetyStock = p.PrSafetyStock.ToString(),
                        PrReleaseDate = p.PrReleaseDate,
                        ScId = p.ScId.ToString(),
                        McId = p.McId.ToString(),
                        ScName = p.ScName,
                        PrFlag = p.PrFlag.ToString(),
                        PrHidden = p.PrHidden,
                    });
                }
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return product;
        }

        ///////////////////////////////
        //メソッド名：CheckProductIDExistence()
        //引　数   ：int 検索する商品ID
        //戻り値   ：bool　結果（True：一致あり、False：一致なし）
        //機　能   ：一致する商品IDの有無を確認(重複チェック)
        ///////////////////////////////
        public bool CheckProductIDExistence(int ProductID)
        {
            bool flg = false;

            try
            {
                var context = new SalesManagementContext();
                //商品IDで一致するデータが存在するか
                flg = context.MProducts.Any(x => x.PrId == ProductID);
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return flg;
        }

        ///////////////////////////////
        //メソッド名：GetProductDspData()
        //引　数   ：なし
        //戻り値   ：List<MProduct>　商品情報
        //機　能   ：表示用商品データの取得(コンボボックス)
        ///////////////////////////////
        public List<MProduct> GetProductDspData()
        {
            List<MProduct> product = new List<MProduct>();
            try
            {
                var context = new SalesManagementContext();
                //非表示フラグが0のデータを抜き取る
                product = context.MProducts.Where(x => x.PrFlag == 0).ToList();
                context.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "例外エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return product;
        }
    }
}
