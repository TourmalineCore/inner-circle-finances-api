using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;

namespace SalaryService.DataAccess.Mapping
{
    public class TotalsMapping : IEntityTypeConfiguration<TotalFinances>
    {
        
        public void Configure(EntityTypeBuilder<TotalFinances> builder)
        {
            builder.HasData(new TotalFinances(1, new Instant(), 0, 0));
        }
    }
}
