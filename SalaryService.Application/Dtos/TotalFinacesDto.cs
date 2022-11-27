namespace SalaryService.Application.Dtos
{
    public class TotalFinancesDto
    {
        public ExpensesDto TotalExpenses { get; private set; }
        public DesiredFinancialMetricsDto DesiredMetrics { get; private set; }

        public TotalFinancesDto(ExpensesDto totalExpenses, DesiredFinancialMetricsDto desiredMetrics)
        {
            TotalExpenses = totalExpenses;
            DesiredMetrics = desiredMetrics;
        }
    }
}
