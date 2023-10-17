namespace SalaryService.Application.Dtos;


public readonly struct CompensationCreateDto
{
    public List<CompensationDto> Compensations { get; init; }

    public DateTime Date { get; init; }
}

public readonly struct CompensationDto
{
    public string Type { get; init; }

    public string Comment { get; init; }

    public double Amount { get; init; }
}
