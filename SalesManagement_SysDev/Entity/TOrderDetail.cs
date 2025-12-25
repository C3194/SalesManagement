using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

namespace SalesManagement_SysDev;

public partial class TOrderDetail
{
    public int OrDetailId { get; set; }

    public int OrId { get; set; }

    public int PrId { get; set; }

    public int OrQuantity { get; set; }

    public decimal OrTotalPrice { get; set; }

    public virtual TOrder Or { get; set; } = null!;

    public virtual MProduct Pr { get; set; } = null!;
   

    internal static DataRow NewRow()
    {
        throw new NotImplementedException();
    }
}

//データグリッドビュー表示用
internal class TOrderDetailDsp 
{
    //[0]
    [DisplayName("受注詳細ID")]
    public string OrDetailId { get; set;}

    //[1]
    [DisplayName("受注ID")]
    public string OrId { get; set;}

    //[2]
    [DisplayName("商品ID")]
    public string PrId { get; set;}

    //[3]
    [DisplayName("商品名")]
    public string PrName { get; set;}

    //[4]
    [DisplayName("数量")]
    public string OrQuantity { get; set;}

    //[5]
    [DisplayName("合計金額")]
    public string OrTotalPrice { get; set;}
}
