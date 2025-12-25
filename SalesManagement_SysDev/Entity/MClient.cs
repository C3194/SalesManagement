using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace SalesManagement_SysDev;

public partial class MClient
{
    public int ClId { get; set; }

    public int SoId { get; set; }

    public string ClName { get; set; } = null!;

    public string ClAddress { get; set; } = null!;

    public string ClPhone { get; set; } = null!;

    public string ClPostal { get; set; } = null!;

    public string ClFax { get; set; } = null!;

    public int ClFlag { get; set; }

    public string? ClHidden { get; set; }

    public virtual MSalesOffice So { get; set; } = null!;

    public virtual ICollection<TArrival> TArrivals { get; set; } = new List<TArrival>();

    public virtual ICollection<TChumon> TChumons { get; set; } = new List<TChumon>();

    public virtual ICollection<TOrder> TOrders { get; set; } = new List<TOrder>();

    public virtual ICollection<TSale> TSales { get; set; } = new List<TSale>();

    public virtual ICollection<TShipment> TShipments { get; set; } = new List<TShipment>();

    public virtual ICollection<TSyukko> TSyukkos { get; set; } = new List<TSyukko>();
}

//データグリッド表示用
internal class MClientDsp
{
    [DisplayName("顧客ID")]
    public string ClId { get; set; }

    [DisplayName("顧客名")]
    public string ClName { get; set; }

    [DisplayName("郵便番号")]
    public string ClPostal { get; set; }

    [DisplayName("住所")]
    public string ClAddress { get; set; }

    [DisplayName("電話番号")]
    public string ClPhone { get; set; }

    [DisplayName("FAX")]
    public string ClFAX { get; set; }

    [DisplayName("営業所ID")]
    public string SoId { get; set; }

    [DisplayName("営業所名")]
    public string SoName { get; set; }

    [DisplayName("非表示フラグ")]
    public int ClFlag { get; set; }

    [DisplayName("非表示理由")]
    public string? ClHidden { get; set; }
}