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

        public Task<EmployeeFinancialMetrics> GetByEmployeeId(long employeeId)
        {
            return _employeeDbContext
                    .Set<EmployeeFinancialMetrics>()
                    .SingleAsync(x => x.EmployeeId == employeeId);
        }
    }
}
