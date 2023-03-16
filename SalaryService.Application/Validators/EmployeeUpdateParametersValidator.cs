using FluentValidation;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Validators;

public class EmployeeUpdateParametersValidator : AbstractValidator<EmployeeUpdateParameters>
{
    public EmployeeUpdateParametersValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull().NotEmpty();
        RuleFor(x => x.Phone).NotNull().NotEmpty();
        RuleFor(x => x.RatePerHour).NotNull().NotEmpty();
        RuleFor(x => x.FullSalary).NotNull().NotEmpty();
        RuleFor(x => x.EmploymentType).NotNull().NotEmpty();
        RuleFor(x => x.Parking).NotNull().NotEmpty();
        RuleFor(x => x.HireDate).NotNull().NotEmpty();
    }
}