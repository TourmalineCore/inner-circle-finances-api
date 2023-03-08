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
    public class GetAllColleaguesForEmployee : IGetAllColleagues
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public GetAllColleaguesForEmployee(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<IEnumerable<CollegueInfoDto>> HandleAsync()
        {
            var employee = await _employeeDbContext.QueryableAsNoTracking<Employee>()
                .Where(x => x.DeletedAtUtc == null)
                .ToListAsync();

            var colleagues = employee.Select(x => new CollegueInfoDto(
                x.Id,
                x.FirstName + " " + x.LastName + " " + x.MiddleName,
                x.CorporateEmail,
                x.PersonalEmail,
                x.Phone,
                x.GitHub,
                x.GitLab
            ));

            return colleagues;
        }
    }
}
