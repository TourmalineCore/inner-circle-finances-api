using FluentValidation;
using FluentValidation.Validators;

namespace SalaryService.Application.Validators;

public class PhoneNumberValidator<T> : PropertyValidator<T, string>
{
    private string _errorMessage = "Phone number is invalid";

    private const string RussiaCountryCode = "+7";
    private const int PhoneNumberLengthWithRussiaCountryCode = 12;

    public override string Name => "PhoneNumberValidator";

    public override bool IsValid(ValidationContext<T> context, string phoneNumber)
    {
        if (string.IsNullOrEmpty(phoneNumber))
        {
            _errorMessage = "The phone number can't be empty";
            return false;
        }

        if (!phoneNumber.StartsWith(RussiaCountryCode))
        {
            _errorMessage = "The phone number must start with +7";
            return false;
        }

        if (phoneNumber.Length != PhoneNumberLengthWithRussiaCountryCode)
        {
            _errorMessage = "The length of the phone number must consist of 11 digits";
            return false;
        }

        return true;
    }


    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return _errorMessage;
    }
}