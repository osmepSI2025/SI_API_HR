using SME_API_HR.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SME_API_HR.Models
{
    public class TOrganizationTreeModels
    {
        [Key]
        public string? BusinessUnitId { get; set; }
        public string? BusinessUnitNameTh { get; set; }
        public string? BusinessUnitNameEn { get; set; }

        public string? ParentId { get; set; }
        public TOrganizationTreeModels? Parent { get; set; }

        public ICollection<TOrganizationTreeModels> Children { get; set; } = new List<TOrganizationTreeModels>();
    }
    public class JsonBusinessUnit
    {
        [JsonPropertyName("businessUnitId")]
        public string? BusinessUnitId { get; set; }

        [JsonPropertyName("businessUnitNameTh")]
        public string? BusinessUnitNameTh { get; set; }

        [JsonPropertyName("businessUnitNameEn")]
        public string? BusinessUnitNameEn { get; set; }

        [JsonPropertyName("children")]
        public List<JsonBusinessUnit> Children { get; set; } = new();
    }

    public class BusinessUnit
    {
        public string? BusinessUnitId { get; set; }
        public string? BusinessUnitNameTh { get; set; }
        public string? BusinessUnitNameEn { get; set; }
        public List<BusinessUnit> Children { get; set; } = new();
    }

    public class ApiTOrganizationTreeResponse
    {
        [JsonPropertyName("results")]
        public List<BusinessUnit> Results { get; set; } = new();
    }
  
}
