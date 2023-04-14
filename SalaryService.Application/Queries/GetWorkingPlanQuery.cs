using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries
{
    public partial class GetWorkingPlanQuery
    {
    }

    public class GetWorkingPlanQueryHandler : IGetWorkingPlanQueryHandler
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
