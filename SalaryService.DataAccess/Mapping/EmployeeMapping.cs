using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Mapping
{
    public class EmployeeMapping : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            var ceoHiringAtUtc = DateTime.SpecifyKind(new DateTime(2020, 01, 01, 0, 0, 0), DateTimeKind.Utc);

            builder.Property(x => x.CorporateEmail).IsRequired();

            builder.Property(x => x.Phone).IsRequired(false);
            builder.Property(x => x.GitHub).IsRequired(false);
            builder.Property(x => x.GitLab).IsRequired(false);

            builder.Property(x => x.IsBlankEmployee).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.IsCurrentEmployee).IsRequired().HasDefaultValue(true);

            builder.HasIndex(x => x.CorporateEmail).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.HasIndex(x => x.GitHub).IsUnique();
            builder.HasIndex(x => x.GitLab).IsUnique();

            builder.HasData(new
            {
                Id = 50L,
                FirstName = "Ceo",
                LastName = "Ceo",
                MiddleName = "Ceo",
                CorporateEmail = "ceo@tourmalinecore.com",
                PersonalEmail = "ceo@gmail.com",
                Phone = "88006663636",
                GitHub = "@ceo.github",
                GitLab = "@ceo.gitlab",
                HireDate = Instant.FromDateTimeUtc(ceoHiringAtUtc),
                IsBlankEmployee = false,
                IsCurrentEmployee = true
            });
        }
    }
}
