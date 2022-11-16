using Microsoft.EntityFrameworkCore;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Repositories
{
    public class EmployeeFinanceForPayrollRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public EmployeeFinanceForPayrollRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<long> CreateAsync(EmployeeFinanceForPayroll employeeFinanceForPayroll)
        {
            await _employeeDbContext.AddAsync(employeeFinanceForPayroll);
            await _employeeDbContext.SaveChangesAsync();
            return employeeFinanceForPayroll.Id;
        }

        public Task UpdateAsync(EmployeeFinanceForPayroll employeeFinanceForPayroll)
        {
            _employeeDbContext.Update(employeeFinanceForPayroll);
            return _employeeDbContext.SaveChangesAsync();
        }

        public Task<EmployeeFinanceForPayroll> GetByEmployeeIdAsync(long employeeId)
        {
            return _employeeDbContext
                    .Set<EmployeeFinanceForPayroll>()
                    .SingleAsync(x => x.EmployeeId == employeeId);
        }
    }
}
