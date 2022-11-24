using SalaryService.Application.Dtos;
using SalaryService.DataAccess.Repositories;

namespace SalaryService.Application.Commands
{
    public partial class UpdateCEOCommand
    {
    }
    public class UpdateCEOCommandHandler
    {
        private readonly EmployeeRepository _employeeRepository;

        public UpdateCEOCommandHandler(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task Handle(CEOUpdatingParameters request)
        {
            var employee = await _employeeRepository.GetCEOAsync();

            employee.Update(request.PersonalEmail, request.Phone, request.GitHub, request.GitLab);

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}
