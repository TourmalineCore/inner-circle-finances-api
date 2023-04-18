using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.Domain;
using SalaryService.DataAccess;

namespace SalaryService.Application.Queries
{
    public partial class GetAnalyticListQuery
    {

    }

    public class GetAnalyticQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetAnalyticQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<IEnumerable<AnalyticDto>> HandleAsync()
        {
            var employees = await _employeeDbContext
                .QueryableAsNoTracking<Employee>()
                .Where(x => x.DeletedAtUtc == null)
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .ToListAsync();

            return employees.Select(employee => new AnalyticDto(employee));
        }
    }
}
