using NodaTime;
using SalaryService.Application.Queries;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FinanceAnalyticService
    {
        private readonly GetCoefficientsQueryHandler _getCoefficientsQueryHandler;
        private readonly IClock _clock;

        public FinanceAnalyticService(GetCoefficientsQueryHandler getCoefficientsQueryHandler,
            IClock clock)
        {
            _getCoefficientsQueryHandler = getCoefficientsQueryHandler;
            _clock = clock;
        }

        public TotalFinances CalculateTotals(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients)
        {
            var payrollExpense = metrics.Select(x => x.Expenses).Sum();
            var totalExpense = payrollExpense + coefficients.OfficeExpenses;
            return new TotalFinances(_clock.GetCurrentInstant(), payrollExpense, totalExpense);
        }

        public DesiredFinancesAndReserve CalculateDesiredAndReserve(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients, double totalExpense)
        {
            var desiredEarnings = metrics.Select(x => x.Earnings).Sum();
            var desiredProfit = metrics.Select(x => x.Profit).Sum() - coefficients.OfficeExpenses;
            var desiredProfitability = desiredProfit / desiredEarnings * 100;
            var reserveForQuarter = totalExpense * 3;
            var reserveForHalfYear = reserveForQuarter * 2;
            var reserveForYear = reserveForHalfYear * 2;

            return new DesiredFinancesAndReserve(desiredEarnings, desiredProfit, desiredProfitability,
                reserveForQuarter, reserveForHalfYear, reserveForYear);
        }

        public async Task<EmployeeFinancialMetrics> CalculateMetrics(double ratePerHour,
            double pay,
            double employmentTypeValue,
            double parkingCostPerMonth)
        {
            var calculateMetrics = new EmployeeFinancialMetrics(
                ratePerHour,
                pay,
                employmentTypeValue,
                parkingCostPerMonth);
            var coefficients = await _getCoefficientsQueryHandler.Handle();

            calculateMetrics.CalculateMetrics(coefficients.DistrictCoefficient,
                coefficients.MinimumWage,
                coefficients.IncomeTaxPercent,
                _clock.GetCurrentInstant());

            return calculateMetrics;
        }
    }
}
