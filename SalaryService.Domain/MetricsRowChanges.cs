namespace SalaryService.Domain;

public class MetricsRowChanges
{
    public string EmployeeId { get; init; }

    public string? EmployeeFullName { get; init; }

    public FinancialMetrics NewMetrics { get; init; }

    public EmployeeFinancialMetricsDiff? MetricsDiff { get; init; }

    public MetricsRowChanges(long employeeId, string employeeFullName, FinancialMetrics sourceMetrics, FinancialMetrics newMetrics)
    {
        EmployeeId = employeeId.ToString();
        EmployeeFullName = employeeFullName;
        NewMetrics = newMetrics;
        MetricsDiff = MetricsDiffCalculator.CalculateDiffBetweenEmployeeFinancialMetrics(sourceMetrics, newMetrics);
    }

    public MetricsRowChanges(long employeeId, string employeeFullName, FinancialMetrics sourceMetrics)
    {
        EmployeeId = employeeId.ToString();
        EmployeeFullName = employeeFullName;
        NewMetrics = sourceMetrics;
        MetricsDiff = null;
    }

    public MetricsRowChanges(string employeeId, FinancialMetrics newMetrics)
    {
        EmployeeId = employeeId;
        EmployeeFullName = null;
        NewMetrics = newMetrics;
        MetricsDiff = null;
    }
}
