namespace SalaryService.Application.Dtos
{
    public class AnalyticDto
    {
        public long Id { get; private set; }

        public string FullName { get; private set; }

        public double Pay { get; private set; }

        public double RatePerHour { get; private set; }

        public double EmploymentType { get; private set; }

        public double Salary { get; private set; }

        public double ParkingCostPerMonth { get; private set; }

        public double AccountingPerMonth { get; private set; }

        public double HourlyCostFact { get; private set; }

        public double HourlyCostHand { get; private set; }

        public double Earnings { get; private set; }

        public double IncomeTaxContributions { get; private set; }

        public double DistrictCoefficient { get; private set; }

        public double PensionContributions { get; set; }

        public double MedicalContributions { get; set; }

        public double SocialInsuranceContributions { get; set; }

        public double InjuriesContributions { get; set; }

        public double Expenses { get; private set; }

        public double Profit { get; private set; }

        public double ProfitAbility { get; private set; }

        public double GrossSalary { get; private set; }

        public double Prepayment { get; private set; }

        public double NetSalary { get; private set; }

        public AnalyticDto(long id,
            string name,
            string surname,
            string middleName,
            double pay,
            double ratePerHour,
            double employmentType,
            double salary,
            double parkingCostPerMonth,
            double accountingPerMonth,
            double hourlyCostFact,
            double hourlyCostHand,
            double earnings,
            double incomeTaxContributions,
            double districtCoefficient,
            double pensionContributions,
            double medicalContributions,
            double socialInsuranceContributions,
            double injuriesContributions,
            double expenses,
            double profit,
            double profitAbility,
            double grossSalary,
            double prepayment,
            double netSalary)
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
}
