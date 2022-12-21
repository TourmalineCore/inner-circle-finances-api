using Microsoft.EntityFrameworkCore;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public partial class GetWorkingPlanQuery
    {
    }

    public class GetWorkingPlanQueryHandler
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public GetWorkingPlanQueryHandler(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        public async Task<WorkingPlan> HandleAsync()
        {
            return await _employeeDbContext.Queryable<WorkingPlan>().SingleAsync();
        }
    }
}
