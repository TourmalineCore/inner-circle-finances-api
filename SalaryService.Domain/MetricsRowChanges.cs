namespace SalaryService.Domain;

public class MetricsRowChanges
{
    public string EmployeeId { get; init; }

    public string EmployeeFullName { get; init; }

    public bool IsCopy { get; init; }

    public bool IsEmployedOfficially { get; init; }

    public FinancialMetrics NewMetrics { get; init; }

    public EmployeeFinancialMetricsDiff? MetricsDiff { get; init; }

    public MetricsRowChanges(long employeeId, string employeeFullName, bool isEmployedOfficially, FinancialMetrics sourceMetrics, FinancialMetrics newMetrics)
    {
        EmployeeId = employeeId.ToString();
        EmployeeFullName = employeeFullName;
        NewMetrics = newMetrics;
        IsEmployedOfficially = isEmployedOfficially;
        MetricsDiff = MetricsDiffCalculator.CalculateDiffBetweenEmployeeFinancialMetrics(sourceMetrics, newMetrics);
        IsCopy = false;
    }

    public MetricsRowChanges(long employeeId, string employeeFullName, bool isEmployedOfficially, FinancialMetrics sourceMetrics)
    {
        EmployeeId = employeeId.ToString();
        EmployeeFullName = employeeFullName;
        NewMetrics = sourceMetrics;
        IsEmployedOfficially = isEmployedOfficially;
        MetricsDiff = null;
        IsCopy = false;
    }

    public MetricsRowChanges(string copyId, string copyFullName, bool isEmployedOfficially, FinancialMetrics newMetrics)
    {
        EmployeeId = copyId;
        EmployeeFullName = copyFullName;
        NewMetrics = newMetrics;
        IsEmployedOfficially = isEmployedOfficially;
        MetricsDiff = null;
        IsCopy = true;
    }
}
