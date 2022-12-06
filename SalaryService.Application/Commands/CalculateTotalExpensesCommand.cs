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

        public CalculateTotalExpensesCommandHandler(EmployeeDbContext employeeDbContext, 
            IClock clock)
        {
            _employeeDbContext = employeeDbContext;
            _clock = clock;
        }

        public async Task Handle(TotalFinances totalFinances, EstimatedFinancialEfficiency estimatedFinancialEfficiency)
        {
            var lastTotals = await _employeeDbContext.Queryable<TotalFinances>().SingleOrDefaultAsync();

            var lastEstimatedFinancialEfficiency = await _employeeDbContext.Queryable<EstimatedFinancialEfficiency>().SingleOrDefaultAsync();

            if (lastTotals == null && lastEstimatedFinancialEfficiency == null)
            {
                _employeeDbContext.Add(totalFinances);
                _employeeDbContext.Add(estimatedFinancialEfficiency);
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
                lastTotals.Update(totalFinances.ActualFromUtc, totalFinances.PayrollExpense, totalFinances.TotalExpense);
                lastEstimatedFinancialEfficiency.Update(estimatedFinancialEfficiency.DesiredEarnings,
                    estimatedFinancialEfficiency.DesiredProfit,
                    estimatedFinancialEfficiency.DesiredProfitability,
                    estimatedFinancialEfficiency.ReserveForQuarter,
                    estimatedFinancialEfficiency.ReserveForHalfYear,
                    estimatedFinancialEfficiency.ReserveForYear);
                _employeeDbContext.Update(lastTotals);
                _employeeDbContext.Update(lastEstimatedFinancialEfficiency);
            }

            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
