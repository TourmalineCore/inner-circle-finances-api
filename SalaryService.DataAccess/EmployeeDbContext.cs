using Microsoft.EntityFrameworkCore;
using SalaryService.Domain;

namespace SalaryService.DataAccess
{
    //Use next command in Package Manager Console to update Dev env DB
    //PM> $env:ASPNETCORE_ENVIRONMENT = 'Debug'; Update-Database
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeFinanceForPayroll> EmployeeFinanceForPayroll { get; set; }

        public DbSet<EmployeeFinancialMetrics> EmployeeFinancialMetrics { get; set; }

        public DbSet<EmployeeFinancialMetricsHistory> EmployeeFinancialMetricsHistory { get; set; }

        public DbSet<CoefficientOptions> CoefficientOptions { get; set; }

        public DbSet<TotalFinances> TotalFinances { get; set; }

        public DbSet<EstimatedFinancialEfficiency> EstimatedFinancialEfficiency { get; set; }

        public DbSet<TotalFinancesHistory> TotalFinancesHistory { get; set; }

        public DbSet<WorkingPlan> WorkingPlan { get; set; }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeFinancialMetricsHistory>().OwnsOne(history => history.Period,
                navigationBuilder =>
                {
                    navigationBuilder.Property(history => history.FromUtc)
                        .HasColumnName("FromUtc");
                    navigationBuilder.Property(history => history.ToUtc)
                        .HasColumnName("ToUtc");
                });

            modelBuilder.Entity<TotalFinancesHistory>().OwnsOne(history => history.Period,
                navigationBuilder =>
                {
                    navigationBuilder.Property(history => history.FromUtc)
                        .HasColumnName("FromUtc");
                    navigationBuilder.Property(history => history.ToUtc)
                        .HasColumnName("ToUtc");
                });

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
