using System;
using System.Collections.Generic;

namespace SME_API_HR.Entities;

public partial class TOrganizationTree
{
    public int Id { get; set; }

    public string? BusinessUnitId { get; set; }

    public string? ParentBusinessUnitId { get; set; }

    public string? BusinessUnitNameTh { get; set; }

    public string? BusinessUnitNameEn { get; set; }

    public int? SortOrder { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }
}
