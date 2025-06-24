public class BusinessUnitTreeApiResponse
{
    public List<BusinessUnitTreeModel> Results { get; set; } = new();
}

public class BusinessUnitTreeModel
{
    public string? BusinessUnitId { get; set; }
    public string? BusinessUnitNameTh { get; set; }
    public string? BusinessUnitNameEn { get; set; }
    public List<BusinessUnitTreeModel> Children { get; set; } = new();
}
