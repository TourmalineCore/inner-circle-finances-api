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

        public double Earnings { get; set; } //Доход

        public double DistrictCoefficient { get; set; } //Рай.коэф.

        public double IncomeTaxContributions { get; set; } //НДФЛ

        public double PensionContributions { get; set; } //ОПС

        public double MedicalContributions { get; set; } //ОМС

        public double SocialInsuranceContributions { get; set; } //ОСС

        public double InjuriesContributions { get; set; } //Взносы на травматизм

        public double Expenses { get; set; } //Расход

        public double Profit { get; set; } //Прибыль

        public double ProfitAbility { get; set; } //Рентабельность

        public double GrossSalary { get; set; } //Зарплата до вычета НДФЛ

        public double NetSalary { get; set; } //Зарплата

        public double RatePerHour { get; set; } 

        public double Pay { get; set; }

        public double Prepayment { get; set; } //Аванас 

        public double EmploymentType { get; set; }

        public double ParkingCostPerMonth { get; set; }

        public double AccountingPerMonth { get; set; }
        public EmployeeFinancialMetrics() { }

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
            return Math.Round(Salary * districtCoeff, 2);
        }

        private double CalculateHourlyCostFact(double workingHoursInMonth)
        {
            return Math.Round(Expenses / workingHoursInMonth, 2);
        }

        private double CalculatePrepayment()
        {
            return Math.Round(NetSalary / 2, 2);
        }

        private double CalculateHourlyCostHand()
        {
            return Math.Round(Salary / 160, 2);
        }

        private double CalculateEarnings(double workingHoursInMonth)
        {
            return Math.Round(RatePerHour * workingHoursInMonth * EmploymentType, 2);
        }

        private double CalculateExpenses()
        {
            return Math.Round(
                IncomeTaxContributions +
                NetSalary +
                PensionContributions +
                MedicalContributions +
                SocialInsuranceContributions +
                InjuriesContributions +
                AccountingPerMonth +
                ParkingCostPerMonth,
                2
            );
        }

        private double GetNdflValue()
        {
            return Math.Round(GrossSalary * 0.13, 2);
        }

        private double GetPensionContributions(double mrot)
        {
            return Math.Round(mrot * 0.22 + (GrossSalary - mrot) * 0.1, 2);
        }

        private double GetMedicalContributions(double mrot)
        {
            return Math.Round(mrot * 0.051 + (GrossSalary - mrot) * 0.05, 2);
        }

        private double GetSocialInsuranceContributions(double mrot)
        {
            return Math.Round(mrot * 0.029, 2);
        }

        private double GetInjuriesContributions()
        {
            return Math.Round(GrossSalary * 0.002, 2);
        }

        private double CalculateProfit()
        {
            return Math.Round(Earnings - Expenses, 2);
        }

        private double CalculateProfitability()
        {
            const double profitabilityWhenZeroEarnings = -100;

            return Math.Round(Earnings != 0
                ? Profit / Earnings * 100
                : profitabilityWhenZeroEarnings, 2);
        }

        private double CalculateGrossSalary(double districtCoeff)
        {
            return Math.Round(Salary + Salary * districtCoeff, 2);
        }

        private double CalculateNetSalary(double tax)
        {
            return Math.Round(GrossSalary - GrossSalary * tax, 2);
        }

        private double CalculateSalary()
        {
            return Math.Round(Pay * EmploymentType, 2);
        }
    }
}

