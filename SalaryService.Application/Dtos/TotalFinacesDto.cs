namespace SalaryService.Application.Dtos
{
    public class TotalFinancesDto
    {
        public ExpensesDto TotalExpenses { get; private set; }

        public DesiredFinancialMetricsDto DesiredFinancialMetrics { get; private set; }

        public ReserveFinanceDto ReserveFinance { get; private set; }

        public TotalFinancesDto(ExpensesDto totalExpenses, DesiredFinancialMetricsDto desiredFinancialMetrics, ReserveFinanceDto reserveFinance)
        {
            TotalExpenses = totalExpenses;
            DesiredFinancialMetrics = desiredFinancialMetrics;
            ReserveFinance = reserveFinance;
        }
    }
}
