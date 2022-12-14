using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Commands
{
    public partial class UpdateProfileCommand
    {
    }

    public class UpdateProfileCommandHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public UpdateProfileCommandHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public async Task HandleAsync(ProfileUpdatingParameters request)
        {
            var employee = await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.AccountId == 1 && x.DeletedAtUtc == null);

            employee.Update(request.PersonalEmail, request.Phone, request.GitHub, request.GitLab);

            _employeeDbContext.Update(employee);
            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
