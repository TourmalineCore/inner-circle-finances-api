using NodaTime;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries.Contracts;
using SalaryService.Domain;

namespace SalaryService.Application.Services
{
    public class FinanceAnalyticService
    {
        private readonly IGetCoefficientsQueryHandler _getCoefficientsQueryHandler;
        private readonly IGetFinancialMetricsQueryHandler _getFinancialMetricsQueryHandler;
        private readonly IGetWorkingPlanQueryHandler _getWorkingPlanQueryHandler;
        private readonly IClock _clock;
        private readonly List<decimal> _availableEmploymentTypes = new() { 0.5m, 1 };

        public FinanceAnalyticService(IGetCoefficientsQueryHandler getCoefficientsQueryHandler,
            IGetFinancialMetricsQueryHandler getFinancialMetricsQueryHandler,
            IGetWorkingPlanQueryHandler getWorkingPlanQueryHandler,
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

        public async Task<AnalyticsMetricChanges> CalculateAnalyticsMetricChangesAsync(IEnumerable<MetricsRowDto> metricsRows)
        {
            var sourceMetrics = await _getFinancialMetricsQueryHandler.HandleAsync();
            var coefficients = await _getCoefficientsQueryHandler.HandleAsync();
            var workingPlan = await _getWorkingPlanQueryHandler.HandleAsync();
            var metricsRowChangesList = new List<MetricsRowChanges>();

            foreach (var metricsRow in metricsRows)
            {
                var employeeId = metricsRow.EmployeeId;

                if (metricsRow.IsCopy)
                {
                    var employeeCopyMetrics = await CalculateMetricsAsync(metricsRow.RatePerHour, metricsRow.Pay,
                        metricsRow.EmploymentType, metricsRow.ParkingCostPerMonth, null, coefficients, workingPlan);

                    metricsRowChangesList.Add(new MetricsRowChanges(metricsRow.EmployeeCopyId, employeeCopyMetrics));
                    continue;
                }

                var employeeSourceMetrics = sourceMetrics.Single(x => x.EmployeeId == employeeId);

                if (IsEmployeeMetricsChanged(metricsRow, employeeSourceMetrics))
                {
                    var employeeNewMetrics = await CalculateMetricsAsync(metricsRow.RatePerHour, metricsRow.Pay,
                    metricsRow.EmploymentType, metricsRow.ParkingCostPerMonth, employeeId, coefficients, workingPlan);
                    
                    metricsRowChangesList.Add(new MetricsRowChanges(employeeId.Value, employeeSourceMetrics, employeeNewMetrics));
                    continue;
                }

                metricsRowChangesList.Add(new MetricsRowChanges(employeeId.Value, employeeSourceMetrics));
            }

            var newMetrics = metricsRowChangesList.Select(x => x.NewMetrics);
            var sourceMetricsTotal = MetricsDiffCalculator.CalculateTotalEmployeeFinancialMetrics(sourceMetrics);
            var newMetricsTotal = MetricsDiffCalculator.CalculateTotalEmployeeFinancialMetrics(newMetrics);

            return new AnalyticsMetricChanges
            {
                MetricsRowsChanges = metricsRowChangesList,
                NewTotalMetrics = newMetricsTotal,
                TotalMetricsDiff = MetricsDiffCalculator.CalculateDiffBetweenTotalEmployeeFinancialMetrics(sourceMetricsTotal, newMetricsTotal),
            };
        }

        public async Task<EmployeeFinancialMetrics> CalculateMetricsAsync(decimal ratePerHour,
            decimal pay,
            decimal employmentTypeValue,
            decimal parkingCostPerMonth,
            long? employeeId = null,
            CoefficientOptions? coefficients = null,
            WorkingPlan? workingPlan = null)
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

            coefficients = coefficients ?? await _getCoefficientsQueryHandler.HandleAsync();
            workingPlan = workingPlan ?? await _getWorkingPlanQueryHandler.HandleAsync();

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

        private static bool IsEmployeeMetricsChanged(MetricsRowDto metricsRow, EmployeeFinancialMetrics employeeMetrics)
        {
            return employeeMetrics.RatePerHour != metricsRow.RatePerHour
                   || employeeMetrics.Pay != metricsRow.Pay
                   || employeeMetrics.EmploymentType != metricsRow.EmploymentType
                   || employeeMetrics.ParkingCostPerMonth != metricsRow.ParkingCostPerMonth;
        }
    }
}

