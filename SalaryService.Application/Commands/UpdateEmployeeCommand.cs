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

        public string WorkEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; private set; }

        public string Skype { get; private set; }

        public string Telegram { get; private set; }
    }
    public class UpdateEmployeeCommandHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Task Handle(UpdateEmployeeCommand request)
        {
            return _employeeRepository.UpdateAsync(new Employee(
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
