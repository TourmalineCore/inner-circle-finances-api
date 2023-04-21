namespace SalaryService.Application.Dtos;

public readonly struct ProfileUpdatingParameters
{
    public long EmployeeId { get; init; }

    public string PersonalEmail { get; init; }

    public string Phone { get; init; }

    public string GitHub { get; init; }

    public string GitLab { get; init; }
}
