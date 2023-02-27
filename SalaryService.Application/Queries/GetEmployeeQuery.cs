using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeQuery
    {
    }

    public class GetEmployeeQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetEmployeeQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<EmployeeProfileDto> HandleAsync(string corporateEmail)
        {
            var employee = await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.CorporateEmail == corporateEmail);

            return new EmployeeProfileDto(employee.Id,
                employee.FirstName,
                employee.LastName,
                employee.MiddleName,
                employee.CorporateEmail,
                employee.PersonalEmail,
                employee.Phone,
                employee.GitHub,
                employee.GitLab);
        }
    }
}
