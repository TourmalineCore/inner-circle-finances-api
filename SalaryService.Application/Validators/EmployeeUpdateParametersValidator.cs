using FluentValidation;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Validators;

public class EmployeeUpdateParametersValidator : AbstractValidator<EmployeeUpdateParameters>
{
    public EmployeeUpdateParametersValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.Phone).NotNull().NotEmpty();
        RuleFor(x => x.RatePerHour).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.FullSalary).NotNull().GreaterThanOrEqualTo(1);
        RuleFor(x => x.EmploymentType).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.Parking).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.HireDate).NotNull().NotEmpty();
    }
}