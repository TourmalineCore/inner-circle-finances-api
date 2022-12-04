using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping
{
    public class DesiredFinancesAndReserveMapping : IEntityTypeConfiguration<DesiredFinancesAndReserve>
    {
        public void Configure(EntityTypeBuilder<DesiredFinancesAndReserve> builder)
        {
            builder.HasData(new DesiredFinancesAndReserve(1, 0, 0, 0, 0, 0, 0));
        }
    }
}
