
using SalaryService.Domain.Common;

namespace SalaryService.Domain
{
    public class EmployeeFinancialMetricsHistory : IIdentityEntity
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public Period Period { get; set; }
        public double Salary { get; set; }

        public double HourlyCostFact { get; set; }

        public double HourlyCostHand { get; set; }

        public double Earnings { get; set; }

        public double IncomeTaxContributions { get; set; }

        public double PensionContributions { get; set; }

        public double MedicalContributions { get; set; }

        public double SocialInsuranceContributions { get; set; }

        public double InjuriesContributions { get; set; }

        public double Expenses { get; set; }

        public double Profit { get; set; }

        public double ProfitAbility { get; set; }

        public double GrossSalary { get; set; }

        public double NetSalary { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public double Retainer { get; set; }

        public double EmploymentType { get; set; }

        public bool HasParking { get; set; }

        public double ParkingCostPerMonth { get; set; }

        public double AccountingPerMonth { get; set; }
    }
}
