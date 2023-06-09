namespace SalaryService.Domain;

public class EmployeePersonnelNumber
{
    public long InternalCompanyNumber { get; private set; }

    public long HiringYear { get; private set; }

    private const char DataDelimiter = '/';

    public EmployeePersonnelNumber(string personnelNumber)
    {
        Validate(personnelNumber);
        Parse(personnelNumber);
    }

    private static void Validate(string personnelNumber)
    {
        if (string.IsNullOrWhiteSpace(personnelNumber))
            throw new ArgumentException("The personnel number can't be empty");

        var delimitersCount = personnelNumber.Count(x => x == DataDelimiter);

        if (delimitersCount != 1) throw new ArgumentException("The personnel number must contain only one delimiter");
    }

    private void Parse(string personnelNumber)
    {
        var data = personnelNumber.Split(DataDelimiter);

        var internalCompanyNumberParsed = long.TryParse(data[0], out var internalCompanyNumber);
        var hiringYearParsed = long.TryParse(data[1], out var hiringYear);

        if (!internalCompanyNumberParsed) throw new ArgumentException("Couldn't parse internal company number");
        if (!hiringYearParsed) throw new ArgumentException("Couldn't parse hiring year");

        if (internalCompanyNumber <= 0)
            throw new ArgumentException("The internal company number of employee must be positive");

        if (hiringYear <= 0)
            throw new ArgumentException("The hiring year must be positive");

        InternalCompanyNumber = internalCompanyNumber;
        HiringYear = hiringYear;
    }

    public override string ToString()
    {
        var internalCompanyNumber = InternalCompanyNumber is >= 1 and <= 9
            ? $"0{InternalCompanyNumber}"
            : InternalCompanyNumber.ToString();

        var hiringYear = HiringYear is >= 1 and <= 9
            ? $"0{HiringYear}"
            : HiringYear.ToString();

        return $"{internalCompanyNumber}{DataDelimiter}{hiringYear}";
    }
}