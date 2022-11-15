
using NodaTime;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CreateEmployeeCommand
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string WorkEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public string Skype { get; set; }

        public string Telegram { get; set; }

    }
    public class CreateEmployeeCommandHandler
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly IClock _clock;

        public CreateEmployeeCommandHandler(EmployeeRepository employeeRepository, IClock clock)
        {
            _employeeRepository = employeeRepository;
            _clock = clock;
        }
        public async Task<long> Handle(CreateEmployeeCommand request)
        {
            return await _employeeRepository.CreateAsync(new Employee(
                request.Name,
                request.Surname,
                request.MiddleName,
                request.WorkEmail,
                request.PersonalEmail,
                request.Phone,
                request.Skype,
                request.Telegram,
                _clock.GetCurrentInstant()));
        }
    }
}
