using Microsoft.EntityFrameworkCore;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public class GetEmployeesForAnalyticsQuery
    {
        private readonly EmployeeDbContext _context;

        public GetEmployeesForAnalyticsQuery(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> HandleAsync()
        {
            return await _context
                .Employees
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .Where(x => x.IsCurrentEmployee && !x.IsBlankEmployee && !x.IsSpecial)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
