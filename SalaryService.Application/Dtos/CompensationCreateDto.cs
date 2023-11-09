namespace SalaryService.Application.Dtos;

public readonly struct CompensationCreateDto
{
    public List<CompensationDto> Compensations { get; init; }

    public long EmployeeId { get; init; }

    public string DateCompensation { get; init; }
}

public readonly struct CompensationDto
{
    //public CompensationType Type { get; init; }

    public string Type { get; init; }

    public string? Comment { get; init; }

    public double Amount { get; init; }

    public bool IsPaid { get; init; }
}