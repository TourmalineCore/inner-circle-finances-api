using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping;

public class EmployeeMapping : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        var ceoHiringAtUtc = DateTime.SpecifyKind(new DateTime(2020, 01, 01, 0, 0, 0), DateTimeKind.Utc);

        builder.Property(x => x.CorporateEmail).IsRequired();
        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.LastName).IsRequired();

        builder.Property(x => x.MiddleName).IsRequired(false);
        builder.Property(x => x.PersonalEmail).IsRequired(false);
        builder.Property(x => x.Phone).IsRequired(false);
        builder.Property(x => x.GitHub).IsRequired(false);
        builder.Property(x => x.GitLab).IsRequired(false);
        builder.Property(x => x.PersonnelNumber).IsRequired(false);

        builder.Property(x => x.IsBlankEmployee).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.IsCurrentEmployee).IsRequired().HasDefaultValue(false);
        builder.Property(x => x.IsEmployedOfficially).IsRequired().HasDefaultValue(false);

        builder
            .Property(x => x.PersonnelNumber)
            .HasConversion(
                personnelNumber => personnelNumber != null ? personnelNumber.ToString() : null,
                personnelNumber => personnelNumber != null ? new EmployeePersonnelNumber(personnelNumber) : null);

        builder.HasIndex(x => x.CorporateEmail).IsUnique();

        builder.OwnsOne(
            x => x.FinancialMetrics,
            fm =>
            {
                fm.Property(x => x.Salary).HasColumnName(nameof(EmployeeFinancialMetrics.Salary));
                fm.Property(x => x.HourlyCostFact).HasColumnName(nameof(EmployeeFinancialMetrics.HourlyCostFact));
                fm.Property(x => x.HourlyCostHand).HasColumnName(nameof(EmployeeFinancialMetrics.HourlyCostHand));
                fm.Property(x => x.Earnings).HasColumnName(nameof(EmployeeFinancialMetrics.Earnings));
                fm.Property(x => x.DistrictCoefficient)
                    .HasColumnName(nameof(EmployeeFinancialMetrics.DistrictCoefficient));
                fm.Property(x => x.IncomeTaxContributions)
                    .HasColumnName(nameof(EmployeeFinancialMetrics.IncomeTaxContributions));
                fm.Property(x => x.PensionContributions)
                    .HasColumnName(nameof(EmployeeFinancialMetrics.PensionContributions));
                fm.Property(x => x.MedicalContributions)
                    .HasColumnName(nameof(EmployeeFinancialMetrics.MedicalContributions));
                fm.Property(x => x.SocialInsuranceContributions)
                    .HasColumnName(nameof(EmployeeFinancialMetrics.SocialInsuranceContributions));
                fm.Property(x => x.Expenses).HasColumnName(nameof(EmployeeFinancialMetrics.Expenses));
                fm.Property(x => x.Profit).HasColumnName(nameof(EmployeeFinancialMetrics.Profit));
                fm.Property(x => x.ProfitAbility).HasColumnName(nameof(EmployeeFinancialMetrics.ProfitAbility));
                fm.Property(x => x.GrossSalary).HasColumnName(nameof(EmployeeFinancialMetrics.GrossSalary));
                fm.Property(x => x.NetSalary).HasColumnName(nameof(EmployeeFinancialMetrics.NetSalary));
                fm.Property(x => x.RatePerHour).HasColumnName(nameof(EmployeeFinancialMetrics.RatePerHour));
                fm.Property(x => x.Pay).HasColumnName(nameof(EmployeeFinancialMetrics.Pay));
                fm.Property(x => x.Prepayment).HasColumnName(nameof(EmployeeFinancialMetrics.Prepayment));
                fm.Property(x => x.EmploymentType).HasColumnName(nameof(EmployeeFinancialMetrics.EmploymentType));
                fm.Property(x => x.ParkingCostPerMonth)
                    .HasColumnName(nameof(EmployeeFinancialMetrics.ParkingCostPerMonth));
                fm.Property(x => x.AccountingPerMonth)
                    .HasColumnName(nameof(EmployeeFinancialMetrics.AccountingPerMonth));
                fm.ToTable(nameof(EmployeeFinancialMetrics));
            });

        builder.HasData(
            new
            {
                Id = 1L,
                FirstName = "Ceo",
                LastName = "Ceo",
                MiddleName = "Ceo",
                CorporateEmail = "ceo@tourmalinecore.com",
                PersonalEmail = "ceo@gmail.com",
                Phone = "88006663636",
                GitHub = "@ceo.github",
                GitLab = "@ceo.gitlab",
                HireDate = Instant.FromDateTimeUtc(ceoHiringAtUtc),
                IsBlankEmployee = true,
                IsCurrentEmployee = true,
                IsEmployedOfficially = true,
                IsSpecial = false
            },
            new
            {
                Id = 2L,
                FirstName = "Admin",
                LastName = "Admin",
                MiddleName = "Admin",
                CorporateEmail = "inner-circle-admin@tourmalinecore.com",
                PersonalEmail = "inner-circle-admin@gmail.com",
                HireDate = Instant.FromDateTimeUtc(ceoHiringAtUtc),
                IsBlankEmployee = true,
                IsCurrentEmployee = true,
                IsEmployedOfficially = true,
                IsSpecial = true
            }
        );
    }
}