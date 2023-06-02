using NodaTime;
using SalaryService.Domain.Common;

namespace SalaryService.Domain;

public class FinancialMetrics
{
    public decimal Salary { get; private set; }

    public decimal HourlyCostFact { get; private set; }

    public decimal HourlyCostHand { get; private set; }

    public decimal Earnings { get; private set; }

    public decimal DistrictCoefficient { get; private set; }

    public decimal IncomeTaxContributions { get; private set; }

    public decimal PensionContributions { get; private set; }

    public decimal MedicalContributions { get; private set; }

    public decimal SocialInsuranceContributions { get; private set; }

    public decimal InjuriesContributions { get; private set; }

    public decimal Expenses { get; private set; }

    public decimal Profit { get; private set; }

    public decimal ProfitAbility { get; private set; }

    public decimal GrossSalary { get; private set; }

    public decimal NetSalary { get; private set; }

    public decimal RatePerHour { get; private set; }

    public decimal Pay { get; private set; }

    public decimal Prepayment { get; private set; }

    public decimal EmploymentType { get; private set; }

    public decimal ParkingCostPerMonth { get; private set; }

    public decimal AccountingPerMonth { get; private set; }

    public bool IsEmployedOfficially { get; private set; }

    public Instant CreatedAtUtc { get; private set; }

    private CoefficientOptions Coefficients { get; init; }

    private WorkingPlan WorkingPlan { get; init; }

    private readonly List<decimal> _availableEmploymentTypes = new() { 0.5m, 1 };

    public FinancialMetrics(FinancesForPayroll financesForPayroll, bool isEmployedOfficially,
        CoefficientOptions coefficients, WorkingPlan workingPlan, Instant createdAtUtc)
    {
        if (!_availableEmploymentTypes.Contains(financesForPayroll.EmploymentType))
            throw new ArgumentException(
                $"Employment type can accept only the following values: {string.Join(",", _availableEmploymentTypes)}");

        RatePerHour = financesForPayroll.RatePerHour;
        Pay = financesForPayroll.Pay;
        EmploymentType = financesForPayroll.EmploymentType;
        ParkingCostPerMonth = financesForPayroll.ParkingCostPerMonth;
        IsEmployedOfficially = isEmployedOfficially;
        Coefficients = coefficients;
        WorkingPlan = workingPlan;
        CreatedAtUtc = createdAtUtc;

        if (IsEmployedOfficially)
            CalculateMetrics();
        else
            CalculateUnofficialEmployeeMetrics();
    }

    private void CalculateUnofficialEmployeeMetrics()
    {
        AccountingPerMonth = 0;
        Salary = CalculateSalary();
        GrossSalary = 0;
        NetSalary = 0;
        DistrictCoefficient = 0;
        Earnings = CalculateEarnings();
        IncomeTaxContributions = 0;
        PensionContributions = 0;
        MedicalContributions = 0;
        SocialInsuranceContributions = 0;
        InjuriesContributions = 0;
        Expenses = CalculateExpenses();
        HourlyCostFact = CalculateHourlyCostFact();
        HourlyCostHand = CalculateHourlyCostHand();
        Prepayment = 0;
        Profit = CalculateProfit();
        ProfitAbility = CalculateProfitability();
    }

    private void CalculateMetrics()
    {
        AccountingPerMonth = ThirdPartyServicesPriceConsts.AccountingPerMonth;
        Salary = CalculateSalary();
        GrossSalary = CalculateGrossSalary();
        NetSalary = CalculateNetSalary();
        DistrictCoefficient = CalculateDistrictCoefficient();
        Earnings = CalculateEarnings();
        IncomeTaxContributions = GetNdflValue();
        PensionContributions = GetPensionContributions();
        MedicalContributions = GetMedicalContributions();
        SocialInsuranceContributions = GetSocialInsuranceContributions();
        InjuriesContributions = GetInjuriesContributions();
        Expenses = CalculateExpenses();
        HourlyCostFact = CalculateHourlyCostFact();
        HourlyCostHand = CalculateHourlyCostHand();
        Prepayment = CalculatePrepayment();
        Profit = CalculateProfit();
        ProfitAbility = CalculateProfitability();
    }

    private decimal CalculateDistrictCoefficient()
    {
        return Salary * Coefficients.DistrictCoefficient;
    }

    private decimal CalculateHourlyCostFact()
    {
        return Expenses / WorkingPlan.WorkingHoursInMonth;
    }

    private decimal CalculatePrepayment()
    {
        return NetSalary / 2;
    }

    private decimal CalculateHourlyCostHand()
    {
        return Salary / 160;
    }

    private decimal CalculateEarnings()
    {
        return RatePerHour * WorkingPlan.WorkingHoursInMonth * EmploymentType;
    }

    private decimal CalculateExpenses()
    {
        return IncomeTaxContributions +
               NetSalary +
               PensionContributions +
               MedicalContributions +
               SocialInsuranceContributions +
               InjuriesContributions +
               AccountingPerMonth +
               ParkingCostPerMonth;
    }

    private decimal GetNdflValue()
    {
        return GrossSalary * 0.13m;
    }

    private decimal GetPensionContributions()
    {
        return Coefficients.MinimumWage * 0.22m + (GrossSalary - Coefficients.MinimumWage) * 0.1m;
    }

    private decimal GetMedicalContributions()
    {
        return Coefficients.MinimumWage * 0.051m + (GrossSalary - Coefficients.MinimumWage) * 0.05m;
    }

    private decimal GetSocialInsuranceContributions()
    {
        return Coefficients.MinimumWage * 0.029m;
    }

    private decimal GetInjuriesContributions()
    {
        return GrossSalary * 0.002m;
    }

    private decimal CalculateProfit()
    {
        return Earnings - Expenses;
    }

    private decimal CalculateProfitability()
    {
        const decimal profitabilityWhenZeroEarnings = -100;

        return Earnings != 0
            ? Profit / Earnings * 100
            : profitabilityWhenZeroEarnings;
    }

    private decimal CalculateGrossSalary()
    {
        return Salary + Salary * Coefficients.DistrictCoefficient;
    }

    private decimal CalculateNetSalary()
    {
        return GrossSalary - GrossSalary * Coefficients.IncomeTaxPercent;
    }

    private decimal CalculateSalary()
    {
        return Pay * EmploymentType;
    }

    private FinancialMetrics()
    {
    }
}