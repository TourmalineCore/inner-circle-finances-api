using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping;

public class CompensationMapping : IEntityTypeConfiguration<Compensation>
{
    public void Configure(EntityTypeBuilder<Compensation> builder)
    {
        var instantConverter =
        new ValueConverter<Instant, DateTime>(v =>
         v.ToDateTimeUtc(),
         v => Instant.FromDateTimeUtc(v));

        builder.Property(e => e.DateCreateCompensation)
            .HasConversion(instantConverter);
        builder.Property(e => e.DateCompensation)
            .HasConversion(instantConverter);
    }
}
