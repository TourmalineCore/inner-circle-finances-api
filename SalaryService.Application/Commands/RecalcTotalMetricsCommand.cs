using Microsoft.EntityFrameworkCore;
using NodaTime;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands;

public class RecalcTotalMetricsCommand
{
    private readonly EmployeeDbContext _context;

    public RecalcTotalMetricsCommand(EmployeeDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<TotalFinances> ExecuteAsync(IEnumerable<EmployeeFinancialMetrics> employeeFinancialMetrics, CoefficientOptions coefficients, Instant utcNow)
    {
        var currentTotalFinances = await _context.Queryable<TotalFinances>().SingleOrDefaultAsync();
        var newTotalFinances = new TotalFinances(employeeFinancialMetrics, coefficients, utcNow);

        if (currentTotalFinances == null)
        {
            await _context.AddAsync(newTotalFinances);
        }
        else
        {
            await _context.AddAsync(new TotalFinancesHistory(currentTotalFinances, utcNow));
            currentTotalFinances.Update(newTotalFinances);
            _context.Update(currentTotalFinances);
        }

        await _context.SaveChangesAsync();
        return newTotalFinances;
    }
}
