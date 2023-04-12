namespace SalaryService.Domain
{
    public class EmployeeFinancialMetricsChanging
    {
        public List<EmployeeFinancialMetricsDiffEntry> EmployeeMetricsDiffEntries { get; set; }

        public TotalEmployeeFinancialMetrics SourceTotalEmployeeFinancialMetrics { get; set; }

        public EmployeeFinancialTotalMetricsDiff TotalMetricsDiff { get; set; }
    }
}
