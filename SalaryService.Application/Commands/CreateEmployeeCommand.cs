using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class CreateEmployeeCommand
    {

    }
    public class CreateEmployeeCommandHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public CreateEmployeeCommandHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task HandleAsync(EmployeeCreationParameters employeeCreationParameters)
        {
            var employee = new Employee(employeeCreationParameters.FirstName,
                employeeCreationParameters.LastName,
                employeeCreationParameters.MiddleName,
                employeeCreationParameters.CorporateEmail);

            await _employeeDbContext.AddAsync(employee);
            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
