namespace SalaryService.Domain
{
    public class MetricsRowChanges
    {
        public string EmployeeId { get; init; }

        public string EmployeeFullName { get; init; }

        public bool IsCopy { get; init; }

        public EmployeeFinancialMetrics NewMetrics { get; init; }

        public EmployeeFinancialMetricsDiff? MetricsDiff { get; init; }

        public MetricsRowChanges(long employeeId, string employeeFullName, EmployeeFinancialMetrics sourceMetrics, EmployeeFinancialMetrics newMetrics)
        {
            EmployeeId = employeeId.ToString();
            EmployeeFullName = employeeFullName;
            NewMetrics = newMetrics;
            MetricsDiff = MetricsDiffCalculator.CalculateDiffBetweenEmployeeFinancialMetrics(sourceMetrics, newMetrics);
            IsCopy = false;
        }

        public MetricsRowChanges(long employeeId, string employeeFullName, EmployeeFinancialMetrics sourceMetrics)
        {
            EmployeeId = employeeId.ToString();
            EmployeeFullName = employeeFullName;
            NewMetrics = sourceMetrics;
            MetricsDiff = null;
            IsCopy = false;
        }

        public MetricsRowChanges(string copyId, string copyFullName, EmployeeFinancialMetrics newMetrics)
        {
            EmployeeId = copyId;
            EmployeeFullName = copyFullName;
            NewMetrics = newMetrics;
            MetricsDiff = null;
            IsCopy = true;
        }
    }
}
