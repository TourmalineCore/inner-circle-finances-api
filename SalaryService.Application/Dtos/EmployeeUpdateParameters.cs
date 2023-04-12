namespace SalaryService.Application.Dtos;

public readonly struct EmployeeUpdateParameters
{
    public long EmployeeId { get; init; }

    public string Phone { get; init; }

    public string? PersonalEmail { get; init; }

    public string? GitHub { get; init; }

    public string? GitLab { get; init; }

    public decimal RatePerHour { get; init; }

    public decimal FullSalary { get; init; }

    public decimal EmploymentType { get; init; }

    public decimal Parking { get; init; }

    public DateTime HireDate { get; init; }

    public bool IsEmployedOfficially { get; init; }

    public string? PersonnelNumber { get; init; }

    public FinanceUpdatingParameters GetFinanceUpdatingParameters()
    {
        return new FinanceUpdatingParameters
        {
            EmployeeId = EmployeeId,
            RatePerHour = RatePerHour,
            Pay = FullSalary,
            EmploymentType = EmploymentType,
            ParkingCostPerMonth = Parking
        };
    }

    public EmployeeInfoUpdateParameters GetEmployeeInfoUpdateParameters()
    {
        return new EmployeeInfoUpdateParameters
        {
            EmployeeId = EmployeeId,
            Phone = Phone,
            PersonalEmail = PersonalEmail,
            GitHub = GitHub,
            GitLab = GitLab,
            HireDate = HireDate,
            IsEmployedOfficially = IsEmployedOfficially,
            PersonnelNumber = PersonnelNumber,
        };
    }
}