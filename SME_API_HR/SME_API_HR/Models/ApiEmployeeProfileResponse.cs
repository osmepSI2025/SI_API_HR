using System.Text.Json.Serialization;

public class ApiEmployeeProfileResponse
{
    [JsonPropertyName("results")]
    public EmployeeProfileResult? Results { get; set; }
}

public class EmployeeProfileResult
{
    [JsonPropertyName("internalPhone")]
    public string? InternalPhone { get; set; }

    [JsonPropertyName("militaryStatus")]
    public string? MilitaryStatus { get; set; }

    [JsonPropertyName("mailingAddrTh")]
    public string? MailingAddrTh { get; set; }

    [JsonPropertyName("mailingAddrEn")]
    public string? MailingAddrEn { get; set; }

    [JsonPropertyName("mailingSubdistrict")]
    public string? MailingSubdistrict { get; set; }

    [JsonPropertyName("mailingDistrict")]
    public string? MailingDistrict { get; set; }

    [JsonPropertyName("mailingProvince")]
    public string? MailingProvince { get; set; }

    [JsonPropertyName("mailingCountry")]
    public string? MailingCountry { get; set; }

    [JsonPropertyName("mailingPostCode")]
    public string? MailingPostCode { get; set; }

    [JsonPropertyName("mailingPhoneNo")]
    public string? MailingPhoneNo { get; set; }

    [JsonPropertyName("regisAddrTh")]
    public string? RegisAddrTh { get; set; }

    [JsonPropertyName("regisAddrEn")]
    public string? RegisAddrEn { get; set; }

    [JsonPropertyName("regisSubdistrict")]
    public string? RegisSubdistrict { get; set; }

    [JsonPropertyName("regisDistrict")]
    public string? RegisDistrict { get; set; }

    [JsonPropertyName("regisProvince")]
    public string? RegisProvince { get; set; }

    [JsonPropertyName("regisCountry")]
    public string? RegisCountry { get; set; }

    [JsonPropertyName("regisPostCode")]
    public string? RegisPostCode { get; set; }

    [JsonPropertyName("regisPhoneNo")]
    public string? RegisPhoneNo { get; set; }

    [JsonPropertyName("bloodGroup")]
    public string? BloodGroup { get; set; }

    [JsonPropertyName("religion")]
    public string? Religion { get; set; }

    [JsonPropertyName("race")]
    public string? Race { get; set; }

    [JsonPropertyName("nationality")]
    public string? Nationality { get; set; }

    [JsonPropertyName("jobDetails")]
    public string? JobDetails { get; set; }

    [JsonPropertyName("nickName")]
    public string? NickName { get; set; }
}
