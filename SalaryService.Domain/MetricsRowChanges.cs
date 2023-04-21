namespace SalaryService.Domain;

public class MetricsRowChanges
{
    public string EmployeeId { get; init; }

    public string EmployeeFullName { get; init; }

    public bool IsCopy { get; init; }

    public FinancialMetrics NewMetrics { get; init; }

    public EmployeeFinancialMetricsDiff? MetricsDiff { get; init; }

    public MetricsRowChanges(long employeeId, string employeeFullName, FinancialMetrics sourceMetrics, FinancialMetrics newMetrics)
    {
        EmployeeId = employeeId.ToString();
        EmployeeFullName = employeeFullName;
        NewMetrics = newMetrics;
        MetricsDiff = MetricsDiffCalculator.CalculateDiffBetweenEmployeeFinancialMetrics(sourceMetrics, newMetrics);
        IsCopy = false;
    }

    public MetricsRowChanges(long employeeId, string employeeFullName, FinancialMetrics sourceMetrics)
    {
        EmployeeId = employeeId.ToString();
        EmployeeFullName = employeeFullName;
        NewMetrics = sourceMetrics;
        MetricsDiff = null;
        IsCopy = false;
    }

    public MetricsRowChanges(string copyId, string copyFullName, FinancialMetrics newMetrics)
    {
        EmployeeId = copyId;
        EmployeeFullName = copyFullName;
        NewMetrics = newMetrics;
        MetricsDiff = null;
        IsCopy = true;
    }
}
