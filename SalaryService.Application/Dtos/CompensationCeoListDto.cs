public class CompensationCeoListDto
{
    public List<CompensationCeoItemDto> List { get; init; }

    public double TotalAmount { get; init; }
}

public class CompensationCeoItemDto
{
    public long Id { get; init; }

    public string EmployeeFullName { get; init; }

    public string? Comment { get; init; }

    public double Amount { get; init; }

    public bool IsPaid { get; init; }

    public string DateCreateCompensation { get; init; }

    public string DateCompensation { get; init; }
}