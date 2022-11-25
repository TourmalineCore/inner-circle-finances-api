using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public class MetricsPreviewDto
    {
        public long Id { get; set; }

        public string FullName { get; set; }

        public double Pay { get; set; }
        public double PayDelta { get; set; }

        public double RatePerHour { get; set; }
        public double RatePerHourDelta { get; set; }

        public double EmploymentType { get; set; }

        public double ParkingCostPerMonth { get; set; }

        public double HourlyCostFact { get; set; }
        public double HourlyCostFactDelta { get; set; }

        public double HourlyCostHand { get; set; }
        public double HourlyCostHandDelta { get; set; }

        public double Earnings { get; set; }
        public double EarningsDelta { get; set; }

        public double IncomeTaxContributions { get; set; }
        public double IncomeTaxContributionsDelta { get; set; }

        public double PensionContributions { get; set; }
        public double PensionContributionsDelta { get; set; }

        public double MedicalContributions { get; set; }
        public double MedicalContributionsDelta { get; set; }

        public double SocialInsuranceContributions { get; set; }

        public double InjuriesContributions { get; set; }
        public double InjuriesContributionsDelta { get; set; }

        public double Expenses { get; set; }
        public double ExpensesDelta { get; set; }

        public double Profit { get; set; }
        public double ProfitDelta { get; set; }

        public double ProfitAbility { get; set; }
        public double ProfitAbilityDelta { get; set; }

        public double GrossSalary { get; set; }
        public double GrossSalaryDelta { get; set; }

        public double Retainer { get; set; }
        public double RetainerDelta { get; set; }

        public double NetSalary { get; set; }
        public double NetSalaryDelta { get; set; }

        public MetricsPreviewDto(long id, 
            string fullName, 
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
            FullName = fullName;
            Pay = Math.Round(pay, 2);
            RatePerHour = Math.Round(ratePerHour, 2);
            EmploymentType = Math.Round(employmentType, 2);
            ParkingCostPerMonth = Math.Round(parkingCostPerMonth, 2);
            HourlyCostFact = Math.Round(hourlyCostFact, 2);
            HourlyCostHand = Math.Round(hourlyCostHand, 2);
            Earnings = Math.Round(earnings, 2);
            IncomeTaxContributions = Math.Round(incomeTaxContributions, 2);
            PensionContributions = Math.Round(pensionContributions, 2);
            MedicalContributions = Math.Round(medicalContributions, 2);
            SocialInsuranceContributions = Math.Round(socialInsuranceContributions, 2);
            InjuriesContributions = Math.Round(injuriesContributions, 2);
            Expenses = Math.Round(expenses, 2);
            Profit = Math.Round(profit, 2);
            ProfitAbility = Math.Round(profitAbility, 2);
            GrossSalary = Math.Round(grossSalary, 2);
            Retainer = Math.Round(retainer, 2);
            NetSalary = Math.Round(netSalary, 2);
        }
    }
}
