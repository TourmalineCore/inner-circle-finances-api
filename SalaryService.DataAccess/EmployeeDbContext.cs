using Microsoft.EntityFrameworkCore;

using SalaryService.Domain;
using System.Reflection.Metadata;

namespace SalaryService.DataAccess
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<BasicSalaryParameters> BasicSalaryParameters { get; set; }

        public DbSet<EmployeeFinancialMetrics> EmployeeFinancialMetrics { get; set; }

        public DbSet<EmployeeFinancialMetricsHistory> EmployeeFinancialMetricsHistory { get; set; }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeFinancialMetricsHistory>().OwnsOne(history => history.MetricsPeriod,
                navigationBuilder =>
                {
                    navigationBuilder.Property(history => history.StartedAtUtc)
                        .HasColumnName("StartedAtUtc");
                    navigationBuilder.Property(history => history.UpdatedAtUtc)
                        .HasColumnName("UpdatedAtUtc");
                });
            modelBuilder.Entity<EmployeeFinancialMetrics>().OwnsOne(history => history.MetricsPeriod,
                navigationBuilder =>
                {
                    navigationBuilder.Property(history => history.StartedAtUtc)
                        .HasColumnName("StartedAtUtc");
                    navigationBuilder.Property(history => history.UpdatedAtUtc)
                        .HasColumnName("UpdatedAtUtc");
                });
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
