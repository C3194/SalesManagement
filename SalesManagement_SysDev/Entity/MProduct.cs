using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SalesManagement_SysDev;

public partial class MProduct
{
    public int PrId { get; set; }

    public int MaId { get; set; }

    public string PrName { get; set; } = null!;

    public decimal Price { get; set; }

    public string? PrJcode { get; set; }

    public int PrSafetyStock { get; set; }

    public int ScId { get; set; }

    public string PrModelNumber { get; set; } = null!;

    public string PrColor { get; set; } = null!;

    public DateTime PrReleaseDate { get; set; }

    public int PrFlag { get; set; }

    public string? PrHidden { get; set; }

    public virtual MSmallClassification Sc { get; set; } = null!;

    public virtual MMaker Ma { get; set; } = null!;

    public virtual ICollection<TArrivalDetail> TArrivalDetails { get; set; } = new List<TArrivalDetail>();

    public virtual ICollection<TChumonDetail> TChumonDetails { get; set; } = new List<TChumonDetail>();

    public virtual ICollection<THattyuDetail> THattyuDetails { get; set; } = new List<THattyuDetail>();

    public virtual ICollection<TOrderDetail> TOrderDetails { get; set; } = new List<TOrderDetail>();

    public virtual ICollection<TSaleDetail> TSaleDetails { get; set; } = new List<TSaleDetail>();

    public virtual ICollection<TShipmentDetail> TShipmentDetails { get; set; } = new List<TShipmentDetail>();

    public virtual ICollection<TStock> TStocks { get; set; } = new List<TStock>();

    public virtual ICollection<TSyukkoDetail> TSyukkoDetails { get; set; } = new List<TSyukkoDetail>();

    public virtual ICollection<TWarehousingDetail> TWarehousingDetails { get; set; } = new List<TWarehousingDetail>();
}

//データグリッド表示用
internal class MProductDsp
{
    [DisplayName("商品ID")]
    public string PrId { get; set; }

    [DisplayName("商品名")]
    public string PrName { get; set; }

    [DisplayName("メーカーID")]
    public string MaId { get; set; }

    [DisplayName("メーカー名")]
    public string MaName { get; set; }

    [DisplayName("価格")]
    public string Price { get; set; }

    [DisplayName("JANコード")]
    public string PrJcode { get; set;}

    [DisplayName("色")]
    public string PrColor { get; set; }

    [DisplayName("型番")]
    public string PrModelNumber { get; set; }

    [DisplayName("安全在庫数")]
    public string PrSafetyStock { get; set; }

    [DisplayName("発売日")]
    public DateTime? PrReleaseDate { get; set; }

    [DisplayName("小分類ID")]
    public string ScId { get; set; }

    [DisplayName("大分類ID")]
    public string McId { get; set; }

    [DisplayName("小分類名")]
    public string ScName { get; set; }

    [DisplayName("非表示フラグ")]
    public string PrFlag { get; set; }

    [DisplayName("非表示理由")]
    public string? PrHidden { get; set; }
}
