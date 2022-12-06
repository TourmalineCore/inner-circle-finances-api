using Microsoft.EntityFrameworkCore;
using NodaTime;
using SalaryService.Application.Services;
using SalaryService.DataAccess;
using SalaryService.Domain;
using Period = SalaryService.Domain.Common.Period;

namespace SalaryService.Application.Commands
{
    public partial class CalculateTotalExpensesCommand
    {
    }

    public class CalculateTotalExpensesCommandHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;
        private readonly FinanceAnalyticService _financeAnalyticService;
        private readonly IClock _clock;

        public CalculateTotalExpensesCommandHandler(EmployeeDbContext employeeDbContext, FinanceAnalyticService financeAnalyticService, IClock clock)
        {
            _employeeDbContext = employeeDbContext;
            _financeAnalyticService = financeAnalyticService;
            _clock = clock;
        }

        public async Task Handle()
        {
            var metrics = await _employeeDbContext.QueryableAsNoTracking<EmployeeFinancialMetrics>()
                .ToListAsync();

            var coefficients = await _employeeDbContext.Queryable<CoefficientOptions>().SingleAsync();

            var actualTotals = _financeAnalyticService.CalculateTotals(metrics, coefficients);

            var actualDesiredAndReserveFinances =
                _financeAnalyticService.CalculateDesiredAndReserve(metrics, coefficients, actualTotals.TotalExpense);

            var lastTotals = await _employeeDbContext.Queryable<TotalFinances>().SingleOrDefaultAsync();

            var lastDesiredAndReserveFinances = await _employeeDbContext.Queryable<EstimatedFinancialEfficiency>().SingleOrDefaultAsync();

            if (lastTotals == null && lastDesiredAndReserveFinances == null)
            {
                _employeeDbContext.Add(actualTotals);
                _employeeDbContext.Add(actualDesiredAndReserveFinances);
            }
            else
            {
                var historyTotals = new TotalFinancesHistory
                {
                    Period = new Period(lastTotals.ActualFromUtc, _clock.GetCurrentInstant()),
                    PayrollExpense = lastTotals.PayrollExpense,
                    TotalExpense = lastTotals.TotalExpense
                };
                _employeeDbContext.Add(historyTotals);
                lastTotals.Update(actualTotals.ActualFromUtc, actualTotals.PayrollExpense, actualTotals.TotalExpense);
                lastDesiredAndReserveFinances.Update(actualDesiredAndReserveFinances.DesiredEarnings,
                    actualDesiredAndReserveFinances.DesiredProfit,
                    actualDesiredAndReserveFinances.DesiredProfitability,
                    actualDesiredAndReserveFinances.ReserveForQuarter,
                    actualDesiredAndReserveFinances.ReserveForHalfYear,
                    actualDesiredAndReserveFinances.ReserveForYear);
                _employeeDbContext.Update(lastTotals);
                _employeeDbContext.Update(lastDesiredAndReserveFinances);
            }

            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
