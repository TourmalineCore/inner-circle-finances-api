using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(e => e.EmployeeFinanceForPayroll);
            builder.HasOne(e => e.EmployeeFinancialMetrics);
            builder.Property(x => x.CorporateEmail).IsRequired();
            builder.HasIndex(x => x.CorporateEmail).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.HasIndex(x => x.Telegram).IsUnique();
            builder.HasIndex(x => x.Skype).IsUnique();
            builder.HasIndex(x => x.PersonalEmail).IsUnique();
        }
    }
}
