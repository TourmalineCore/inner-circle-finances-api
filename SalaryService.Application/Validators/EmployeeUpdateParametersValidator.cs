using FluentValidation;
using SalaryService.Application.Dtos;

namespace SalaryService.Application.Validators;

public class EmployeeUpdateParametersValidator : AbstractValidator<EmployeeUpdateDto>
{
    public EmployeeUpdateParametersValidator()
    {
        RuleFor(x => x.EmployeeId).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.Phone).NotNull().NotEmpty();
        RuleFor(x => x.FinancesForPayroll.RatePerHour).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.FinancesForPayroll.Pay).NotNull().GreaterThanOrEqualTo(1);
        RuleFor(x => x.FinancesForPayroll.EmploymentType).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.FinancesForPayroll.ParkingCostPerMonth).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(x => x.HireDate).NotNull().NotEmpty();
    }
}