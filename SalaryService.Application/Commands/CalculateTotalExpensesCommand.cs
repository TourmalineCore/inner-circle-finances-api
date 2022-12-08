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

        public TotalFinances Handle(TotalFinances totalFinances)
        {
            var lastTotals = _employeeDbContext.Queryable<TotalFinances>().SingleOrDefaultAsync().Result;

            if (lastTotals == null)
            {
                _employeeDbContext.Add(totalFinances);
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
                _employeeDbContext.Update(lastTotals);
            }
            
            _employeeDbContext.SaveChanges();
            return lastTotals;
        }
    }
}
