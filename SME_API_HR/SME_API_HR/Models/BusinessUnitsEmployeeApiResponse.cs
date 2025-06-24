using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

public class BusinessUnitsEmployeeApiResponse
{
    [JsonPropertyName("results")]
    public List<BusinessUnitsEmployeeResult>? Results { get; set; }
}

public class BusinessUnitsEmployeeResult
{
    [JsonPropertyName("employeeId")]
    public string? EmployeeId { get; set; }

    [JsonPropertyName("employeeCode")]
    public string? EmployeeCode { get; set; }

    [JsonPropertyName("nameTh")]
    public string? NameTh { get; set; }

    [JsonPropertyName("nameEn")]
    public string? NameEn { get; set; }

    [JsonPropertyName("firstNameTh")]
    public string? FirstNameTh { get; set; }

    [JsonPropertyName("firstNameEn")]
    public string? FirstNameEn { get; set; }

    [JsonPropertyName("lastNameTh")]
    public string? LastNameTh { get; set; }

    [JsonPropertyName("lastNameEn")]
    public string? LastNameEn { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("mobile")]
    public string? Mobile { get; set; }

    [JsonPropertyName("employmentDate")]
    public DateTime? EmploymentDate { get; set; }

    [JsonPropertyName("terminationDate")]
    public DateTime? TerminationDate { get; set; }

    [JsonPropertyName("employeeType")]
    public string? EmployeeType { get; set; }

    [JsonPropertyName("employeeStatus")]
    public string? EmployeeStatus { get; set; }

    [JsonPropertyName("supervisorId")]
    public string? SupervisorId { get; set; }

    [JsonPropertyName("companyId")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("businessUnitId")]
    public string? BusinessUnitId { get; set; }

    [JsonPropertyName("positionId")]
    public string? PositionId { get; set; }
}
