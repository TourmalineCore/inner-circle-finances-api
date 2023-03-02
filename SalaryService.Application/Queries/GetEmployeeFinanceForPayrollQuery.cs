using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public partial class GetEmployeeFinanceForPayrollQuery
    {
    }

    public class GetEmployeeFinanceForPayrollQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetEmployeeFinanceForPayrollQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<ColleagueFinancesDto> HandleAsync(long employeeId)
        {
            var employee = await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == employeeId && x.DeletedAtUtc == null);

            return new ColleagueFinancesDto(employee.Id,
                employee.FirstName + " " + employee.LastName + " " + employee.MiddleName,
                employee.EmployeeFinanceForPayroll.RatePerHour,
                employee.EmployeeFinanceForPayroll.Pay,
                employee.EmployeeFinanceForPayroll.EmploymentType,
                employee.EmployeeFinancialMetrics.NetSalary,
                employee.EmployeeFinancialMetrics.ParkingCostPerMonth);
        }
    }
}
