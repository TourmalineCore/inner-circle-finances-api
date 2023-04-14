namespace SalaryService.Domain
{
    public readonly struct TotalEmployeeFinancialMetricsEntry
    {
        public decimal ParkingCostPerMonth { get; init; }

        public decimal AccountingPerMonth { get; init; }

        public decimal Earnings { get; init; }

        public decimal Expenses { get; init; }

        public decimal IncomeTaxContributions { get; init; }

        public decimal PensionContributions { get; init; }

        public decimal MedicalContributions { get; init; }

        public decimal SocialInsuranceContributions { get; init; }

        public decimal InjuriesContributions { get; init; }

        public decimal Profit { get; init; }

        public decimal Prepayment { get; init; }

        public decimal NetSalary { get; init; }

        public decimal ProfitAbility => Earnings != 0
            ? Profit / Earnings * 100
            : TotalProfitabilityIfEarningsIsZero;


        private const decimal TotalProfitabilityIfEarningsIsZero = -100;
    }
}
