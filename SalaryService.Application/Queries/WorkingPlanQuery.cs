using Microsoft.EntityFrameworkCore;
using SalaryService.Application.Queries.Contracts;
using SalaryService.DataAccess;
using SalaryService.Domain;

namespace SalaryService.Application.Queries;

public class WorkingPlanQuery : IWorkingPlanQuery
{
    private readonly EmployeeDbContext _context;

    public WorkingPlanQuery(EmployeeDbContext employeeDbContext)
    {
        _context = employeeDbContext;
    }

    public async Task<WorkingPlan> GetWorkingPlanAsync()
    {
        return await _context.QueryableAsNoTracking<WorkingPlan>().SingleAsync();
    }
}
