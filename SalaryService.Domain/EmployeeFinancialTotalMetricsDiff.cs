namespace SalaryService.Domain
{
    public readonly struct EmployeeFinancialTotalMetricsDiff
    {
        public double ParkingCostPerMonth { get; init; }

        public double AccountingPerMonth { get; init; }

        public double Earnings { get; init; }

        public double Expenses { get; init; }

        public double IncomeTaxContributions { get; init; }

        public double PensionContributions { get; init; }

        public double MedicalContributions { get; init; }

        public double SocialInsuranceContributions { get; init; }

        public double InjuriesContributions { get; init; }

        public double Profit { get; init; }

        public double ProfitAbility { get; init; }

        public double Prepayment { get; init; }

        public double NetSalary { get; init; }
    }
}
