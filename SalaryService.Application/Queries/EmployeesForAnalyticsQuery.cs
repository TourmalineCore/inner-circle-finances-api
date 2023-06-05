using Microsoft.EntityFrameworkCore;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class EmployeesForAnalyticsQuery
{
    private readonly EmployeeDbContext _context;

    public EmployeesForAnalyticsQuery(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesForAnalyticsAsync()
    {
        return await _context
            .Employees
            .Where(x => x.IsCurrentEmployee && !x.IsBlankEmployee && !x.IsSpecial)
            .AsNoTracking()
            .ToListAsync();
    }
}