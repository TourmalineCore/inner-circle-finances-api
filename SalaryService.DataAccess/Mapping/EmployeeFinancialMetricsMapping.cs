using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping
{
    public class EmployeeFinancialMetricsMapping : IEntityTypeConfiguration<EmployeeFinancialMetrics>
    {
        public void Configure(EntityTypeBuilder<EmployeeFinancialMetrics> builder)
        {
            builder.HasOne(e => e.Employee);
        }
    }
}
