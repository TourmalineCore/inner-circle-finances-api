namespace SalaryService.Application.Dtos
{
    public readonly struct EmployeeRowDto
    {
        public long? EmployeeId { get; init; }

        public string? EmployeeCopyId { get; init; }

        public double RatePerHour { get; init; }

        public double Pay { get; init; }

        public double EmploymentType { get; init; }

        public double ParkingCostPerMonth { get; init; }

        public bool IsCopyRow => !EmployeeId.HasValue;
    }
}
