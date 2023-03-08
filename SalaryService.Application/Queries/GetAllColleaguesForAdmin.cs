using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Queries
{
    public class GetAllColleaguesForAdmin : IGetAllColleagues
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public GetAllColleaguesForAdmin(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<IEnumerable<CollegueInfoDto>> HandleAsync()
        {
            var employee = await _employeeDbContext.QueryableAsNoTracking<Employee>()
                .Where(x => x.DeletedAtUtc == null)
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .ToListAsync();

            var colleagues = employee.Select(x => new CollegueInfoDto(
                x.Id,
                x.FirstName + " " + x.LastName + " " + x.MiddleName,
                x.CorporateEmail,
                x.PersonalEmail,
                x.Phone,
                x.GitHub,
                x.GitLab,
                x.EmployeeFinancialMetrics.Pay,
                x.EmployeeFinancialMetrics.RatePerHour,
                x.EmployeeFinancialMetrics.NetSalary,
                x.EmployeeFinancialMetrics.EmploymentType,
                x.EmployeeFinancialMetrics.ParkingCostPerMonth,
                "",
                x.HireDate
            ));

            return colleagues;
        }
    }
}
