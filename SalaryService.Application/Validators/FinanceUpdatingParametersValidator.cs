using FluentValidation;
using SalaryService.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Validators
{
    public class FinanceUpdatingParametersValidator : AbstractValidator<FinanceUpdatingParameters>
    {
        public FinanceUpdatingParametersValidator()
        {
            RuleFor(el => el.EmploymentTypeValue).NotNull().NotEmpty()
                .Must(Validators.CompareEmployeeTypeValue);
        }
    }
}
