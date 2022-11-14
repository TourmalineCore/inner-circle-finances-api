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

        public Task CreateAsync(EmployeeFinancialMetrics metrics)
        {
            _employeeDbContext.AddAsync(metrics);
            return _employeeDbContext.SaveChangesAsync();
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

        public async Task<IEnumerable<EmployeeFinancialMetrics>> GetAllAsync()
        {
            return await _employeeDbContext
                .QueryableAsNoTracking<EmployeeFinancialMetrics>()
                .ToListAsync();
        }
    }
}
