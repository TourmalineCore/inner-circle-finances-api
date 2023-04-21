using NodaTime;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries;
using SalaryService.Application.Queries.Contracts;
using SalaryService.Domain;

namespace SalaryService.Application.Services;

public class FinancesService
{
    private readonly ICoefficientsQuery _coefficientsQuery;
    private readonly IFinancialMetricsQuery _financialMetricsQuery;
    private readonly IWorkingPlanQuery _workingPlanQuery;
    private readonly EmployeesQuery _employeesQuery;
    private readonly TotalFinancesQuery _totalFinancesQuery;
    private readonly EstimatedFinancialEfficiencyQuery _estimatedFinancialEfficiencyQuery;
    private readonly IClock _clock;

    public FinancesService(ICoefficientsQuery getCoefficientsQueryHandler,
        IFinancialMetricsQuery getFinancialMetricsQueryHandler,
        IWorkingPlanQuery getWorkingPlanQueryHandler,
        IClock clock,
        EmployeesQuery employeesQuery,
        TotalFinancesQuery totalFinancesQuery,
        EstimatedFinancialEfficiencyQuery estimatedFinancialEfficiencyQuery)
    {
        _coefficientsQuery = getCoefficientsQueryHandler;
        _financialMetricsQuery = getFinancialMetricsQueryHandler;
        _workingPlanQuery = getWorkingPlanQueryHandler;
        _clock = clock;
        _employeesQuery = employeesQuery;
        _totalFinancesQuery = totalFinancesQuery;
        _estimatedFinancialEfficiencyQuery = estimatedFinancialEfficiencyQuery;
    }

    public async Task<TotalFinances?> GetTotalFinancesAsync()
    {
        return await _totalFinancesQuery.GetTotalFinancesAsync();
    }

    public async Task<EstimatedFinancialEfficiency?> GetEstimatedFinancialEfficiencyAsync()
    {
        return await _estimatedFinancialEfficiencyQuery.GetEstimatedFinancialEfficiencyAsync();
    }

    public async Task<CoefficientOptions> GetCoefficientsAsync()
    {
        return await _coefficientsQuery.GetCoefficientsAsync();
    }

    public async Task<WorkingPlan> GetWorkingPlanAsync()
    {
        return await _workingPlanQuery.GetWorkingPlanAsync();
    }

    public async Task<AnalyticsMetricChanges> CalculateAnalyticsMetricChangesAsync(IEnumerable<MetricsRowDto> metricsRows)
    {
        var sourceMetrics = await _financialMetricsQuery.HandleAsync();
        var coefficients = await GetCoefficientsAsync();
        var workingPlan = await GetWorkingPlanAsync();
        var employees = await _employeesQuery.GetEmployeesAsync();
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

            var employee = employees.Single(x => x.Id == employeeId);
            var employeeSourceMetrics = employee.FinancialMetrics;

            if (IsEmployeeMetricsChanged(metricsRow, employeeSourceMetrics))
            {
                var employeeNewMetrics = await CalculateMetricsAsync(metricsRow.RatePerHour, metricsRow.Pay,
                metricsRow.EmploymentType, metricsRow.ParkingCostPerMonth, employeeId, coefficients, workingPlan);
                
                metricsRowChangesList.Add(new MetricsRowChanges(employeeId.Value, employee.GetFullName(), employeeSourceMetrics, employeeNewMetrics));
                continue;
            }

            metricsRowChangesList.Add(new MetricsRowChanges(employeeId.Value, employee.GetFullName(), employeeSourceMetrics));
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

    public async Task<FinancialMetrics> CalculateMetricsAsync(decimal ratePerHour,
        decimal pay,
        decimal employmentTypeValue,
        decimal parkingCostPerMonth,
        long? employeeId = null,
        CoefficientOptions? coefficients = null,
        WorkingPlan? workingPlan = null)
    {
        var financesForPayroll = new FinancesForPayroll(ratePerHour, pay, parkingCostPerMonth, employmentTypeValue, true);
        return await CalculateMetricsAsync(financesForPayroll, coefficients, workingPlan);
    }

    public async Task<FinancialMetrics> CalculateMetricsAsync(
        FinancesForPayroll financesForPayroll,
        CoefficientOptions? coefficients = null,
        WorkingPlan? workingPlan = null)
    {
        coefficients = coefficients ?? await GetCoefficientsAsync();
        workingPlan = workingPlan ?? await GetWorkingPlanAsync();

        return new FinancialMetrics(financesForPayroll, coefficients, workingPlan, _clock.GetCurrentInstant());
    }

    private static bool IsEmployeeMetricsChanged(MetricsRowDto metricsRow, FinancialMetrics employeeMetrics)
    {
        return employeeMetrics.RatePerHour != metricsRow.RatePerHour
               || employeeMetrics.Pay != metricsRow.Pay
               || employeeMetrics.EmploymentType != metricsRow.EmploymentType
               || employeeMetrics.ParkingCostPerMonth != metricsRow.ParkingCostPerMonth;
    }
}

