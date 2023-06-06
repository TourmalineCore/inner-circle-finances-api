using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class TotalFinancesQuery : ITotalFinancesQuery
{
    private readonly EmployeeDbContext _context;

    public TotalFinancesQuery(EmployeeDbContext context)
    {
        _context = context;
    }

    public async Task<TotalFinances?> GetTotalFinancesAsync()
    {
        return await _context.QueryableAsNoTracking<TotalFinances>().SingleOrDefaultAsync();
    }
}
