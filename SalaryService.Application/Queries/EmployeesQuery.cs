using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class EmployeesQuery : IEmployeesQuery
{
    private readonly EmployeeDbContext _context;

    public EmployeesQuery(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync(long tenantId)
    {
        return await _context
            .Employees
            .Include(x => x.FinancialMetrics)
            .Where(x => !x.IsSpecial && x.TenantId == tenantId)
            .ToListAsync();
    }
}