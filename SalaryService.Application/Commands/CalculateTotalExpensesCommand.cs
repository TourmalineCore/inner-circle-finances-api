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

        public async Task<TotalFinancesDto> Handle()
        {
            var expenses = await CalculateTotalExpenses();

            var desiredFinances = await CalculateDesiredFinance();
            return new TotalFinancesDto(expenses, desiredFinances);
        }

        private async Task<ExpensesDto> CalculateTotalExpenses()
        {
            var metrics = await _employeeDbContext.QueryableAsNoTracking<EmployeeFinancialMetrics>()
                .ToListAsync();
            return new ExpensesDto(Math.Round(metrics.Select(x => x.Expenses).Sum(), 2), _coefficientOptions.OfficeExpenses);
        }

        private async Task<DesiredFinancialMetricsDto> CalculateDesiredFinance()
        {
            var metrics = await _employeeDbContext.QueryableAsNoTracking<EmployeeFinancialMetrics>()
                .ToListAsync();
            var desiredIncome = metrics.Select(x => x.Earnings).Sum();
            var desiredProfit = desiredIncome - _coefficientOptions.OfficeExpenses;
            return new DesiredFinancialMetricsDto(Math.Round(desiredIncome, 2), Math.Round(desiredProfit, 2), Math.Round((desiredProfit / desiredIncome) * 100, 2));
        }
    }
}
