using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Queries;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FinanceAnalyticService
    {
        private readonly GetCoefficientsQueryHandler _getCoefficientsQueryHandler;
        private readonly GetFinancialMetricsQueryHandler _getFinancialMetricsQueryHandler;
        private readonly CalculateTotalExpensesCommandHandler _calculateTotalExpensesCommandHandler;
        private readonly IClock _clock;

        public FinanceAnalyticService(GetCoefficientsQueryHandler getCoefficientsQueryHandler,
            GetFinancialMetricsQueryHandler getFinancialMetricsQueryHandler,
            CalculateTotalExpensesCommandHandler calculateTotalExpensesCommandHandler,
            IClock clock)
        {
            _getCoefficientsQueryHandler = getCoefficientsQueryHandler;
            _getFinancialMetricsQueryHandler = getFinancialMetricsQueryHandler;
            _calculateTotalExpensesCommandHandler = calculateTotalExpensesCommandHandler;
            _clock = clock;
        }

        public async Task CalculateTotalAndEstimatedFinancialEfficiency()
        {
            var metrics = await _getFinancialMetricsQueryHandler.Handle();

            var coefficients = await _getCoefficientsQueryHandler.Handle();

            var totals = CalculateTotals(metrics, coefficients);

            var estimatedFinancialEfficiency =
                CalculateEstimatedFinancialEfficiency(metrics, coefficients, totals.TotalExpense);

            await _calculateTotalExpensesCommandHandler.Handle(totals, estimatedFinancialEfficiency);
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

        private TotalFinances CalculateTotals(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients)
        {
            var payrollExpense = metrics.Select(x => x.Expenses).Sum();
            var totalExpense = payrollExpense + coefficients.OfficeExpenses;
            return new TotalFinances(_clock.GetCurrentInstant(), payrollExpense, totalExpense);
        }

        private EstimatedFinancialEfficiency CalculateEstimatedFinancialEfficiency(IEnumerable<EmployeeFinancialMetrics> metrics, CoefficientOptions coefficients, double totalExpense)
        {
            var desiredEarnings = metrics.Select(x => x.Earnings).Sum();
            var desiredProfit = metrics.Select(x => x.Profit).Sum() - coefficients.OfficeExpenses;
            var desiredProfitability = desiredProfit / desiredEarnings * 100;
            var reserveForQuarter = totalExpense * 3;
            var reserveForHalfYear = reserveForQuarter * 2;
            var reserveForYear = reserveForHalfYear * 2;

            return new EstimatedFinancialEfficiency(desiredEarnings, desiredProfit, desiredProfitability,
                reserveForQuarter, reserveForHalfYear, reserveForYear);
        }
    }
}
