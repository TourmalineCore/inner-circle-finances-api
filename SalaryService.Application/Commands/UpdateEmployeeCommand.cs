using SalaryService.DataAccess.Repositories;

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
        public async Task Handle(UpdateEmployeeCommand request)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);

            employee.Update(request.Name, request.Surname, request.WorkEmail, request.PersonalEmail, request.Phone, request.Skype, request.Telegram);

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
