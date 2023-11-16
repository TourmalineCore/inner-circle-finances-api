namespace SalaryService.Domain;

public class CompensationType
{
    public long Id { get; set; }

    public string Label { get; set; }

    public CompensationType(long id, string label)
    {
        Id = id;
        Label = label;
    }
}
