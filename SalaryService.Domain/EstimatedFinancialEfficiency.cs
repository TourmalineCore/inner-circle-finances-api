namespace SalaryService.Domain
{
    public class EstimatedFinancialEfficiency : IIdentityEntity
    {
        public long Id { get; set; }

        public double DesiredEarnings { get; set; }

        public double DesiredProfit { get; set; }

        public double DesiredProfitability { get; set; }

        public double ReserveForQuarter { get; set; }

        public double ReserveForHalfYear { get; set; }

        public double ReserveForYear { get; set; }
        

        public void CalculateEstimatedFinancialEfficiency(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients, double totalExpense)
        {
            const double desiredProfitabilityWhenZeroDesiredEarnings = -100;

            DesiredEarnings = metrics.Select(x => x.Earnings).Sum();
            DesiredProfit = metrics.Select(x => x.Profit).Sum() - coefficients.OfficeExpenses;
            ReserveForQuarter = totalExpense * 3;
            ReserveForHalfYear = ReserveForQuarter * 2;
            ReserveForYear = ReserveForHalfYear * 2;
            DesiredProfitability = DesiredEarnings != 0
                ? DesiredProfit / DesiredEarnings * 100
                : desiredProfitabilityWhenZeroDesiredEarnings;
        }

        public void Update(double desiredEarnings, double desiredProfit, double desiredProfitability, double reserveForQuarter, double reserveForHalfYear, double reserveForYear)
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
