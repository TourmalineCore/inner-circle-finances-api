using SalaryService.Domain;

namespace SalaryService.Api.Comparers;

public class EmployeesComparer : Comparer<Employee>
{
    public override int Compare(Employee? x, Employee? y)
    {
        if (x == null || y == null) throw new ArgumentException("Can't compare employee with null");

        if (x.IsEmployedOfficially && y.IsEmployedOfficially)
            return x.PersonnelNumber?.InternalCompanyNumber > y.PersonnelNumber?.InternalCompanyNumber ? 1 : -1;

        if (x.IsEmployedOfficially && !y.IsEmployedOfficially) return -1;
        if (!x.IsEmployedOfficially && y.IsEmployedOfficially) return 1;

        if (!x.IsEmployedOfficially && !y.IsEmployedOfficially)
            return string.Compare(x.GetFullName(), y.GetFullName(), StringComparison.Ordinal);

        return 0;
    }
}