using NodaTime;
using SalaryService.Application.Queries;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FinanceAnalyticService
    {
        private readonly GetCoefficientsQueryHandler _getCoefficientsQueryHandler;
        private readonly GetFinancialMetricsQueryHandler _getFinancialMetricsQueryHandler;
        private readonly GetWorkingPlanQueryHandler _getWorkingPlanQueryHandler;
        private readonly IClock _clock;
        private readonly List<decimal> _availableEmploymentTypes = new() { 0.5m, 1 };

        public FinanceAnalyticService(GetCoefficientsQueryHandler getCoefficientsQueryHandler,
            GetFinancialMetricsQueryHandler getFinancialMetricsQueryHandler,
            GetWorkingPlanQueryHandler getWorkingPlanQueryHandler,
            IClock clock)
        {
            _getCoefficientsQueryHandler = getCoefficientsQueryHandler;
            _getFinancialMetricsQueryHandler = getFinancialMetricsQueryHandler;
            _getWorkingPlanQueryHandler = getWorkingPlanQueryHandler;
            _clock = clock;
        }

        public async Task<TotalFinances> CalculateTotalFinances()
        {
            var metrics = await _getFinancialMetricsQueryHandler.HandleAsync();
            var coefficients = await _getCoefficientsQueryHandler.HandleAsync();

            var totals = new TotalFinances(_clock.GetCurrentInstant());
            totals.CalculateTotals(metrics, coefficients);
            return totals;
        }

        public async Task<EstimatedFinancialEfficiency> CalculateEstimatedFinancialEfficiency(decimal totalExpenses)
        {
            var metrics = await _getFinancialMetricsQueryHandler.HandleAsync();
            var coefficients = await _getCoefficientsQueryHandler.HandleAsync();

            var estimatedFinancialEfficiency = new EstimatedFinancialEfficiency();
            estimatedFinancialEfficiency.CalculateEstimatedFinancialEfficiency(metrics, coefficients, totalExpenses);
            return estimatedFinancialEfficiency;
        }

        public async Task<EmployeeFinancialMetrics> CalculateMetrics(decimal ratePerHour,
            decimal pay,
            decimal employmentTypeValue,
            decimal parkingCostPerMonth,
            long? employeeId = null)
        {
            //TODO: #861m9k5f6: make refactoring of using employee finances for payroll in an employee model

            if (!_availableEmploymentTypes.Contains(employmentTypeValue))
            {
                throw new ArgumentException("Employment type can accept only the following values: 0.5, 1");
            }

            var employeeFinancialMetrics = new EmployeeFinancialMetrics(
                ratePerHour,
                pay,
                employmentTypeValue,
                parkingCostPerMonth);
            var coefficients = await _getCoefficientsQueryHandler.HandleAsync();
            var workingPlan = await _getWorkingPlanQueryHandler.HandleAsync();

            employeeFinancialMetrics.CalculateMetrics(coefficients.DistrictCoefficient,
                coefficients.MinimumWage,
                coefficients.IncomeTaxPercent,
                workingPlan.WorkingHoursInMonth,
                _clock.GetCurrentInstant());

            if (employeeId.HasValue)
            {
                employeeFinancialMetrics.EmployeeId = employeeId.Value;
            }

            return employeeFinancialMetrics;
        }
    }
}
