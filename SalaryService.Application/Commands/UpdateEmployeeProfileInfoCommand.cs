using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Commands
{
    public partial class UpdateEmployeeProfileInfoCommand
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string MiddleName { get; set; }

        public string WorkEmail { get; set; }

        public string PersonalEmail { get; set; }

        public string Phone { get; set; }

        public string Skype { get; set; }

        public string Telegram { get; set; }
    }
    public class UpdateEmployeeProfileInfoCommandHandler
    {
        private readonly EmployeeProfileInfoRepository _employeeProfileInfoRepository;

        public UpdateEmployeeProfileInfoCommandHandler(EmployeeProfileInfoRepository employeeProfileInfoRepository)
        {
            _employeeProfileInfoRepository = employeeProfileInfoRepository;
        }
        public async Task Handle(UpdateEmployeeProfileInfoCommand request)
        {
            var employee = await _employeeProfileInfoRepository.GetByIdAsync(request.Id);

            employee.Update(request.Name, request.Surname, request.MiddleName, request.WorkEmail, request.PersonalEmail, request.Phone, request.Skype, request.Telegram);

            await _employeeProfileInfoRepository.UpdateAsync(employee);
        }
    }
}
