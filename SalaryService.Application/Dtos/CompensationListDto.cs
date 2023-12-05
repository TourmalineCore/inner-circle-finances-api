public class CompensationListDto
{
    public List<CompensationItemDto> List { get; init; }

    public double TotalAmount { get; init; }

    public CompensationListDto (List<CompensationItemDto> compensationList, double totalAmount)
    {
        List = compensationList;
        TotalAmount = totalAmount;
    }
}

public class CompensationItemDto
{
    public long Id { get; init; }

    public string EmployeeFullName { get; init; }

    public string? Comment { get; init; }

    public double Amount { get; init; }

    public bool IsPaid { get; init; }

    public string DateCreateCompensation { get; init; }

    public string DateCompensation { get; init; }

    public CompensationItemDto(long id, string employee, string? comment, double amount, bool isPaid, string dateCreateCompensation, string dateCompensation)
    {
        Id = id;
        EmployeeFullName = employee;
        Comment = comment;
        Amount = amount;
        IsPaid = isPaid;
        DateCreateCompensation = dateCreateCompensation;
        DateCompensation = dateCompensation;
    }
}