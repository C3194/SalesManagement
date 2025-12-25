using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SalesManagement_SysDev;

public partial class TOrder
{
    public int OrId { get; set; }

    public int SoId { get; set; }

    public int EmId { get; set; }

    public int ClId { get; set; }

    public string ClCharge { get; set; } = null!;

    public DateTime OrDate { get; set; }

    public int? OrStateFlag { get; set; }

    public int OrFlag { get; set; }

    public string? OrHidden { get; set; }

    public virtual MClient Cl { get; set; } = null!;

    public virtual MEmployee Em { get; set; } = null!;

    public virtual MSalesOffice So { get; set; } = null!;

    public virtual ICollection<TArrival> TArrivals { get; set; } = new List<TArrival>();

    public virtual ICollection<TChumon> TChumons { get; set; } = new List<TChumon>();

    public virtual ICollection<TOrderDetail> TOrderDetails { get; set; } = new List<TOrderDetail>();

    public virtual ICollection<TShipment> TShipments { get; set; } = new List<TShipment>();

    public virtual ICollection<TSyukko> TSyukkos { get; set; } = new List<TSyukko>();

    public virtual ICollection<TSale> TSales { get; set; } = new List<TSale>();
}

//データグリッドビュー表示用
internal class TOrderDsp
{
    //[0]
    [DisplayName("受注ID")]
    public string OrId { get; set; }

    //[1]
    [DisplayName("顧客担当者名")]
    public string ClCharge { get; set; }

    //[2]
    [DisplayName("顧客ID")]
    public string ClId { get; set; }

    //[3]
    [DisplayName("顧客名")]
    public string ClName { get; set; }

    //[4]
    [DisplayName("社員ID")]
    public string EmId { get; set; }

    //[5]
    [DisplayName("社員名")]
    public string EmName { get; set; }

    //[6]
    [DisplayName("営業所ID")]
    public string SoId { get; set; }

    //[7]
    [DisplayName("営業署名")]
    public string SoName { get; set; }

    //[8]
    [DisplayName("受注年月日")]
    public DateTime OrDate { get; set; }

    //[9]
    [DisplayName("受注状態フラグ")]
    public string OrStateFlag { get; set; }

    //[10]
    [DisplayName("非表示フラグ")]
    public string OrFlag { get; set; }

    //[11]
    [DisplayName("非表示理由")]
    public string? OrHidden { get; set; }
}