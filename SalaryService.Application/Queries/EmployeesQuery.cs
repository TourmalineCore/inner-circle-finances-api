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
        var employeesRequest = _context.QueryableAsNoTracking<Employee>();

        if (includeFinanceInfo)
        {
            employeesRequest = employeesRequest.Include(x => x.FinancialMetrics);
        }

        return await employeesRequest.ToListAsync();
    }
}
