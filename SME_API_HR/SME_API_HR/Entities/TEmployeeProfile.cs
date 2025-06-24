using System;
using System.Collections.Generic;

namespace SME_API_HR.Entities;

public partial class TEmployeeProfile
{
    public int Id { get; set; }

    public string? EmployeeId { get; set; }

    public string? InternalPhone { get; set; }

    public string? MilitaryStatus { get; set; }

    public string? MailingAddrTh { get; set; }

    public string? MailingAddrEn { get; set; }

    public string? MailingSubdistrict { get; set; }

    public string? MailingDistrict { get; set; }

    public string? MailingProvince { get; set; }

    public string? MailingCountry { get; set; }

    public string? MailingPostCode { get; set; }

    public string? MailingPhoneNo { get; set; }

    public string? RegisAddrTh { get; set; }

    public string? RegisAddrEn { get; set; }

    public string? RegisSubdistrict { get; set; }

    public string? RegisDistrict { get; set; }

    public string? RegisProvince { get; set; }

    public string? RegisCountry { get; set; }

    public string? RegisPostCode { get; set; }

    public string? RegisPhoneNo { get; set; }

    public string? BloodGroup { get; set; }

    public string? Religion { get; set; }

    public string? Race { get; set; }

    public string? Nationality { get; set; }

    public string? JobDetails { get; set; }

    public string? NickName { get; set; }

    public DateTime? CreateDate { get; set; }
}
