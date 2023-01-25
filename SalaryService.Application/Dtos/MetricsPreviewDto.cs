namespace SalaryService.Application.Dtos
{
    public class MetricsPreviewDto
    {
        public double Pay { get; set; }

        public double RatePerHour { get; set; }

        public double EmploymentType
        {
            get => employmentType;
            private set
            {
                if (!_availableEmploymentRateTypes.Contains(value))
                {
                    throw new ArgumentException("Employment rate type can accept only the following values: 0.5, 1");
                }

                employmentType = value;
            }
        }
        private double employmentType;
        private readonly List<double> _availableEmploymentRateTypes = new() { 0.5, 1 };

        public double Salary { get; set; }

        public double ParkingCostPerMonth { get; set; }

        public double AccountingPerMonth { get; set; }

        public double HourlyCostFact { get; set; }

        public double HourlyCostHand { get; set; }

        public double Earnings { get; set; }

        public double IncomeTaxContributions { get; set; }

        public double DistrictCoefficient { get; set; }

        public double PensionContributions { get; set; }

        public double MedicalContributions { get; set; }

        public double SocialInsuranceContributions { get; set; }

        public double InjuriesContributions { get; set; }

        public double Expenses { get; set; }

        public double Profit { get; set; }

        public double ProfitAbility { get; set; }

        public double GrossSalary { get; set; }

        public double Prepayment { get; set; }

        public double NetSalary { get; set; }

        public MetricsPreviewDto(
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
            Pay = Math.Round(pay, 2);
            RatePerHour = Math.Round(ratePerHour, 2);
            EmploymentType = Math.Round(employmentType, 2);
            Salary = Math.Round(salary, 2);
            ParkingCostPerMonth = Math.Round(parkingCostPerMonth, 2);
            AccountingPerMonth = Math.Round(accountingPerMonth, 2);
            HourlyCostFact = Math.Round(hourlyCostFact, 2);
            HourlyCostHand = Math.Round(hourlyCostHand, 2);
            Earnings = Math.Round(earnings, 2);
            IncomeTaxContributions = Math.Round(incomeTaxContributions, 2);
            DistrictCoefficient = Math.Round(districtCoefficient, 2);
            PensionContributions = Math.Round(pensionContributions, 2);
            MedicalContributions = Math.Round(medicalContributions, 2);
            SocialInsuranceContributions = Math.Round(socialInsuranceContributions, 2);
            InjuriesContributions = Math.Round(injuriesContributions, 2);
            Expenses = Math.Round(expenses, 2);
            Profit = Math.Round(profit, 2);
            ProfitAbility = Math.Round(profitAbility, 2);
            GrossSalary = Math.Round(grossSalary, 2);
            Prepayment = Math.Round(prepayment, 2);
            NetSalary = Math.Round(netSalary, 2);
        }
    }
}
