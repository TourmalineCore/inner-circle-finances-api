using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Dtos;
using SalaryService.DataAccess;
using SalaryService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryService.Application.Commands
{
    public partial class EmployeeUpdateCommand
    {
    }
    
    public class EmployeeUpdateCommandHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeUpdateCommandHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task HandleAsync(EmployeeUpdateParameters request)
        {
            var employee = await _employeeDbContext
                .Queryable<Employee>()
                .SingleAsync(x => x.Id == request.EmployeeId && x.DeletedAtUtc == null);

            var currentFinanceForPayroll = (await _employeeDbContext
                .Queryable<Employee>()
                .Include(x => x.EmployeeFinanceForPayroll)
                .Include(x => x.EmployeeFinancialMetrics)
                .SingleAsync(x => x.Id == request.EmployeeId && x.DeletedAtUtc == null)).EmployeeFinanceForPayroll;

            employee.Update(
                request.Phone,
                request.PersonalEmail,
                request.GitHub,
                request.GitLab,
                request.HireDate,
                true,
                ""
            );            

            currentFinanceForPayroll.Update(request.RatePerHour,
                request.FullSalary,
                request.EmploymentType,
                request.Parking);

            _employeeDbContext.Update(employee);
            _employeeDbContext.Update(currentFinanceForPayroll);
            await _employeeDbContext.SaveChangesAsync();
        }
    }
}
