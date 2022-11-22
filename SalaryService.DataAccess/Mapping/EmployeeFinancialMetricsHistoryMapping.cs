using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping
{
    public class EmployeeFinancialMetricsHistoryMapping : IEntityTypeConfiguration<EmployeeFinancialMetricsHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeeFinancialMetricsHistory> builder)
        {
            builder.HasOne(e => e.Employee);
        }
    }
}
