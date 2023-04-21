using SalaryService.Domain;

namespace SalaryService.Api.Responses;

public readonly struct EmployeeResponse
{
    public long EmployeeId { get; init; }

    public string FullName { get; init; }

    public string CorporateEmail { get; init; }

    public string? PersonalEmail { get; init; }

    public string? Phone { get; init; }

    public string? GitHub { get; init; }

    public string? GitLab { get; init; }

    public bool IsBlankEmployee { get; init; }

    public bool IsCurrentEmployee { get; init; }

    public bool IsEmployedOfficially { get; init; }

    public decimal? NetSalary { get; init; } = null;

    public decimal? RatePerHour { get; init; } = null;

    public decimal? FullSalary { get; init; } = null;

    public decimal? EmploymentType { get; init; } = null;

    public decimal? Parking { get; init; } = null;

    public string? PersonnelNumber { get; init; }

    public DateTime? HireDate { get; init; }

    public EmployeeResponse(Employee employee)
    {
        EmployeeId = employee.Id;
        FullName = employee.GetFullName();
        CorporateEmail = employee.CorporateEmail;
        PersonalEmail = employee.PersonalEmail;
        Phone = employee.Phone;
        GitHub = employee.GitHub;
        GitLab = employee.GitLab;
        IsBlankEmployee = employee.IsBlankEmployee;
        IsCurrentEmployee = employee.IsCurrentEmployee;
        IsEmployedOfficially = employee.IsEmployedOfficially;

        if (employee.FinancialMetrics != null)
        {
            NetSalary = Math.Round(employee.FinancialMetrics.NetSalary, 2);
            RatePerHour = Math.Round(employee.FinancialMetrics.RatePerHour, 2);
            FullSalary = Math.Round(employee.FinancialMetrics.Pay, 2);
            Parking = Math.Round(employee.FinancialMetrics.ParkingCostPerMonth, 2);
            EmploymentType = employee.FinancialMetrics.EmploymentType;
        }

        PersonnelNumber = employee.PersonnelNumber;
        HireDate = employee.HireDate?.ToDateTimeUtc();
    }
}
