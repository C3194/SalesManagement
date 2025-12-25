using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagement_SysDev;

public partial class MEmployee
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int EmId { get; set; }

    public string EmName { get; set; } = null!;

    public int SoId { get; set; }

    public int PoId { get; set; }

    public DateTime EmHiredate { get; set; }

    public string EmPassword { get; set; } = null!;

    public string EmPhone { get; set; } = null!;

    public int EmFlag { get; set; }

    public string? EmHidden { get; set; }


    public virtual MPosition Po { get; set; } = null!;

    public virtual MSalesOffice So { get; set; } = null!;

    public virtual ICollection<TArrival> TArrivals { get; set; } = new List<TArrival>();

    public virtual ICollection<TChumon> TChumons { get; set; } = new List<TChumon>();

    public virtual ICollection<THattyu> THattyus { get; set; } = new List<THattyu>();

    public virtual ICollection<TOrder> TOrders { get; set; } = new List<TOrder>();

    public virtual ICollection<TSale> TSales { get; set; } = new List<TSale>();

    public virtual ICollection<TShipment> TShipments { get; set; } = new List<TShipment>();

    public virtual ICollection<TSyukko> TSyukkos { get; set; } = new List<TSyukko>();

    public virtual ICollection<TWarehousing> TWarehousings { get; set; } = new List<TWarehousing>();

   
}

//データグリッド表示用
internal class MEmployeeDsp
{
    [DisplayName("社員ID")]
    public string EmId { get; set; }

    [DisplayName("社員名")]
    public string EmName { get; set; }

    [DisplayName("営業所ID")]
    public string SoId { get; set; }

    [DisplayName("役職ID")]
    public string PoId { get; set; }

    [DisplayName("生年月日")]
    public DateTime? EmHiredate { get; set; }

    [DisplayName("パスワード")]
    public string EmPassword { get; set; }

    [DisplayName("電話番号")]
    public string EmPhone { get; set; }

    [DisplayName("非表示フラグ")]
    public string EmFlag { get; set; }

    [DisplayName("非表示理由")]
    public string EmHidden { get; set; }
}
