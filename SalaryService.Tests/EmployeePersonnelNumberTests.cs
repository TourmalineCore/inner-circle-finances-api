using SalaryService.Domain;

namespace SalaryService.Tests;

public class EmployeePersonnelNumberTests
{
    [Fact]
    public void CannotBeEmpty()
    {
        var exception = Assert.Throws<ArgumentException>(() => new EmployeePersonnelNumber(string.Empty));
        Assert.Equal("The personnel number can't be empty", exception.Message);
    }

    [Fact]
    public void MustContainOnlyOneDelimiter()
    {
        var exception = Assert.Throws<ArgumentException>(() => new EmployeePersonnelNumber("02//21"));
        Assert.Equal("The personnel number must contain only one delimiter", exception.Message);
    }

    [Theory]
    [InlineData("-02/21")]
    [InlineData("-2/21")]
    [InlineData("0/21")]
    public void InternalCompanyNumberMustBePositive(string personnelNumber)
    {
        var exception = Assert.Throws<ArgumentException>(() => new EmployeePersonnelNumber(personnelNumber));
        Assert.Equal("The internal company number of employee must be positive", exception.Message);
    }

    [Theory]
    [InlineData("02/-02")]
    [InlineData("02/-2")]
    [InlineData("02/0")]
    public void HiringYearMustBePositive(string personnelNumber)
    {
        var exception = Assert.Throws<ArgumentException>(() => new EmployeePersonnelNumber(personnelNumber));
        Assert.Equal("The hiring year must be positive", exception.Message);
    }

    [Theory]
    [InlineData("0.2/21")]
    [InlineData("02$/21")]
    [InlineData("*02/21")]
    [InlineData("*02$/21")]
    public void CantCreateIfInternalCompanyNumberIsInvalid(string personnelNumber)
    {
        var exception = Assert.Throws<ArgumentException>(() => new EmployeePersonnelNumber(personnelNumber));
        Assert.Equal("Couldn't parse internal company number", exception.Message);
    }

    [Theory]
    [InlineData("02/2.1")]
    [InlineData("02/*21")]
    [InlineData("02/21$")]
    [InlineData("02/*21$")]
    public void CantCreateIfHiringYearIsInvalid(string personnelNumber)
    {
        var exception = Assert.Throws<ArgumentException>(() => new EmployeePersonnelNumber(personnelNumber));
        Assert.Equal("Couldn't parse hiring year", exception.Message);
    }

    [Theory]
    [InlineData("01/21", "01/21")]
    [InlineData("1/21", "01/21")]
    [InlineData("09/21", "09/21")]
    [InlineData("9/21", "09/21")]
    [InlineData("10/21", "10/21")]
    public void CanCreateIfAllParamsAreValid(string personnelNumber, string expectedPersonnelNumber)
    {
        var employeePersonnelNumber = new EmployeePersonnelNumber(personnelNumber);
        Assert.Equal(expectedPersonnelNumber, employeePersonnelNumber.ToString());
    }
}