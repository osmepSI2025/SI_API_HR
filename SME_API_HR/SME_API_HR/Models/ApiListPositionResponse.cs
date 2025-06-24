using System.Text.Json.Serialization;

public class ApiListPositionResponse
{
    [JsonPropertyName("results")]
    public List<PositionResult>? Results { get; set; }
}
public class ApiPositionResponse
{
    [JsonPropertyName("results")]
    public PositionResult? Results { get; set; }
}
public class PositionResult
{
    [JsonPropertyName("seq")]
    public int? Seq { get; set; }

    [JsonPropertyName("projectCode")]
    public string? ProjectCode { get; set; }

    [JsonPropertyName("code")]
    public string? Code { get; set; }

    [JsonPropertyName("typeCode")]
    public string? TypeCode { get; set; }

    [JsonPropertyName("module")]
    public string? Module { get; set; }

    [JsonPropertyName("grouping")]
    public string? Grouping { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("nameTh")]
    public string? NameTh { get; set; }

    [JsonPropertyName("nameEn")]
    public string? NameEn { get; set; }

    [JsonPropertyName("descriptionTh")]
    public string? DescriptionTh { get; set; }

    [JsonPropertyName("descriptionEn")]
    public string? DescriptionEn { get; set; }
}
