namespace SalaryService.Domain
{
    public class MetricsRowChanges
    {
        public string EmployeeId { get; init; }

        public EmployeeFinancialMetrics NewMetrics { get; init; }

        public EmployeeFinancialMetricsDiff? MetricsDiff { get; init; }

        public MetricsRowChanges(long employeeId, EmployeeFinancialMetrics sourceMetrics, EmployeeFinancialMetrics newMetrics)
        {
            EmployeeId = employeeId.ToString();
            NewMetrics = newMetrics;
            MetricsDiff = MetricsDiffCalculator.CalculateDiffBetweenEmployeeFinancialMetrics(sourceMetrics, newMetrics);
        }

        public MetricsRowChanges(long employeeId, EmployeeFinancialMetrics sourceMetrics)
        {
            EmployeeId = employeeId.ToString();
            NewMetrics = sourceMetrics;
            MetricsDiff = null;
        }

        public MetricsRowChanges(string employeeId, EmployeeFinancialMetrics newMetrics)
        {
            EmployeeId = employeeId;
            NewMetrics = newMetrics;
            MetricsDiff = null;
        }
    }
}
