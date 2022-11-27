using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SalaryService.Application.Dtos;
using SalaryService.Application.Services;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CalculateTotalExpensesCommand
    {
    }

    public class CalculateTotalExpensesCommandHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;
        private readonly CoefficientOptions _coefficientOptions;

        public CalculateTotalExpensesCommandHandler(EmployeeDbContext employeeDbContext, 
            IOptions<CoefficientOptions> coefficientOptions)
        {
            _employeeDbContext = employeeDbContext;
            _coefficientOptions = coefficientOptions.Value;
        }

        public async Task Handle()
        {
            await CalculateTotalExpenses();
            await CalculateDesiredFinance();
            await CalculateReserveFinance();
        }

        private async Task CalculateTotalExpenses()
        {
            var metrics = await _employeeDbContext.QueryableAsNoTracking<EmployeeFinancialMetrics>()
                .ToListAsync();
            TotalFinances.PayrollExpense = metrics.Select(x => x.Expenses).Sum();
            TotalFinances.OfficeExpense = _coefficientOptions.OfficeExpenses;
            TotalFinances.TotalExpense = TotalFinances.PayrollExpense + TotalFinances.OfficeExpense;
        }

        private async Task CalculateDesiredFinance()
        {
            var metrics = await _employeeDbContext.QueryableAsNoTracking<EmployeeFinancialMetrics>()
                .ToListAsync();
            var desiredIncome = metrics.Select(x => x.Earnings).Sum();
            var desiredProfit = desiredIncome - _coefficientOptions.OfficeExpenses;
            TotalFinances.DesiredIncome = Math.Round(desiredIncome, 2);
            TotalFinances.DesiredProfit = Math.Round(desiredProfit, 2);
            TotalFinances.DesiredProfitability = Math.Round((desiredProfit / desiredIncome) * 100, 2);
        }
        private async Task CalculateReserveFinance()
        {
            TotalFinances.ReserveForQuarter = Math.Round(TotalFinances.TotalExpense * 3, 2);
            TotalFinances.ReserveForHalfYear = Math.Round(TotalFinances.ReserveForQuarter * 2, 2);
            TotalFinances.ReserveForYear = Math.Round(TotalFinances.ReserveForHalfYear * 2, 2);
        }
    }
}
