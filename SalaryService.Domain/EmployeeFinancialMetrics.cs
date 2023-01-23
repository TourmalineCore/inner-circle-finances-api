using NodaTime;
using SalaryService.Domain.Common;

namespace SalaryService.Domain
{
    public class EmployeeFinancialMetrics : IIdentityEntity
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Instant ActualFromUtc { get; set; }
        public double Salary { get; set; }        
        public double HourlyCostFact { get; set; }
        public double HourlyCostHand { get; set; }
        public double Earnings { get; set; }        
        public double DistrictCoefficient { get; set; }
        public double IncomeTaxContributions { get; set; }
        public double PensionContributions { get; set; }
        public double MedicalContributions { get; set; }
        public double SocialInsuranceContributions { get; set; }
        public double InjuriesContributions { get; set; }
        public double Expenses { get; set; }
        public double Profit { get; set; }
        public double ProfitAbility { get; set; }
        public double GrossSalary { get; set; }
        public double NetSalary { get; set; }
        public double RatePerHour { get; set; }
        public double Pay { get; set; }
        public double Prepayment { get; set; }
        public double EmploymentType { get; set; }
        public double ParkingCostPerMonth { get; set; }
        public double AccountingPerMonth { get; set; }
        public EmployeeFinancialMetrics(double ratePerHour, double pay, double employmentType, double parkingCostPerMonth)
        {
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            ParkingCostPerMonth = parkingCostPerMonth;
            AccountingPerMonth = ThirdPartyServicesPriceConsts.AccountingPerMonth;
        }
        public void CalculateMetrics(double districtCoeff,
            double mrot,
            double tax,
            double workingHoursInMonth,
            Instant actualFromUtc)
        {
            ActualFromUtc = actualFromUtc;
            Salary = CalculateSalary();
            GrossSalary = CalculateGrossSalary(districtCoeff);
            NetSalary = CalculateNetSalary(tax);
            DistrictCoefficient = CalculateDistrictCoefficient(districtCoeff);
            Earnings = CalculateEarnings(workingHoursInMonth);
            IncomeTaxContributions = GetNdflValue();
            PensionContributions = GetPensionContributions(mrot);
            MedicalContributions = GetMedicalContributions(mrot);
            SocialInsuranceContributions = GetSocialInsuranceContributions(mrot);
            InjuriesContributions = GetInjuriesContributions();
            Expenses = CalculateExpenses();
            HourlyCostFact = CalculateHourlyCostFact(workingHoursInMonth);
            HourlyCostHand = CalculateHourlyCostHand();
            Prepayment = CalculatePrepayment();
            Profit = CalculateProfit();
            ProfitAbility = CalculateProfitability();
        }
        public void Update(double salary,
            double grossSalary,
            double netSalary,
            double districtCoefficient,
            double earnings,
            double incomeTaxContributions,
            double pensionContributions,
            double medicalContributions,
            double socialInsuranceContributions,
            double injuriesContributions,
            double expenses,
            double hourlyCostFact,
            double hourlyCostHand,
            double prepayment,
            double profit,
            double profitability,
            double ratePerHour,
            double pay,
            double employmentType,
            double parkingCostPerMonth,
            Instant actualFromUtc)
        {
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            ParkingCostPerMonth = parkingCostPerMonth;
            Salary = salary;
            GrossSalary = grossSalary;
            NetSalary = netSalary;
            DistrictCoefficient = districtCoefficient;
            Earnings = earnings;
            IncomeTaxContributions = incomeTaxContributions;
            PensionContributions = pensionContributions;
            MedicalContributions = medicalContributions;
            SocialInsuranceContributions = socialInsuranceContributions;
            InjuriesContributions = injuriesContributions;
            Expenses = expenses;
            HourlyCostFact = hourlyCostFact;
            HourlyCostHand = hourlyCostHand;
            Prepayment = prepayment;
            Profit = profit;
            ProfitAbility = profitability;
            ActualFromUtc = actualFromUtc;
        }
        private double CalculateDistrictCoefficient(double districtCoeff)
        {
            return Salary * districtCoeff;
        }
        private double CalculateHourlyCostFact(double workingHoursInMonth)
        {
            return Expenses / workingHoursInMonth;
        }
        private double CalculatePrepayment()
        {
            return NetSalary / 2;
        }
        private double CalculateHourlyCostHand()
        {
            return Salary / 160;
        }
        private double CalculateEarnings(double workingHoursInMonth)
        {
            return RatePerHour * workingHoursInMonth * EmploymentType;
        }
        private double CalculateExpenses()
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
        private double GetNdflValue()
        {
            return GrossSalary * 0.13;
        }
        private double GetPensionContributions(double mrot)
        {
            return mrot * 0.22 + (GrossSalary - mrot) * 0.1;
        }
        private double GetMedicalContributions(double mrot)
        {
            return mrot * 0.051 + (GrossSalary - mrot) * 0.05;
        }
        private double GetSocialInsuranceContributions(double mrot)
        {
            return mrot * 0.029;
        }
        private double GetInjuriesContributions()
        {
            return GrossSalary * 0.002;
        }
        private double CalculateProfit()
        {
            return Earnings - Expenses;
        }
        private double CalculateProfitability()
        {
            return (Earnings - Expenses) / Earnings * 100;
        }
        private double CalculateGrossSalary(double districtCoeff)
        {
            return Salary + Salary * districtCoeff;
        }
        private double CalculateNetSalary(double tax)
        {
            return GrossSalary - GrossSalary * tax;
        }
        private double CalculateSalary()
        {
            return Pay * EmploymentType;
        }
    }
}

