namespace SalaryService.Application.Dtos
{
    public class MetricsPreviewDto
    {
        public decimal Pay { get; set; }

        public decimal RatePerHour { get; set; }

        public decimal EmploymentType { get; set; }

        public decimal Salary { get; set; }

        public decimal ParkingCostPerMonth { get; set; }

        public decimal AccountingPerMonth { get; set; }

        public decimal HourlyCostFact { get; set; }

        public decimal HourlyCostHand { get; set; }

        public decimal Earnings { get; set; }

        public decimal IncomeTaxContributions { get; set; }

        public decimal DistrictCoefficient { get; set; }

        public decimal PensionContributions { get; set; }

        public decimal MedicalContributions { get; set; }

        public decimal SocialInsuranceContributions { get; set; }

        public decimal InjuriesContributions { get; set; }

        public decimal Expenses { get; set; }

        public decimal Profit { get; set; }

        public decimal ProfitAbility { get; set; }

        public decimal GrossSalary { get; set; }

        public decimal Prepayment { get; set; }

        public decimal NetSalary { get; set; }

        public MetricsPreviewDto(
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
