using Microsoft.EntityFrameworkCore;
using NodaTime;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands;

public class RecalcFinancialMetricsCommand
{
    private readonly EmployeeDbContext _context;
    private readonly RecalcEstimatedFinancialEfficiencyCommand _recalcEstimatedFinancialEfficiencyCommand;
    private readonly RecalcTotalMetricsCommand _recalcTotalMetricsCommand;
    private readonly IFinancialMetricsQuery _financialMetricsQueryHandler;

    public RecalcFinancialMetricsCommand(
        EmployeeDbContext context, 
        RecalcEstimatedFinancialEfficiencyCommand recalcEstimatedFinancialEfficiencyCommand, 
        RecalcTotalMetricsCommand recalcTotalMetricsCommand, 
        IFinancialMetricsQuery financialMetricsQueryHandler)
    {
        _context = context;
        _recalcEstimatedFinancialEfficiencyCommand = recalcEstimatedFinancialEfficiencyCommand;
        _recalcTotalMetricsCommand = recalcTotalMetricsCommand;
        _financialMetricsQueryHandler = financialMetricsQueryHandler;
    }

    public async Task ExecuteAsync(CoefficientOptions coefficients, Instant utcNow)
    {
        var employeeFinancialMetrics = await _financialMetricsQueryHandler.HandleAsync();
        await _recalcTotalMetricsCommand.ExecuteAsync(employeeFinancialMetrics, coefficients, utcNow);

        var totals = await _context.Queryable<TotalFinances>().SingleAsync();
        await _recalcEstimatedFinancialEfficiencyCommand.ExecuteAsync(employeeFinancialMetrics, coefficients, totals.TotalExpense, utcNow);
    }
}

