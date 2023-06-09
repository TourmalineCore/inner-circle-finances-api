using NodaTime;
using SalaryService.Api.Comparers;
using SalaryService.Domain;

namespace SalaryService.Tests;

public class EmployeesComparerTests
{
    [Fact]
    public void UnofficialEmployeesShouldBeSortedByFullName()
    {
        var unofficialEmployees = new List<Employee>
        {
            new("aaa", "aaa", "aaa", "test1@tourmalinecore.com"),
            new("abc", "abc", "abc", "test2@tourmalinecore.com")
        };

        var orderedEmployees = unofficialEmployees.OrderBy(x => x, new EmployeesComparer()).ToList();

        Assert.Equal("aaa aaa aaa", orderedEmployees[0].GetFullName());
        Assert.Equal("abc abc abc", orderedEmployees[1].GetFullName());
    }

    [Fact]
    public void OfficialEmployeesShouldBeSortedByPersonnelNumber()
    {
        var employee1 = new Employee("employee1", "employee1", "employee1", "employee1@tourmalinecore.com", true);
        employee1.Update("+71234567891", "employee1@mail.ru", "@employee1github", "@employee1gitlab",
            Instant.FromUtc(2021, 01, 01, 0, 0), true, "01/21");

        var employee2 = new Employee("employee2", "employee2", "employee2", "employee2@tourmalinecore.com", true);
        employee2.Update("+71234567892", "employee2@mail.ru", "@employee2github", "@employee2gitlab",
            Instant.FromUtc(2021, 01, 02, 0, 0), true, "02/21");

        var officialEmployees = new List<Employee>
        {
            employee2,
            employee1
        };

        var orderedEmployees = officialEmployees.OrderBy(x => x, new EmployeesComparer()).ToList();

        Assert.Equal("01/21", orderedEmployees[0].PersonnelNumber?.ToString());
        Assert.Equal("02/21", orderedEmployees[1].PersonnelNumber?.ToString());
    }

    [Fact]
    public void OfficialEmployeesPrecedeUnofficialOnes()
    {
        var employee1 = new Employee("employee1", "employee1", "employee1", "employee1@tourmalinecore.com", true);
        employee1.Update("+71234567891", "employee1@mail.ru", "@employee1github", "@employee1gitlab",
            Instant.FromUtc(2021, 01, 01, 0, 0), true, "01/21");

        var employee2 = new Employee("employee2", "employee2", "employee2", "employee2@tourmalinecore.com", true);
        employee2.Update("+71234567892", "employee2@mail.ru", "@employee2github", "@employee2gitlab",
            Instant.FromUtc(2021, 01, 02, 0, 0), true, "02/21");

        var officialEmployees = new List<Employee>
        {
            new("unofficial", "unofficial", "unofficial", "unofficial@tourmalinecore.com"),
            new("unofficial2", "unofficial2", "unofficial2", "unofficial2@tourmalinecore.com"),
            employee2,
            employee1
        };

        var orderedEmployees = officialEmployees.OrderBy(x => x, new EmployeesComparer()).ToList();

        Assert.Equal("employee1 employee1 employee1", orderedEmployees[0].GetFullName());
        Assert.Equal("employee2 employee2 employee2", orderedEmployees[1].GetFullName());
        Assert.Equal("unofficial unofficial unofficial", orderedEmployees[2].GetFullName());
        Assert.Equal("unofficial2 unofficial2 unofficial2", orderedEmployees[3].GetFullName());
    }
}