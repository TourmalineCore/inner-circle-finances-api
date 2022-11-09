using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mappings
{
    internal class SalaryPerformanceMapping : IEntityTypeConfiguration<EmployeeSalaryPerformance>
    {
        public void Configure(EntityTypeBuilder<EmployeeSalaryPerformance> builder)
        {
            
        }
    }
}
