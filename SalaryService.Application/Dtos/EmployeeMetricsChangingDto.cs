using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public class EmployeeMetricsChangingDto
    {
        public EmployeeChangingDto Employees { get; init; }

        public TotalChangingDto Total { get; init; }

        public EmployeeMetricsChangingDto(EmployeeFinancialMetricsChanging metricsChanging)
        {
            var employees = metricsChanging.EmployeeMetricsDiffEntries.Select(x => new EmployeeChangingDto
            {
                EmployeeId = x.EmployeeId.ToString(),
                Metrics = new EmployeeMetricsDto
                {
                    RatePerHour = x.SourceMetrics.RatePerHour,
                    Pay = x.SourceMetrics.Pay,
                    EmploymentType = x.SourceMetrics.EmploymentType,
                    ParkingCostPerMonth = x.SourceMetrics.ParkingCostPerMonth,
                    Salary = x.SourceMetrics.Salary,
                    AccountingPerMonth = x.SourceMetrics.AccountingPerMonth,
                    HourlyCostFact = x.SourceMetrics.HourlyCostFact,
                    HourlyCostHand = x.SourceMetrics.HourlyCostHand,
                    Earnings = x.SourceMetrics.Earnings,
                    Expenses = x.SourceMetrics.Expenses,
                    IncomeTaxContributions = x.SourceMetrics.IncomeTaxContributions,
                    DistrictCoefficient = x.SourceMetrics.DistrictCoefficient,
                    PensionContributions = x.SourceMetrics.PensionContributions,
                    MedicalContributions = x.SourceMetrics.MedicalContributions,
                    SocialInsuranceContributions = x.SourceMetrics.SocialInsuranceContributions,
                    InjuriesContributions = x.SourceMetrics.InjuriesContributions,
                    Profit = x.SourceMetrics.Profit,
                    ProfitAbility = x.SourceMetrics.ProfitAbility,
                    GrossSalary = x.SourceMetrics.GrossSalary,
                    Prepayment = x.SourceMetrics.Prepayment,
                    NetSalary = x.SourceMetrics.NetSalary,
                },
                MetricsDiff = x.MetricsDiff.HasValue ? new EmployeeMetricsDiffDto
                {
                    RatePerHour = x.MetricsDiff.Value.RatePerHour,
                    Pay = x.MetricsDiff.Value.Pay,
                    ParkingCostPerMonth = x.MetricsDiff.Value.ParkingCostPerMonth,
                    Salary = x.MetricsDiff.Value.Salary,
                    AccountingPerMonth = x.MetricsDiff.Value.AccountingPerMonth,
                    HourlyCostFact = x.MetricsDiff.Value.HourlyCostFact,
                    HourlyCostHand = x.MetricsDiff.Value.HourlyCostHand,
                    Earnings = x.MetricsDiff.Value.Earnings,
                    Expenses = x.MetricsDiff.Value.Expenses,
                    IncomeTaxContributions = x.MetricsDiff.Value.IncomeTaxContributions,
                    DistrictCoefficient = x.MetricsDiff.Value.DistrictCoefficient,
                    PensionContributions = x.MetricsDiff.Value.PensionContributions,
                    MedicalContributions = x.MetricsDiff.Value.MedicalContributions,
                    SocialInsuranceContributions = x.MetricsDiff.Value.SocialInsuranceContributions,
                    InjuriesContributions = x.MetricsDiff.Value.InjuriesContributions,
                    Profit = x.MetricsDiff.Value.Profit,
                    ProfitAbility = x.MetricsDiff.Value.ProfitAbility,
                    GrossSalary = x.MetricsDiff.Value.GrossSalary,
                    Prepayment = x.MetricsDiff.Value.Prepayment,
                    NetSalary = x.MetricsDiff.Value.NetSalary,
                } : null,
            });
        }
    }

    public class EmployeeChangingDto
    {
        public string EmployeeId { get; init; }

        public EmployeeMetricsDto Metrics { get; init; }

        public EmployeeMetricsDiffDto? MetricsDiff { get; init; }
    }

    public class EmployeeMetricsDto
    {
        public decimal RatePerHour { get; init; }

        public decimal Pay { get; init; }

        public decimal EmploymentType { get; init; }

        public decimal ParkingCostPerMonth { get; init; }

        public decimal Salary { get; init; }

        public decimal AccountingPerMonth { get; init; }

        public decimal HourlyCostFact { get; init; }

        public decimal HourlyCostHand { get; init; }

        public decimal Earnings { get; init; }

        public decimal Expenses { get; init; }

        public decimal IncomeTaxContributions { get; init; }

        public decimal DistrictCoefficient { get; init; }

        public decimal PensionContributions { get; init; }

        public decimal MedicalContributions { get; init; }

        public decimal SocialInsuranceContributions { get; init; }

        public decimal InjuriesContributions { get; init; }

        public decimal Profit { get; init; }

        public decimal ProfitAbility { get; init; }

        public decimal GrossSalary { get; init; }

        public decimal Prepayment { get; init; }

        public decimal NetSalary { get; init; }
    }

    // can use as a base type for EmployeeMetricsDto
    public class EmployeeMetricsDiffDto
    {
        public decimal RatePerHour { get; init; }

        public decimal Pay { get; init; }

        public decimal ParkingCostPerMonth { get; init; }

        public decimal Salary { get; init; }

        public decimal AccountingPerMonth { get; init; }

        public decimal HourlyCostFact { get; init; }

        public decimal HourlyCostHand { get; init; }

        public decimal Earnings { get; init; }

        public decimal Expenses { get; init; }

        public decimal IncomeTaxContributions { get; init; }

        public decimal DistrictCoefficient { get; init; }

        public decimal PensionContributions { get; init; }

        public decimal MedicalContributions { get; init; }

        public decimal SocialInsuranceContributions { get; init; }

        public decimal InjuriesContributions { get; init; }

        public decimal Profit { get; init; }

        public decimal ProfitAbility { get; init; }

        public decimal GrossSalary { get; init; }

        public decimal Prepayment { get; init; }

        public decimal NetSalary { get; init; }
    }

    public class TotalChangingDto
    {
        public TotalChangingEntryDto Source { get; init; }

        public TotalChangingEntryDto MetricsDiff { get; init; }
    }

    public class TotalChangingEntryDto
    {
        public decimal ParkingCostPerMonth { get; init; }

        public decimal AccountingPerMonth { get; init; }

        public decimal Earnings { get; init; }

        public decimal Expenses { get; init; }

        public decimal IncomeTaxContributions { get; init; }

        public decimal PensionContributions { get; init; }

        public decimal MedicalContributions { get; init; }

        public decimal SocialInsuranceContributions { get; init; }

        public decimal InjuriesContributions { get; init; }

        public decimal Profit { get; init; }

        public decimal ProfitAbility { get; init; }

        public decimal Prepayment { get; init; }

        public decimal NetSalary { get; init; }
    }
}
