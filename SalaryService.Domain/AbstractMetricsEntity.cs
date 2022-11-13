using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Domain
{
    public abstract class AbstractMetricsEntity
    {
        public long Id { get; protected set; }
        [JsonProperty]
        public long EmployeeId { get; protected set; }
        [JsonProperty]
        public DateTime CreatedAt { get; protected set; }

        private double salary;
        [JsonProperty]
        public double Salary { get { return salary; } protected set { if (value >= 0) salary = value; else throw new ArgumentException(); } }

        private double hourlyCostFact;
        [JsonProperty]
        public double HourlyCostFact { get { return hourlyCostFact; } protected set { if (value >= 0) hourlyCostFact = value; else throw new ArgumentException(); } }

        private double hourlyCostHand;
        [JsonProperty]
        public double HourlyCostHand { get { return hourlyCostHand; } protected set { if (value >= 0) hourlyCostHand = value; else throw new ArgumentException(); } }

        private double earnings;
        [JsonProperty]
        public double Earnings { get { return earnings; } protected set { if (value >= 0) earnings = value; else throw new ArgumentException(); } }

        private double expenses;
        [JsonProperty]
        public double Expenses { get { return expenses; } protected set { if (value >= 0) expenses = value; else throw new ArgumentException(); } }

        private double profit;
        [JsonProperty]
        public double Profit { get { return profit; } protected set { if (value >= 0) profit = value; else throw new ArgumentException(); } }

        private double profitability;
        [JsonProperty]
        public double ProfitAbility { get { return profitability; } protected set { if (value >= 0) profitability = value; else throw new ArgumentException(); } }

        private double grossSalary;
        [JsonProperty]
        public double GrossSalary { get { return grossSalary; } protected set { if (value >= 0) grossSalary = value; else throw new ArgumentException(); } }

        private double netSalary;
        [JsonProperty]
        public double NetSalary { get { return netSalary; } protected set { if (value >= 0) netSalary = value; else throw new ArgumentException(); } }

        private double ratePerHour;

        public double RatePerHour { get { return ratePerHour; } protected set { if (value >= 0) ratePerHour = value; else throw new ArgumentException(); } }

        private double pay;

        public double Pay { get { return pay; } protected set { if (value >= 0) pay = value; else throw new ArgumentException(); } }

        private double retainer;
        [JsonProperty]
        public double Retainer { get { return retainer; } protected set { if (value >= 0) retainer = value; else throw new ArgumentException(); } }

        public double EmploymentType { get; protected set; }

        public bool HasParking { get; protected set; }
        [JsonProperty]
        public double ParkingCostPerMonth { get; protected set; }
        [JsonProperty]
        public double AccountingPerMonth { get; protected set; }


        public const double WorkingDaysInYear = 247;

        public const double WorkingDaysInYearWithoutVacation = WorkingDaysInYear - 24;

        public const double WorkingDaysInYearWithoutVacationAndSick = WorkingDaysInYearWithoutVacation - 20;

        public const double WorkingDaysInMonth = WorkingDaysInYearWithoutVacationAndSick / 12;

        public const double WorkingHoursInMonth = WorkingDaysInMonth * 8;

        public AbstractMetricsEntity(long employeeId, double ratePerHour, double pay, double employmentType, bool hasParking)
        {
            EmployeeId = employeeId;
            CreatedAt = DateTime.UtcNow.Date;
            RatePerHour = ratePerHour;
            Pay = pay;
            EmploymentType = employmentType;
            HasParking = hasParking;
            ParkingCostPerMonth = hasParking ? 1800 : 0;
            AccountingPerMonth = 600;
        }

        private double CalculateHourlyCostFact()
        {
            return Expenses / WorkingHoursInMonth;
        }

        private double CalculateRetainer()
        {
            return NetSalary / 2;
        }

        private double CalculateHourlyCostHand()
        {
            return Salary / 160;
        }

        private double CalculateEarnings()
        {
            return RatePerHour * WorkingHoursInMonth * EmploymentType;
        }

        private double CalculateExpenses(double mrot)
        {
            return GetNdflValue() +
                NetSalary +
                GetPensionContributions(mrot) +
                GetMedicalContributions(mrot) +
                GetSocialInsuranceContributions(mrot) +
                GetInjuriesContributions() +
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

        public void CalculateMetrics(double districtCoeff,
            double mrot,
            double tax)
        {
            Salary = CalculateSalary();
            GrossSalary = CalculateGrossSalary(districtCoeff);
            NetSalary = CalculateNetSalary(tax);
            Earnings = CalculateEarnings();
            Expenses = CalculateExpenses(mrot);
            HourlyCostFact = CalculateHourlyCostFact();
            HourlyCostHand = CalculateHourlyCostHand();
            Retainer = CalculateRetainer();
            Profit = CalculateProfit();
            ProfitAbility = CalculateProfitability();
        }
    }
}
