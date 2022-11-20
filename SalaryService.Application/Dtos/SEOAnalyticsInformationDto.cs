
using NodaTime;

namespace SalaryService.Application.Dtos
{
    public class SEOAnalyticsInformationDto
    {
        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string MiddleName { get; private set; }

        public string EmploymentDate { get; private set; }

        public double Pay { get; private set; }

        public double RatePerHour { get; private set; }

        public double EmploymentType { get; private set; }

        public double ParkingCostPerMonth { get; private set; }

        public double HourlyCostFact { get; private set; }

        public double HourlyCostHand { get; private set; }

        public double Earnings { get; private set; }

        public double IncomeTaxContributions { get; private set; }

        public double PensionContributions { get; set; }

        public double MedicalContributions { get; set; }

        public double SocialInsuranceContributions { get; set; }

        public double InjuriesContributions { get; set; }

        public double Expenses { get; private set; }

        public double Profit { get; private set; }

        public double ProfitAbility { get; private set; }

        public double GrossSalary { get; private set; }

        public double Retainer { get; private set; }

        public double NetSalary { get; private set; }

        public SEOAnalyticsInformationDto(long id,
            string name,
            string surname,
            string middleName,
            string employmentDate,
            double pay,
            double ratePerHour,
            double employmentType,
            double parkingCostPerMonth,
            double hourlyCostFact,
            double hourlyCostHand,
            double earnings,
            double incomeTaxContributions,
            double pensionContributions,
            double medicalContributions,
            double socialInsuranceContributions,
            double injuriesContributions,
            double expenses,
            double profit,
            double profitAbility,
            double grossSalary,
            double retainer,
            double netSalary)
        {
            Id = id;
            Name = name;
            Surname = surname;
            MiddleName = middleName;
            EmploymentDate = employmentDate;
            Pay = pay;
            RatePerHour = ratePerHour;
            EmploymentType = employmentType;
            ParkingCostPerMonth = parkingCostPerMonth;
            HourlyCostFact = hourlyCostFact;
            HourlyCostHand = hourlyCostHand;
            Earnings = earnings;
            IncomeTaxContributions = incomeTaxContributions;
            PensionContributions = pensionContributions;
            MedicalContributions = medicalContributions;
            SocialInsuranceContributions = socialInsuranceContributions;
            InjuriesContributions = injuriesContributions;
            Expenses = expenses;
            Profit = profit;
            ProfitAbility = profitAbility;
            GrossSalary = grossSalary;
            Retainer = retainer;
            NetSalary = netSalary;
        }
    }
}
