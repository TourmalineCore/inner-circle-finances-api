using SalaryService.DataAccess;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class UpdateEmployeeCommand
    {
        public long Id { get; private set; }

        public string Name { get; private set; }

        public string Surname { get; private set; }

        public string Email { get; private set; }

        public double RatePerHour { get; set; }

        public double FullSalary { get; set; }

        public double EmploymentType { get; set; }
    }
    public class UpdateEmployeeCommandHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void Handle(UpdateEmployeeCommand request)
        {
            _employeeRepository.UpdateEmployee(new Employee(
                request.Id,
                request.Name,
                request.Surname,
                request.Email,
                request.RatePerHour,
                request.FullSalary,
                request.EmploymentType));
        }
    }
}
