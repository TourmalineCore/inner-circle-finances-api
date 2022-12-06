using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class UpdateEmployeeCommand
    {
        
    }
    public class UpdateEmployeeCommandHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public UpdateEmployeeCommandHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public async Task Handle(EmployeeUpdatingParameters request)
        {
            var employee = await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == request.EmployeeId && x.DeletedAtUtc == null);

            employee.Update(request.Name, request.Surname, request.MiddleName, request.CorporateEmail, request.PersonalEmail, request.Phone, request.GitHub, request.GitLab);

            _employeeDbContext.Update(employee);
            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
