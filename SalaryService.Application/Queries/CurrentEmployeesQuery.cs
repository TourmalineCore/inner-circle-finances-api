using Microsoft.EntityFrameworkCore;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class CurrentEmployeesQuery
{
    private readonly EmployeeDbContext _context;

    public CurrentEmployeesQuery(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetCurrentEmployeesAsync(long tenantId)
    {
        return await _context
            .Employees
            .Include(x => x.FinancialMetrics)
            .Where(x => !x.IsSpecial && x.IsCurrentEmployee && x.TenantId == tenantId)
            .ToListAsync();
    }
}