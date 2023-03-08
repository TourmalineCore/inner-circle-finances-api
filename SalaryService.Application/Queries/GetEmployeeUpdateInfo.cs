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
    public class GetEmployeeUpdateInfo
    {
    }
    public class GetEmployeeUpdateHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetEmployeeUpdateHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<EmployeeUpdateInfo> HandleAsync(long employeeId)
        {
            var employee = await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == employeeId && x.DeletedAtUtc == null);

            return new EmployeeUpdateInfo(
                employee.FirstName + " " + employee.LastName + " " + employee.MiddleName,
                employee.CorporateEmail,
                employee.PersonalEmail,
                employee.Phone,
                employee.GitHub,
                employee.GitLab,
                employee.EmployeeFinanceForPayroll.RatePerHour,
                employee.EmployeeFinanceForPayroll.Pay,
                employee.EmployeeFinanceForPayroll.EmploymentType,
                employee.EmployeeFinanceForPayroll.ParkingCostPerMonth,
                employee.HireDate,
                true,
                ""
            );
        }
    }
}
