using System;
using System.Collections.Generic;

namespace VuVanDoan_CNTT1_231230746.String;

public partial class LoaiXe
{
    public int MaLoaiXe { get; set; }

    public string? TenLoaiXe { get; set; }

    public virtual ICollection<Xe> Xes { get; set; } = new List<Xe>();
}
