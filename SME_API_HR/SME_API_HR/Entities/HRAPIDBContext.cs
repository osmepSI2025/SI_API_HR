using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SME_API_HR.Entities;

public partial class HRAPIDBContext : DbContext
{
    public HRAPIDBContext()
    {
    }

    public HRAPIDBContext(DbContextOptions<HRAPIDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MApiInformation> MApiInformations { get; set; }

    public virtual DbSet<MBusinessUnit> MBusinessUnits { get; set; }

    public virtual DbSet<MEmployee> MEmployees { get; set; }

    public virtual DbSet<MEmployeeById> MEmployeeByIds { get; set; }

    public virtual DbSet<MJobLevel> MJobLevels { get; set; }

    public virtual DbSet<MJobTitle> MJobTitles { get; set; }

    public virtual DbSet<MPosition> MPositions { get; set; }

    public virtual DbSet<MScheduledJob> MScheduledJobs { get; set; }

    public virtual DbSet<TEmployeeContract> TEmployeeContracts { get; set; }

    public virtual DbSet<TEmployeeMovement> TEmployeeMovements { get; set; }

    public virtual DbSet<TEmployeeProfile> TEmployeeProfiles { get; set; }

    public virtual DbSet<TOrganizationTree> TOrganizationTrees { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=27.254.173.62;Database=bluecarg_SMEHR;User Id=SMEHR;Password=jEm949s@3;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("SMEHR");

        modelBuilder.Entity<MApiInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MApiInformation");

            entity.ToTable("M_ApiInformation");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApiKey).HasMaxLength(150);
            entity.Property(e => e.AuthorizationType).HasMaxLength(50);
            entity.Property(e => e.ContentType).HasMaxLength(150);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.MethodType).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.ServiceNameCode).HasMaxLength(250);
            entity.Property(e => e.ServiceNameTh).HasMaxLength(250);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.Urldevelopment).HasColumnName("URLDevelopment");
            entity.Property(e => e.Urlproduction).HasColumnName("URLProduction");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<MBusinessUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__m_busine__3213E83F215A8FAF");

            entity.ToTable("m_business_units");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AbbreviationEn).HasMaxLength(100);
            entity.Property(e => e.AbbreviationTh).HasMaxLength(100);
            entity.Property(e => e.BusinessUnitCode).HasMaxLength(50);
            entity.Property(e => e.BusinessUnitId).HasMaxLength(50);
            entity.Property(e => e.CompanyId).HasMaxLength(50);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
            entity.Property(e => e.NameEn).HasMaxLength(500);
            entity.Property(e => e.NameTh).HasMaxLength(500);
            entity.Property(e => e.ParentId).HasMaxLength(50);
        });

        modelBuilder.Entity<MEmployee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83FE0901328");

            entity.ToTable("M_Employees");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessUnitId).HasMaxLength(50);
            entity.Property(e => e.CompanyId).HasMaxLength(50);
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeId).HasMaxLength(50);
            entity.Property(e => e.EmployeeStatus).HasMaxLength(10);
            entity.Property(e => e.EmployeeType).HasMaxLength(50);
            entity.Property(e => e.EmploymentDate).HasColumnType("datetime");
            entity.Property(e => e.FirstNameEn).HasMaxLength(100);
            entity.Property(e => e.FirstNameTh).HasMaxLength(100);
            entity.Property(e => e.LastNameEn).HasMaxLength(100);
            entity.Property(e => e.LastNameTh).HasMaxLength(100);
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.NameEn).HasMaxLength(200);
            entity.Property(e => e.NameTh).HasMaxLength(200);
            entity.Property(e => e.PositionId).HasMaxLength(50);
            entity.Property(e => e.SupervisorId).HasMaxLength(50);
            entity.Property(e => e.TerminationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MEmployeeById>(entity =>
        {
            entity.ToTable("M_EmployeeByID");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessUnitId).HasMaxLength(50);
            entity.Property(e => e.CompanyId).HasMaxLength(50);
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeId).HasMaxLength(50);
            entity.Property(e => e.EmployeeStatus).HasMaxLength(10);
            entity.Property(e => e.EmployeeType).HasMaxLength(50);
            entity.Property(e => e.EmploymentDate).HasColumnType("datetime");
            entity.Property(e => e.FirstNameEn).HasMaxLength(100);
            entity.Property(e => e.FirstNameTh).HasMaxLength(100);
            entity.Property(e => e.LastNameEn).HasMaxLength(100);
            entity.Property(e => e.LastNameTh).HasMaxLength(100);
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.NameEn).HasMaxLength(200);
            entity.Property(e => e.NameTh).HasMaxLength(200);
            entity.Property(e => e.PositionId).HasMaxLength(50);
            entity.Property(e => e.SupervisorId).HasMaxLength(50);
            entity.Property(e => e.TerminationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<MJobLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__m_job_le__3213E83F9B76C295");

            entity.ToTable("m_job_level");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Grouping).HasMaxLength(100);
            entity.Property(e => e.Module).HasMaxLength(50);
            entity.Property(e => e.NameEn).HasMaxLength(200);
            entity.Property(e => e.NameTh).HasMaxLength(200);
            entity.Property(e => e.ProjectCode).HasMaxLength(50);
            entity.Property(e => e.TypeCode).HasMaxLength(50);
        });

        modelBuilder.Entity<MJobTitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__m_job_ti__3213E83FC8E2E107");

            entity.ToTable("m_job_titles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Grouping).HasMaxLength(100);
            entity.Property(e => e.Module).HasMaxLength(50);
            entity.Property(e => e.NameEn).HasMaxLength(200);
            entity.Property(e => e.NameTh).HasMaxLength(200);
            entity.Property(e => e.ProjectCode).HasMaxLength(50);
            entity.Property(e => e.TypeCode).HasMaxLength(50);
        });

        modelBuilder.Entity<MPosition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__m_positi__3213E83F653991F5");

            entity.ToTable("m_positions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Grouping).HasMaxLength(100);
            entity.Property(e => e.Module).HasMaxLength(50);
            entity.Property(e => e.NameEn).HasMaxLength(200);
            entity.Property(e => e.NameTh).HasMaxLength(200);
            entity.Property(e => e.ProjectCode).HasMaxLength(50);
            entity.Property(e => e.TypeCode).HasMaxLength(50);
        });

        modelBuilder.Entity<MScheduledJob>(entity =>
        {
            entity.ToTable("M_ScheduledJobs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.JobName).HasMaxLength(150);
        });

        modelBuilder.Entity<TEmployeeContract>(entity =>
        {
            entity.ToTable("T_Employee_Contract");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessUnitId).HasMaxLength(50);
            entity.Property(e => e.CompanyId).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.EmployeeId).HasMaxLength(50);
            entity.Property(e => e.EmployeeStatus).HasMaxLength(10);
            entity.Property(e => e.EmployeeType).HasMaxLength(50);
            entity.Property(e => e.EmploymentDate).HasColumnType("datetime");
            entity.Property(e => e.FirstNameEn).HasMaxLength(100);
            entity.Property(e => e.FirstNameTh).HasMaxLength(100);
            entity.Property(e => e.IdCard).HasMaxLength(200);
            entity.Property(e => e.LastNameEn).HasMaxLength(100);
            entity.Property(e => e.LastNameTh).HasMaxLength(100);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.NameEn).HasMaxLength(100);
            entity.Property(e => e.NameTh).HasMaxLength(100);
            entity.Property(e => e.PassportNo).HasMaxLength(100);
            entity.Property(e => e.PositionId).HasMaxLength(50);
            entity.Property(e => e.Salary).HasMaxLength(200);
            entity.Property(e => e.SupervisorId).HasMaxLength(50);
            entity.Property(e => e.TerminationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TEmployeeMovement>(entity =>
        {
            entity.ToTable("T_Employee_Movements");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.BusinessUnitId)
                .HasMaxLength(50)
                .HasColumnName("businessUnitId");
            entity.Property(e => e.CalendarGroupId)
                .HasMaxLength(50)
                .HasColumnName("calendarGroupId");
            entity.Property(e => e.CompanyId)
                .HasMaxLength(50)
                .HasColumnName("companyId");
            entity.Property(e => e.ContractEndDate)
                .HasColumnType("datetime")
                .HasColumnName("contractEndDate");
            entity.Property(e => e.ContractStartDate)
                .HasColumnType("datetime")
                .HasColumnName("contractStartDate");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.EffectiveDate)
                .HasColumnType("datetime")
                .HasColumnName("effectiveDate");
            entity.Property(e => e.EmployeeCode)
                .HasMaxLength(50)
                .HasColumnName("employeeCode");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(50)
                .HasColumnName("employeeId");
            entity.Property(e => e.EmployeeStatus)
                .HasMaxLength(50)
                .HasColumnName("employeeStatus");
            entity.Property(e => e.EmployeeTypeId)
                .HasMaxLength(50)
                .HasColumnName("employeeTypeId");
            entity.Property(e => e.Employment)
                .HasMaxLength(50)
                .HasColumnName("employment");
            entity.Property(e => e.EmploymentDate)
                .HasColumnType("datetime")
                .HasColumnName("employmentDate");
            entity.Property(e => e.IsBlacklist).HasColumnName("isBlacklist");
            entity.Property(e => e.JobGradeId)
                .HasMaxLength(50)
                .HasColumnName("jobGradeId");
            entity.Property(e => e.JobLevelId)
                .HasMaxLength(50)
                .HasColumnName("jobLevelId");
            entity.Property(e => e.JobTitleId)
                .HasMaxLength(50)
                .HasColumnName("jobTitleId");
            entity.Property(e => e.JoinDate)
                .HasColumnType("datetime")
                .HasColumnName("joinDate");
            entity.Property(e => e.MovementReasonId)
                .HasMaxLength(50)
                .HasColumnName("movementReasonId");
            entity.Property(e => e.MovementTypeId)
                .HasMaxLength(50)
                .HasColumnName("movementTypeId");
            entity.Property(e => e.OccupationDate)
                .HasColumnType("datetime")
                .HasColumnName("occupationDate");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("paymentDate");
            entity.Property(e => e.PayrollGroupId)
                .HasMaxLength(50)
                .HasColumnName("payrollGroupId");
            entity.Property(e => e.PositionId)
                .HasMaxLength(50)
                .HasColumnName("positionId");
            entity.Property(e => e.ProbationDate)
                .HasColumnType("datetime")
                .HasColumnName("probationDate");
            entity.Property(e => e.ProbationDuration).HasColumnName("probationDuration");
            entity.Property(e => e.ProbationExtend)
                .HasMaxLength(50)
                .HasColumnName("probationExtend");
            entity.Property(e => e.ProbationResult)
                .HasMaxLength(50)
                .HasColumnName("probationResult");
            entity.Property(e => e.Remark)
                .HasColumnType("ntext")
                .HasColumnName("remark");
            entity.Property(e => e.RenewContractCount)
                .HasMaxLength(50)
                .HasColumnName("renewContractCount");
            entity.Property(e => e.ServiceYearAdjust)
                .HasMaxLength(50)
                .HasColumnName("serviceYearAdjust");
            entity.Property(e => e.StandardWorkHoursId)
                .HasMaxLength(50)
                .HasColumnName("standardWorkHoursID");
            entity.Property(e => e.SupervisorCode)
                .HasMaxLength(50)
                .HasColumnName("supervisorCode");
            entity.Property(e => e.TerminationDate)
                .HasColumnType("datetime")
                .HasColumnName("terminationDate");
            entity.Property(e => e.TerminationReason)
                .HasColumnType("ntext")
                .HasColumnName("terminationReason");
            entity.Property(e => e.TerminationSso)
                .HasMaxLength(50)
                .HasColumnName("terminationSSO");
            entity.Property(e => e.WorkLocationId)
                .HasMaxLength(50)
                .HasColumnName("workLocationId");
            entity.Property(e => e.WorkOperation)
                .HasMaxLength(50)
                .HasColumnName("workOperation");
        });

        modelBuilder.Entity<TEmployeeProfile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Employ__3213E83F8DBBAE50");

            entity.ToTable("T_Employee_Profile");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BloodGroup).HasMaxLength(10);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasMaxLength(50);
            entity.Property(e => e.InternalPhone).HasMaxLength(50);
            entity.Property(e => e.MailingCountry).HasMaxLength(100);
            entity.Property(e => e.MailingDistrict).HasMaxLength(100);
            entity.Property(e => e.MailingPhoneNo).HasMaxLength(50);
            entity.Property(e => e.MailingPostCode).HasMaxLength(20);
            entity.Property(e => e.MailingProvince).HasMaxLength(100);
            entity.Property(e => e.MailingSubdistrict).HasMaxLength(100);
            entity.Property(e => e.MilitaryStatus).HasMaxLength(50);
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.NickName).HasMaxLength(100);
            entity.Property(e => e.Race).HasMaxLength(50);
            entity.Property(e => e.RegisCountry).HasMaxLength(100);
            entity.Property(e => e.RegisDistrict).HasMaxLength(100);
            entity.Property(e => e.RegisPhoneNo).HasMaxLength(50);
            entity.Property(e => e.RegisPostCode).HasMaxLength(20);
            entity.Property(e => e.RegisProvince).HasMaxLength(100);
            entity.Property(e => e.RegisSubdistrict).HasMaxLength(100);
            entity.Property(e => e.Religion).HasMaxLength(50);
        });

        modelBuilder.Entity<TOrganizationTree>(entity =>
        {
            entity.ToTable("T_Organization_Tree");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BusinessUnitId).HasMaxLength(50);
            entity.Property(e => e.BusinessUnitNameEn).HasMaxLength(250);
            entity.Property(e => e.BusinessUnitNameTh).HasMaxLength(250);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ParentBusinessUnitId).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
