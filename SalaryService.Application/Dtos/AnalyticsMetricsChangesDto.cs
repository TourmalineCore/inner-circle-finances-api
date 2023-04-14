using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public class AnalyticsMetricsChangesDto
    {
        public IEnumerable<RowChangesDto> Rows { get; init; }

        public TotalChangesDto Total { get; init; }

        public AnalyticsMetricsChangesDto(AnalyticsMetricChanges metricsChanges)
        {
            Rows = metricsChanges.MetricsRowsChanges.Select(x => new RowChangesDto
            {
                EmployeeId = x.EmployeeId.ToString(),
                Metrics = new EmployeeMetricsRowDto
                {
                    RatePerHour = Math.Round(x.NewMetrics.RatePerHour, 2),
                    Pay = Math.Round(x.NewMetrics.Pay, 2),
                    EmploymentType = Math.Round(x.NewMetrics.EmploymentType, 2),
                    ParkingCostPerMonth = Math.Round(x.NewMetrics.ParkingCostPerMonth, 2),
                    Salary = Math.Round(x.NewMetrics.Salary, 2),
                    AccountingPerMonth = Math.Round(x.NewMetrics.AccountingPerMonth, 2),
                    HourlyCostFact = Math.Round(x.NewMetrics.HourlyCostFact, 2),
                    HourlyCostHand = Math.Round(x.NewMetrics.HourlyCostHand, 2),
                    Earnings = Math.Round(x.NewMetrics.Earnings, 2),
                    Expenses = Math.Round(x.NewMetrics.Expenses, 2),
                    IncomeTaxContributions = Math.Round(x.NewMetrics.IncomeTaxContributions, 2),
                    DistrictCoefficient = Math.Round(x.NewMetrics.DistrictCoefficient, 2),
                    PensionContributions = Math.Round(x.NewMetrics.PensionContributions, 2),
                    MedicalContributions = Math.Round(x.NewMetrics.MedicalContributions, 2),
                    SocialInsuranceContributions = Math.Round(x.NewMetrics.SocialInsuranceContributions, 2),
                    InjuriesContributions = Math.Round(x.NewMetrics.InjuriesContributions, 2),
                    Profit = Math.Round(x.NewMetrics.Profit, 2),
                    ProfitAbility = Math.Round(x.NewMetrics.ProfitAbility, 2),
                    GrossSalary = Math.Round(x.NewMetrics.GrossSalary, 2),
                    Prepayment = Math.Round(x.NewMetrics.Prepayment, 2),
                    NetSalary = Math.Round(x.NewMetrics.NetSalary, 2),
                },
                MetricsDiff = x.MetricsDiff.HasValue ? new EmployeeMetricsDiffDto
                {
                    RatePerHour = Math.Round(x.MetricsDiff.Value.RatePerHour, 2),
                    Pay = Math.Round(x.MetricsDiff.Value.Pay, 2),
                    ParkingCostPerMonth = Math.Round(x.MetricsDiff.Value.ParkingCostPerMonth, 2),
                    Salary = Math.Round(x.MetricsDiff.Value.Salary, 2),
                    AccountingPerMonth = Math.Round(x.MetricsDiff.Value.AccountingPerMonth, 2),
                    HourlyCostFact = Math.Round(x.MetricsDiff.Value.HourlyCostFact, 2),
                    HourlyCostHand = Math.Round(x.MetricsDiff.Value.HourlyCostHand, 2),
                    Earnings = Math.Round(x.MetricsDiff.Value.Earnings, 2),
                    Expenses = Math.Round(x.MetricsDiff.Value.Expenses, 2),
                    IncomeTaxContributions = Math.Round(x.MetricsDiff.Value.IncomeTaxContributions, 2),
                    DistrictCoefficient = Math.Round(x.MetricsDiff.Value.DistrictCoefficient, 2),
                    PensionContributions = Math.Round(x.MetricsDiff.Value.PensionContributions, 2),
                    MedicalContributions = Math.Round(x.MetricsDiff.Value.MedicalContributions, 2),
                    SocialInsuranceContributions = Math.Round(x.MetricsDiff.Value.SocialInsuranceContributions, 2),
                    InjuriesContributions = Math.Round(x.MetricsDiff.Value.InjuriesContributions, 2),
                    Profit = Math.Round(x.MetricsDiff.Value.Profit, 2),
                    ProfitAbility = Math.Round(x.MetricsDiff.Value.ProfitAbility, 2),
                    GrossSalary = Math.Round(x.MetricsDiff.Value.GrossSalary, 2),
                    Prepayment = Math.Round(x.MetricsDiff.Value.Prepayment, 2),
                    NetSalary = Math.Round(x.MetricsDiff.Value.NetSalary, 2),
                } : null
            });

            var newTotalMetrics = metricsChanges.NewTotalMetrics;
            var totalMetricsDiff = metricsChanges.TotalMetricsDiff;

            Total = new TotalChangesDto
            {
                Metrics = new TotalChangesEntryDto
                {
                    ParkingCostPerMonth = Math.Round(newTotalMetrics.ParkingCostPerMonth, 2),
                    AccountingPerMonth = Math.Round(newTotalMetrics.AccountingPerMonth, 2),
                    Earnings = Math.Round(newTotalMetrics.Earnings, 2),
                    Expenses = Math.Round(newTotalMetrics.Expenses, 2),
                    IncomeTaxContributions = Math.Round(newTotalMetrics.IncomeTaxContributions, 2),
                    PensionContributions = Math.Round(newTotalMetrics.PensionContributions, 2),
                    MedicalContributions = Math.Round(newTotalMetrics.MedicalContributions, 2),
                    SocialInsuranceContributions = Math.Round(newTotalMetrics.SocialInsuranceContributions, 2),
                    InjuriesContributions = Math.Round(newTotalMetrics.InjuriesContributions, 2),
                    Profit = Math.Round(newTotalMetrics.Profit, 2),
                    ProfitAbility = Math.Round(newTotalMetrics.ProfitAbility, 2),
                    Prepayment = Math.Round(newTotalMetrics.Prepayment, 2),
                    NetSalary = Math.Round(newTotalMetrics.NetSalary, 2),
                },
                MetricsDiff = new TotalChangesEntryDto
                {
                    ParkingCostPerMonth = Math.Round(totalMetricsDiff.ParkingCostPerMonth, 2),
                    AccountingPerMonth = Math.Round(totalMetricsDiff.AccountingPerMonth, 2),
                    Earnings = Math.Round(totalMetricsDiff.Earnings, 2),
                    Expenses = Math.Round(totalMetricsDiff.Expenses, 2),
                    IncomeTaxContributions = Math.Round(totalMetricsDiff.IncomeTaxContributions, 2),
                    PensionContributions = Math.Round(totalMetricsDiff.PensionContributions, 2),
                    MedicalContributions = Math.Round(totalMetricsDiff.MedicalContributions, 2),
                    SocialInsuranceContributions = Math.Round(totalMetricsDiff.SocialInsuranceContributions, 2),
                    InjuriesContributions = Math.Round(totalMetricsDiff.InjuriesContributions, 2),
                    Profit = Math.Round(totalMetricsDiff.Profit, 2),
                    ProfitAbility = Math.Round(totalMetricsDiff.ProfitAbility, 2),
                    Prepayment = Math.Round(totalMetricsDiff.Prepayment, 2),
                    NetSalary = Math.Round(totalMetricsDiff.NetSalary, 2),
                },
            };
        }
    }

    public class RowChangesDto
    {
        public string EmployeeId { get; init; }

        public EmployeeMetricsRowDto Metrics { get; init; }

        public EmployeeMetricsDiffDto? MetricsDiff { get; init; }
    }

    public class EmployeeMetricsRowDto
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

    public class TotalChangesDto
    {
        public TotalChangesEntryDto Metrics { get; init; }

        public TotalChangesEntryDto MetricsDiff { get; init; }
    }

    public class TotalChangesEntryDto
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
