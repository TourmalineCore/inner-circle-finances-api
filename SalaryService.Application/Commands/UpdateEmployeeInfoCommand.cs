using Microsoft.EntityFrameworkCore;
using NodaTime;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class UpdateEmployeeInfoCommand
    {
        
    }
    public class UpdateEmployeeInfoCommandHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public UpdateEmployeeInfoCommandHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task HandleAsync(EmployeeInfoUpdateParameters parameters)
        {
            var employee = await _employeeDbContext
                .Queryable<Employee>()
                .SingleAsync(x => x.Id == parameters.EmployeeId);

            employee.Update(
                parameters.Phone,
                parameters.PersonalEmail,
                parameters.GitHub,
                parameters.GitLab,
                Instant.FromDateTimeUtc(DateTime.SpecifyKind(parameters.HireDate, DateTimeKind.Utc)),
                parameters.IsEmployedOfficially,
                parameters.PersonnelNumber
            );

            _employeeDbContext.Update(employee);
            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
