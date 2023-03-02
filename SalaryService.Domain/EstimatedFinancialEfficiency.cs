namespace SalaryService.Domain
{
    public class EstimatedFinancialEfficiency : IIdentityEntity
    {
        public long Id { get; set; }

        public decimal DesiredEarnings { get; set; }

        public decimal DesiredProfit { get; set; }

        public decimal DesiredProfitability { get; set; }

        public decimal ReserveForQuarter { get; set; }

        public decimal ReserveForHalfYear { get; set; }

        public decimal ReserveForYear { get; set; }
        

        public void CalculateEstimatedFinancialEfficiency(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients, decimal totalExpense)
        {
            const decimal desiredProfitabilityWhenZeroDesiredEarnings = -100;

            DesiredEarnings = metrics.Select(x => x.Earnings).Sum();
            DesiredProfit = metrics.Select(x => x.Profit).Sum() - coefficients.OfficeExpenses;
            ReserveForQuarter = totalExpense * 3;
            ReserveForHalfYear = ReserveForQuarter * 2;
            ReserveForYear = ReserveForHalfYear * 2;
            DesiredProfitability = DesiredEarnings != 0
                ? DesiredProfit / DesiredEarnings * 100
                : desiredProfitabilityWhenZeroDesiredEarnings;
        }

        public void Update(decimal desiredEarnings, decimal desiredProfit, decimal desiredProfitability, decimal reserveForQuarter, decimal reserveForHalfYear, decimal reserveForYear)
        {
            DesiredEarnings = desiredEarnings;
            DesiredProfit = desiredProfit;
            DesiredProfitability = desiredProfitability;
            ReserveForQuarter = reserveForQuarter;
            ReserveForHalfYear = reserveForHalfYear;
            ReserveForYear = reserveForYear;
        }
    }
}
