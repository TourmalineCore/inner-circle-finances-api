using NodaTime;

public class CompensationListDto
{
    public List<CompensationItemDto> List { get; init; }

    public double TotalUnpaidAmount { get; init; }
}

public class CompensationItemDto
{
    public long Id { get; init; }

    public string? Comment { get; init; }

    public double Amount { get; init; }

    public bool IsPaid { get; init; }

    public string DateCreateCompensation { get; init; }

    public string DateCompensation { get; init; }
}