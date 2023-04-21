namespace SalaryService.Application.Dtos;

public class AnalyticDto
{
    public long Id { get; private set; }

    public string FullName { get; private set; }

    public decimal Pay { get; private set; }

    public decimal RatePerHour { get; private set; }

    public decimal EmploymentType { get; private set; }

    public decimal Salary { get; private set; }

    public decimal ParkingCostPerMonth { get; private set; }

    public decimal AccountingPerMonth { get; private set; }

    public decimal HourlyCostFact { get; private set; }

    public decimal HourlyCostHand { get; private set; }

    public decimal Earnings { get; private set; }

    public decimal IncomeTaxContributions { get; private set; }

    public decimal DistrictCoefficient { get; private set; }

    public decimal PensionContributions { get; set; }

    public decimal MedicalContributions { get; set; }

    public decimal SocialInsuranceContributions { get; set; }

    public decimal InjuriesContributions { get; set; }

    public decimal Expenses { get; private set; }

    public decimal Profit { get; private set; }

    public decimal ProfitAbility { get; private set; }

    public decimal GrossSalary { get; private set; }

    public decimal Prepayment { get; private set; }

    public decimal NetSalary { get; private set; }

    public AnalyticDto(long id,
        string name,
        string surname,
        string middleName,
        decimal pay,
        decimal ratePerHour,
        decimal employmentType,
        decimal salary,
        decimal parkingCostPerMonth,
        decimal accountingPerMonth,
        decimal hourlyCostFact,
        decimal hourlyCostHand,
        decimal earnings,
        decimal incomeTaxContributions,
        decimal districtCoefficient,
        decimal pensionContributions,
        decimal medicalContributions,
        decimal socialInsuranceContributions,
        decimal injuriesContributions,
        decimal expenses,
        decimal profit,
        decimal profitAbility,
        decimal grossSalary,
        decimal prepayment,
        decimal netSalary)
    {
        Id = id;
        FullName = name + " " + surname + " " + middleName;
        Pay = pay;
        RatePerHour = ratePerHour;
        EmploymentType = employmentType;
        Salary = salary;
        ParkingCostPerMonth = parkingCostPerMonth;
        AccountingPerMonth = accountingPerMonth;
        HourlyCostFact = hourlyCostFact;
        HourlyCostHand = hourlyCostHand;
        Earnings = earnings;
        IncomeTaxContributions = incomeTaxContributions;
        DistrictCoefficient = districtCoefficient;
        PensionContributions = pensionContributions;
        MedicalContributions = medicalContributions;
        SocialInsuranceContributions = socialInsuranceContributions;
        InjuriesContributions = injuriesContributions;
        Expenses = expenses;
        Profit = profit;
        ProfitAbility = profitAbility;
        GrossSalary = grossSalary;
        Prepayment = prepayment;
        NetSalary = netSalary;
    }
}
