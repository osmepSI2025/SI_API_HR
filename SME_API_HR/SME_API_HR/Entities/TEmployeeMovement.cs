using System;
using System.Collections.Generic;

namespace SME_API_HR.Entities;

public partial class TEmployeeMovement
{
    public int Id { get; set; }

    public string? EmployeeId { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public string? MovementTypeId { get; set; }

    public string? MovementReasonId { get; set; }

    public string? EmployeeCode { get; set; }

    public string? Employment { get; set; }

    public string? EmployeeStatus { get; set; }

    public string? EmployeeTypeId { get; set; }

    public string? PayrollGroupId { get; set; }

    public string? CompanyId { get; set; }

    public string? BusinessUnitId { get; set; }

    public string? PositionId { get; set; }

    public string? WorkLocationId { get; set; }

    public string? CalendarGroupId { get; set; }

    public string? JobTitleId { get; set; }

    public string? JobLevelId { get; set; }

    public string? JobGradeId { get; set; }

    public DateTime? ContractStartDate { get; set; }

    public DateTime? ContractEndDate { get; set; }

    public string? RenewContractCount { get; set; }

    public DateTime? ProbationDate { get; set; }

    public int? ProbationDuration { get; set; }

    public string? ProbationResult { get; set; }

    public string? ProbationExtend { get; set; }

    public DateTime? EmploymentDate { get; set; }

    public DateTime? JoinDate { get; set; }

    public DateTime? OccupationDate { get; set; }

    public DateTime? TerminationDate { get; set; }

    public string? TerminationReason { get; set; }

    public string? TerminationSso { get; set; }

    public bool? IsBlacklist { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? Remark { get; set; }

    public string? ServiceYearAdjust { get; set; }

    public string? SupervisorCode { get; set; }

    public string? StandardWorkHoursId { get; set; }

    public string? WorkOperation { get; set; }

    public DateTime? CreateDate { get; set; }
}
