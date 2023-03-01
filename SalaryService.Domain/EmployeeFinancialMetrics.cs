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

        public decimal Salary { get; set; }
        
        public decimal HourlyCostFact { get; set; }

        public decimal HourlyCostHand { get; set; }

        public decimal Earnings { get; set; }

        public decimal DistrictCoefficient { get; set; }

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
        public EmployeeFinancialMetrics() { }

        public EmployeeFinancialMetrics(decimal ratePerHour, decimal pay, decimal employmentType, decimal parkingCostPerMonth)
        {
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            ParkingCostPerMonth = parkingCostPerMonth;
            AccountingPerMonth = ThirdPartyServicesPriceConsts.AccountingPerMonth;
        }

        public void CalculateMetrics(decimal districtCoeff,
            decimal mrot,
            decimal tax,
            decimal workingHoursInMonth,
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

        public void Update(decimal salary,
            decimal grossSalary,
            decimal netSalary,
            decimal districtCoefficient,
            decimal earnings,
            decimal incomeTaxContributions,
            decimal pensionContributions,
            decimal medicalContributions,
            decimal socialInsuranceContributions,
            decimal injuriesContributions,
            decimal expenses,
            decimal hourlyCostFact,
            decimal hourlyCostHand,
            decimal prepayment,
            decimal profit,
            decimal profitability,
            decimal ratePerHour,
            decimal pay,
            decimal employmentType,
            decimal parkingCostPerMonth,
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

        private decimal CalculateDistrictCoefficient(decimal districtCoeff)
        {
            return Salary * districtCoeff;
        }

        private decimal CalculateHourlyCostFact(decimal workingHoursInMonth)
        {
            return Expenses / workingHoursInMonth;
        }

        private decimal CalculatePrepayment()
        {
            return NetSalary / 2;
        }

        private decimal CalculateHourlyCostHand()
        {
            return Salary / 160;
        }

        private decimal CalculateEarnings(decimal workingHoursInMonth)
        {
            return RatePerHour * workingHoursInMonth * EmploymentType;
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

        private decimal GetPensionContributions(decimal mrot)
        {
            return mrot * 0.22m + (GrossSalary - mrot) * 0.1m;
        }

        private decimal GetMedicalContributions(decimal mrot)
        {
            return mrot * 0.051m + (GrossSalary - mrot) * 0.05m;
        }

        private decimal GetSocialInsuranceContributions(decimal mrot)
        {
            return mrot * 0.029m;
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

        private decimal CalculateGrossSalary(decimal districtCoeff)
        {
            return Salary + Salary * districtCoeff;
        }

        private decimal CalculateNetSalary(decimal tax)
        {
            return GrossSalary - GrossSalary * tax;
        }

        private decimal CalculateSalary()
        {
            return Pay * EmploymentType;
        }
    }
}

