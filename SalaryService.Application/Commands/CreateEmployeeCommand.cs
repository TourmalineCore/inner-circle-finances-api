using SalaryService.DataAccess;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CreateEmployeeCommand
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public double RatePerHour { get; set; }

        public double FullSalary { get; set; }

        public double EmploymentType { get; set; }
    }
    public class CreateEmployeeCommandHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void Handle(CreateEmployeeCommand request)
        {
            _employeeRepository.CreateEmployee(new Employee(
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
