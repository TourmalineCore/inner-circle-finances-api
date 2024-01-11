using SalaryService.Domain;

namespace SalaryService.Application.Dtos;

public class AllCompensationsListDto
{
    public double TotalAmount { get; init; }

    public IEnumerable<ItemDto> Items { get; init; }

    public AllCompensationsListDto(IEnumerable<Compensation> compensations)
	{
        Items = compensations.GroupBy(x => x.EmployeeId).Select(x => new ItemDto(x.ToList()));
        TotalAmount = Math.Round(compensations.Sum(x => x.Amount), 2);
    }
}

public class ItemDto
{
    public string EmployeeFullName { get; init; }

    public long EmployeeId { get; init; }

    public string DateCompensation { get; init; }

    public double TotalAmount { get; init; }

    public bool IsPaid { get; init; }

    public IEnumerable<EmployeeCompensationDto> Compensations { get; init; }

    public ItemDto(List<Compensation> employeeCompensations)
    {
        EmployeeFullName = employeeCompensations[0].Employee.GetFullName();
        DateCompensation = employeeCompensations[0].DateCompensation.ToString();
        TotalAmount = Math.Round(employeeCompensations.Sum(x => x.Amount), 2);
        Compensations = employeeCompensations.Select(x => new EmployeeCompensationDto(x));
        EmployeeId = employeeCompensations[0].Employee.Id;
        IsPaid = false;
    }
}

public class EmployeeCompensationDto
{
    public long Id { get; init; }

    public string CompensationType { get; init; }

    public string? Comment { get; init; }

    public double Amount { get; init; }

    public string DateCreateCompensation { get; init; }

    public EmployeeCompensationDto(Compensation compensation)
    {
        Id = compensation.Id;
        CompensationType = CompensationTypes.GetTypeNameByTypeId(compensation.TypeId);
        Comment = compensation.Comment;
        Amount = compensation.Amount;
        DateCreateCompensation = compensation.DateCreateCompensation.ToString();
    }
}