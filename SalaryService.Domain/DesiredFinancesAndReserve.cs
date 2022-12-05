namespace SalaryService.Domain
{
    public class DesiredFinancesAndReserve : IIdentityEntity
    {
        public long Id { get; set; }

        public double DesiredEarnings { get; set; }

        public double DesiredProfit { get; set; }

        public double DesiredProfitability { get; set; }

        public double ReserveForQuarter { get; set; }

        public double ReserveForHalfYear { get; set; }

        public double ReserveForYear { get; set; }

        public DesiredFinancesAndReserve(double desiredEarnings, double desiredProfit, double desiredProfitability, double reserveForQuarter, double reserveForHalfYear, double reserveForYear)
        {
            DesiredEarnings = desiredEarnings;
            DesiredProfit = desiredProfit;
            DesiredProfitability = desiredProfitability;
            ReserveForQuarter = reserveForQuarter;
            ReserveForHalfYear = reserveForHalfYear;
            ReserveForYear = reserveForYear;
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
