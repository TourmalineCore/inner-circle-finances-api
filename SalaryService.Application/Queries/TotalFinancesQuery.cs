using Microsoft.EntityFrameworkCore;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class TotalFinancesQuery
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
