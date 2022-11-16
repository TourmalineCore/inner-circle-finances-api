using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class UpdateEmployeeFinanceForPayrollCommand
    {
        public long EmployeeId { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public EmploymentTypes EmploymentType { get; set; }

        public bool HasParking { get; set; }
    }

    public class UpdateEmployeeFinanceForPayrollCommandHandler
    {
        private readonly EmployeeFinanceForPayrollRepository _employeeFinanceForPayrollRepository;

        public UpdateEmployeeFinanceForPayrollCommandHandler(EmployeeFinanceForPayrollRepository employeeFinanceForPayrollRepository)
        {
            _employeeFinanceForPayrollRepository = employeeFinanceForPayrollRepository;
        }

        public async Task Handle(UpdateEmployeeFinanceForPayrollCommand request)
        {
            var basicParameters = _employeeFinanceForPayrollRepository.GetByEmployeeIdAsync(request.EmployeeId).Result;
            basicParameters.Update(request.RatePerHour,
                request.Pay,
                request.EmploymentType,
                request.HasParking);

           await _employeeFinanceForPayrollRepository.UpdateAsync(basicParameters);
        }
    }
}
