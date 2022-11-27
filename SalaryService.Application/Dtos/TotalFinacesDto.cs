namespace SalaryService.Application.Dtos
{
    public class TotalFinancesDto
    {
        public ExpensesDto TotalExpenses { get; private set; }
        public DesiredFinancialMetricsDto DesiredMetrics { get; private set; }

        public ReserveFinanceDto ReserveFinance { get; private set; }

        public TotalFinancesDto(ExpensesDto totalExpenses, DesiredFinancialMetricsDto desiredMetrics, ReserveFinanceDto reserveFinance)
        {
            TotalExpenses = totalExpenses;
            DesiredMetrics = desiredMetrics;
            ReserveFinance = reserveFinance;
        }
    }
}
