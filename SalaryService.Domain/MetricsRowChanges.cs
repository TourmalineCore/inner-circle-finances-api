namespace SalaryService.Domain
{
    public class MetricsRowChanges
    {
        public string EmployeeId { get; init; }

        public string? EmployeeFullName { get; init; }

        public EmployeeFinancialMetrics NewMetrics { get; init; }

        public EmployeeFinancialMetricsDiff? MetricsDiff { get; init; }

        public MetricsRowChanges(long employeeId, string employeeFullName, EmployeeFinancialMetrics sourceMetrics, EmployeeFinancialMetrics newMetrics)
        {
            EmployeeId = employeeId.ToString();
            EmployeeFullName = employeeFullName;
            NewMetrics = newMetrics;
            MetricsDiff = MetricsDiffCalculator.CalculateDiffBetweenEmployeeFinancialMetrics(sourceMetrics, newMetrics);
        }

        public MetricsRowChanges(long employeeId, string employeeFullName, EmployeeFinancialMetrics sourceMetrics)
        {
            EmployeeId = employeeId.ToString();
            EmployeeFullName = employeeFullName;
            NewMetrics = sourceMetrics;
            MetricsDiff = null;
        }

        public MetricsRowChanges(string employeeId, EmployeeFinancialMetrics newMetrics)
        {
            EmployeeId = employeeId;
            EmployeeFullName = null;
            NewMetrics = newMetrics;
            MetricsDiff = null;
        }
    }
}
