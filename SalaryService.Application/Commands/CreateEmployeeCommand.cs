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

        public string WorkEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public string Skype { get; set; }

        public string Telegram { get; set; }
    }
    public class CreateEmployeeCommandHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task<long> Handle(CreateEmployeeCommand request)
        {
            return _employeeRepository.CreateEmployee(new Employee(
                request.Id,
                request.Name,
                request.Surname,
                request.WorkEmail,
                request.PersonalEmail,
                request.Phone,
                request.Skype,
                request.Telegram));
        }
    }
}
