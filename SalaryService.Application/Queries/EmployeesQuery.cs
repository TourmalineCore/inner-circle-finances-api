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

    public async Task<IEnumerable<Employee>> GetEmployeesAsync(bool includeFinanceInfo = false)
    {
        var employeesQuery = _context
            .QueryableAsNoTracking<Employee>()
            .Where(x => !x.IsSpecial);

        if (includeFinanceInfo) employeesQuery = employeesQuery.Include(x => x.FinancialMetrics);

        return await employeesQuery.ToListAsync();
    }
}