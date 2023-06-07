using SalaryService.Domain;

namespace SalaryService.Application.Dtos;

public class EmployeeUpdateDto
{
    public long EmployeeId { get; init; }

    public string Phone { get; init; }

    public string? PersonalEmail { get; init; }

    public string? GitHub { get; init; }

    public string? GitLab { get; init; }

    public DateTime HireDate { get; init; }

    public bool IsEmployedOfficially { get; init; }

    public string? PersonnelNumber { get; init; }

    public decimal RatePerHour { get; init; }

    public decimal FullSalary { get; init; }

    public decimal ParkingCostPerMonth { get; init; }

    public decimal EmploymentType { get; init; }

    public FinancesForPayroll FinancesForPayroll =>
        new(RatePerHour, FullSalary, EmploymentType, ParkingCostPerMonth);
}