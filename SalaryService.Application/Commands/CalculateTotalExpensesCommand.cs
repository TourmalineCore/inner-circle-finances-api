using Microsoft.EntityFrameworkCore;
using NodaTime;
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
        private readonly IClock _clock;

        public CalculateTotalExpensesCommandHandler(EmployeeDbContext employeeDbContext, IClock clock)
        {
            _employeeDbContext = employeeDbContext;
            _clock = clock;
        }

        public async Task Handle()
        {
            var metrics = await _employeeDbContext.QueryableAsNoTracking<EmployeeFinancialMetrics>()
                .ToListAsync();

            var coefficients = await _employeeDbContext.Set<CoefficientOptions>().SingleAsync();

            var totals = await CalculateTotals(metrics, coefficients);

            await CalculateDesiredAndReserve(metrics, coefficients, totals.TotalExpense);

            await _employeeDbContext.SaveChangesAsync();
        }

        private async Task<TotalFinances> CalculateTotals(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients)
        {
            var payrollExpense = metrics.Select(x => x.Expenses).Sum();
            var totalExpense = payrollExpense + coefficients.OfficeExpenses;
            var totals = await _employeeDbContext.Set<TotalFinances>().SingleAsync();
            var historyTotals = new TotalFinancesHistory
            {
                Period = new Period(totals.ActualFromUtc, _clock.GetCurrentInstant()),
                PayrollExpense = payrollExpense,
                TotalExpense = totalExpense
            };
            _employeeDbContext.Add(historyTotals);
            totals.Update(_clock.GetCurrentInstant(), payrollExpense, totalExpense);
            _employeeDbContext.Update(totals);
            return totals;
        }

        private async Task CalculateDesiredAndReserve(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients, double totalExpense)
        {
            var desiredFinancesAndReserve = await _employeeDbContext.Set<DesiredFinancesAndReserve>().SingleAsync();
            var desiredEarnings = metrics.Select(x => x.Earnings).Sum();
            var desiredProfit = metrics.Select(x => x.Profit).Sum() - coefficients.OfficeExpenses;
            var reserveForQuarter = totalExpense * 3;
            var reserveForHalfYear = reserveForQuarter * 2;
            var reserveForYear = reserveForHalfYear * 2;
            desiredFinancesAndReserve.Update(desiredEarnings,
                desiredProfit,
                (desiredProfit / desiredEarnings) * 100,
                reserveForQuarter,
                reserveForHalfYear,
                reserveForYear
            );
            _employeeDbContext.Update(desiredFinancesAndReserve);
        }
    }
}
