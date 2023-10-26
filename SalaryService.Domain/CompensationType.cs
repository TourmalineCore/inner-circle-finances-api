namespace SalaryService.Domain;

public class CompensationType
{
    public long Id { get; set; }

    public string Name { get; set; }

    public CompensationType(long id, string name)
    {
        Id = id;
        Name = name;
    }
}
