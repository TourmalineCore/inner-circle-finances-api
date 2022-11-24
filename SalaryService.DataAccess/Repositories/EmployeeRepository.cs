using Microsoft.EntityFrameworkCore;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Repositories
{
    public class EmployeeRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        
        public Task CreateAsync(Employee employee, EmployeeFinanceForPayroll financeForPayroll, EmployeeFinancialMetrics metrics)
        {
            using(var transaction = _employeeDbContext.Database.BeginTransaction())
            {
                try
                {
                    employee.EmployeeFinanceForPayroll = financeForPayroll;
                    employee.EmployeeFinancialMetrics = metrics;
                    financeForPayroll.Employee = employee;
                    metrics.Employee = employee;
                    _employeeDbContext.Add(employee);
                    _employeeDbContext.Add(financeForPayroll);
                    _employeeDbContext.Add(metrics);
                    _employeeDbContext.SaveChanges();
                    transaction.Commit();

                } catch(Exception exception)
                {
                    transaction.Rollback();
                }
                
            }
            return _employeeDbContext.SaveChangesAsync();
        }

        public Task<Employee> GetByIdAsync(long employeeId)
        {
            return _employeeDbContext
                    .Set<Employee>()
                    .Include(x => x.EmployeeFinanceForPayroll)
                    .Include(x => x.EmployeeFinancialMetrics)
                    .SingleAsync(x => x.Id == employeeId && x.DeletedAtUtc == null);
        }

        public Task<Employee> GetCEOAsync()
        {
            return _employeeDbContext
                    .Set<Employee>()
                    .Include(x => x.EmployeeFinanceForPayroll)
                    .Include(x => x.EmployeeFinancialMetrics)
                    .SingleAsync(x => x.DeletedAtUtc == null && x.AccountId == 1);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbContext
                .QueryableAsNoTracking<Employee>()
                .Where(x => x.DeletedAtUtc == null && x.AccountId != 1)
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .ToListAsync();
        }

        public Task UpdateAsync(Employee employee)
        {
            _employeeDbContext.Update(employee);
            return _employeeDbContext.SaveChangesAsync();
        }

        public Task DeleteEmployeeAsync(Employee employee, 
            EmployeeFinanceForPayroll employeeFinanceForPayroll, 
            EmployeeFinancialMetrics employeeFinancialMetrics,
            EmployeeFinancialMetricsHistory employeeFinancialMetricsHistory)
        {
            using(var transaction = _employeeDbContext.Database.BeginTransaction())
            {
                try
                {
                    _employeeDbContext.Add(employeeFinancialMetricsHistory);
                    _employeeDbContext.Remove(employeeFinancialMetrics);
                    _employeeDbContext.Remove(employeeFinanceForPayroll);
                    _employeeDbContext.Update(employee);
                    transaction.Commit();

                } catch(Exception exception)
                {
                    transaction.Rollback();
                }
            }
           
            return _employeeDbContext.SaveChangesAsync();
        }
    }
}
