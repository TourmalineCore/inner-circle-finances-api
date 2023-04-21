namespace SalaryService.Application.Dtos;

public readonly struct MetricsRowDto
{
    public string EmployeeId { get; init; }

    public string EmployeeFullName { get; init; }

    public decimal RatePerHour { get; init; }

    public decimal Pay { get; init; }

    public decimal EmploymentType { get; init; }

    public decimal ParkingCostPerMonth { get; init; }

    public bool IsCopy { get; init; }
}