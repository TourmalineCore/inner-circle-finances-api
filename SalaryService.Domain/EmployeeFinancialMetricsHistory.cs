using Period = SalaryService.Domain.Common.Period;

namespace SalaryService.Domain
{
    public class EmployeeFinancialMetricsHistory : IIdentityEntity
    {
        public long Id { get; set; }

        public long EmployeeId { get; set; }

        public Employee Employee { get; set; }
        
        public Period Period { get; set; }

        public decimal Salary { get; set; }

        public decimal HourlyCostFact { get; set; }

        public decimal HourlyCostHand { get; set; }

        public decimal Earnings { get; set; }

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
    }
}
