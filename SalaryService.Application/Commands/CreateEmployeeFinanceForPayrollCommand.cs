
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CreateEmployeeFinanceForPayrollCommand
    {
        public long EmployeeId { get; set; }

        public double RatePerHour { get; set; }

        public double Pay { get; set; }

        public EmploymentTypes EmploymentType { get; set; }

        public bool HasParking { get; set; }
    }

    public class CreateEmployeeFinanceForPayrollCommandHandler
    {
        private readonly EmployeeFinanceForPayrollRepository _employeeFinanceForPayrollRepository;

        public CreateEmployeeFinanceForPayrollCommandHandler(EmployeeFinanceForPayrollRepository employeeFinanceForPayroll)
        {
            _employeeFinanceForPayrollRepository = employeeFinanceForPayroll;
        }

        public async Task<long> Handle(CreateEmployeeFinanceForPayrollCommand request)
        {
            var basicSalaryParameters = new EmployeeFinanceForPayroll(
                request.EmployeeId,
                request.RatePerHour,
                request.Pay,
                request.EmploymentType,
                request.HasParking);

            return await _employeeFinanceForPayrollRepository.CreateAsync(basicSalaryParameters);
        }
    }
}
