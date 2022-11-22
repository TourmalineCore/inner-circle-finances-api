using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping
{
    public class EmployeeFinanceForPayrollMapping : IEntityTypeConfiguration<EmployeeFinanceForPayroll>
    {
        public void Configure(EntityTypeBuilder<EmployeeFinanceForPayroll> builder)
        {
            builder.HasOne(e => e.Employee);
        }
    }
}
