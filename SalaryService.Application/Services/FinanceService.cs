using NodaTime;
using SalaryService.Application.Dtos;
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

        public async Task<EmployeeFinancialMetricsChanging> CalculateEmployeeMetricsDiffAsync(IEnumerable<EmployeeRowDto> employeeDtos)
        {
            var sourceMetrics = await _getFinancialMetricsQueryHandler.HandleAsync();
            var coefficients = await _getCoefficientsQueryHandler.HandleAsync();
            var workingPlan = await _getWorkingPlanQueryHandler.HandleAsync();

            var newMetrics = new List<EmployeeFinancialMetrics>();
            var metricsDiffEntries = new List<EmployeeFinancialMetricsDiffEntry>();

            foreach (var employeeDto in employeeDtos)
            {
                if (employeeDto.IsCopyRow)
                {
                    var employeeCopyMetrics = await CalculateMetricsAsync(employeeDto.RatePerHour, employeeDto.Pay,
                        employeeDto.EmploymentType, employeeDto.ParkingCostPerMonth, coefficients, workingPlan);
                    newMetrics.Add(employeeCopyMetrics);
                    metricsDiffEntries.Add(new EmployeeFinancialMetricsDiffEntry
                    {
                        SourceMetrics = employeeCopyMetrics,
                        EmployeeId = null,
                        MetricsDiff = null,
                    });

                    continue;
                }

                var sourceEmployeeMetrics = sourceMetrics.Single(x => x.EmployeeId == employeeDto.EmployeeId);

                if (!IsEmployeeMetricsChanged(employeeDto, sourceEmployeeMetrics))
                {
                    newMetrics.Add(sourceEmployeeMetrics);
                    metricsDiffEntries.Add(new EmployeeFinancialMetricsDiffEntry
                    {
                        EmployeeId = employeeDto.EmployeeId,
                        SourceMetrics = sourceEmployeeMetrics,
                        MetricsDiff = null,
                    });

                    continue;
                }

                var newEmployeeMetrics = await CalculateMetricsAsync(employeeDto.RatePerHour, employeeDto.Pay,
                    employeeDto.EmploymentType, employeeDto.ParkingCostPerMonth, coefficients, workingPlan);

                newMetrics.Add(newEmployeeMetrics);
                metricsDiffEntries.Add(new EmployeeFinancialMetricsDiffEntry
                {
                    EmployeeId = employeeDto.EmployeeId,
                    SourceMetrics = sourceEmployeeMetrics,
                    MetricsDiff = MetricsDiffCalculator.CalculateDiffBetweenEmployeeFinancialMetrics(sourceEmployeeMetrics, newEmployeeMetrics)
                });
            }

            var sourceMetricsTotal = MetricsDiffCalculator.CalculateTotalEmployeeFinancialMetrics(sourceMetrics.ToList());
            var newMetricsTotal = MetricsDiffCalculator.CalculateTotalEmployeeFinancialMetrics(newMetrics);

            return new EmployeeFinancialMetricsChanging
            {
                EmployeeMetricsDiffEntries = metricsDiffEntries,
                SourceTotalEmployeeFinancialMetrics = sourceMetricsTotal,
                TotalMetricsDiff = MetricsDiffCalculator.CalculateDiffBetweenTotalEmployeeFinancialMetrics(sourceMetricsTotal, newMetricsTotal),
            };
        }

        private static bool IsEmployeeMetricsChanged(EmployeeRowDto employeeDto, EmployeeFinancialMetrics employeeMetrics)
        {
            return employeeMetrics.RatePerHour != employeeDto.RatePerHour
                   || employeeMetrics.Pay != employeeDto.Pay
                   || employeeMetrics.EmploymentType != employeeDto.EmploymentType
                   || employeeMetrics.ParkingCostPerMonth != employeeDto.ParkingCostPerMonth;
        }

        public async Task<EmployeeFinancialMetrics> CalculateMetricsAsync(decimal ratePerHour,
            decimal pay,
            decimal employmentTypeValue,
            decimal parkingCostPerMonth,
            CoefficientOptions? coefficients = null,
            WorkingPlan? workingPlan = null)
        {
            //TODO: #861m9k5f6: make refactoring of using employee finances for payroll in an employee model

            if (!_availableEmploymentTypes.Contains(employmentTypeValue))
            {
                throw new ArgumentException("Employment type can accept only the following values: 0.5, 1");
            }

            var calculateMetrics = new EmployeeFinancialMetrics(
                ratePerHour,
                pay,
                employmentTypeValue,
                parkingCostPerMonth);

            coefficients = coefficients ?? await _getCoefficientsQueryHandler.HandleAsync();
            workingPlan = workingPlan ?? await _getWorkingPlanQueryHandler.HandleAsync();

            calculateMetrics.CalculateMetrics(coefficients.DistrictCoefficient,
                coefficients.MinimumWage,
                coefficients.IncomeTaxPercent,
                workingPlan.WorkingHoursInMonth,
                _clock.GetCurrentInstant());

            return calculateMetrics;
        }
    }
}

