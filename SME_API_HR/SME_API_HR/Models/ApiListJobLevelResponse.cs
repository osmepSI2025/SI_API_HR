using System.Text.Json.Serialization;

public class ApiListJobLevelResponse
{
    [JsonPropertyName("results")]
    public List<JobLevelResult>? Results { get; set; }
}
public class ApiJobLevelResponse
{
    [JsonPropertyName("results")]
    public JobLevelResult? Results { get; set; }
}
public class JobLevelResult
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
