using SalaryService.Domain;

namespace SalaryService.DataAccess.Repositories
{
    public class MetricsHistoryRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public MetricsHistoryRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<long> CreateAsync(EmployeeFinancialMetricsHistory metrics)
        {
            await _employeeDbContext.AddAsync(metrics);
            await _employeeDbContext.SaveChangesAsync();

            return metrics.Id;
        }
    }
}
