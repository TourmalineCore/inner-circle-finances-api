using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class FinancialMetricsQuery : IFinancialMetricsQuery
{
    private readonly EmployeeDbContext _context;

    public FinancialMetricsQuery(EmployeeDbContext employeeDbContext)
    {
        _context = employeeDbContext;
    }

    public async Task<IEnumerable<FinancialMetrics>> HandleAsync()
    {
        return await _context
            .Employees
            .Include(x => x.FinancialMetrics)
            .Where(x => x.FinancialMetrics != null)
            .Select(x => x.FinancialMetrics)
            .AsNoTracking()
            .ToListAsync();
    }
}