using SalaryService.Application.Dtos;
using SalaryService.Application.Services;

namespace SalaryService.Application.Queries
{
    public partial class GetTotalFinancesQuery
    {
    }

    public class GetTotalFinancesQueryHandler
    {
        public async Task<TotalFinancesDto> Handle()
        {
            return new TotalFinancesDto(new ExpensesDto(TotalFinances.PayrollExpense, TotalFinances.OfficeExpense, TotalFinances.TotalExpense), 
                new DesiredFinancialMetricsDto(TotalFinances.DesiredIncome, TotalFinances.DesiredProfit, TotalFinances.DesiredProfitability),
                new ReserveFinanceDto(TotalFinances.ReserveForQuarter, TotalFinances.ReserveForHalfYear, TotalFinances.ReserveForYear));
        }
    }
}
