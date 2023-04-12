namespace SalaryService.Domain
{
    public readonly struct EmployeeFinancialMetricsDiffEntry
    {
        public long? EmployeeId { get; init; }

        public EmployeeFinancialMetrics SourceMetrics { get; init; }

        public EmployeeFinancialMetricsDiff? MetricsDiff { get; init; }
    }
}
