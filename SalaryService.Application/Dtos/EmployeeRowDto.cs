namespace SalaryService.Application.Dtos
{
    public readonly struct EmployeeRowDto
    {
        public long? EmployeeId { get; init; }

        public string? EmployeeCopyId { get; init; }

        public decimal RatePerHour { get; init; }

        public decimal Pay { get; init; }

        public decimal EmploymentType { get; init; }

        public decimal ParkingCostPerMonth { get; init; }

        public bool IsCopyRow => !EmployeeId.HasValue;
    }
}
