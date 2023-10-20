using NodaTime;

public class CompensationListDto
{
    public List<RowCompensationDto> Rows { get; init; }

    public double TotalUnpaidAmount { get; init; }
}

public class RowCompensationDto
{
    public long Id { get; init; }

    public string? Comment { get; init; }

    public double Amount { get; init; }

    public bool IsPaid { get; init; }

    public Instant CreatedAtUtc { get; init; }

    public Instant Date { get; init; }
}