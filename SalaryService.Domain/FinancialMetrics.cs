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

    private CoefficientOptions _coefficients { get; init; }

    private WorkingPlan _workingPlan { get; init; }

    private readonly List<decimal> _availableEmploymentTypes = new() { 0.5m, 1 };

    public FinancialMetrics(FinancesForPayroll financesForPayroll, CoefficientOptions coefficients, WorkingPlan workingPlan, Instant createdAtUtc)
    {
        if (!_availableEmploymentTypes.Contains(financesForPayroll.EmploymentType))
        {
            throw new ArgumentException($"Employment type can accept only the following values: {string.Join(",", _availableEmploymentTypes)}");
        }

        RatePerHour = financesForPayroll.RatePerHour;
        Pay = financesForPayroll.Pay;
        EmploymentType = financesForPayroll.EmploymentType;
        ParkingCostPerMonth = financesForPayroll.ParkingCostPerMonth;
        IsEmployedOfficially = financesForPayroll.IsEmployedOfficially;
        AccountingPerMonth = ThirdPartyServicesPriceConsts.AccountingPerMonth;
        _coefficients = coefficients;
        _workingPlan = workingPlan;
        CalculateMetrics();
        CreatedAtUtc = createdAtUtc;
    }

    protected void CalculateMetrics()
    {
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

    protected virtual decimal CalculateDistrictCoefficient() => Salary * _coefficients.DistrictCoefficient;

    protected decimal CalculateHourlyCostFact() => Expenses / _workingPlan.WorkingHoursInMonth;

    protected virtual decimal CalculatePrepayment() => NetSalary / 2;

    protected decimal CalculateHourlyCostHand() => Salary / 160;

    protected decimal CalculateEarnings() => RatePerHour * _workingPlan.WorkingHoursInMonth * EmploymentType;

    protected decimal CalculateExpenses()
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

    protected virtual decimal GetNdflValue() => GrossSalary * 0.13m;

    protected virtual decimal GetPensionContributions() => _coefficients.MinimumWage * 0.22m + (GrossSalary - _coefficients.MinimumWage) * 0.1m;

    protected virtual decimal GetMedicalContributions() => _coefficients.MinimumWage * 0.051m + (GrossSalary - _coefficients.MinimumWage) * 0.05m;

    protected virtual decimal GetSocialInsuranceContributions() => _coefficients.MinimumWage * 0.029m;

    protected virtual decimal GetInjuriesContributions() => GrossSalary * 0.002m;

    protected decimal CalculateProfit() => Earnings - Expenses;

    protected decimal CalculateProfitability()
    {
        const decimal profitabilityWhenZeroEarnings = -100;

        return Earnings != 0
            ? Profit / Earnings * 100
            : profitabilityWhenZeroEarnings;
    }

    protected virtual decimal CalculateGrossSalary() => Salary + Salary * _coefficients.DistrictCoefficient;

    protected virtual decimal CalculateNetSalary() => GrossSalary - GrossSalary * _coefficients.IncomeTaxPercent;

    protected decimal CalculateSalary() => Pay * EmploymentType;

    private FinancialMetrics() { }
}
