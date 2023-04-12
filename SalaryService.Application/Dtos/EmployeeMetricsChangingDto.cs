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
        public double RatePerHour { get; init; }

        public double Pay { get; init; }

        public double EmploymentType { get; init; }

        public double ParkingCostPerMonth { get; init; }

        public double Salary { get; init; }

        public double AccountingPerMonth { get; init; }

        public double HourlyCostFact { get; init; }

        public double HourlyCostHand { get; init; }

        public double Earnings { get; init; }

        public double Expenses { get; init; }

        public double IncomeTaxContributions { get; init; }

        public double DistrictCoefficient { get; init; }

        public double PensionContributions { get; init; }

        public double MedicalContributions { get; init; }

        public double SocialInsuranceContributions { get; init; }

        public double InjuriesContributions { get; init; }

        public double Profit { get; init; }

        public double ProfitAbility { get; init; }

        public double GrossSalary { get; init; }

        public double Prepayment { get; init; }

        public double NetSalary { get; init; }
    }

    // can use as a base type for EmployeeMetricsDto
    public class EmployeeMetricsDiffDto
    {
        public double RatePerHour { get; init; }

        public double Pay { get; init; }

        public double ParkingCostPerMonth { get; init; }

        public double Salary { get; init; }

        public double AccountingPerMonth { get; init; }

        public double HourlyCostFact { get; init; }

        public double HourlyCostHand { get; init; }

        public double Earnings { get; init; }

        public double Expenses { get; init; }

        public double IncomeTaxContributions { get; init; }

        public double DistrictCoefficient { get; init; }

        public double PensionContributions { get; init; }

        public double MedicalContributions { get; init; }

        public double SocialInsuranceContributions { get; init; }

        public double InjuriesContributions { get; init; }

        public double Profit { get; init; }

        public double ProfitAbility { get; init; }

        public double GrossSalary { get; init; }

        public double Prepayment { get; init; }

        public double NetSalary { get; init; }
    }

    public class TotalChangingDto
    {
        public TotalChangingEntryDto Source { get; init; }

        public TotalChangingEntryDto MetricsDiff { get; init; }
    }

    public class TotalChangingEntryDto
    {
        public double ParkingCostPerMonth { get; init; }

        public double AccountingPerMonth { get; init; }

        public double Earnings { get; init; }

        public double Expenses { get; init; }

        public double IncomeTaxContributions { get; init; }

        public double PensionContributions { get; init; }

        public double MedicalContributions { get; init; }

        public double SocialInsuranceContributions { get; init; }

        public double InjuriesContributions { get; init; }

        public double Profit { get; init; }

        public double ProfitAbility { get; init; }

        public double Prepayment { get; init; }

        public double NetSalary { get; init; }
    }
}
