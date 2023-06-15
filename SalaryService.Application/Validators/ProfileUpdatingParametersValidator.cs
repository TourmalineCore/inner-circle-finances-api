using FluentValidation;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Validators;

public class ProfileUpdatingParametersValidator : AbstractValidator<ProfileUpdatingParameters>
{
    public ProfileUpdatingParametersValidator()
    {
        RuleFor(x => x.Phone).PhoneNumberMustBeValid();
    }
}