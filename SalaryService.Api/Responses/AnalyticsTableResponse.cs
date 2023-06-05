using SalaryService.Domain;

namespace SalaryService.Api.Responses;

public class AnalyticsTableResponse
{
    public IEnumerable<RowDto> Rows { get; init; }

    public TotalDto Total { get; init; }

    public AnalyticsTableResponse(IEnumerable<Employee> employees, TotalEmployeeFinancialMetricsEntry employeesTotalFinancialMetrics)
    {
        Rows = employees.Select(x => new RowDto
        {
            EmployeeId = x.Id.ToString(),
            EmployeeFullName = x.GetFullName(),
            IsCopy = false,
            Metrics = new EmployeeMetricsRowDto(x.IsEmployedOfficially, x.FinancialMetrics),
            MetricsDiff = null
        });

        Total = new TotalDto
        {
            Metrics = new TotalMetricsDto(employeesTotalFinancialMetrics),
            MetricsDiff = null
        };
    }

    public AnalyticsTableResponse(AnalyticsMetricChanges metricsChanges)
    {
        Rows = metricsChanges.MetricsRowsChanges.Select(x => new RowDto
        {
            EmployeeId = x.EmployeeId.ToString(),
            EmployeeFullName = x.EmployeeFullName,
            IsCopy = x.IsCopy,
            Metrics = new EmployeeMetricsRowDto(x.IsEmployedOfficially, x.NewMetrics),
            MetricsDiff = x.MetricsDiff.HasValue ? new EmployeeMetricsDiffDto(x.MetricsDiff.Value) : null
        });

        Total = new TotalDto
        {
            Metrics = new TotalMetricsDto(metricsChanges.NewTotalMetrics),
            MetricsDiff = new TotalMetricsDto(metricsChanges.TotalMetricsDiff)
        };
    }
}

public class RowDto
{
    public string EmployeeId { get; init; }

    public string EmployeeFullName { get; init; }

    public bool IsCopy { get; init; }

    public EmployeeMetricsRowDto Metrics { get; init; }

    public EmployeeMetricsDiffDto? MetricsDiff { get; init; }
}

public class EmployeeMetricsRowDto
{
    public decimal? RatePerHour { get; init; }

    public decimal? Pay { get; init; }

    public decimal? EmploymentType { get; init; }

    public decimal? ParkingCostPerMonth { get; init; }

    public decimal? Salary { get; init; }

    public decimal? AccountingPerMonth { get; init; }

    public decimal? HourlyCostFact { get; init; }

    public decimal? HourlyCostHand { get; init; }

    public decimal? Earnings { get; init; }

    public decimal? Expenses { get; init; }

    public decimal? IncomeTaxContributions { get; init; }

    public decimal? DistrictCoefficient { get; init; }

    public decimal? PensionContributions { get; init; }

    public decimal? MedicalContributions { get; init; }

    public decimal? SocialInsuranceContributions { get; init; }

    public decimal? InjuriesContributions { get; init; }

    public decimal? Profit { get; init; }

    public decimal? ProfitAbility { get; init; }

    public decimal? GrossSalary { get; init; }

    public decimal? Prepayment { get; init; }

    public decimal? NetSalary { get; init; }

    public bool IsEmployedOfficially { get; init; }

    public EmployeeMetricsRowDto(bool isEmployedOfficially, FinancialMetrics? employeeFinancialMetrics)
    {
        IsEmployedOfficially = isEmployedOfficially;

        if (employeeFinancialMetrics != null)
        {
            Pay = Math.Round(employeeFinancialMetrics.Pay, 2);
            RatePerHour = Math.Round(employeeFinancialMetrics.RatePerHour, 2);
            EmploymentType = Math.Round(employeeFinancialMetrics.EmploymentType, 2);
            Salary = Math.Round(employeeFinancialMetrics.Salary, 2);
            ParkingCostPerMonth = Math.Round(employeeFinancialMetrics.ParkingCostPerMonth, 2);
            AccountingPerMonth = Math.Round(employeeFinancialMetrics.AccountingPerMonth, 2);
            HourlyCostFact = Math.Round(employeeFinancialMetrics.HourlyCostFact, 2);
            HourlyCostHand = Math.Round(employeeFinancialMetrics.HourlyCostHand, 2);
            Earnings = Math.Round(employeeFinancialMetrics.Earnings, 2);
            IncomeTaxContributions = Math.Round(employeeFinancialMetrics.IncomeTaxContributions, 2);
            DistrictCoefficient = Math.Round(employeeFinancialMetrics.DistrictCoefficient, 2);
            PensionContributions = Math.Round(employeeFinancialMetrics.PensionContributions, 2);
            MedicalContributions = Math.Round(employeeFinancialMetrics.MedicalContributions, 2);
            SocialInsuranceContributions = Math.Round(employeeFinancialMetrics.SocialInsuranceContributions, 2);
            InjuriesContributions = Math.Round(employeeFinancialMetrics.InjuriesContributions, 2);
            Expenses = Math.Round(employeeFinancialMetrics.Expenses, 2);
            Profit = Math.Round(employeeFinancialMetrics.Profit, 2);
            ProfitAbility = Math.Round(employeeFinancialMetrics.ProfitAbility, 2);
            GrossSalary = Math.Round(employeeFinancialMetrics.GrossSalary, 2);
            Prepayment = Math.Round(employeeFinancialMetrics.Prepayment, 2);
            NetSalary = Math.Round(employeeFinancialMetrics.NetSalary, 2);
        }
    }
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

    public EmployeeMetricsDiffDto(EmployeeFinancialMetricsDiff metricsDiff)
    {
        RatePerHour = Math.Round(metricsDiff.RatePerHour, 2);
        Pay = Math.Round(metricsDiff.Pay, 2);
        ParkingCostPerMonth = Math.Round(metricsDiff.ParkingCostPerMonth, 2);
        Salary = Math.Round(metricsDiff.Salary, 2);
        AccountingPerMonth = Math.Round(metricsDiff.AccountingPerMonth, 2);
        HourlyCostFact = Math.Round(metricsDiff.HourlyCostFact, 2);
        HourlyCostHand = Math.Round(metricsDiff.HourlyCostHand, 2);
        Earnings = Math.Round(metricsDiff.Earnings, 2);
        Expenses = Math.Round(metricsDiff.Expenses, 2);
        IncomeTaxContributions = Math.Round(metricsDiff.IncomeTaxContributions, 2);
        DistrictCoefficient = Math.Round(metricsDiff.DistrictCoefficient, 2);
        PensionContributions = Math.Round(metricsDiff.PensionContributions, 2);
        MedicalContributions = Math.Round(metricsDiff.MedicalContributions, 2);
        SocialInsuranceContributions = Math.Round(metricsDiff.SocialInsuranceContributions, 2);
        InjuriesContributions = Math.Round(metricsDiff.InjuriesContributions, 2);
        Profit = Math.Round(metricsDiff.Profit, 2);
        ProfitAbility = Math.Round(metricsDiff.ProfitAbility, 2);
        GrossSalary = Math.Round(metricsDiff.GrossSalary, 2);
        Prepayment = Math.Round(metricsDiff.Prepayment, 2);
        NetSalary = Math.Round(metricsDiff.NetSalary, 2);
    }
}

public class TotalDto
{
    public TotalMetricsDto Metrics { get; init; }

    public TotalMetricsDto? MetricsDiff { get; init; }
}

public class TotalMetricsDto
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

    public TotalMetricsDto(TotalEmployeeFinancialMetricsEntry totalMetrics)
    {
        ParkingCostPerMonth = Math.Round(totalMetrics.ParkingCostPerMonth, 2);
        AccountingPerMonth = Math.Round(totalMetrics.AccountingPerMonth, 2);
        Earnings = Math.Round(totalMetrics.Earnings, 2);
        Expenses = Math.Round(totalMetrics.Expenses, 2);
        IncomeTaxContributions = Math.Round(totalMetrics.IncomeTaxContributions, 2);
        PensionContributions = Math.Round(totalMetrics.PensionContributions, 2);
        MedicalContributions = Math.Round(totalMetrics.MedicalContributions, 2);
        SocialInsuranceContributions = Math.Round(totalMetrics.SocialInsuranceContributions, 2);
        InjuriesContributions = Math.Round(totalMetrics.InjuriesContributions, 2);
        Profit = Math.Round(totalMetrics.Profit, 2);
        ProfitAbility = Math.Round(totalMetrics.ProfitAbility, 2);
        Prepayment = Math.Round(totalMetrics.Prepayment, 2);
        NetSalary = Math.Round(totalMetrics.NetSalary, 2);
    }

    public TotalMetricsDto(EmployeeFinancialTotalMetricsDiff totalMetricsDiff)
    {
        ParkingCostPerMonth = Math.Round(totalMetricsDiff.ParkingCostPerMonth, 2);
        AccountingPerMonth = Math.Round(totalMetricsDiff.AccountingPerMonth, 2);
        Earnings = Math.Round(totalMetricsDiff.Earnings, 2);
        Expenses = Math.Round(totalMetricsDiff.Expenses, 2);
        IncomeTaxContributions = Math.Round(totalMetricsDiff.IncomeTaxContributions, 2);
        PensionContributions = Math.Round(totalMetricsDiff.PensionContributions, 2);
        MedicalContributions = Math.Round(totalMetricsDiff.MedicalContributions, 2);
        SocialInsuranceContributions = Math.Round(totalMetricsDiff.SocialInsuranceContributions, 2);
        InjuriesContributions = Math.Round(totalMetricsDiff.InjuriesContributions, 2);
        Profit = Math.Round(totalMetricsDiff.Profit, 2);
        ProfitAbility = Math.Round(totalMetricsDiff.ProfitAbility, 2);
        Prepayment = Math.Round(totalMetricsDiff.Prepayment, 2);
        NetSalary = Math.Round(totalMetricsDiff.NetSalary, 2);
    }
}
