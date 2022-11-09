using Microsoft.EntityFrameworkCore;
using SalaryService.Domain;

namespace SalaryService.DataAccess.Repositories
{
    public class SalaryPerformanceRepository
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public SalaryPerformanceRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<long> CreateEmployeeSalaryPerformance(EmployeeSalaryPerformance employeeSalaryPerformance)
        {
            await _employeeDbContext.AddAsync(employeeSalaryPerformance);
            await _employeeDbContext.SaveChangesAsync();
            return employeeSalaryPerformance.EmployeeId;
        }

        public async Task UpdateEmployeeSalaryPerformance(EmployeeSalaryPerformance employeeSalaryPerformance)
        {
            _employeeDbContext.Update(employeeSalaryPerformance);
            await _employeeDbContext.SaveChangesAsync();
        }

        public Task<EmployeeSalaryPerformance> GetSalaryPerformanceByEmployeeIdAsync(long employeeId)
        {
            return _employeeDbContext
                    .Set<EmployeeSalaryPerformance>()
                    .SingleAsync(x => x.EmployeeId == employeeId);
        }
    }
}
