using FluentValidation;

namespace SalaryService.Application.Validators;

public static class ValidatorExtensions
{
    public static void PhoneNumberMustBeValid<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        ruleBuilder.SetValidator(new PhoneNumberValidator<T>());
    }
}