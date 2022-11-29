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

        private double salary;
        public double Salary
        {
            get { return salary; }
            set
            {
                if (value >= 0)
                    salary = value;
                else
                    throw new ArgumentException();
            }
        }

        private double hourlyCostFact;
        public double HourlyCostFact
        {
            get { return hourlyCostFact; }
            set
            {
                if (value >= 0)
                    hourlyCostFact = value;
                else
                    throw new ArgumentException();
            }
        }

        private double hourlyCostHand;
        public double HourlyCostHand
        {
            get { return hourlyCostHand; }
            set
            {
                if (value >= 0)
                    hourlyCostHand = value;
                else
                    throw new ArgumentException();
            }
        }

        private double earnings;
        public double Earnings
        {
            get { return earnings; }
            set
            {
                if (value >= 0)
                    earnings = value;
                else
                    throw new ArgumentException();
            }
        }

        private double incomeTaxContributions;
        public double IncomeTaxContributions
        {
            get { return incomeTaxContributions; }
            set
            {
                if (value >= 0)
                    incomeTaxContributions = value;
                else
                    throw new ArgumentException();
            }
        }

        private double pensionContributions;
        public double PensionContributions
        {
            get { return pensionContributions; }
            set
            {
                if (value >= 0)
                    pensionContributions = value;
                else
                    throw new ArgumentException();
            }
        }

        private double medicalContributions;
        public double MedicalContributions
        {
            get { return medicalContributions; }
            set
            {
                if (value >= 0)
                    medicalContributions = value;
                else
                    throw new ArgumentException();
            }
        }

        private double socialInsuranceContributions;
        public double SocialInsuranceContributions
        {
            get { return socialInsuranceContributions; }
            set
            {
                if (value >= 0)
                    socialInsuranceContributions = value;
                else
                    throw new ArgumentException();
            }
        }

        private double injuriesContributions;
        public double InjuriesContributions
        {
            get { return injuriesContributions; }
            set
            {
                if (value >= 0)
                    injuriesContributions = value;
                else
                    throw new ArgumentException();
            }
        }

        private double expenses;
        public double Expenses
        {
            get { return expenses; }
            set
            {
                if (value >= 0)
                    expenses = value;
                else
                    throw new ArgumentException();
            }
        }

        private double profit;
        public double Profit
        {
            get { return profit; }
            set
            {
                if (value >= 0)
                    profit = value;
                else
                    throw new ArgumentException();
            }
        }

        private double profitability;
        public double ProfitAbility
        {
            get { return profitability; }
            set
            {
                if (value >= 0)
                    profitability = value;
                else
                    throw new ArgumentException();
            }
        }

        private double grossSalary;
        public double GrossSalary
        {
            get { return grossSalary; }
            set
            {
                if (value >= 0)
                    grossSalary = value;
                else
                    throw new ArgumentException();
            }
        }

        private double netSalary;
        public double NetSalary
        {
            get { return netSalary; }
            set
            {
                if (value >= 0)
                    netSalary = value;
                else
                    throw new ArgumentException();
            }
        }

        private double ratePerHour;

        public double RatePerHour
        {
            get { return ratePerHour; }
            set
            {
                if (value >= 0)
                    ratePerHour = value;
                else
                    throw new ArgumentException();
            }
        }

        private double pay;

        public double Pay
        {
            get { return pay; }
            set
            {
                if (value >= 0)
                    pay = value;
                else
                    throw new ArgumentException();
            }
        }

        private double prepayment;
        public double Prepayment
        {
            get { return prepayment; }
            set
            {
                if (value >= 0)
                    prepayment = value;
                else
                    throw new ArgumentException();
            }
        }

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
            Instant actualFromUtc)
        {
            ActualFromUtc = actualFromUtc;
            Salary = CalculateSalary();
            GrossSalary = CalculateGrossSalary(districtCoeff);
            NetSalary = CalculateNetSalary(tax);
            Earnings = CalculateEarnings();
            IncomeTaxContributions = GetNdflValue();
            PensionContributions = GetPensionContributions(mrot);
            MedicalContributions = GetMedicalContributions(mrot);
            SocialInsuranceContributions = GetSocialInsuranceContributions(mrot);
            InjuriesContributions = GetInjuriesContributions();
            Expenses = CalculateExpenses();
            HourlyCostFact = CalculateHourlyCostFact();
            HourlyCostHand = CalculateHourlyCostHand();
            Prepayment = CalculatePrepayment();
            Profit = CalculateProfit();
            ProfitAbility = CalculateProfitability();
        }

        public void Update(double salary,
            double grossSalary,
            double netSalary,
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

        private double CalculateHourlyCostFact()
        {
            return Expenses / WorkingPlanConsts.WorkingHoursInMonth;
        }

        private double CalculatePrepayment()
        {
            return NetSalary / 2;
        }

        private double CalculateHourlyCostHand()
        {
            return Salary / 160;
        }

        private double CalculateEarnings()
        {
            return RatePerHour * WorkingPlanConsts.WorkingHoursInMonth * EmploymentType;
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

