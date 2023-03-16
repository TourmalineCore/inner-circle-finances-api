using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public partial class GetColleaguesQuery
    {
    }

    public class GetEmployeesQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetEmployeesQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<IEnumerable<EmployeeDto>> HandleAsync(bool includeFinanceInfo)
        {
            var employeesRequest = _employeeDbContext.QueryableAsNoTracking<Employee>();

            if (includeFinanceInfo)
            {
                employeesRequest = employeesRequest
                    .Include(x => x.EmployeeFinanceForPayroll)
                    .Include(x => x.EmployeeFinancialMetrics);
            }

            var employees = await employeesRequest.ToListAsync();

            return employees.Select(employee => new EmployeeDto(employee));
        }
    }
}
