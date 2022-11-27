namespace SalaryService.Application.Dtos
{
    public class DesiredFinancialMetricsDto
    {
        public double DesiredIncome { get; private set; }
        public double DesiredProfit { get; private set; }
        public double DesiredProfitability { get; private set; }

        public DesiredFinancialMetricsDto(double desiredIncome, double desiredProfit, double desiredProfitability)
        {
            DesiredIncome = desiredIncome;
            DesiredProfit = desiredProfit;
            DesiredProfitability = desiredProfitability;
        }
    }
}
