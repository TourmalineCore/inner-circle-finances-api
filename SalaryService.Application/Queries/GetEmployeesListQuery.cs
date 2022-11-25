using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public partial class GetColleaguesQuery
    {
    }

    public class GetColleaguesQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetColleaguesQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<ColleagueDto> Handle()
        {
            var employees = await _employeeDbContext
                .QueryableAsNoTracking<Employee>()
                .Where(x => x.DeletedAtUtc == null && x.AccountId != 1)
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .ToListAsync();

            var employeesContacts = employees.Select(x => new ColleagueContactsDto(x.Id,
                x.Name + " " + x.Surname + " " + x.MiddleName,
                x.CorporateEmail,
                x.PersonalEmail,
                x.Phone,
                x.GitHub,
                x.GitLab));

            var employeesFinances = employees.Select(x => new ColleagueFinancesDto(x.Id,
                x.EmployeeFinanceForPayroll.RatePerHour,
                x.EmployeeFinanceForPayroll.Pay,
                x.EmployeeFinanceForPayroll.EmploymentType,
                x.EmployeeFinancialMetrics.NetSalary,
                x.EmployeeFinancialMetrics.ParkingCostPerMonth));

            return new ColleagueDto(employeesContacts, employeesFinances);
        }
    }
}
