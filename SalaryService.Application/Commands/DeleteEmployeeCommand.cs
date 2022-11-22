

using NodaTime;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Commands
{
    public partial class DeleteEmployeeCommand
    {
        
    }

    public class DeleteEmployeeCommandHandler
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly IClock _clock;

        public DeleteEmployeeCommandHandler(EmployeeRepository employeeRepository, IClock clock)
        {
            _employeeRepository = employeeRepository;
            _clock = clock;
        }

        public async Task Handle(long employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);

            employee.Delete(_clock.GetCurrentInstant());

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
