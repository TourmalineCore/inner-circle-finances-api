using Microsoft.EntityFrameworkCore;
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

        public Task<EmployeeFinancialMetricsHistory> GetByIdAsync(long id)
        {
            return _employeeDbContext
                    .Set<EmployeeFinancialMetricsHistory>()
                    .SingleAsync(x => x.Id == id);
        }

        public Task UpdateAsync(EmployeeFinancialMetricsHistory metrics)
        {
            _employeeDbContext.Update(metrics);
            return _employeeDbContext.SaveChangesAsync();
        }
    }
}
