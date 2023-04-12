namespace SalaryService.Application.Dtos
{
    public class DesiredFinancialMetricsDto
    {
        public decimal DesiredIncome { get; private set; }
        public decimal DesiredProfit { get; private set; }
        public decimal DesiredProfitability { get; private set; }

        public DesiredFinancialMetricsDto(decimal desiredIncome, decimal desiredProfit, decimal desiredProfitability)
        {
            DesiredIncome = desiredIncome;
            DesiredProfit = desiredProfit;
            DesiredProfitability = desiredProfitability;
        }
    }
}
