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
            builder.Property(x => x.CorporateEmail).IsRequired();
            builder.Property(x => x.PersonalEmail).IsRequired();

            builder.HasIndex(x => x.CorporateEmail).IsUnique();
            builder.HasIndex(x => x.PersonalEmail).IsUnique();
            builder.HasIndex(x => x.Phone).IsUnique();
            builder.HasIndex(x => x.GitHub).IsUnique();
            builder.HasIndex(x => x.GitLab).IsUnique();

            builder.HasData(new Employee("Ceo", "Ceo", "Ceo", "ceo@tourmalinecore.com", "ceo@gmail.com", "88006663636",
                "@ceo.github", "@ceo.gitlab", new Instant()) { Id = 50, AccountId = 1});
        }
    }
}
