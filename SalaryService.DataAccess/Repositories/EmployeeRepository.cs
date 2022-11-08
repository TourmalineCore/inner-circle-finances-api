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

        public async Task<long> CreateEmployee(Employee employee)
        {
            await _employeeDbContext.AddAsync(employee);
            await _employeeDbContext.SaveChangesAsync();
            return employee.Id;
        }

        public Task<Employee> GetEmployeeByIdAsync(long employeeId)
        {
            return _employeeDbContext
                    .Set<Employee>()
                    .SingleAsync(x => x.Id == employeeId);
        }

        public async void UpdateEmployee(Employee employee)
        {
            _employeeDbContext.Update(employee);
            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
