using NodaTime;
using SalaryService.Application.Commands;
using SalaryService.Application.Queries;
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

            var totals = new TotalFinances(_clock.GetCurrentInstant());
            totals.CalculateTotals(metrics, coefficients);

            var estimatedFinancialEfficiency = new EstimatedFinancialEfficiency();
            estimatedFinancialEfficiency.CalculateEstimatedFinancialEfficiency(metrics, coefficients, totals.TotalExpense);

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
    }
}
