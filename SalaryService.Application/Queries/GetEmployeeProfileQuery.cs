using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeProfileQuery
    {
    }

    public class GetEmployeeProfileQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetEmployeeProfileQueryHandler(EmployeeDbContext employeeDbContext)
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

            return new EmployeeProfileDto(employee);
        }
    }
}
