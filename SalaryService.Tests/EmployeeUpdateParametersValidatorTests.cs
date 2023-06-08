using Moq;
using SalaryService.Application.Dtos;
using SalaryService.Application.Validators;

namespace SalaryService.Tests;

public class EmployeeUpdateParametersValidatorTests
{
    [Theory]
    [InlineData("", "The phone number can't be empty")]
    [InlineData("+711", "The length of the phone number must consist of 11 digits")]
    [InlineData("82234567890", "The phone number must start with +7")]
    public async Task PhoneNumberIsInvalid_ReturnFalse(string phoneNumber, string expectedErrorMessage)
    {
        var request = new EmployeeUpdateDto
        {
            EmployeeId = It.IsAny<long>(),
            EmploymentType = 0.5m,
            FullSalary = 1,
            RatePerHour = 0,
            Phone = phoneNumber,
            HireDate = DateTime.Now
        };

        var employeeUpdateParametersValidator = new EmployeeUpdateParametersValidator();
        var validationResult = await employeeUpdateParametersValidator.ValidateAsync(request);

        Assert.False(validationResult.IsValid);
        Assert.Equal(expectedErrorMessage, validationResult.Errors[0].ErrorMessage);
    }

    [Fact]
    public async Task PhoneNumberIsValid_ReturnTrue()
    {
        const string validPhoneNumber = "+71234567890";

        var request = new EmployeeUpdateDto
        {
            EmployeeId = It.IsAny<long>(),
            EmploymentType = 0.5m,
            FullSalary = 1,
            RatePerHour = 0,
            Phone = validPhoneNumber,
            HireDate = DateTime.Now
        };

        var employeeUpdateParametersValidator = new EmployeeUpdateParametersValidator();
        var validationResult = await employeeUpdateParametersValidator.ValidateAsync(request);

        Assert.True(validationResult.IsValid);
    }
}