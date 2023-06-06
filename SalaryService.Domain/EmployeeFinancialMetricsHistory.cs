using NodaTime;
using Period = SalaryService.Domain.Common.Period;

namespace SalaryService.Domain;

public class EmployeeFinancialMetricsHistory : IIdentityEntity
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public Employee Employee { get; set; }
    
    public Period Period { get; set; }

    public decimal Salary { get; set; }

    public decimal HourlyCostFact { get; set; }

    public decimal HourlyCostHand { get; set; }

    public decimal Earnings { get; set; }

    public decimal IncomeTaxContributions { get; set; }

    public decimal PensionContributions { get; set; }

    public decimal MedicalContributions { get; set; }

    public decimal SocialInsuranceContributions { get; set; }

    public decimal InjuriesContributions { get; set; }

    public decimal Expenses { get; set; }

    public decimal Profit { get; set; }

    public decimal ProfitAbility { get; set; }

    public decimal GrossSalary { get; set; }

    public decimal NetSalary { get; set; }

    public decimal RatePerHour { get; set; }

    public decimal Pay { get; set; }

    public decimal Prepayment { get; set; }

    public decimal EmploymentType { get; set; }

    public decimal ParkingCostPerMonth { get; set; }

    public decimal AccountingPerMonth { get; set; }

    public EmployeeFinancialMetricsHistory(Employee employee, Instant utcNow)
    {
        var financialMetrics = employee.FinancialMetrics;

        EmployeeId = employee.Id;
        Period = new Period(financialMetrics.CreatedAtUtc, utcNow);
        Salary = financialMetrics.Salary;
        HourlyCostFact = financialMetrics.HourlyCostFact;
        HourlyCostHand = financialMetrics.HourlyCostHand;
        Earnings = financialMetrics.Earnings;
        IncomeTaxContributions = financialMetrics.IncomeTaxContributions;
        PensionContributions = financialMetrics.PensionContributions;
        MedicalContributions = financialMetrics.MedicalContributions;
        SocialInsuranceContributions = financialMetrics.SocialInsuranceContributions;
        InjuriesContributions = financialMetrics.InjuriesContributions;
        Expenses = financialMetrics.Expenses;
        Profit = financialMetrics.Profit;
        ProfitAbility = financialMetrics.ProfitAbility;
        GrossSalary = financialMetrics.GrossSalary;
        NetSalary = financialMetrics.NetSalary;
        RatePerHour = financialMetrics.RatePerHour;
        Pay = financialMetrics.Pay;
        Prepayment = financialMetrics.Prepayment;
        EmploymentType = financialMetrics.EmploymentType;
        ParkingCostPerMonth = financialMetrics.ParkingCostPerMonth;
        AccountingPerMonth = financialMetrics.AccountingPerMonth;
    }

    private EmployeeFinancialMetricsHistory() { }
}
