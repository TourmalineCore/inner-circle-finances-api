namespace SalaryService.Domain;

public class AnalyticsMetricChanges
{
    public List<MetricsRowChanges> MetricsRowsChanges { get; set; }

    public TotalEmployeeFinancialMetricsEntry NewTotalMetrics { get; set; }

    public EmployeeFinancialTotalMetricsDiff TotalMetricsDiff { get; set; }
}
