
using Microsoft.EntityFrameworkCore;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Repositories
{
    public class EmployeeProfileInfoRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeProfileInfoRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public Task AddFinanceForPayrollAndMetrics(Employee employee, 
            long financeForPayrollId,
            long financialMetricsId)
        {
            employee.AddMetricsAndFinanceForpayroll(financeForPayrollId, financialMetricsId);

            return _employeeDbContext.SaveChangesAsync();
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _employeeDbContext.AddAsync(employee);
            await _employeeDbContext.SaveChangesAsync();

            return employee;
        }

        public Task<Employee> GetByIdAsync(long employeeId)
        {
            return _employeeDbContext
                    .Set<Employee>()
                    .Include(x => x.EmployeeFinanceForPayroll)
                    .Include(x => x.EmployeeFinancialMetrics)
                    .SingleAsync(x => x.Id == employeeId);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbContext
                .QueryableAsNoTracking<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .ToListAsync();
        }

        public Task UpdateAsync(Employee employee)
        {
            _employeeDbContext.Update(employee);
            return _employeeDbContext.SaveChangesAsync();
        }
    }
}
