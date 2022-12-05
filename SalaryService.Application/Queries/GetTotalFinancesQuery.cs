using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public class GetTotalFinancesQuery
    {
    }
    public class GetTotalFinancesQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetTotalFinancesQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public async Task<TotalFinancesDto> Handle()
        {
            var totals = await _employeeDbContext.Queryable<TotalFinances>().SingleAsync();
            var coefficients = await _employeeDbContext.Queryable<CoefficientOptions>().SingleAsync();
            var desiredFinances = await _employeeDbContext.Queryable<DesiredFinancesAndReserve>().SingleAsync();

            return new TotalFinancesDto(new ExpensesDto(
                    Math.Round(totals.PayrollExpense, 2),
                    Math.Round(coefficients.OfficeExpenses, 2),
                    Math.Round(totals.TotalExpense, 2)),
                new DesiredFinancialMetricsDto(
                    Math.Round(desiredFinances.DesiredEarnings, 2),
                    Math.Round(desiredFinances.DesiredProfit, 2),
                    Math.Round(desiredFinances.DesiredProfitability, 2)),
                new ReserveFinanceDto(
                    Math.Round(desiredFinances.ReserveForQuarter, 2),
                    Math.Round(desiredFinances.ReserveForHalfYear, 2),
                    Math.Round(desiredFinances.ReserveForYear, 2)));
        }
    }
}
