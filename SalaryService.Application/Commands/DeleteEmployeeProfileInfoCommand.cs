

using NodaTime;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Commands
{
    public partial class DeleteEmployeeProfileInfoCommand
    {
        public long EmployeeProfileId { get; set; }
    }

    public class DeleteEmployeeProfileInfoCommandHandler
    {
        private readonly EmployeeProfileInfoRepository _employeeProfileInfoRepository;
        private readonly IClock _clock;

        public DeleteEmployeeProfileInfoCommandHandler(EmployeeProfileInfoRepository employeeProfileInfoRepository, IClock clock)
        {
            _employeeProfileInfoRepository = employeeProfileInfoRepository;
            _clock = clock;
        }

        public async Task Handle(DeleteEmployeeProfileInfoCommand request)
        {
            var employee = await _employeeProfileInfoRepository.GetByIdAsync(request.EmployeeProfileId);

            employee.Delete(_clock.GetCurrentInstant());

            await _employeeProfileInfoRepository.UpdateAsync(employee);
        }
    }
}
