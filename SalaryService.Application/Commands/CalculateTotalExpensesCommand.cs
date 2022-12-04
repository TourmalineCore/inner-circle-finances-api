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

            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
