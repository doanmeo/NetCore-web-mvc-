using System;
using System.Collections.Generic;

namespace VuVanDoan_CNTT1_231230746.String;

public partial class TinhThanh
{
    public string MaTinhThanh { get; set; } = null!;

    public string TenTinhThanh { get; set; } = null!;

    public string? VietTat { get; set; }

    public virtual ICollection<Tuyen> TuyenMaDiemCuoiNavigations { get; set; } = new List<Tuyen>();

    public virtual ICollection<Tuyen> TuyenMaDiemDauNavigations { get; set; } = new List<Tuyen>();
}
