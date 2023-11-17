namespace SalaryService.Domain;

public class CompensationType
{
    public long TypeId { get; set; }

    public string Label { get; set; }

    public CompensationType(long typeId, string label)
    {
        TypeId = typeId;
        Label = label;
    }
}
