public class PersonalCompensationListDto
{
    public List<PersonalCompensationItemDto> List { get; init; }

    public double TotalUnpaidAmount { get; init; }

    public PersonalCompensationListDto (List<PersonalCompensationItemDto> compensationList, double totalUnpaidAmount)
    {
        List = compensationList;
        TotalUnpaidAmount = totalUnpaidAmount;
    }
}

public class PersonalCompensationItemDto
{
    public long Id { get; init; }

    public string? Comment { get; init; }

    public double Amount { get; init; }

    public bool IsPaid { get; init; }

    public string DateCreateCompensation { get; init; }

    public string DateCompensation { get; init; }

    public PersonalCompensationItemDto (long id, string? comment, double amount, bool isPaid, string dateCreateCompensation, string dateCompensation)
    {
        Id = id;
        Comment = comment;
        Amount = amount;
        IsPaid = isPaid;
        DateCreateCompensation = dateCreateCompensation;
        DateCompensation = dateCompensation;
    }
}