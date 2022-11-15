
using NodaTime;
using SalaryService.DataAccess.Repositories;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CreateEmployeeProfileInfoCommand
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
    public class CreateEmployeeProfileInfoCommandHandler
    {
        private readonly EmployeeProfileInfoRepository _employeeProfileInfoRepository;
        private readonly IClock _clock;

        public CreateEmployeeProfileInfoCommandHandler(EmployeeProfileInfoRepository employeeProfileInfoRepository, IClock clock)
        {
            _employeeProfileInfoRepository = employeeProfileInfoRepository;
            _clock = clock;
        }
        public async Task<long> Handle(CreateEmployeeProfileInfoCommand request)
        {
            return await _employeeProfileInfoRepository.CreateAsync(new Employee(
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
