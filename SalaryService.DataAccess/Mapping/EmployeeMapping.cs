using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping
{
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

            builder.OwnsOne(
                x => x.FinancialMetrics,
                fm =>
                {
                    fm.Property(x => x.Salary).HasColumnName(nameof(FinancialMetrics.Salary));
                    fm.Property(x => x.HourlyCostFact).HasColumnName(nameof(FinancialMetrics.HourlyCostFact));
                    fm.Property(x => x.HourlyCostHand).HasColumnName(nameof(FinancialMetrics.HourlyCostHand));
                    fm.Property(x => x.Earnings).HasColumnName(nameof(FinancialMetrics.Earnings));
                    fm.Property(x => x.DistrictCoefficient).HasColumnName(nameof(FinancialMetrics.DistrictCoefficient));
                    fm.Property(x => x.IncomeTaxContributions).HasColumnName(nameof(FinancialMetrics.IncomeTaxContributions));
                    fm.Property(x => x.PensionContributions).HasColumnName(nameof(FinancialMetrics.PensionContributions));
                    fm.Property(x => x.MedicalContributions).HasColumnName(nameof(FinancialMetrics.MedicalContributions));
                    fm.Property(x => x.SocialInsuranceContributions).HasColumnName(nameof(FinancialMetrics.SocialInsuranceContributions));
                    fm.Property(x => x.Expenses).HasColumnName(nameof(FinancialMetrics.Expenses));
                    fm.Property(x => x.Profit).HasColumnName(nameof(FinancialMetrics.Profit));
                    fm.Property(x => x.ProfitAbility).HasColumnName(nameof(FinancialMetrics.ProfitAbility));
                    fm.Property(x => x.GrossSalary).HasColumnName(nameof(FinancialMetrics.GrossSalary));
                    fm.Property(x => x.NetSalary).HasColumnName(nameof(FinancialMetrics.NetSalary));
                    fm.Property(x => x.RatePerHour).HasColumnName(nameof(FinancialMetrics.RatePerHour));
                    fm.Property(x => x.Pay).HasColumnName(nameof(FinancialMetrics.Pay));
                    fm.Property(x => x.Prepayment).HasColumnName(nameof(FinancialMetrics.Prepayment));
                    fm.Property(x => x.EmploymentType).HasColumnName(nameof(FinancialMetrics.EmploymentType));
                    fm.Property(x => x.ParkingCostPerMonth).HasColumnName(nameof(FinancialMetrics.ParkingCostPerMonth));
                    fm.Property(x => x.AccountingPerMonth).HasColumnName(nameof(FinancialMetrics.AccountingPerMonth));
                    fm.Property(x => x.IsEmployedOfficially).HasColumnName(nameof(FinancialMetrics.IsEmployedOfficially));
                    fm.ToTable("EmployeeFinancialMetrics");
                });

            builder.HasIndex(x => x.CorporateEmail).IsUnique();

            builder.HasData(new
            {
                Id = 50L,
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
                IsEmployedOfficially = true
            });
        }
    }
}
