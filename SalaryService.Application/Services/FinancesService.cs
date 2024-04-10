using NodaTime;
using SalaryService.Application.Dtos;
using SalaryService.Application.Queries.Contracts;
using SalaryService.Domain;

namespace SalaryService.Application.Services;

public class FinancesService
{
    private readonly ICoefficientsQuery _coefficientsQuery;
    private readonly IFinancialMetricsQuery _financialMetricsQuery;
    private readonly IWorkingPlanQuery _workingPlanQuery;
    private readonly IEmployeesQuery _employeesQuery;
    private readonly ITotalFinancesQuery _totalFinancesQuery;
    private readonly IEstimatedFinancialEfficiencyQuery _estimatedFinancialEfficiencyQuery;
    private readonly IClock _clock;

    public FinancesService(ICoefficientsQuery getCoefficientsQueryHandler,
        IFinancialMetricsQuery getFinancialMetricsQueryHandler,
        IWorkingPlanQuery getWorkingPlanQueryHandler,
        IEmployeesQuery employeesQuery,
        ITotalFinancesQuery totalFinancesQuery,
        IEstimatedFinancialEfficiencyQuery estimatedFinancialEfficiencyQuery,
        IClock clock)
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

    public async Task<TotalEmployeeFinancialMetricsEntry> CalculateEmployeesTotalFinancialMetricsAsync()
    {
        var employeeFinancialMetrics = await _financialMetricsQuery.HandleAsync();
        return MetricsDiffCalculator.CalculateTotalEmployeeFinancialMetrics(employeeFinancialMetrics);
    }

    public async Task<AnalyticsMetricChanges> CalculateAnalyticsMetricChangesAsync(
        IEnumerable<MetricsRowDto> metricsRows, long tenantId)
    {
        var sourceMetrics = await _financialMetricsQuery.HandleAsync();
        var coefficients = await GetCoefficientsAsync();
        var workingPlan = await GetWorkingPlanAsync();
        var employees = await _employeesQuery.GetEmployeesAsync(tenantId);
        var metricsRowChangesList = new List<MetricsRowChanges>();

        foreach (var metricsRow in metricsRows)
        {
            if (metricsRow.IsCopy)
            {
                var employeeCopyMetrics = await CalculateMetricsAsync(
                    new FinancesForPayroll(
                        metricsRow.RatePerHour,
                        metricsRow.Pay,
                        metricsRow.EmploymentType,
                        metricsRow.ParkingCostPerMonth
                    ),
                    metricsRow.IsEmployedOfficially,
                    coefficients,
                    workingPlan);

                metricsRowChangesList.Add(new MetricsRowChanges(
                    metricsRow.EmployeeId,
                    metricsRow.EmployeeFullName,
                    metricsRow.IsEmployedOfficially,
                    employeeCopyMetrics)
                );

                continue;
            }

            var employeeId = long.Parse(metricsRow.EmployeeId);
            var employee = employees.Single(x => x.Id == employeeId);
            var employeeSourceMetrics = employee.FinancialMetrics;

            if (IsEmployeeMetricsChanged(metricsRow, employee))
            {
                var employeeNewMetrics = await CalculateMetricsAsync(
                    new FinancesForPayroll(
                        metricsRow.RatePerHour,
                        metricsRow.Pay,
                        metricsRow.EmploymentType,
                        metricsRow.ParkingCostPerMonth
                    ),
                    metricsRow.IsEmployedOfficially,
                    coefficients,
                    workingPlan);

                metricsRowChangesList.Add(new MetricsRowChanges(
                    employeeId,
                    employee.GetFullName(),
                    metricsRow.IsEmployedOfficially,
                    employeeSourceMetrics,
                    employeeNewMetrics)
                );

                continue;
            }

            metricsRowChangesList.Add(new MetricsRowChanges(
                employeeId,
                employee.GetFullName(),
                employee.IsEmployedOfficially,
                employeeSourceMetrics)
            );
        }

        var newMetrics = metricsRowChangesList.Select(x => x.NewMetrics);
        var sourceMetricsTotal = MetricsDiffCalculator.CalculateTotalEmployeeFinancialMetrics(sourceMetrics);
        var newMetricsTotal = MetricsDiffCalculator.CalculateTotalEmployeeFinancialMetrics(newMetrics);

        return new AnalyticsMetricChanges
        {
            MetricsRowsChanges = metricsRowChangesList,
            NewTotalMetrics = newMetricsTotal,
            TotalMetricsDiff =
                MetricsDiffCalculator.CalculateDiffBetweenTotalEmployeeFinancialMetrics(sourceMetricsTotal,
                    newMetricsTotal)
        };
    }

    public async Task<EmployeeFinancialMetrics> CalculateMetricsAsync(
        FinancesForPayroll financesForPayroll,
        bool isEmployedOfficially,
        CoefficientOptions? coefficients = null,
        WorkingPlan? workingPlan = null)
    {
        coefficients ??= await GetCoefficientsAsync();
        workingPlan ??= await GetWorkingPlanAsync();

        return new EmployeeFinancialMetrics(
            financesForPayroll,
            isEmployedOfficially,
            coefficients,
            workingPlan,
            _clock.GetCurrentInstant());
    }

    private static bool IsEmployeeMetricsChanged(MetricsRowDto metricsRow, Employee employee)
    {
        var employeeFinancialMetrics = employee.FinancialMetrics;

        return employeeFinancialMetrics.RatePerHour != metricsRow.RatePerHour
               || employeeFinancialMetrics.Pay != metricsRow.Pay
               || employeeFinancialMetrics.EmploymentType != metricsRow.EmploymentType
               || employeeFinancialMetrics.ParkingCostPerMonth != metricsRow.ParkingCostPerMonth
               || employee.IsEmployedOfficially != metricsRow.IsEmployedOfficially;
    }
}