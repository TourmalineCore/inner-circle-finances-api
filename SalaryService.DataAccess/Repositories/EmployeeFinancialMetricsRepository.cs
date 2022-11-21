using Microsoft.EntityFrameworkCore;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Repositories
{
    public class EmployeeFinancialMetricsRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeFinancialMetricsRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<long> CreateAsync(EmployeeFinancialMetrics metrics)
        {
            await _employeeDbContext.AddAsync(metrics);
            await _employeeDbContext.SaveChangesAsync();

            return metrics.Id;
        }

        public Task UpdateAsync(EmployeeFinancialMetrics metrics)
        {
            _employeeDbContext.Update(metrics);
            return _employeeDbContext.SaveChangesAsync();
        }

        public Task<EmployeeFinancialMetrics> GetByEmployeeId(long employeeId)
        {
            return _employeeDbContext
                    .Set<EmployeeFinancialMetrics>()
                    .SingleAsync(x => x.EmployeeId == employeeId);
        }

        public Task RemoveAsync(EmployeeFinancialMetrics metrics)
        {
            _employeeDbContext.Remove(metrics);

            return _employeeDbContext.SaveChangesAsync();
        }

        public Task<EmployeeFinancialMetrics> GetByIdAsync(long id)
        {
            return _employeeDbContext
                    .Set<EmployeeFinancialMetrics>()
                    .SingleAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<EmployeeFinancialMetrics>> GetAllAsync()
        {
            return await _employeeDbContext
                .QueryableAsNoTracking<EmployeeFinancialMetrics>()
                .ToListAsync();
        }
    }
}
