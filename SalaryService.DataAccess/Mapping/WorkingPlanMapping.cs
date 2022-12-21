using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping
{
    public class WorkingPlanMapping : IEntityTypeConfiguration<WorkingPlan>
    {
        public void Configure(EntityTypeBuilder<WorkingPlan> builder)
        {
            builder.HasData(new WorkingPlan(1L,
                247, 
                223, 
                203, 
                16.91666667, 
                135.3333333)
            );
        }
    }
}
