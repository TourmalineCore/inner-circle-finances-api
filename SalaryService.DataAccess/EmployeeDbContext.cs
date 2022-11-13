using Microsoft.EntityFrameworkCore;

using SalaryService.Domain;

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
            
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(modelBuilder);

            

        }
    }
}
