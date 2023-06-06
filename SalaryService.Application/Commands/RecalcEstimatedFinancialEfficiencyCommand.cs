using Microsoft.EntityFrameworkCore;
using NodaTime;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands;

public class RecalcEstimatedFinancialEfficiencyCommand
{
    private readonly EmployeeDbContext _context;

    public RecalcEstimatedFinancialEfficiencyCommand(EmployeeDbContext employeeDbContext)
    {
        _context = employeeDbContext;
    }

    public async Task ExecuteAsync(IEnumerable<EmployeeFinancialMetrics> employeeFinancialMetrics, CoefficientOptions coefficients, decimal totalExpenses, Instant utcNow)
    {
        var currentEstimatedFinancialEfficiency = await _context.Queryable<EstimatedFinancialEfficiency>().SingleOrDefaultAsync();
        var newEstimatedFinancialEfficiency = new EstimatedFinancialEfficiency(employeeFinancialMetrics, coefficients, totalExpenses, utcNow);

        if (currentEstimatedFinancialEfficiency == null)
        {
            await _context.AddAsync(newEstimatedFinancialEfficiency);
        }
        else
        {
            currentEstimatedFinancialEfficiency.Update(newEstimatedFinancialEfficiency);
            _context.Update(currentEstimatedFinancialEfficiency);
        }

        await _context.SaveChangesAsync();
    }
}
