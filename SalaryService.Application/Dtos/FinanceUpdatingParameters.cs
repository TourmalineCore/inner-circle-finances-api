namespace SalaryService.Application.Dtos
{
    public readonly struct FinanceUpdatingParameters
    {
        public long EmployeeId { get; init; }

        public decimal RatePerHour { get; init; }

        public decimal Pay { get; init; }

        public decimal EmploymentType { get; init; }

        public decimal ParkingCostPerMonth { get; init; }
    }
}
