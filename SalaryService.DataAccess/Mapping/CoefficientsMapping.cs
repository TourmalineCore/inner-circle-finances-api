using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping;

public class CoefficientsMapping : IEntityTypeConfiguration<CoefficientOptions>
{
    public void Configure(EntityTypeBuilder<CoefficientOptions> builder)
    {
        builder.HasData(new CoefficientOptions(1, 0.15m, 15279, 0.13m, 49000));
    }
}
