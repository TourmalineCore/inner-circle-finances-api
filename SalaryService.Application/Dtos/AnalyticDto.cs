using SalaryService.Domain;

namespace SalaryService.Application.Dtos
{
    public class AnalyticDto
    {
        public long EmployeeId { get; private set; }

        public string FullName { get; private set; }

        public decimal? Pay { get; private set; }

        public decimal? RatePerHour { get; private set; }

        public decimal? EmploymentType { get; private set; }

        public decimal? Salary { get; private set; }

        public decimal? ParkingCostPerMonth { get; private set; }

        public decimal? AccountingPerMonth { get; private set; }

        public decimal? HourlyCostFact { get; private set; }

        public decimal? HourlyCostHand { get; private set; }

        public decimal? Earnings { get; private set; }

        public decimal? IncomeTaxContributions { get; private set; }

        public decimal? DistrictCoefficient { get; private set; }

        public decimal? PensionContributions { get; set; }

        public decimal? MedicalContributions { get; set; }

        public decimal? SocialInsuranceContributions { get; set; }

        public decimal? InjuriesContributions { get; set; }

        public decimal? Expenses { get; private set; }

        public decimal? Profit { get; private set; }

        public decimal? ProfitAbility { get; private set; }

        public decimal? GrossSalary { get; private set; }

        public decimal? Prepayment { get; private set; }

        public decimal? NetSalary { get; private set; }

        public AnalyticDto(Employee employee)
        {
            EmployeeId = employee.Id;
            FullName = employee.GetFullName();

            var employeeFinancialMetrics = employee.EmployeeFinancialMetrics;

            Pay = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.Pay, 2) : null;
            RatePerHour = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.RatePerHour, 2) : null;
            EmploymentType = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.EmploymentType, 2) : null;
            Salary = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.Salary, 2) : null;
            ParkingCostPerMonth = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.ParkingCostPerMonth, 2) : null;
            AccountingPerMonth = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.AccountingPerMonth, 2) : null;
            HourlyCostFact = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.HourlyCostFact, 2) : null;
            HourlyCostHand = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.HourlyCostHand, 2) : null;
            Earnings = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.Earnings, 2) : null;
            IncomeTaxContributions = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.IncomeTaxContributions, 2) : null;
            DistrictCoefficient = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.DistrictCoefficient, 2) : null;
            PensionContributions = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.PensionContributions, 2) : null;
            MedicalContributions = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.MedicalContributions, 2) : null;
            SocialInsuranceContributions = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.SocialInsuranceContributions, 2) : null;
            InjuriesContributions = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.InjuriesContributions, 2) : null;
            Expenses = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.Expenses, 2) : null;
            Profit = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.Profit, 2) : null;
            ProfitAbility = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.ProfitAbility, 2) : null;
            GrossSalary = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.GrossSalary, 2) : null;
            Prepayment = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.Prepayment, 2) : null;
            NetSalary = employeeFinancialMetrics != null ? Math.Round(employeeFinancialMetrics.NetSalary, 2) : null;
        }
    }
}
